﻿//MIT License

//Copyright (c) 2022-2025 Ezequiel Medina
//Copyright (c) 2024 Peter Kirmeier (Update new HCM interface)

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using HitCounterManager;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Windows.Forms;

namespace AutoSplitterCore
{
    public class AutoSplitterMainModule
    {
        public static SekiroSplitter sekiroSplitter = SekiroSplitter.GetIntance();
        public static Ds1Splitter ds1Splitter = Ds1Splitter.GetIntance();
        public static Ds2Splitter ds2Splitter = Ds2Splitter.GetIntance();
        public static Ds3Splitter ds3Splitter = Ds3Splitter.GetIntance();
        public static EldenSplitter eldenSplitter = EldenSplitter.GetIntance();
        public static HollowSplitter hollowSplitter = HollowSplitter.GetIntance();
        public static CelesteSplitter celesteSplitter = CelesteSplitter.GetIntance();
        public static CupheadSplitter cupSplitter = CupheadSplitter.GetIntance();
        public static DishonoredSplitter dishonoredSplitter = DishonoredSplitter.GetIntance();
        public static ASLSplitter aslSplitter = ASLSplitter.GetInstance();

        public static IGTModule igtModule = new IGTModule();
        public static SaveModule saveModule = new SaveModule();
        public static UpdateModule updateModule = UpdateModule.GetIntance();
        public static WebSockets webSockets;

        private ISplitterControl splitterControl = SplitterControl.GetControl();

        public bool _PracticeMode = false;
        public bool _ShowSettings = false;
        private Timer updateTimer;

        public List<string> GetGames() => GameConstruction.GameList;

        #region Settings
        public void InitDebug()
        {
            splitterControl.SetDebug(true);
            hollowSplitter.EnableDebugMode();
        }

        public void AutoSplitterForm(bool darkMode)
        {
            SetShowSettings(true);
            ReaLTaiizor.Forms.PoisonForm form = new AutoSplitter(saveModule, darkMode);
            form.ShowDialog();
            SetShowSettings(false);
        }

        public AutoSplitterMainModule()
        {
            DebugLog.Initialize();

            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                aslSplitter.Dispose();
                DebugLog.Close();
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    DebugLog.LogMessage($"Unhandled exception {ex.Message}", ex);
                }
            };

            RxApp.DefaultExceptionHandler = Observer.Create<Exception>(ex =>
            {
                DebugLog.LogMessage($"[RxApp.WinForms] Global Rx Exception: {ex}");
            });
        }

        private void SetShowDialogClose(object sender, EventArgs e) => SetShowSettings(false); //For debugmode can interact with interface when config is open

        public void RegisterHitCounterManagerInterface(IAutoSplitterCoreInterface interfaceASC)
        {
            //LoadSettings
            HitterControl._saveModule = saveModule;        
            saveModule.LoadAutoSplitterSettings();

            updateTimer = new Timer { Interval = 500 };
            updateTimer.Tick += UpdateTimer_Tick; // Check AutoTimers, AutoResets
            if (!splitterControl.GetDebug()) updateTimer.Start();

            updateModule.CheckUpdates(false);

            WebSockets.saveModule = saveModule;
            webSockets = WebSockets.Instance;
            webSockets.StartAsync();

            if (interfaceASC != null)
            {
                interfaceASC.GameList.Clear();
                foreach (string game in GetGames())
                {
                    interfaceASC.GameList.Add(game);
                }

                interfaceASC.ActiveGameIndex = GetSplitterEnable();

                interfaceASC.SetActiveGameIndexMethod = (splitter) =>
                {
                    //Disable all games
                    EnableSplitting(0);
                    //Ask Selected index
                    EnableSplitting(splitter);
                };
                interfaceASC.PracticeMode = GetPracticeMode();
                interfaceASC.SetPracticeModeMethod = SetPracticeMode;

                interfaceASC.OpenSettingsMethod = AutoSplitterForm;
                interfaceASC.SaveSettingsMethod = SaveAutoSplitterSettings;

                interfaceASC.GetCurrentInGameTimeMethod = GetCurrentInGameTime;
                bool GetCurrentInGameTime(out long totalTimeMs)
                {
                    if (GetIsIGTActive())
                    {
                        totalTimeMs = ReturnCurrentIGT();
                        return true;
                    }

                    totalTimeMs = 0;
                    return false;
                }

                interfaceASC.SplitterResetMethod = ResetSplitterFlags;
                interfaceASC.ProfileSelectedMethod = splitterControl.ProfileChange;
            }

            splitterControl.SetInterface(interfaceASC);
            splitterControl.SetWebSocket(webSockets);
            splitterControl.SetSaveModule(saveModule);
        }

        public static void SaveAutoSplitterSettings() => saveModule.SaveAutoSplitterSettings();

#endregion
        #region SplitterManagement
        public static bool GetPracticeMode() => saveModule._PracticeMode;

        public void SetPracticeMode(bool status)
        {
            _PracticeMode = status;
            saveModule._PracticeMode = status;
            sekiroSplitter._PracticeMode = status;
            ds1Splitter._PracticeMode = status;
            ds2Splitter._PracticeMode = status;
            ds3Splitter._PracticeMode = status;
            eldenSplitter._PracticeMode = status;
            hollowSplitter._PracticeMode = status;
            celesteSplitter._PracticeMode = status;
            cupSplitter._PracticeMode = status;
            dishonoredSplitter._PracticeMode = status;
            aslSplitter.PracticeMode = status;
            splitterControl.SetChecking(!status);
        }

        public void SetShowSettings(bool status)
        {
            _ShowSettings = status;
            sekiroSplitter._ShowSettings = status;
            ds1Splitter._ShowSettings = status;
            ds2Splitter._ShowSettings = status;
            ds3Splitter._ShowSettings = status;
            eldenSplitter._ShowSettings = status;
            hollowSplitter._ShowSettings = status;
            celesteSplitter._ShowSettings = status;
            cupSplitter._ShowSettings = status;
            dishonoredSplitter._ShowSettings = status;
            splitterControl.SetChecking(!status);
        }

        private static readonly Dictionary<GameConstruction.Game, Func<bool>> splitterEnablerCheckers = new Dictionary<GameConstruction.Game, Func<bool>>()
        {
            { GameConstruction.Game.Sekiro, () => sekiroSplitter.GetDataSekiro().enableSplitting },
            { GameConstruction.Game.DarkSouls1, () => ds1Splitter.GetDataDs1().enableSplitting },
            { GameConstruction.Game.DarkSouls2, () => ds2Splitter.GetDataDs2().enableSplitting },
            { GameConstruction.Game.DarkSouls3, () => ds3Splitter.GetDataDs3().enableSplitting },
            { GameConstruction.Game.EldenRing, () => eldenSplitter.GetDataElden().enableSplitting },
            { GameConstruction.Game.HollowKnight, () => hollowSplitter.GetDataHollow().enableSplitting },
            { GameConstruction.Game.Celeste, () => celesteSplitter.GetDataCeleste().enableSplitting },
            { GameConstruction.Game.Dishonored, () => dishonoredSplitter.GetDataDishonored().enableSplitting },
            { GameConstruction.Game.Cuphead, () => cupSplitter.GetDataCuphead().enableSplitting },
            { GameConstruction.Game.ASLMethod, () => aslSplitter._AslActive }
        };
        public static int GetSplitterEnable()
        {
            var activeSplitter = splitterEnablerCheckers.FirstOrDefault(kv => kv.Value()).Key;

            return GameConstruction.GetGameIndex(activeSplitter != 0 ? activeSplitter : GameConstruction.Game.None);
        }

        private readonly Dictionary<int, Action<bool>> splitterActions = new Dictionary<int, Action<bool>>()
        {
            { GameConstruction.GetGameIndex(GameConstruction.Game.Sekiro), status => sekiroSplitter.SetStatusSplitting(status) },
            { GameConstruction.GetGameIndex(GameConstruction.Game.DarkSouls1), status => ds1Splitter.SetStatusSplitting(status) },
            { GameConstruction.GetGameIndex(GameConstruction.Game.DarkSouls2), status => ds2Splitter.SetStatusSplitting(status) },
            { GameConstruction.GetGameIndex(GameConstruction.Game.DarkSouls3), status => ds3Splitter.SetStatusSplitting(status) },
            { GameConstruction.GetGameIndex(GameConstruction.Game.EldenRing), status => eldenSplitter.SetStatusSplitting(status) },
            { GameConstruction.GetGameIndex(GameConstruction.Game.HollowKnight), status => hollowSplitter.SetStatusSplitting(status) },
            { GameConstruction.GetGameIndex(GameConstruction.Game.Celeste), status => celesteSplitter.SetStatusSplitting(status) },
            { GameConstruction.GetGameIndex(GameConstruction.Game.Dishonored), status => dishonoredSplitter.SetStatusSplitting(status) },
            { GameConstruction.GetGameIndex(GameConstruction.Game.Cuphead), status => cupSplitter.SetStatusSplitting(status) },
            { GameConstruction.GetGameIndex(GameConstruction.Game.ASLMethod), status => aslSplitter.SetStatusSplitting(status) }
        };

        public void EnableSplitting(int splitter)
        {
            gameActive = splitter;
            igtModule.gameSelect = splitter;
            anyGameTime = false;

            if (splitter == GameConstruction.GetGameIndex(GameConstruction.Game.None))
            {
                splitterControl.SetChecking(false);

                // Disable all Splitters
                foreach (var action in splitterActions.Values)
                {
                    action(false);
                }
            }
            else
            {
                foreach (var action in splitterActions.Values)
                {
                    action(false);
                }

                if (splitterActions.TryGetValue(splitter, out var enableAction))
                {
                    enableAction(true);
                }
                else
                {
                    EnableSplitting(0);
                    return;
                }

                splitterControl.SetChecking(true);
            }
        }

        public static void ResetSplitterFlags()
        {
            sekiroSplitter.ResetSplited();
            ds1Splitter.ResetSplited();
            ds2Splitter.ResetSplited();
            ds3Splitter.ResetSplited();
            eldenSplitter.ResetSplited();
            hollowSplitter.ResetSplited();
            celesteSplitter.ResetSplited();
            cupSplitter.ResetSplited();

            if (webSockets.HasConnections && saveModule.generalAS.WebSocketSettings.Reset.Enabled)
            {
                webSockets.BroadcastAsync(saveModule.generalAS.WebSocketSettings.Reset.Message);
            }
        }
        #endregion
        #region IGT & Timmer 
        public long ReturnCurrentIGT()
        {
            long igtTime;
            if (GameOn())
            {
                try
                {
                    igtTime = igtModule.ReturnCurrentIGT();
                }
                catch (Exception) { igtTime = -1; }
            }
            else { igtTime = -1; }

            return igtTime;
        }

        public bool GetIsIGTActive()
        {
            return this.anyGameTime && ReturnCurrentIGT() > 0;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            CheckAutoTimers();
            CheckAutoResetSplit();
        }

        public int gameActive = 0;
        private bool anyGameTime = false;
        private bool autoTimer = false;
        private bool profileResetDone = false;
        private long _lastCelesteTime;
        private long _lastTime;

        public void CheckAutoTimers()
        {
            anyGameTime = false;
            autoTimer = false;
            switch (gameActive)
            {
                case (int)GameConstruction.Game.Sekiro: //Sekiro
                    if (sekiroSplitter.GetDataSekiro().autoTimer && !_PracticeMode)
                    {
                        autoTimer = true;
                        if (sekiroSplitter.GetDataSekiro().gameTimer)
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case (int)GameConstruction.Game.DarkSouls1: //DS1
                    if (ds1Splitter.GetDataDs1().autoTimer && !_PracticeMode)
                    {
                        autoTimer = true;
                        if (ds1Splitter.GetDataDs1().gameTimer)
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case (int)GameConstruction.Game.DarkSouls3: //Ds3
                    if (ds3Splitter.GetDataDs3().autoTimer && !_PracticeMode)
                    {
                        autoTimer = true;
                        if (!ds3Splitter.GetDataDs3().gameTimer)
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case (int)GameConstruction.Game.EldenRing: //Elden
                    if (eldenSplitter.GetDataElden().autoTimer && !_PracticeMode)
                    {
                        autoTimer = true;
                        if (eldenSplitter.GetDataElden().gameTimer)
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case (int)GameConstruction.Game.ASLMethod:
                    if (aslSplitter.IGTEnable && !_PracticeMode) { autoTimer = true; anyGameTime = true; }
                    break;

                //Special Case
                case (int)GameConstruction.Game.Celeste: // Celeste
                    if (celesteSplitter.GetDataCeleste().autoTimer && !_PracticeMode)
                    {
                        if (!celesteSplitter.GetDataCeleste().gameTimer)
                        {
                            if (!celesteSplitter._runStarted && celesteSplitter.IsInGame())
                            {
                                if (!splitterControl.GetTimerRunning() && GameOn())
                                    splitterControl.StartStopTimer(true);
                                celesteSplitter._runStarted = true;
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                            var currentCelesteTime = celesteSplitter.GetTimeInGame();

                            if (currentCelesteTime > 0 && celesteSplitter.IsInGame() && GameOn())
                            {
                                if (!splitterControl.GetTimerRunning())
                                    splitterControl.StartStopTimer(true);
                            }
                            else if (currentCelesteTime <= 0)
                            {
                                if (splitterControl.GetTimerRunning())
                                    splitterControl.StartStopTimer(false);
                            }

                            if (currentCelesteTime > _lastCelesteTime)
                                _lastCelesteTime = currentCelesteTime;
                        }
                    }
                    break;
                case (int)GameConstruction.Game.Cuphead: //Cuphead
                    if (cupSplitter.GetDataCuphead().autoTimer && !_PracticeMode)
                    {
                        if (!cupSplitter.GetDataCuphead().gameTimer)
                        {
                            if (!cupSplitter.IsInGame() || !GameOn())
                            {
                                if (splitterControl.GetTimerRunning())
                                    splitterControl.StartStopTimer(false);
                            }
                            else
                            {
                                if (!splitterControl.GetTimerRunning())
                                    splitterControl.StartStopTimer(true);
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                            if (!cupSplitter.IsInGame() || cupSplitter.LevelCompleted() || !GameOn())
                            {
                                if (splitterControl.GetTimerRunning())
                                    splitterControl.StartStopTimer(false);
                            }
                            else
                            {
                                if (!splitterControl.GetTimerRunning())
                                    splitterControl.StartStopTimer(true);
                            }
                        }
                    }
                    break;

                //Manual Controller with Loading Events           
                case (int)GameConstruction.Game.DarkSouls2: //DS2
                    if (ds2Splitter.GetDataDs2().autoTimer && !_PracticeMode)
                    {
                        if (ds2Splitter._runStarted && !splitterControl.GetTimerRunning() && GameOn())
                            splitterControl.StartStopTimer(true);

                        if ((!ds2Splitter._runStarted || !GameOn()) && splitterControl.GetTimerRunning())
                            splitterControl.StartStopTimer(false);
                    }
                    break;
                case (int)GameConstruction.Game.HollowKnight: //Hollow
                    if (hollowSplitter.GetDataHollow().autoTimer && !_PracticeMode)
                    {
                        if (hollowSplitter._runStarted && !splitterControl.GetTimerRunning() && GameOn())
                            splitterControl.StartStopTimer(true);

                        if ((!hollowSplitter._runStarted || !GameOn()) && splitterControl.GetTimerRunning())
                            splitterControl.StartStopTimer(false);
                    }
                    break;
                case (int)GameConstruction.Game.Dishonored:
                    if (dishonoredSplitter.GetDataDishonored().autoTimer && !_PracticeMode)
                    {
                        if (!dishonoredSplitter.GetDataDishonored().gameTimer)
                        {
                            if (dishonoredSplitter._runStarted && !splitterControl.GetTimerRunning() && GameOn())
                                splitterControl.StartStopTimer(true);

                            if ((!dishonoredSplitter._runStarted || !GameOn()) && splitterControl.GetTimerRunning())
                                splitterControl.StartStopTimer(false);
                        }
                        else
                        {
                            if (dishonoredSplitter._runStarted && !dishonoredSplitter.isLoading && !splitterControl.GetTimerRunning() && GameOn())
                                splitterControl.StartStopTimer(true);

                            if ((!dishonoredSplitter._runStarted || dishonoredSplitter.isLoading || !GameOn()) && splitterControl.GetTimerRunning())
                                splitterControl.StartStopTimer(false);
                        }
                    }
                    break;


                case (int)GameConstruction.Game.None:
                default: anyGameTime = false; autoTimer = false; break;
            }

            if (!autoTimer && !anyGameTime) return;

            bool gameIsRunning = GameOn();
            long inGameTime;

            try
            {
                inGameTime = gameIsRunning ? igtModule.ReturnCurrentIGT() : -1;
            }
            catch
            {
                inGameTime = -1;
            }

            if (autoTimer)
            {
                bool validIGT = inGameTime > 0;
                bool finalSplit = splitterControl.CurrentFinalSplit();
                bool timerRunning = splitterControl.GetTimerRunning();

                if (anyGameTime)
                {
                    bool timeAdvanced = validIGT && _lastTime != inGameTime;

                    if (timeAdvanced && !finalSplit && gameIsRunning && !timerRunning)
                        TryStartTimer();

                    if ((!timeAdvanced || finalSplit || !gameIsRunning) && timerRunning)
                        TryStopTimer();

                    if (validIGT)
                        _lastTime = inGameTime;
                }
                else
                {
                    if (validIGT && !timerRunning && !finalSplit && gameIsRunning)
                        TryStartTimer();

                    if ((!validIGT || finalSplit || !gameIsRunning) && timerRunning)
                        TryStopTimer();
                }
            }
        }

        void TryStartTimer()
        {
            if (!splitterControl.GetTimerRunning())
            {
                if (webSockets.HasConnections && saveModule.generalAS.WebSocketSettings.Start.Enabled)
                    webSockets.BroadcastAsync(saveModule.generalAS.WebSocketSettings.Start.Message);

                splitterControl.StartStopTimer(true);
            }
        }

        void TryStopTimer()
        {
            if (splitterControl.GetTimerRunning())
            {
                splitterControl.StartStopTimer(false);
            }
        }

        public void CheckAutoResetSplit()
        {
            //AutoResetSplits Checking
            if (saveModule.generalAS.AutoResetSplit)
            {
                long inGameTime = -1;
                bool SpecialCaseReset = false;
                if (GameOn())
                {
                    try
                    {
                        inGameTime = igtModule.ReturnCurrentIGT();
                    }
                    catch (Exception) { inGameTime = -1; }

                    //Ds2 and Hollow Cases
                    if (gameActive == (int)GameConstruction.Game.DarkSouls2)
                    {
                        SpecialCaseReset = true;
                        var loading = ds2Splitter.Ds2IsLoading();
                        if (!loading)
                        {
                            var position = ds2Splitter.GetCurrentPosition();
                            if (
                                position.Y < -322.0f && position.Y > -323.0f &&
                                position.X < -213.0f && position.X > -214.0f)
                            {
                                if (!profileResetDone)
                                {
                                    profileResetDone = true;
                                    ResetSplitterFlags();
                                    if (!splitterControl.GetDebug())
                                    {
                                        splitterControl.StartStopTimer(false);
                                        splitterControl.ProfileReset();
                                    }
                                }
                            }
                            else
                                profileResetDone = false;
                        }
                    }

                    if (gameActive == (int)GameConstruction.Game.HollowKnight)
                    {
                        SpecialCaseReset = true;
                        if (hollowSplitter.IsNewgame())
                        {
                            if (!profileResetDone)
                            {
                                profileResetDone = true;
                                ResetSplitterFlags();

                                if (!splitterControl.GetDebug())
                                {
                                    splitterControl.StartStopTimer(false);
                                    splitterControl.ProfileReset();
                                }
                            }
                        }
                        else
                            profileResetDone = false;
                    }
                }


                if (!SpecialCaseReset)
                {
                    if (inGameTime > 0 && inGameTime <= 10000)
                    {
                        if (!profileResetDone)
                        {
                            profileResetDone = true;
                            ResetSplitterFlags();

                            if (!splitterControl.GetDebug())
                            {
                                splitterControl.StartStopTimer(false);
                                splitterControl.ProfileReset();
                            }
                        }
                    }
                    else
                    {
                        profileResetDone = false;
                    }
                }
            }
        }

        private readonly Dictionary<int, Func<bool>> splitterStatusCheckers = new Dictionary<int, Func<bool>>
        {
            { (int) GameConstruction.Game.Sekiro, () => sekiroSplitter._StatusSekiro },
            { (int) GameConstruction.Game.DarkSouls1, () => ds1Splitter._StatusDs1 },
            { (int) GameConstruction.Game.DarkSouls2, () => ds2Splitter._StatusDs2 },
            { (int) GameConstruction.Game.DarkSouls3, () => ds3Splitter._StatusDs3},
            { (int) GameConstruction.Game.EldenRing, () => eldenSplitter._StatusElden },
            { (int) GameConstruction.Game.HollowKnight, () => hollowSplitter._StatusHollow },
            { (int) GameConstruction.Game.Celeste, () => celesteSplitter._StatusCeleste },
            { (int) GameConstruction.Game.Dishonored, () => dishonoredSplitter._StatusDish },
            { (int) GameConstruction.Game.Cuphead, () => cupSplitter._StatusCuphead },
            { (int) GameConstruction.Game.ASLMethod, () => aslSplitter.GetStatusGame() }
        };

        public bool GameOn()
        {
            if (splitterStatusCheckers.TryGetValue(gameActive, out var statusChecker))
            {
                return statusChecker.Invoke();
            }

            return false;
        }
        #endregion
        #region Common
        /// <summary>
        /// Tries to open an URI with the system's registered default browser.
        /// (See: https://github.com/dotnet/runtime/issues/21798)
        /// </summary>
        /// <param name="uri">URI that shall be opened</param>
        public static void OpenWithBrowser(Uri uri) => Process.Start(new ProcessStartInfo("cmd", $"/c start {uri.OriginalString.Replace("&", "^&")}") { CreateNoWindow = true, UseShellExecute = true });
        #endregion
    }
}

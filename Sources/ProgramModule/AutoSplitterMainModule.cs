//MIT License

//Copyright (c) 2022-2024 Ezequiel Medina
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using HitCounterManager;

namespace AutoSplitterCore
{
    public class AutoSplitterMainModule
    {
        public SekiroSplitter sekiroSplitter = new SekiroSplitter();
        public Ds1Splitter ds1Splitter = new Ds1Splitter();
        public Ds2Splitter ds2Splitter = new Ds2Splitter();
        public Ds3Splitter ds3Splitter = new Ds3Splitter();
        public EldenSplitter eldenSplitter = new EldenSplitter();
        public HollowSplitter hollowSplitter = new HollowSplitter();
        public CelesteSplitter celesteSplitter = new CelesteSplitter();
        public CupheadSplitter cupSplitter = new CupheadSplitter();
        public DishonoredSplitter dishonoredSplitter = new DishonoredSplitter();
        public IGTModule igtModule = new IGTModule();
        public SaveModule saveModule = new SaveModule();
        public UpdateModule updateModule = new UpdateModule();
        #if !HCMv2
        public Debug debugForm;
        #endif
        public bool DebugMode = false;
        public bool _PracticeMode = false;
        public bool _ShowSettings = false;
        public IAutoSplitterCoreInterface main;
        private IAutoSplitterCoreInterface profCtrl;
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 500 };

        public List<string> GetGames() { return GameConstruction.GameList; }


        #region Settings
        public void InitDebug()
        {
            sekiroSplitter.DebugMode = true;
            ds1Splitter.DebugMode = true;
            ds2Splitter.DebugMode = true;
            ds3Splitter.DebugMode = true;
            eldenSplitter.DebugMode = true;
            hollowSplitter.DebugMode = true;
            celesteSplitter.DebugMode = true;
            cupSplitter.DebugMode = true;
            dishonoredSplitter.DebugMode = true;
            updateModule.DebugMode = true;
            saveModule._DebugMode = true;
        }

        public void AutoSplitterForm(bool darkMode)
        {
            SetShowSettings(true);
            ReaLTaiizor.Forms.PoisonForm form = new AutoSplitter
                   (sekiroSplitter, 
                    hollowSplitter, 
                    eldenSplitter, 
                    ds3Splitter,
                    celesteSplitter, 
                    ds2Splitter, 
                    cupSplitter,
                    ds1Splitter,
                    dishonoredSplitter, 
                    updateModule, 
                    saveModule, 
                    darkMode);
            form.ShowDialog();
            SetShowSettings(false);
        }

        public void RegisterHitCounterManagerInterface(IAutoSplitterCoreInterface interfaceASC)
        {
            //SetPointers
            igtModule.setSplitterPointers(sekiroSplitter, eldenSplitter, ds3Splitter, celesteSplitter, cupSplitter, ds1Splitter);
            saveModule.SetPointers
                    (sekiroSplitter,
                    ds1Splitter,
                    ds2Splitter,
                    ds3Splitter,
                    eldenSplitter,
                    hollowSplitter,
                    celesteSplitter,
                    cupSplitter,
                    dishonoredSplitter,
                    updateModule,
                    this);
            //LoadSettings
            saveModule.LoadAutoSplitterSettings(interfaceASC);
            this.profCtrl = interfaceASC;
            this.main = interfaceASC;
            if (main != null)
                _update_timer.Tick += (senderT, args) => CheckAutoTimers();

            _update_timer.Tick += (senderT, args) => CheckAutoResetSplit();
            _update_timer.Enabled = true;

            updateModule.CheckUpdates(false);

            interfaceASC.GameList.Clear();
            foreach (string game in GetGames())
            {
                interfaceASC.GameList.Add(game);
            }

            main.ActiveGameIndex = GetSplitterEnable();
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
        }

        public void SaveAutoSplitterSettings()
        {
            saveModule.SaveAutoSplitterSettings();
        }
        #endregion
        #region SplitterManagement
        public bool GetPracticeMode()
        {
            return saveModule._PracticeMode;
        }

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
        }

        public int GetSplitterEnable()
        {
            if (sekiroSplitter.dataSekiro.enableSplitting) { return GameConstruction.SekiroSplitterIndex; }
            if (ds1Splitter.dataDs1.enableSplitting) { return GameConstruction.Ds1SplitterIndex; }
            if (ds2Splitter.dataDs2.enableSplitting) { return GameConstruction.Ds2SplitterIndex; }
            if (ds3Splitter.dataDs3.enableSplitting) { return GameConstruction.Ds3SplitterIndex; }
            if (eldenSplitter.dataElden.enableSplitting) { return GameConstruction.EldenSplitterIndex; }
            if (hollowSplitter.dataHollow.enableSplitting) { return GameConstruction.HollowSplitterIndex; }
            if (celesteSplitter.dataCeleste.enableSplitting) { return GameConstruction.CelesteSplitterIndex; }
            if (dishonoredSplitter.dataDish.enableSplitting) { return GameConstruction.DishonoredSplitterIndex; }
            if (cupSplitter.dataCuphead.enableSplitting) { return GameConstruction.CupheadSplitterIndex; }
            return GameConstruction.NoneSplitterIndex;
        }

        public void EnableSplitting(int splitter)
        {
            gameActive = splitter;
            igtModule.gameSelect = splitter;
            anyGameTime = false;
            if (splitter == GameConstruction.NoneSplitterIndex)
            {
                sekiroSplitter.setStatusSplitting(false);
                ds1Splitter.setStatusSplitting(false);
                ds2Splitter.setStatusSplitting(false);
                ds3Splitter.setStatusSplitting(false);
                eldenSplitter.setStatusSplitting(false);
                hollowSplitter.setStatusSplitting(false);
                celesteSplitter.setStatusSplitting(false);
                dishonoredSplitter.setStatusSplitting(false);
                cupSplitter.setStatusSplitting(false);
            }
            else
            {
                switch (splitter)
                {
                    case GameConstruction.SekiroSplitterIndex: sekiroSplitter.setStatusSplitting(true); break;
                    case GameConstruction.Ds1SplitterIndex: ds1Splitter.setStatusSplitting(true); break;
                    case GameConstruction.Ds2SplitterIndex: ds2Splitter.setStatusSplitting(true); break;
                    case GameConstruction.Ds3SplitterIndex: ds3Splitter.setStatusSplitting(true); break;
                    case GameConstruction.EldenSplitterIndex: eldenSplitter.setStatusSplitting(true); break;
                    case GameConstruction.HollowSplitterIndex: hollowSplitter.setStatusSplitting(true); break;
                    case GameConstruction.CelesteSplitterIndex: celesteSplitter.setStatusSplitting(true); break;
                    case GameConstruction.DishonoredSplitterIndex: dishonoredSplitter.setStatusSplitting(true); break;
                    case GameConstruction.CupheadSplitterIndex: cupSplitter.setStatusSplitting(true); break;
                    default: EnableSplitting(0); break;
                }
            }
        }

        public void ResetSplitterFlags()
        {
            sekiroSplitter.resetSplited();
            ds1Splitter.resetSplited();
            ds2Splitter.resetSplited();
            ds3Splitter.resetSplited();
            eldenSplitter.resetSplited();
            hollowSplitter.resetSplited();
            celesteSplitter.resetSplited();
            cupSplitter.resetSplited();
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
                    igtTime = (long)igtModule.ReturnCurrentIGT();
                }
                catch (Exception) { igtTime = -1; }
            }else { igtTime = -1; }

            return igtTime;
        }

        public bool GetIsIGTActive()
        {
            return this.anyGameTime && ReturnCurrentIGT() > 0;
        }

        private int gameActive = 0;
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
                case GameConstruction.SekiroSplitterIndex: //Sekiro
                    if (sekiroSplitter.dataSekiro.autoTimer && !_PracticeMode)
                    {
                        autoTimer = true;
                        if (sekiroSplitter.dataSekiro.gameTimer)
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case GameConstruction.Ds1SplitterIndex: //DS1
                    if (ds1Splitter.dataDs1.autoTimer && !_PracticeMode)
                    {
                        autoTimer = true;
                        if (ds1Splitter.dataDs1.gameTimer)
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case GameConstruction.Ds3SplitterIndex: //Ds3
                    if (ds3Splitter.dataDs3.autoTimer && !_PracticeMode)
                    {
                        autoTimer = true;
                        if (!ds3Splitter.dataDs3.gameTimer)
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case GameConstruction.EldenSplitterIndex: //Elden
                    if (eldenSplitter.dataElden.autoTimer && !_PracticeMode)
                    {
                        autoTimer = true;
                        if (eldenSplitter.dataElden.gameTimer)
                        {
                            anyGameTime = true;
                        }
                    }
                    break;

                //Special Case
                case GameConstruction.CelesteSplitterIndex: //Celeste
                    if (celesteSplitter.dataCeleste.autoTimer && !_PracticeMode)
                    {
                        if (!celesteSplitter.dataCeleste.gameTimer)
                        {                           
                            if (!celesteSplitter._runStarted && celesteSplitter.IsInGame())
                            {
                                if (!profCtrl.TimerRunning && GameOn())
                                    main.StartStopTimer(true);
                                celesteSplitter._runStarted = true;
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                            var currentCelesteTime = celesteSplitter.getTimeInGame();
                            if (currentCelesteTime > 0 && currentCelesteTime != _lastCelesteTime && celesteSplitter.IsInGame() && GameOn())
                            {
                                if (!profCtrl.TimerRunning)
                                    main.StartStopTimer(true);
                            }
                            else
                            {
                                if (profCtrl.TimerRunning)
                                    main.StartStopTimer(false);
                            }
                                

                            if (currentCelesteTime > 0)
                                _lastCelesteTime = currentCelesteTime;
                        }
                    }
                    break;
                case GameConstruction.CupheadSplitterIndex: //Cuphead
                    if (cupSplitter.dataCuphead.autoTimer && !_PracticeMode)
                    {
                        if (!cupSplitter.dataCuphead.gameTimer)
                        {
                            if (!cupSplitter.IsInGame() || !GameOn())
                            {
                                if (profCtrl.TimerRunning)
                                    main.StartStopTimer(false);
                            }
                            else
                            {
                                if (!profCtrl.TimerRunning)
                                    main.StartStopTimer(true);
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                            if (!cupSplitter.IsInGame() || cupSplitter.levelCompleted() || !GameOn())
                            {
                                if (profCtrl.TimerRunning)
                                    main.StartStopTimer(false);
                            }
                            else
                            {
                                if (!profCtrl.TimerRunning)
                                    main.StartStopTimer(true);
                            }
                        }                     
                    }
                    break;

                //Manual Controller with Loading Events           
                case GameConstruction.Ds2SplitterIndex: //DS2
                    if (ds2Splitter.dataDs2.autoTimer && !_PracticeMode)
                    {
                        if (ds2Splitter._runStarted && !profCtrl.TimerRunning && GameOn())
                            main.StartStopTimer(true);

                        if ((!ds2Splitter._runStarted || !GameOn()) && profCtrl.TimerRunning)
                            main.StartStopTimer(false);
                    }
                    break;
                case GameConstruction.HollowSplitterIndex: //Hollow
                    if (hollowSplitter.dataHollow.autoTimer && !_PracticeMode)
                    {
                        if (hollowSplitter._runStarted && !profCtrl.TimerRunning && GameOn())
                            main.StartStopTimer(true);

                        if ((!hollowSplitter._runStarted || !GameOn()) && profCtrl.TimerRunning)
                            main.StartStopTimer(false);
                    }
                    break;
                case GameConstruction.DishonoredSplitterIndex:
                    if (dishonoredSplitter.dataDish.autoTimer && !_PracticeMode)
                    {
                        if (!dishonoredSplitter.dataDish.gameTimer)
                        {
                            if (dishonoredSplitter._runStarted && !profCtrl.TimerRunning && GameOn())
                                main.StartStopTimer(true);
                            
                            if ((!dishonoredSplitter._runStarted || !GameOn()) && profCtrl.TimerRunning)
                                main.StartStopTimer(false);
                        }
                        else
                        {
                            if (dishonoredSplitter._runStarted && !dishonoredSplitter.isLoading && !profCtrl.TimerRunning && GameOn())
                                main.StartStopTimer(true);

                            if ((!dishonoredSplitter._runStarted || dishonoredSplitter.isLoading || !GameOn()) && profCtrl.TimerRunning)
                                main.StartStopTimer(false);
                        }
                    }
                    break;


                case GameConstruction.NoneSplitterIndex:               
                default: anyGameTime = false; autoTimer = false; break;
            }


            //AutoTimerReset Checking
            if (autoTimer && anyGameTime)
            {                
                int inGameTime = -1;
                if (GameOn()) {
                    try {
                        inGameTime = igtModule.ReturnCurrentIGT(); 
                    } catch (Exception) { inGameTime = -1; }
                }

                if (inGameTime > 0 && _lastTime != inGameTime && !profCtrl.TimerRunning && profCtrl.ActiveSplit != profCtrl.SplitCount && GameOn())
                {
                    profCtrl.UpdateDuration();
                    main.StartStopTimer(true);
                    profCtrl.UpdateDuration();
                }
                    
                if ((inGameTime <= 0 || (inGameTime > 0 && _lastTime == inGameTime) || profCtrl.ActiveSplit == profCtrl.SplitCount || !GameOn()) && profCtrl.TimerRunning)
                {
                    profCtrl.UpdateDuration();
                    main.StartStopTimer(false);
                    profCtrl.UpdateDuration();
                }

                if (inGameTime > 0)
                    _lastTime = inGameTime;
            }

            if (autoTimer && !anyGameTime)
            {
                int inGameTime = -1;
                if (GameOn())
                {
                    try
                    {
                        inGameTime = igtModule.ReturnCurrentIGT();
                    }
                    catch (Exception) { inGameTime = -1; }
                }

                if (inGameTime > 0 && !profCtrl.TimerRunning && profCtrl.ActiveSplit != profCtrl.SplitCount && GameOn())
                    main.StartStopTimer(true);

                if ((inGameTime <= 0 || profCtrl.ActiveSplit == profCtrl.SplitCount || !GameOn()) && profCtrl.TimerRunning)
                    main.StartStopTimer(false);
            }
        }

        public void CheckAutoResetSplit()
        {
            //AutoResetSplits Checking
            if (saveModule.dataAS.AutoResetSplit)
            {
                int inGameTime = -1;
                bool SpecialCaseReset = false;
                if (GameOn())
                {
                    try
                    {
                        inGameTime = igtModule.ReturnCurrentIGT();
                    }
                    catch (Exception) { inGameTime = -1; }

                    //Ds2 and Hollow Cases
                    if (gameActive == GameConstruction.Ds2SplitterIndex)
                    {
                        SpecialCaseReset = true;
                        var loading = ds2Splitter.Ds2IsLoading();
                        if (!loading)
                        {
                            var position = ds2Splitter.getCurrentPosition();
                            if (
                                position.Y < -322.0f && position.Y > -323.0f &&
                                position.X < -213.0f && position.X > -214.0f)
                            {
                                if (!profileResetDone)
                                {
                                    profileResetDone = true;
                                    ResetSplitterFlags();
                                    if (!DebugMode)
                                    {
                                        main.StartStopTimer(false);
                                        profCtrl.ProfileReset();
                                    }                                   
                                }
                            }
                            else
                                profileResetDone = false;
                        }
                    }

                    if (gameActive == GameConstruction.HollowSplitterIndex)
                    {
                        SpecialCaseReset = true;
                        if (hollowSplitter.IsNewgame())
                        {
                            if (!profileResetDone)
                            {
                                profileResetDone = true;
                                ResetSplitterFlags();

                                if (!DebugMode)
                                {
                                    main.StartStopTimer(false);
                                    profCtrl.ProfileReset();
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

                            if (!DebugMode)
                            {
                                main.StartStopTimer(false);
                                profCtrl.ProfileReset();
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

        public bool GameOn()
        {
            switch (gameActive)
            {
                case GameConstruction.SekiroSplitterIndex: 
                    return sekiroSplitter._StatusSekiro;
                case GameConstruction.Ds1SplitterIndex: 
                    return ds1Splitter._StatusDs1;
                case GameConstruction.Ds2SplitterIndex:
                    return ds2Splitter._StatusDs2;
                case GameConstruction.Ds3SplitterIndex:
                    return ds3Splitter._StatusDs3;
                case GameConstruction.EldenSplitterIndex:
                    return eldenSplitter._StatusElden;
                case GameConstruction.HollowSplitterIndex:
                    return hollowSplitter._StatusHollow;
                case GameConstruction.CelesteSplitterIndex:
                    return celesteSplitter._StatusCeleste;
                case GameConstruction.DishonoredSplitterIndex:
                    return dishonoredSplitter._StatusDish;
                case GameConstruction.CupheadSplitterIndex:
                    return cupSplitter._StatusCuphead;  
                case GameConstruction.NoneSplitterIndex:
                default: return false;
            }
        }
        #endregion
        #region Common
        /// <summary>
        /// Tries to open an URI with the system's registered default browser.
        /// (See: https://github.com/dotnet/runtime/issues/21798)
        /// </summary>
        /// <param name="uri">URI that shall be opened</param>
        public static void OpenWithBrowser(Uri uri) => Process.Start(new ProcessStartInfo("cmd", $"/c start {uri.OriginalString.Replace("&", "^&")}") { CreateNoWindow = true });
        #endregion
    }
}

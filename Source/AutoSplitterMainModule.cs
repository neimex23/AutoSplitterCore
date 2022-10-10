//MIT License

//Copyright (c) 2022 Ezequiel Medina

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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using HitCounterManager;
using System.Windows.Forms;
using System.Net.NetworkInformation;

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
        public AslSplitter aslSplitter = new AslSplitter();
        public IGTModule igtModule = new IGTModule();
        public SaveModule saveModule = new SaveModule();
        public UpdateModule updateModule = new UpdateModule();
        public bool DebugMode = false;
        public bool _PracticeMode = false;
        private ProfilesControl profCtrl;
        private Form1 main;
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1500 };
        public List<string> GameList = new List<string>()
        {
            "Sekiro",
            "Dark Souls 1",
            "Dark Souls 2",
            "Dark Souls 3",
            "Elden Ring",
            "Hollow Knight",
            "Celeste",
            "Cuphead",
            "ASL Method"
        };
        public List<string> GetGames() { return this.GameList; }

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
            aslSplitter.DebugMode = true;
            updateModule.DebugMode = true;
        }

        public void AutoSplitterForm(bool darkMode)
        {
            Form form = new AutoSplitter(sekiroSplitter, hollowSplitter, eldenSplitter, ds3Splitter, celesteSplitter, ds2Splitter, aslSplitter, cupSplitter, ds1Splitter, updateModule, darkMode);
            form.ShowDialog();
        }

        public void LoadAutoSplitterSettings(ProfilesControl profiles, Form1 main)
        {
            //SetPointers
            igtModule.setSplitterPointers(sekiroSplitter, eldenSplitter, ds3Splitter, celesteSplitter, cupSplitter, ds1Splitter);
            saveModule.SetPointers(sekiroSplitter, ds1Splitter, ds2Splitter, ds3Splitter, eldenSplitter, hollowSplitter, celesteSplitter, cupSplitter, aslSplitter, updateModule);
            //LoadSettings
            saveModule.LoadAutoSplitterSettings(profiles);
            this.profCtrl = profiles;
            this.main = main;         
            if (main != null)
            {
                _update_timer.Tick += (senderT, args) => CheckAutoTimers();
                _update_timer.Enabled = true;
            }
            updateModule.CheckUpdates(false);
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
            aslSplitter._PracticeMode = status;
        }
        public int GetSplitterEnable()
        {
            if (sekiroSplitter.dataSekiro.enableSplitting) { return 1; }
            if (ds1Splitter.dataDs1.enableSplitting) { return 2; }
            if (ds2Splitter.dataDs2.enableSplitting) { return 3; }
            if (ds3Splitter.dataDs3.enableSplitting) { return 4; }
            if (eldenSplitter.dataElden.enableSplitting) { return 5; }
            if (hollowSplitter.dataHollow.enableSplitting) { return 6; }
            if (celesteSplitter.dataCeleste.enableSplitting) { return 7; }
            if (cupSplitter.dataCuphead.enableSplitting) { return 8; }
            if (aslSplitter.enableSplitting) { return 9; }
            return 0;
        }
        public void EnableSplitting(int splitter)
        {
            gameActive = splitter;
            igtModule.gameSelect = splitter;
            anyGameTime = false;
            if (splitter == 0)
            {
                sekiroSplitter.setStatusSplitting(false);
                ds1Splitter.setStatusSplitting(false);
                ds2Splitter.setStatusSplitting(false);
                ds3Splitter.setStatusSplitting(false);
                eldenSplitter.setStatusSplitting(false);
                hollowSplitter.setStatusSplitting(false);
                celesteSplitter.setStatusSplitting(false);
                cupSplitter.setStatusSplitting(false);
                aslSplitter.setStatusSplitting(false);
            }
            else
            {
                switch (splitter)
                {
                    case 1: sekiroSplitter.setStatusSplitting(true); break;
                    case 2: ds1Splitter.setStatusSplitting(true); break;
                    case 3: ds2Splitter.setStatusSplitting(true); break;
                    case 4: ds3Splitter.setStatusSplitting(true); break;
                    case 5: eldenSplitter.setStatusSplitting(true); break;
                    case 6: hollowSplitter.setStatusSplitting(true); break;
                    case 7: celesteSplitter.setStatusSplitting(true); break;
                    case 8: cupSplitter.setStatusSplitting(true); break;
                    case 9: aslSplitter.setStatusSplitting(true); break;
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
            return (long)igtModule.ReturnCurrentIGT();
        }

        public bool GetIsIGTActive()
        {
            return this.anyGameTime && ReturnCurrentIGT() > 0;
        }

        private int gameActive = 0;
        private bool anyGameTime = false;
        private bool autoTimer = false;
        private long _lastCelesteTime;
        public void CheckAutoTimers()
        {
            anyGameTime = false;
            autoTimer = false;
            switch (gameActive)
            {
                case 1: //Sekiro
                    if (sekiroSplitter.dataSekiro.autoTimer && !_PracticeMode)
                    {
                        autoTimer = true;
                        if (sekiroSplitter.dataSekiro.gameTimer)
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 2: //DS1
                    if (ds1Splitter.dataDs1.autoTimer && !_PracticeMode)
                    {
                        autoTimer = true;
                        if (ds1Splitter.dataDs1.gameTimer)
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 4: //Ds3
                    if (ds3Splitter.dataDs3.autoTimer && !_PracticeMode)
                    {
                        autoTimer = true;
                        if (!ds3Splitter.dataDs3.gameTimer)
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 5: //Elden
                    if (eldenSplitter.dataElden.autoTimer && !_PracticeMode)
                    {
                        autoTimer = true;
                        if (eldenSplitter.dataElden.gameTimer)
                        {
                            anyGameTime = true;
                        }
                    }
                    break;
                case 7: //Celeste
                    if (celesteSplitter.dataCeleste.autoTimer && !_PracticeMode)
                    {
                        if (!celesteSplitter.dataCeleste.gameTimer)
                        {                           
                            if (!celesteSplitter._runStarted && celesteSplitter.IsInGame())
                            {
                                main.StartStopTimer(true);
                                celesteSplitter._runStarted = true;
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                            var currentCelesteTime = celesteSplitter.getTimeInGame();
                            if (currentCelesteTime > 0 && currentCelesteTime != _lastCelesteTime && celesteSplitter.IsInGame())
                                main.StartStopTimer(true);
                            else
                                main.StartStopTimer(false);

                            if (currentCelesteTime > 0)
                                _lastCelesteTime = currentCelesteTime;
                        }
                    }
                    break;
                case 8: //Cuphead
                    if (cupSplitter.dataCuphead.autoTimer && !_PracticeMode)
                    {
                        if (!cupSplitter.dataCuphead.gameTimer)
                        {
                            if (cupSplitter.GetSceneName() == string.Empty || cupSplitter.GetSceneName() == "scene_title")
                            {
                                main.StartStopTimer(false);
                            }
                            else
                            {
                                main.StartStopTimer(true);
                            }
                        }
                        else
                        {
                            anyGameTime = true;
                            if (cupSplitter.GetSceneName() == string.Empty || cupSplitter.GetSceneName() == "scene_title" || cupSplitter.levelCompleted())
                            {
                                main.StartStopTimer(false);
                            }
                            else
                            {
                                main.StartStopTimer(true);
                            }
                        }                     
                    }
                    break;

                //Manual Controller with Loading Events
                case 3: //DS2
                    if (ds2Splitter.dataDs2.autoTimer && !_PracticeMode)
                    {
                        if (ds2Splitter._runStarted)
                        {
                            main.StartStopTimer(true);
                        }
                        else
                        {
                            if (!ds2Splitter._runStarted)
                            {
                                main.StartStopTimer(false);
                            }
                        }
                    }
                    break;
                case 6: //Hollow
                    if (hollowSplitter.dataHollow.autoTimer && !_PracticeMode)
                    {
                        if (hollowSplitter._runStarted)
                        {
                            main.StartStopTimer(true);
                        }
                        else
                        {
                            if (!hollowSplitter._runStarted)
                            {
                                main.StartStopTimer(false);
                            }
                        }
                    }
                    break;


                case 0:
                case 9:
                default: anyGameTime = false; autoTimer = false; break;
            }

            if (autoTimer)
            {
                var inGameTime = igtModule.ReturnCurrentIGT();
                if (inGameTime > 0 && !profCtrl.TimerRunning)
                    main.StartStopTimer(true);
                if (inGameTime <= 0 && profCtrl.TimerRunning)
                    main.StartStopTimer(false);
            }
        } 
        #endregion
    }
}

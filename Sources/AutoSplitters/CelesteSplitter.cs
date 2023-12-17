﻿//MIT License

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
using System.Threading.Tasks;
using System.Threading;
using LiveSplit.Celeste;
using HitCounterManager;

namespace AutoSplitterCore
{
    public class CelesteSplitter
    {
        public static ISplitterMemory celeste = new SplitterMemoryDispatch();
        public bool _StatusCeleste = false;
        public bool _runStarted = false;
        public bool _SplitGo = false;
        public bool _PracticeMode = false;
        private bool PK = true;
        public bool _ShowSettings = false;
        public DTCeleste dataCeleste;
        public ProfilesControl _profile;
        public DefinitionsCeleste.InfoPlayerCeleste infoPlayer = new DefinitionsCeleste.InfoPlayerCeleste(); 
        private static readonly object _object = new object();
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };
        public bool DebugMode = false;

        #region Control Management
        public DTCeleste getDataCeleste()
        {
            return this.dataCeleste;
        }
        public void setDataCeleste(DTCeleste data, ProfilesControl profile)
        {
            this.dataCeleste = data;
            this._profile = profile;
            _update_timer.Tick += (sender, args) => SplitGo();
        }

        public bool getCelesteStatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            try
            {
                _StatusCeleste = celeste.HookProcess();
            } catch (Exception) { _StatusCeleste = false; }
            return _StatusCeleste;
        }

        public void SplitGo()
        {
            if (_SplitGo && !DebugMode)
            {
                try { _profile.ProfileSplitGo(+1); } catch (Exception) { }
                _SplitGo = false;
            }
        }

        private void SplitCheck()
        {
            lock (_object)
            {
                if (_PracticeMode)
                    PK = false;
                else
                {
                    if (_SplitGo) { Thread.Sleep(2000); }
                    _SplitGo = true;
                    PK = true;
                }
            }
        }

        public void setStatusSplitting(bool status)
        {
            dataCeleste.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }

        public void resetSplited()
        {
            if (dataCeleste.getChapterToSplit().Count > 0)
            {
                foreach (var c in dataCeleste.getChapterToSplit())
                {
                    c.IsSplited = false;
                }
            }
            _runStarted = false;
        }
        #endregion
        #region Object Management
        public void AddChapter(string chapter)
        {
            DefinitionsCeleste.ElementToSplitCeleste element = new DefinitionsCeleste.ElementToSplitCeleste()
            {
                Title = chapter
            };
            dataCeleste.chapterToSplit.Add(element);
        }

        public void RemoveChapter(string chapter)
        {
            dataCeleste.chapterToSplit.RemoveAll(ichapter => ichapter.Title == chapter);
        }

        public void clearData()
        {
            dataCeleste.chapterToSplit.Clear();
            _runStarted = false;
        }
        #endregion
        #region Checking
        public int getTimeInGame()
        {
            //Game Time return Second converted to ms and added +1 Because Condition in HCM to set Time into Split if Diferences are bigger than 1 second
            if (!_StatusCeleste) getCelesteStatusProcess(0);
            return (int)celeste.GameTime()*1000+1; 
        }

        public string getLevelName()
        {
            if (!_StatusCeleste) getCelesteStatusProcess(0);
            if (_StatusCeleste)
            {
                string AreaID;
                string LevelName;
                switch (infoPlayer.areaID)
                {
                    case Area.Prologue: AreaID = "Prologue"; break;
                    case Area.ForsakenCity: AreaID = "Forsaken City"; break;
                    case Area.OldSite: AreaID = "Old Site"; break;
                    case Area.CelestialResort: AreaID = "Celestial Resort"; break;
                    case Area.GoldenRidge: AreaID = "Golden Ridge"; break;
                    case Area.MirrorTemple: AreaID = "Mirror Temple"; break;
                    case Area.Reflection:  AreaID = "Reflection"; break;
                    case Area.TheSummit: AreaID = "TheSummit"; break;
                    case Area.Epilogue:  AreaID = "Epilogue"; break;
                    case Area.Core: AreaID = "Core"; break;
                    case Area.Farewell: AreaID = "Farewell"; break;
                    case Area.Menu: AreaID = "Menu"; break;
                    default: AreaID = string.Empty; break;
                }
                _ = infoPlayer.levelName == null ? LevelName = "None" : LevelName =  infoPlayer.levelName.ToString();
                return LevelName + " - " + AreaID;
            }
            else
                return string.Empty;
        }

        public bool IsInGame()
        {
            if (!_StatusCeleste) getCelesteStatusProcess(0);
            return _StatusCeleste && infoPlayer.areaID != Area.Menu;
        }
        #endregion
        #region Procedure
        public void LoadAutoSplitterProcedure()
        {
            var taskRefresh = new Task(() =>
            {
                RefreshCeleste();
            });
            var taskRefreshInfo = new Task(() =>
            {
                checkInfoPlayer();
            });
            var task1 = new Task(() =>
            {
                chapterToSplit();
            });
            taskRefresh.Start();
            taskRefreshInfo.Start();
            task1.Start();
        }
        #endregion
        #region CheckFlag Init()   
        private void RefreshCeleste()
        {
            int delay = 2000;
            getCelesteStatusProcess(delay);
            while (dataCeleste.enableSplitting)
            {
                Thread.Sleep(10);
                getCelesteStatusProcess(delay);
                if (!_StatusCeleste) { delay = 2000; } else { delay = 5000; }
            }
        }

        public void checkInfoPlayer()
        {
            while (dataCeleste.enableSplitting)
            {
                Thread.Sleep(10);
                if (_StatusCeleste)
                {
                    try
                    {
                        infoPlayer.elapsed = celeste.GameTime();
                        infoPlayer.completed = celeste.ChapterCompleted();
                        infoPlayer.areaID = celeste.AreaID();
                        infoPlayer.levelName = celeste.LevelName();
                    }
                    catch (Exception) { }
                }
            }
        }


        private int lastCassettes;
        private int lastHeartGems;

        private bool CheckItems(string level)
        {
            bool shouldSplit = false;
            bool chapterCassette = celeste.ChapterCassetteCollected();
            bool chapterHeart = celeste.ChapterHeartCollected();
            int cassettes = celeste.Cassettes();
            int heartGems = celeste.HeartGems();
            switch (level)
            {
                case "Chapter 1 - Forsaken City - Cassette":
                    shouldSplit = (infoPlayer.areaID == Area.ForsakenCity && (chapterCassette || cassettes == lastCassettes + 1)); break;
                case "Chapter 2 - Old Site - Cassette":
                    shouldSplit = (infoPlayer.areaID == Area.OldSite && (chapterCassette || cassettes == lastCassettes + 1)); break;
                case "Chapter 3 - Celestial Resort - Cassette":
                    shouldSplit = (infoPlayer.areaID == Area.CelestialResort && (chapterCassette || cassettes == lastCassettes + 1)); break;
                case "Chapter 4 - Golden Ridge - Cassette":
                    shouldSplit = (infoPlayer.areaID == Area.GoldenRidge && (chapterCassette || cassettes == lastCassettes + 1)); break;
                case "Chapter 5 - Mirror Temple - Cassette":
                    shouldSplit = (infoPlayer.areaID == Area.MirrorTemple && (chapterCassette || cassettes == lastCassettes + 1)); break;
                case "Chapter 6 - Reflection - Cassette":
                    shouldSplit = (infoPlayer.areaID == Area.Reflection && (chapterCassette || cassettes == lastCassettes + 1)); break;
                case "Chapter 7 - The Summit - Cassette":
                    shouldSplit = (infoPlayer.areaID == Area.TheSummit && (chapterCassette || cassettes == lastCassettes + 1)); break;
                case "Chapter 8 - Core - Cassette":
                    shouldSplit = (infoPlayer.areaID == Area.Core && (chapterCassette || cassettes == lastCassettes + 1)); break;

                case "Chapter 1 - Forsaken City - Heart Gem":
                    if (infoPlayer.areaID == Area.ForsakenCity && (chapterHeart || heartGems == lastHeartGems + 1)) shouldSplit = true; break;
                case "Chapter 2 - Old Site - Heart Gem":
                    if (infoPlayer.areaID == Area.OldSite && (chapterHeart || heartGems == lastHeartGems + 1)) shouldSplit = true; break;
                case "Chapter 3 - Celestial Resort - Heart Gem":
                    if (infoPlayer.areaID == Area.CelestialResort && (chapterHeart || heartGems == lastHeartGems + 1)) shouldSplit = true; break;
                case "Chapter 4 - Golden Ridge - Heart Gem":
                    if (infoPlayer.areaID == Area.GoldenRidge && (chapterHeart || heartGems == lastHeartGems + 1)) shouldSplit = true; break;
                case "Chapter 5 - Mirror Temple - Heart Gem":
                    if (infoPlayer.areaID == Area.MirrorTemple && (chapterHeart || heartGems == lastHeartGems + 1)) shouldSplit = true; break;
                case "Chapter 6 - Reflection - Heart Gem":
                    if (infoPlayer.areaID == Area.Reflection && (chapterHeart || heartGems == lastHeartGems + 1)) shouldSplit = true; break;
                case "Chapter 7 - The Summit - Heart Gem":
                    if (infoPlayer.areaID == Area.TheSummit && (chapterHeart || heartGems == lastHeartGems + 1)) shouldSplit = true; break;
                case "Chapter 8 - Core - Heart Gem":
                    if (infoPlayer.areaID == Area.Core && (chapterHeart || heartGems == lastHeartGems + 1)) shouldSplit = true; break;
                default: shouldSplit = false; break;
            }
            lastCassettes = cassettes;
            lastHeartGems = heartGems;
            return shouldSplit;
        }

        private void chapterToSplit()
        {
            bool shouldSplit = false;
            var ChapterToSplit = dataCeleste.getChapterToSplit();
            while (dataCeleste.enableSplitting)
            {
                Thread.Sleep(10);
                if (_StatusCeleste && !_PracticeMode && !_ShowSettings)
                {
                    if (ChapterToSplit != dataCeleste.getChapterToSplit()) ChapterToSplit = dataCeleste.getChapterToSplit();
                    foreach (var element in ChapterToSplit)
                    {
                        if (!element.IsSplited)
                        {
                            switch (element.Title)
                            {
                                case "Prologue (Complete)":
                                    shouldSplit = infoPlayer.areaID == Area.Prologue && infoPlayer.completed; break;
                                case "Chapter 1 - Forsaken City A/B/C (Complete)":
                                    shouldSplit = infoPlayer.areaID == Area.ForsakenCity && infoPlayer.completed; break;
                                case "Chapter 2 - Old Site A/B/C (Complete)":
                                    shouldSplit = infoPlayer.areaID == Area.OldSite && infoPlayer.completed; break;
                                case "Chapter 3 - Celestial Resort A/B/C (Complete)":
                                    shouldSplit = infoPlayer.areaID == Area.CelestialResort && infoPlayer.completed; break;
                                case "Chapter 4 - Golden Ridge A/B/C (Complete)":
                                    shouldSplit = infoPlayer.areaID == Area.GoldenRidge && infoPlayer.completed; break;
                                case "Chapter 5 - Mirror Temple A/B/C (Complete)":
                                    shouldSplit = infoPlayer.areaID == Area.MirrorTemple && infoPlayer.completed; break;
                                case "Chapter 6 - Reflection A/B/C (Complete)":
                                    shouldSplit = infoPlayer.areaID == Area.Reflection && infoPlayer.completed; break;
                                case "Chapter 7 - The Summit A/B/C (Complete)":
                                    shouldSplit = infoPlayer.areaID == Area.TheSummit && infoPlayer.completed; break;
                                case "Epilogue (Complete)":
                                    shouldSplit = infoPlayer.areaID == Area.Epilogue && infoPlayer.completed; break;
                                case "Chapter 8 - Core A/B/C (Complete)":
                                    shouldSplit = infoPlayer.areaID == Area.Core && infoPlayer.completed; break;
                                case "Chapter 9 - Farewell (Complete)":
                                    shouldSplit = infoPlayer.areaID == Area.Farewell && infoPlayer.completed; break;

                                case "Chapter 1 - Crossing (A) / Contraption (B) (CP 1)":
                                    shouldSplit = infoPlayer.areaID == Area.ForsakenCity && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "6" : "04"); break;
                                case "Chapter 1 - Chasm (A) / Scrap Pit (B) (CP 2)":
                                    shouldSplit = infoPlayer.areaID == Area.ForsakenCity && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "9b" : "08"); break;
                                case "Chapter 2 - Intervention (A) / Combination Lock (B) (CP 1)":
                                    shouldSplit = infoPlayer.areaID == Area.OldSite && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "3" : "03"); break;
                                case "Chapter 2 - Awake (A) / Dream Altar (B) (CP 2)":
                                    shouldSplit = infoPlayer.areaID == Area.OldSite && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "end_3" : "08b"); break;
                                case "Chapter 3 - Huge Mess (A) / Staff Quarters (B) (CP 1)":
                                    shouldSplit = infoPlayer.areaID == Area.CelestialResort && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "08-a" : "06"); break;
                                case "Chapter 3 - Elevator Shaft (A) / Library (B) (CP 2)":
                                    shouldSplit = infoPlayer.areaID == Area.CelestialResort && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "09-d" : "11"); break;
                                case "Chapter 3 - Presidential Suite (A) / Rooftop (B) (CP 3)":
                                    shouldSplit = infoPlayer.areaID == Area.CelestialResort && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "00-d" : "16"); break;
                                case "Chapter 4 - Shrine (A) / Stepping Stones (B) (CP 1)":
                                    shouldSplit = infoPlayer.areaID == Area.GoldenRidge && infoPlayer.levelName == "b-00"; break;
                                case "Chapter 4 - Old Trail (A) / Gusty Canyon (B) (CP 2)":
                                    shouldSplit = infoPlayer.areaID == Area.GoldenRidge && infoPlayer.levelName == "c-00"; break;
                                case "Chapter 4 - Cliff Face (A) / Eye Of The Storm (B) (CP 3)":
                                    shouldSplit = infoPlayer.areaID == Area.GoldenRidge && infoPlayer.levelName == "d-00"; break;
                                case "Chapter 5 - Depths (A) / Central Chamber (B) (CP 1)":
                                    shouldSplit = infoPlayer.areaID == Area.MirrorTemple && infoPlayer.levelName == "b-00"; break;
                                case "Chapter 5 - Unravelling (A) / Through The Mirror (B) (CP 2)":
                                    shouldSplit = infoPlayer.areaID == Area.MirrorTemple && infoPlayer.levelName == "c-00"; break;
                                case "Chapter 5 - Search (A) / Mix Master (B) (CP 3)":
                                    shouldSplit = infoPlayer.areaID == Area.MirrorTemple && infoPlayer.levelName == "d-00"; break;
                                case "Chapter 5 - Rescue (A) (CP 4)":
                                    shouldSplit = infoPlayer.areaID == Area.MirrorTemple && infoPlayer.levelName == "e-00"; break;
                                case "Chapter 6 - Lake (A) / Reflection (B) (CP 1)":
                                    shouldSplit = infoPlayer.areaID == Area.Reflection && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "00" : "b-00"); break;
                                case "Chapter 6 - Hollows (A) / Rock Bottom (B) (CP 2)":
                                    shouldSplit = infoPlayer.areaID == Area.Reflection && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "04" : "c-00"); break;
                                case "Chapter 6 - Reflection (A) / Reprieve (B) (CP 3)":
                                    shouldSplit = infoPlayer.areaID == Area.Reflection && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "b-00" : "d-00"); break;
                                case "Chapter 6 - Rock Bottom (A) (CP 4)":
                                    shouldSplit = infoPlayer.areaID == Area.Reflection && infoPlayer.levelName == "boss-00"; break;
                                case "Chapter 6 - Resolution (A) (CP 5)":
                                    shouldSplit = infoPlayer.areaID == Area.Reflection && infoPlayer.levelName == "after-00"; break;
                                case "Chapter 7 - 500M (A) / 500M (B) (CP 1)":
                                    shouldSplit = infoPlayer.areaID == Area.TheSummit && infoPlayer.levelName == "b-00"; break;
                                case "Chapter 7 - 1000M (A) / 1000M (B) (CP 2)":
                                    shouldSplit = infoPlayer.areaID == Area.TheSummit && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "c-00" : "c-01"); break;
                                case "Chapter 7 - 1500M (A) / 1500M (B) (CP 3)":
                                    shouldSplit = infoPlayer.areaID == Area.TheSummit && infoPlayer.levelName == "d-00"; break;
                                case "Chapter 7 - 2000M (A) / 2000M (B) (CP 4)":
                                    shouldSplit = infoPlayer.areaID == Area.TheSummit && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "e-00b" : "e-00"); break;
                                case "Chapter 7 - 2500M (A) / 2500M (B) (CP 5)":
                                    shouldSplit = infoPlayer.areaID == Area.TheSummit && infoPlayer.levelName == "f-00"; break;
                                case "Chapter 7 - 3000M (A) / 3000M (B) (CP 6)":
                                    shouldSplit = infoPlayer.areaID == Area.TheSummit && infoPlayer.levelName == "g-00"; break;
                                case "Chapter 8 - Into The Core (A) / Into The Core (B) (CP 1)":
                                    shouldSplit = infoPlayer.areaID == Area.Core && infoPlayer.levelName == "a-00"; break;
                                case "Chapter 8 - Hot And Cold (A) / Burning Or Freezing (B) (CP 2)":
                                    shouldSplit = infoPlayer.areaID == Area.Core && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "c-00" : "b-00"); break;
                                case "Chapter 8 - Heart Of The Mountain (A) / Heartbeat (B) (CP 3)":
                                    shouldSplit = infoPlayer.areaID == Area.Core && infoPlayer.levelName == (celeste.AreaDifficulty() == AreaMode.ASide ? "d-00" : "c-01"); break;
                                case "Chapter 9 - Singular (CP 1)":
                                    shouldSplit = infoPlayer.areaID == Area.Farewell && infoPlayer.levelName == "a-00"; break;
                                case "Chapter 9 - Power Source (CP 2)":
                                    shouldSplit = infoPlayer.areaID == Area.Farewell && infoPlayer.levelName == "c-00"; break;
                                case "Chapter 9 - Remembered (CP 3)":
                                    shouldSplit = infoPlayer.areaID == Area.Farewell && infoPlayer.levelName == "e-00z"; break;
                                case "Chapter 9 - Event Horizon (CP 4)":
                                    shouldSplit = infoPlayer.areaID == Area.Farewell && infoPlayer.levelName == "f-door"; break;
                                case "Chapter 9 - Determination (CP 5)":
                                    shouldSplit = infoPlayer.areaID == Area.Farewell && infoPlayer.levelName == "h-00b"; break;
                                case "Chapter 9 - Stubbornness (CP 6)":
                                    shouldSplit = infoPlayer.areaID == Area.Farewell && infoPlayer.levelName == "i-00"; break;
                                case "Chapter 9 - Reconciliation (CP 7)":
                                    shouldSplit = infoPlayer.areaID == Area.Farewell && infoPlayer.levelName == "j-00"; break;
                                case "Chapter 9 - Farewell (CP 8)":
                                    shouldSplit = infoPlayer.areaID == Area.Farewell && infoPlayer.levelName == "j-16"; break;

                                case "Chapter 1 - Forsaken City - Cassette":                                   
                                case "Chapter 2 - Old Site - Cassette":                               
                                case "Chapter 3 - Celestial Resort - Cassette":                                  
                                case "Chapter 4 - Golden Ridge - Cassette":                                   
                                case "Chapter 5 - Mirror Temple - Cassette":                                 
                                case "Chapter 6 - Reflection - Cassette":
                                case "Chapter 7 - The Summit - Cassette":                                  
                                case "Chapter 8 - Core - Cassette":                                   
                                case "Chapter 1 - Forsaken City - Heart Gem":                                   
                                case "Chapter 2 - Old Site - Heart Gem":                                    
                                case "Chapter 3 - Celestial Resort - Heart Gem":
                                case "Chapter 4 - Golden Ridge - Heart Gem":
                                case "Chapter 5 - Mirror Temple - Heart Gem":
                                case "Chapter 6 - Reflection - Heart Gem":
                                case "Chapter 7 - The Summit - Heart Gem":
                                case "Chapter 8 - Core - Heart Gem":
                                    shouldSplit = CheckItems(element.Title); break;
    
                                case "===========================":
                                    RemoveChapter(element.Title); shouldSplit = false; break;

                                default:
                                    shouldSplit = false; break;
                            }

                            if (shouldSplit)
                            {                       
                                SplitCheck();
                                element.IsSplited = PK;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
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
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using LiveSplit.HollowKnight;
using HitCounterManager;

namespace AutoSplitterCore
{
    public class HollowSplitter
    {
        public static HollowKnightInfo hollow = new HollowKnightInfo();
        public DefinitionHollow defH = new DefinitionHollow();      
        public bool _StatusHollow = false;
        public bool _runStarted = false;
        public bool _SplitGo = false;
        public DTHollow dataHollow;
        public ProfilesControl _profile;    
        public DefinitionHollow.Vector3F currentPosition = new DefinitionHollow.Vector3F();
        private static readonly object _object = new object();
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };
        public bool DebugMode = false;
        public bool _PracticeMode = false;
        public bool _ShowSettings = false;

        #region Control Management
        public DTHollow getDataHollow()
        {
            return this.dataHollow;
        }

        public void setDataHollow(DTHollow data, ProfilesControl profile)
        {
            this.dataHollow = data;
            this._profile = profile;
            _update_timer.Tick += (sender, args) => SplitGo();
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
                if (_SplitGo) { Thread.Sleep(2000); }
                _SplitGo = true;
            }
        }

        public bool getHollowStatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            return _StatusHollow = hollow.Memory.HookProcess();
        }

        public void setStatusSplitting(bool status)
        {
            dataHollow.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }

        public void resetSplited()
        {
            if (dataHollow.getBosstoSplit().Count > 0)
            {
                foreach (var b in dataHollow.getBosstoSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataHollow.getMiniBossToSplit().Count > 0)
            {
                foreach (var mb in dataHollow.getMiniBossToSplit())
                {
                    mb.IsSplited = false;
                }
            }

            if (dataHollow.getPhanteonToSplit().Count > 0)
            {
                foreach (var p in dataHollow.getPhanteonToSplit())
                {
                    p.IsSplited = false;
                }
            }

            if (dataHollow.getCharmToSplit().Count > 0)
            {
                foreach (var c in dataHollow.getCharmToSplit())
                {
                    c.IsSplited = false;
                }
            }

            if (dataHollow.getSkillsToSplit().Count > 0)
            {
                foreach (var s in dataHollow.getSkillsToSplit())
                {
                    s.IsSplited = false;
                }
            }

            if (dataHollow.getPositionToSplit().Count > 0)
            {
                foreach (var p in dataHollow.getPositionToSplit())
                {
                    p.IsSplited = false;
                }
            }
            _runStarted = false;
        }
        #endregion
        #region Object Management
        public void AddBoss(string boss)
        {
            DefinitionHollow.ElementToSplitH element = defH.stringToEnum(boss);
            dataHollow.bossToSplit.Add(element);

        }
        public void AddMiniBoss(string boss)
        {
            DefinitionHollow.ElementToSplitH element = defH.stringToEnum(boss);
            dataHollow.miniBossToSplit.Add(element);
        }


        public void AddPantheon(string Pantheon)
        {
            DefinitionHollow.Pantheon phan = new DefinitionHollow.Pantheon() { Title = Pantheon };
            dataHollow.phanteonToSplit.Add(phan);
        }

        public void AddCharm(string charm)
        {
            DefinitionHollow.ElementToSplitH element = defH.stringToEnum(charm);
            dataHollow.charmToSplit.Add(element);

        }

        public void AddSkill(string skill)
        {
            DefinitionHollow.ElementToSplitH element = defH.stringToEnum(skill);
            dataHollow.skillsToSplit.Add(element);

        }

        public void AddPosition(PointF position, string scene)
        {
            DefinitionHollow.Vector3F vector = new DefinitionHollow.Vector3F()
            { position = position, sceneName = scene, previousScene = null };
            dataHollow.positionToSplit.Add(vector);

        }

        public void RemoveBoss(string boss)
        {
            dataHollow.bossToSplit.RemoveAll(i => i.Title == boss);
        }

        public void RemoveMiniBoss(string boss)
        {
            dataHollow.miniBossToSplit.RemoveAll(i => i.Title == boss);

        }

        public void RemovePantheon(string Pantheon)
        {
            dataHollow.phanteonToSplit.RemoveAll(i => i.Title == Pantheon);
        }


        public void RemoveCharm(string charm)
        {
            dataHollow.charmToSplit.RemoveAll(i => i.Title == charm);
        }

        public void RemoveSkill(string skill)
        {
            dataHollow.skillsToSplit.RemoveAll(i => i.Title == skill);

        }

        public void RemovePosition(int position)
        {
            dataHollow.positionToSplit.RemoveAt(position);
        }

        public void clearData()
        {
            dataHollow.bossToSplit.Clear();
            dataHollow.miniBossToSplit.Clear();
            dataHollow.phanteonToSplit.Clear();
            dataHollow.charmToSplit.Clear();
            dataHollow.skillsToSplit.Clear();
            dataHollow.positionToSplit.Clear();
            dataHollow.positionMargin = 3;
            _runStarted = false;
        }
        #endregion
        #region Checking
        public PointF getCurrentPosition()
        {
            manualRefreshPosition();
            return this.currentPosition.position;
        }

        public bool getIsLoading()
        {
            return hollow.Memory.GameState() == GameState.LOADING ? true : false;
        }

        #endregion
        #region Procedure
        public void LoadAutoSplitterProcedure()
        {
            var taskRefresh = new Task(() =>
            {
                RefreshHollow();
            });

            var taskRefreshPosition = new Task(() =>
            {
                RefreshPosition();
            });

            var taskCheckStart = new Task(() =>
            {
                checkStart();
            });

            var task1 = new Task(() =>
            {
                bossToSplit();
            });

            var task2 = new Task(() =>
            {
                miniBossToSplit();
            });

            var task3 = new Task(() =>
            {
                pantheonToSplit();
            });

            var task4 = new Task(() =>
            {
                charmToSplit();
            });

            var task5 = new Task(() =>
            {
                skillsToSplit();
            });

            var task6 = new Task(() =>
            {
                positionToSplit();
            });

            taskRefresh.Start();
            taskRefreshPosition.Start();
            taskCheckStart.Start();
            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();
            task5.Start();
            task6.Start();
        }
        #endregion
        #region CheckFlag Init()   
        private void RefreshHollow()
        {
            int delay = 2000;
            getHollowStatusProcess(0);
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(10);
                getHollowStatusProcess(delay);
                if (!_StatusHollow) { delay = 2000; } else { delay = 20000; }
            }
        }

        private void checkStart()
        {
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(500);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (hollow.Memory.GameState() == GameState.PLAYING)
                    {
                        _runStarted = true;
                    }
                    if (dataHollow.gameTimer && (hollow.Memory.GameState() == GameState.LOADING || hollow.Memory.GameState() == GameState.CUTSCENE))
                    {
                        do
                        {
                            _runStarted = false;
                        } while (hollow.Memory.GameState() == GameState.LOADING || hollow.Memory.GameState() == GameState.ENTERING_LEVEL || hollow.Memory.GameState() == GameState.EXITING_LEVEL);
                        _runStarted = true;
                    }

                    if (currentPosition.sceneName.StartsWith("Quit_To_Menu") || currentPosition.sceneName.StartsWith("Menu_Title") || currentPosition.sceneName.StartsWith("PermaDeath") || hollow.Memory.GameState() == GameState.MAIN_MENU)
                    {
                        _runStarted = false;
                    }
                }
            }
        }

        private void RefreshPosition()
        {     
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(10);
                if (_StatusHollow)
                {
                    currentPosition.position = hollow.Memory.GetCameraTarget();
                    if (hollow.Memory.SceneName() != currentPosition.sceneName)
                    {
                        currentPosition.previousScene = currentPosition.sceneName;
                        currentPosition.sceneName = hollow.Memory.SceneName();
                    }
                }
            }
        }

        private void manualRefreshPosition()
        {
            getHollowStatusProcess(0);
            currentPosition.position = hollow.Memory.GetCameraTarget();
            currentPosition.sceneName = hollow.Memory.SceneName();
        }

       
        private void bossToSplit()
        {
            var BossToSplit = dataHollow.getBosstoSplit();
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(3000);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (BossToSplit != dataHollow.getBosstoSplit()) BossToSplit = dataHollow.getBosstoSplit();
                    foreach (var element in BossToSplit)
                    {
                        if (!element.IsSplited && hollow.Memory.PlayerData<bool>(element.Offset))
                        {
                            element.IsSplited = true;
                            SplitCheck();
                        }
                    }
                }
            }
        }

        private void miniBossToSplit()
        {
            var MiniBossToSplit = dataHollow.getMiniBossToSplit();
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(3000);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (MiniBossToSplit != dataHollow.getMiniBossToSplit()) MiniBossToSplit = dataHollow.getMiniBossToSplit();
                    foreach (var element in MiniBossToSplit)
                    {
                        if (!element.IsSplited)
                        {
                            if (element.intMethod)
                            {
                                if (_StatusHollow && hollow.Memory.PlayerData<int>(element.Offset) == element.intCompare)
                                {
                                    element.IsSplited = true;
                                    SplitCheck();
                                }
                            }
                            else
                            {
                                if (hollow.Memory.PlayerData<bool>(element.Offset))
                                {
                                    element.IsSplited = true;
                                    SplitCheck();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void charmToSplit()
        {
            var CharmToSplit = dataHollow.getCharmToSplit();
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(3000);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (CharmToSplit != dataHollow.getCharmToSplit()) CharmToSplit = dataHollow.getCharmToSplit();
                    foreach (var element in CharmToSplit)
                    {
                        if (!element.IsSplited)
                        {
                            if (element.intMethod)
                            {
                                if (_StatusHollow && hollow.Memory.PlayerData<int>(element.Offset) == element.intCompare)
                                {
                                    element.IsSplited = true;
                                    SplitCheck();
                                }
                            }
                            else
                            {
                                if (hollow.Memory.PlayerData<bool>(element.Offset) && !element.kingSoulsCase)
                                {
                                    element.IsSplited = true;
                                    SplitCheck();
                                }
                                else
                                {
                                    if (hollow.Memory.PlayerData<int>(Offset.charmCost_36) == 5 && hollow.Memory.PlayerData<int>(Offset.royalCharmState) == 3)
                                    {
                                        element.IsSplited = true;
                                        SplitCheck();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void skillsToSplit()
        {
            var SkillsToSplit = dataHollow.getSkillsToSplit();
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(3000);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (SkillsToSplit != dataHollow.getSkillsToSplit()) SkillsToSplit = dataHollow.getSkillsToSplit();
                    foreach (var element in SkillsToSplit)
                    {
                        if (!element.IsSplited)
                        {
                            if (element.intMethod)
                            {
                                if (_StatusHollow && hollow.Memory.PlayerData<int>(element.Offset) == element.intCompare)
                                {
                                    element.IsSplited = true;
                                    SplitCheck();
                                }
                            }
                            else
                            {
                                if (hollow.Memory.PlayerData<bool>(element.Offset))
                                {
                                    element.IsSplited = true;
                                    SplitCheck();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void positionToSplit()
        {
            var PositionToSplit = dataHollow.getPositionToSplit();
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(100);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (PositionToSplit != dataHollow.getPositionToSplit()) PositionToSplit = dataHollow.getPositionToSplit();
                    foreach (var p in PositionToSplit)
                    {
                        if (!p.IsSplited)
                        {
                            var currentlyPosition = this.currentPosition;
                            var rangeX = ((currentlyPosition.position.X - p.position.X) <= dataHollow.positionMargin) && ((currentlyPosition.position.X - p.position.X) >= -dataHollow.positionMargin);
                            var rangeY = ((currentlyPosition.position.Y - p.position.Y) <= dataHollow.positionMargin) && ((currentlyPosition.position.Y - p.position.Y) >= -dataHollow.positionMargin);
                            var rangeZ = currentPosition.sceneName == p.sceneName;
                            if (rangeX && rangeY && rangeZ)
                            {
                                p.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }


        private bool PantheonCase(string title)
        {
            bool shouldSplit = false;
            bool NotDeath = !currentPosition.sceneName.StartsWith("GG_Atrium") && !currentPosition.sceneName.StartsWith("Quit_To_Menu") && !currentPosition.sceneName.StartsWith("PermaDeath");
            switch (title)
            {
                case "Vengefly King":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Vengefly") && NotDeath; break;
                case "Gruz Mother":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Gruz_Mother") && NotDeath; break;
                case "False Knight":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_False_Knight") && NotDeath; break;
                case "Massive Moss Charger":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Mega_Moss_Charger") && NotDeath; break;
                case "Hornet (Protector)":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Hornet_1") && NotDeath; break;
                case "Gorb":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Gorb") && NotDeath; break;
                case "Dung Defender":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Dung_Defender") && NotDeath; break;
                case "Soul Warrior":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Mage_Knight") && NotDeath; break;
                case "Brooding Mawlek":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Brooding_Mawlek") && NotDeath; break;
                case "Oro & Mato Nail Bros":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Nailmasters") && NotDeath; break;
                case "Xero":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Xero") && NotDeath; break;
                case "Crystal Guardian":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Crystal_Guardian") && NotDeath; break;
                case "Soul Master":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Soul_Master") && NotDeath; break;
                case "Oblobbles":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Oblobble") && NotDeath; break;
                case "Sisters of Battle":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Mantis_Lord") && NotDeath; break;
                case "Marmu":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Marmu") && NotDeath; break;
                case "Flukemarm":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Flukemarm") && NotDeath; break;
                case "Broken Vessel":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Broken_Vessel") && NotDeath; break;
                case "Galien":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Galien") && NotDeath; break;
                case "Paintmaster Sheo":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Painter") && NotDeath; break;
                case "Hive Knight":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Hive_Knight") && NotDeath; break;
                case "Elder Hu":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Hu") && NotDeath; break;
                case "The Collector":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Collector") && NotDeath; break;
                case "God Tamer":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_God_Tamer") && NotDeath; break;
                case "Troupe Master Grim":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Grimm") && NotDeath; break;
                case "Watcher Knights":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Watcher_Knights") && NotDeath; break;
                case "Uumuu":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Uumuu") && NotDeath; break;
                case "Nosk":
                    shouldSplit = currentPosition.previousScene == "GG_Nosk" && NotDeath; break;
                case "Winged Nosk":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Nosk_Hornet") && NotDeath; break;
                case "Great Nailsage Slay":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Sly") && NotDeath; break;
                case "Hornet (Sentinel)":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Hornet_2") && NotDeath; break;
                case "Enraged Guardian":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Crystal_Guardian_2") && NotDeath; break;
                case "Lost Kin":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Lost_Kin") && NotDeath; break;
                case "No Eyes":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_No_Eyes") && NotDeath; break;
                case "Traitor Lord":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Traitor_Lord") && NotDeath; break;
                case "White Defender":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_White_Defender") && NotDeath; break;
                case "Soul Tyrant":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Soul_Tyrant") && NotDeath; break;
                case "Markoth":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Ghost_Markoth") && NotDeath; break;
                case "Grey Prince Zote":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Grey_Prince_Zote") && NotDeath; break;
                case "Failed Champion":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Failed_Champion") && NotDeath; break;
                case "Nightmare King Grimm":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Grimm_Nightmare") && NotDeath; break;
                case "Pure Vessel":
                    shouldSplit = currentPosition.previousScene.StartsWith("GG_Hollow_Knight") && NotDeath; break;
                case "Absolute Radiance":
                    shouldSplit = currentPosition.sceneName.StartsWith("Cinematic_Ending_E"); break;
            }
            return shouldSplit;
        }

        private void pantheonToSplit()
        {
            List <DefinitionHollow.Pantheon> PantheonToSplit = dataHollow.getPhanteonToSplit();
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(10);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (PantheonToSplit != dataHollow.getPhanteonToSplit()) PantheonToSplit = dataHollow.getPhanteonToSplit();
                    if (dataHollow.PantheonMode == 0)
                    {
                        foreach (var element in PantheonToSplit)
                        {
                            if (!element.IsSplited && PantheonCase(element.Title))
                            {
                                element.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                    else
                    {
                        foreach (var element in PantheonToSplit)
                        {
                            if (element.Title == "Pantheon of the Master" && !element.IsSplited)
                            {
                                if (currentPosition.previousScene.StartsWith("GG_Nailmasters") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                                {
                                    SplitCheck();
                                    element.IsSplited = true;
                                }
                            }


                            if (element.Title == "Pantheon of the Artist" && !element.IsSplited)
                            {
                                if (currentPosition.previousScene.StartsWith("GG_Painter") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                                {
                                    SplitCheck();
                                    element.IsSplited = true;
                                }
                            }

                            if (element.Title == "Pantheon of the Sage" && !element.IsSplited)
                            {
                                if (currentPosition.previousScene.StartsWith("GG_Sly") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                                {
                                    SplitCheck();
                                    element.IsSplited = true;
                                }

                            }

                            if (element.Title == "Pantheon of the Knight" && !element.IsSplited)
                            {
                                if (currentPosition.previousScene.StartsWith("GG_Hollow_Knight") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                                {
                                    SplitCheck();
                                    element.IsSplited = true;
                                }
                            }

                            if (element.Title == "Pantheon of Hallownest" && !element.IsSplited)
                            {
                                if (currentPosition.sceneName.StartsWith("Cinematic_Ending_E"))
                                {
                                    SplitCheck();
                                    element.IsSplited = true;
                                }
                            }
                        }
                    }
                }
            }
        } 
       
        #endregion
    }
}

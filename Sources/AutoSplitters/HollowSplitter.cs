//MIT License

//Copyright (c) 2022-2025 Ezequiel Medina

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
using System.Xml.Linq;

namespace AutoSplitterCore
{
    public class HollowSplitter
    {   
        private static HollowKnightInfo hollow = new HollowKnightInfo();
        private ISplitterControl splitterControl = SplitterControl.GetControl();
        private DefinitionHollow defH = new DefinitionHollow();      
        private DTHollow dataHollow;

        public DefinitionHollow.Vector3F currentPosition = new DefinitionHollow.Vector3F();

        public bool _StatusHollow = false;
        public bool _PracticeMode = false;
        public bool _ShowSettings = false;
        public bool _runStarted = false;

        #region Control Management
        public DTHollow GetDataHollow() => dataHollow;

        public void SetDataHollow(DTHollow data) => dataHollow = data;

        public bool GetHollowStatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            try
            {
                _StatusHollow = hollow.Memory.HookProcess();
            } catch (Exception) { _StatusHollow = false; }
            return _StatusHollow;
        }

        public void SetStatusSplitting(bool status)
        {
            dataHollow.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); }
        }

        public void ResetSplited()
        {
            if (dataHollow.GetBosstoSplit().Count > 0)
            {
                foreach (var b in dataHollow.GetBosstoSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataHollow.GetMiniBossToSplit().Count > 0)
            {
                foreach (var mb in dataHollow.GetMiniBossToSplit())
                {
                    mb.IsSplited = false;
                }
            }

            if (dataHollow.GetPhanteonToSplit().Count > 0)
            {
                foreach (var p in dataHollow.GetPhanteonToSplit())
                {
                    p.IsSplited = false;
                }
            }

            if (dataHollow.GetCharmToSplit().Count > 0)
            {
                foreach (var c in dataHollow.GetCharmToSplit())
                {
                    c.IsSplited = false;
                }
            }

            if (dataHollow.GetSkillsToSplit().Count > 0)
            {
                foreach (var s in dataHollow.GetSkillsToSplit())
                {
                    s.IsSplited = false;
                }
            }

            if (dataHollow.GetPositionToSplit().Count > 0)
            {
                foreach (var p in dataHollow.GetPositionToSplit())
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
            DefinitionHollow.ElementToSplitH element = defH.StringToEnum(boss);
            dataHollow.bossToSplit.Add(element);

        }
        public void AddMiniBoss(string boss)
        {
            DefinitionHollow.ElementToSplitH element = defH.StringToEnum(boss);
            dataHollow.miniBossToSplit.Add(element);
        }


        public void AddPantheon(string Pantheon)
        {
            DefinitionHollow.Pantheon phan = new DefinitionHollow.Pantheon() { Title = Pantheon };
            dataHollow.phanteonToSplit.Add(phan);
        }

        public void AddCharm(string charm)
        {
            DefinitionHollow.ElementToSplitH element = defH.StringToEnum(charm);
            dataHollow.charmToSplit.Add(element);

        }

        public void AddSkill(string skill)
        {
            DefinitionHollow.ElementToSplitH element = defH.StringToEnum(skill);
            dataHollow.skillsToSplit.Add(element);

        }

        public void AddPosition(PointF position, string scene, string title)
        {
            DefinitionHollow.Vector3F vector = new DefinitionHollow.Vector3F()
            { position = position, sceneName = scene, previousScene = null, Title = title };
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

        public void ClearData()
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
        public PointF GetCurrentPosition()
        {
            ManualRefreshPosition();
            return this.currentPosition.position;
        }

        public bool IsNewgame()
        {
            if (!_StatusHollow) GetHollowStatusProcess(0);
            return currentPosition.sceneName.StartsWith("Opening_Sequence");
        }
        #endregion
        #region Procedure
        public void LoadAutoSplitterProcedure()
        {
            Task.Run(() => RefreshHollow());
            Task.Run(() => RefreshPosition());
            Task.Run(() => CheckStart());
            Task.Run(() => BossToSplit());
            Task.Run(() => MiniBossToSplit());
            Task.Run(() => PantheonToSplit());
            Task.Run(() => CharmToSplit());
            Task.Run(() => SkillsToSplit());
            Task.Run(() => PositionToSplit());
        }
        #endregion
        #region CheckFlag Init()   
        private void RefreshHollow()
        {
            int delay = 2000;
            GetHollowStatusProcess(0);
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(10);
                GetHollowStatusProcess(delay);
                if (!_StatusHollow) { delay = 2000; } else { delay = 5000; }
            }
        }

        private void CheckStart()
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

        private void ManualRefreshPosition()
        {
            GetHollowStatusProcess(0);
            currentPosition.position = hollow.Memory.GetCameraTarget();
            currentPosition.sceneName = hollow.Memory.SceneName();
        }

       
        private void BossToSplit()
        {
            var BossToSplit = dataHollow.GetBosstoSplit();
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (BossToSplit != dataHollow.GetBosstoSplit()) BossToSplit = dataHollow.GetBosstoSplit();
                    foreach (var element in BossToSplit)
                    {
                        if (!element.IsSplited && hollow.Memory.PlayerData<bool>(element.Offset))
                        {
                            splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT BOSS -> {element.Title}");
                            element.IsSplited = splitterControl.GetSplitStatus();
                        }
                    }
                }
            }
        }

        private void MiniBossToSplit()
        {
            var MiniBossToSplit = dataHollow.GetMiniBossToSplit();
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (MiniBossToSplit != dataHollow.GetMiniBossToSplit()) MiniBossToSplit = dataHollow.GetMiniBossToSplit();
                    foreach (var element in MiniBossToSplit)
                    {
                        if (!element.IsSplited)
                        {
                            if (element.intMethod)
                            {
                                if (_StatusHollow && hollow.Memory.PlayerData<int>(element.Offset) == element.intCompare)
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT MINIBOSS -> {element.Title}");
                                    element.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }
                            else
                            {
                                if (hollow.Memory.PlayerData<bool>(element.Offset))
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT MINIBOSS -> {element.Title}");
                                    element.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CharmToSplit()
        {
            var CharmToSplit = dataHollow.GetCharmToSplit();
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (CharmToSplit != dataHollow.GetCharmToSplit()) CharmToSplit = dataHollow.GetCharmToSplit();
                    foreach (var element in CharmToSplit)
                    {
                        if (!element.IsSplited)
                        {
                            if (element.intMethod)
                            {
                                if (_StatusHollow && hollow.Memory.PlayerData<int>(element.Offset) == element.intCompare)
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT CHARM -> {element.Title}");
                                    element.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }
                            else
                            {
                                if (hollow.Memory.PlayerData<bool>(element.Offset) && !element.kingSoulsCase)
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT CHARM -> {element.Title}");
                                    element.IsSplited = splitterControl.GetSplitStatus();
                                }
                                else
                                {
                                    if (hollow.Memory.PlayerData<int>(Offset.charmCost_36) == 5 && hollow.Memory.PlayerData<int>(Offset.royalCharmState) == 3)
                                    {
                                        splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT CHARM -> {element.Title}");
                                        element.IsSplited = splitterControl.GetSplitStatus();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SkillsToSplit()
        {
            var SkillsToSplit = dataHollow.GetSkillsToSplit();
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (SkillsToSplit != dataHollow.GetSkillsToSplit()) SkillsToSplit = dataHollow.GetSkillsToSplit();
                    foreach (var element in SkillsToSplit)
                    {
                        if (!element.IsSplited)
                        {
                            if (element.intMethod)
                            {
                                if (_StatusHollow && hollow.Memory.PlayerData<int>(element.Offset) == element.intCompare)
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT SKILL -> {element.Title}");
                                    element.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }
                            else
                            {
                                if (hollow.Memory.PlayerData<bool>(element.Offset))
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT SKILL -> {element.Title}");
                                    element.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void PositionToSplit()
        {
            var PositionToSplit = dataHollow.GetPositionToSplit();
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(100);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (PositionToSplit != dataHollow.GetPositionToSplit()) PositionToSplit = dataHollow.GetPositionToSplit();
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
                                splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT Position -> {p.Title} - {p.position}");
                                p.IsSplited = splitterControl.GetSplitStatus();
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

        private void PantheonToSplit()
        {
            List <DefinitionHollow.Pantheon> PantheonToSplit = dataHollow.GetPhanteonToSplit();
            while (dataHollow.enableSplitting)
            {
                Thread.Sleep(10);
                if (_StatusHollow && !_PracticeMode && !_ShowSettings)
                {
                    if (PantheonToSplit != dataHollow.GetPhanteonToSplit()) PantheonToSplit = dataHollow.GetPhanteonToSplit();
                    if (dataHollow.PantheonMode == 0)
                    {
                        foreach (var element in PantheonToSplit)
                        {
                            if (!element.IsSplited && PantheonCase(element.Title))
                            {
                                splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT PHANTEON -> {element.Title}");
                                element.IsSplited = splitterControl.GetSplitStatus();
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
                                    splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT PHANTEON -> {element.Title}");
                                    element.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }


                            if (element.Title == "Pantheon of the Artist" && !element.IsSplited)
                            {
                                if (currentPosition.previousScene.StartsWith("GG_Painter") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT PHANTEON -> {element.Title}");
                                    element.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }

                            if (element.Title == "Pantheon of the Sage" && !element.IsSplited)
                            {
                                if (currentPosition.previousScene.StartsWith("GG_Sly") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT PHANTEON -> {element.Title}");
                                    element.IsSplited = splitterControl.GetSplitStatus();
                                }

                            }

                            if (element.Title == "Pantheon of the Knight" && !element.IsSplited)
                            {
                                if (currentPosition.previousScene.StartsWith("GG_Hollow_Knight") && !currentPosition.sceneName.StartsWith("GG_Atrium"))
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT PHANTEON -> {element.Title}");
                                    element.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }

                            if (element.Title == "Pantheon of Hallownest" && !element.IsSplited)
                            {
                                if (currentPosition.sceneName.StartsWith("Cinematic_Ending_E"))
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: HOLLOW KNIGHT PHANTEON -> {element.Title}");
                                    element.IsSplited = splitterControl.GetSplitStatus();
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

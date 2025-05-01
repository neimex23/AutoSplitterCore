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

using LiveSplit.Cuphead;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace AutoSplitterCore
{
    public class CupheadSplitter
    {
        private static MemoryManager cup = new MemoryManager();
        private DTCuphead dataCuphead;
        private ISplitterControl splitterControl = SplitterControl.GetControl();

        public bool _StatusCuphead = false;
        public bool _PracticeMode = false;
        public bool _ShowSettings = false;

        #region SingletonFactory
        private static CupheadSplitter _intance = new CupheadSplitter();

        private CupheadSplitter() { }

        public static CupheadSplitter GetIntance() { return _intance; }
        #endregion

        #region Control Management
        public DTCuphead GetDataCuphead() => dataCuphead;

        public void SetDataCuphead(DTCuphead data) => dataCuphead = data;

        public bool GetCupheadStatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            try
            {
                cup.HookProcess();
                cup.GamePointers();
            }
            catch (Exception) { return _StatusCuphead = false; }

            return _StatusCuphead = cup.IsHooked;
        }

        public void SetStatusSplitting(bool status)
        {
            dataCuphead.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); }
        }

        public void ResetSplited()
        {
            if (dataCuphead.GetElementToSplit().Count > 0)
            {
                foreach (var b in dataCuphead.GetElementToSplit())
                {
                    b.IsSplited = false;
                }
            }
        }
        #endregion
        #region Object Management
        public void AddElement(string element)
        {
            if (element != "-------DLC--------")
            {
                DefinitionCuphead.ElementsToSplitCup cElem = new DefinitionCuphead.ElementsToSplitCup()
                {
                    Title = element
                };
                dataCuphead.elementToSplit.Add(cElem);
            }
        }

        public void RemoveElement(string element)
        {
            dataCuphead.elementToSplit.RemoveAll(i => i.Title == element);
        }

        public void ClearData()
        {
            dataCuphead.elementToSplit.Clear();
        }
        #endregion
        #region Checking
        public int GetTimeInGame()
        {
            //Game Time return Second converted to ms and added +1 Because Condition in HCM to set Time into Split if Diferences are bigger than 1 second
            //Warning Return Time per Level and is seted to 0 after finish.
            return (int)cup.LevelTime() * 1000 + 1;
        }

        public string GetSceneName()
        {
            if (!_StatusCuphead) GetCupheadStatusProcess(0);
            return _StatusCuphead ? cup.SceneName() : string.Empty;
        }

        public bool LevelCompleted()
        {
            if (!_StatusCuphead) GetCupheadStatusProcess(0);
            return _StatusCuphead && cup.LevelEnding();
        }

        public bool IsInGame()
        {
            if (!_StatusCuphead) GetCupheadStatusProcess(0);
            return _StatusCuphead && cup.InGame();
        }
        #endregion
        #region Procedure
        public void LoadAutoSplitterProcedure()
        {
            var actions = new Action[]
                {
                    () => RefreshCuphead(),
                    () => ElementToSplit()
                };

            foreach (var action in actions)
            {
                Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Current);
            }
        }
        #endregion
        #region CheckFlag Init()
        private void RefreshCuphead()
        {
            int delay = 2000;
            GetCupheadStatusProcess(0);
            while (dataCuphead.enableSplitting)
            {
                Thread.Sleep(10);
                GetCupheadStatusProcess(delay);
                if (!_StatusCuphead) { delay = 2000; } else { delay = 5000; }
            }
        }

        private bool ElementCase(string Title)
        {
            bool shouldSplit = false;
            switch (Title)
            {
                //Levels
                case "Forest Follies":
                    shouldSplit = cup.SceneName() == "scene_level_platforming_1_1F" && cup.LevelEnding(); break;
                case "Treetop Trouble":
                    shouldSplit = cup.SceneName() == "scene_level_platforming_1_2F" && cup.LevelEnding(); break;
                case "Funfair Fever":
                    shouldSplit = cup.SceneName() == "scene_level_platforming_2_1F" && cup.LevelEnding(); break;
                case "Funhouse Frazzle":
                    shouldSplit = cup.SceneName() == "scene_level_platforming_2_2F" && cup.LevelEnding(); break;
                case "Perilous Piers":
                    shouldSplit = cup.SceneName() == "scene_level_platforming_3_1F" && cup.LevelEnding(); break;
                case "Rugged Ridge":
                    shouldSplit = cup.SceneName() == "scene_level_platforming_3_2F" && cup.LevelEnding(); break;
                case "Mausoleum I":
                    shouldSplit = cup.SceneName() == "scene_level_mausoleum" && cup.LevelMode() == Mode.Easy && (cup.LevelEnding() && cup.LevelWon()); break;
                case "Mausoleum II":
                    shouldSplit = cup.SceneName() == "scene_level_mausoleum" && cup.LevelMode() == Mode.Normal && (cup.LevelEnding() && cup.LevelWon()); break;
                case "Mausoleum III":
                    shouldSplit = cup.SceneName() == "scene_level_mausoleum" && cup.LevelMode() == Mode.Hard && (cup.LevelEnding() && cup.LevelWon()); break;

                //Bosses
                case "The Root Pack":
                    shouldSplit = cup.SceneName() == "scene_level_veggies" && cup.LevelComplete(Levels.Veggies); break;
                case "Goopy Le Grande":
                    shouldSplit = cup.SceneName() == "scene_level_slime" && cup.LevelComplete(Levels.Slime); break;
                case "Cagney Carnation":
                    shouldSplit = cup.SceneName() == "scene_level_flower" && cup.LevelComplete(Levels.Flower); break;
                case "Ribby And Croaks":
                    shouldSplit = cup.SceneName() == "scene_level_frogs" && cup.LevelComplete(Levels.Frogs); break;
                case "Hilda Berg":
                    shouldSplit = cup.SceneName() == "scene_level_flying_blimp" && cup.LevelComplete(Levels.FlyingBlimp); break;
                case "Baroness Von Bon Bon":
                    shouldSplit = cup.SceneName() == "scene_level_baroness" && cup.LevelComplete(Levels.Baroness); break;
                case "Djimmi The Great":
                    shouldSplit = cup.SceneName() == "scene_level_flying_genie" && cup.LevelComplete(Levels.FlyingGenie); break;
                case "Beppi The Clown":
                    shouldSplit = cup.SceneName() == "scene_level_clown" && cup.LevelComplete(Levels.Clown); break;
                case "Wally Warbles":
                    shouldSplit = cup.SceneName() == "scene_level_flying_bird" && cup.LevelComplete(Levels.FlyingBird); break;
                case "Grim Matchstick":
                    shouldSplit = cup.SceneName() == "scene_level_dragon" && cup.LevelComplete(Levels.Dragon); break;
                case "Rumor Honeybottoms":
                    shouldSplit = cup.SceneName() == "scene_level_bee" && cup.LevelComplete(Levels.Bee); break;
                case "Captin Brineybeard":
                    shouldSplit = cup.SceneName() == "scene_level_pirate" && cup.LevelComplete(Levels.Pirate); break;
                case "Werner Werman":
                    shouldSplit = cup.SceneName() == "scene_level_mouse" && cup.LevelComplete(Levels.Mouse); break;
                case "Dr. Kahl's Robot":
                    shouldSplit = cup.SceneName() == "scene_level_robot" && cup.LevelComplete(Levels.Robot); break;
                case "Sally Stageplay":
                    shouldSplit = cup.SceneName() == "scene_level_sally_stage_play" && cup.LevelComplete(Levels.SallyStagePlay); break;
                case "Cala Maria":
                    shouldSplit = cup.SceneName() == "scene_level_flying_mermaid" && cup.LevelComplete(Levels.FlyingMermaid); break;
                case "Phantom Express":
                    shouldSplit = cup.SceneName() == "scene_level_train" && cup.LevelComplete(Levels.Train); break;
                case "King Dice":
                    shouldSplit = cup.SceneName() == "scene_level_dice_palace_main" && cup.LevelComplete(Levels.DicePalaceMain); break;
                case "Devil":
                    shouldSplit = cup.SceneName() == "scene_level_devil" && cup.LevelComplete(Levels.Devil); break;

                //DLC Bosses
                case "Glumstone The Giant":
                    shouldSplit = cup.SceneName() == "scene_level_old_man" && cup.LevelComplete(Levels.OldMan); break;
                case "Mortimer Freeze":
                    shouldSplit = cup.SceneName() == "scene_level_snow_cult" && cup.LevelComplete(Levels.SnowCult); break;
                case "The Howling Aces":
                    shouldSplit = cup.SceneName() == "scene_level_airplane" && cup.LevelComplete(Levels.Airplane); break;
                case "Esther Winchester":
                    shouldSplit = cup.SceneName() == "scene_level_flying_cowboy" && cup.LevelComplete(Levels.FlyingCowboy); break;
                case "Moonshine Mob":
                    shouldSplit = cup.SceneName() == "scene_level_rum_runners" && cup.LevelComplete(Levels.RumRunners); break;
                case "Chef Saltbaker":
                    shouldSplit = cup.SceneName() == "scene_level_saltbaker" && cup.LevelComplete(Levels.Saltbaker); break;
                case "Demon and Angel":
                    shouldSplit = cup.SceneName() == "scene_level_graveyard" && cup.LevelComplete(Levels.Graveyard); break;
                case "Chess Pawns":
                    shouldSplit = cup.SceneName() == "scene_level_chess_pawn" && cup.LevelComplete(Levels.ChessPawn); break;
                case "Chess Knight":
                    shouldSplit = cup.SceneName() == "scene_level_chess_knight" && cup.LevelComplete(Levels.ChessKnight); break;
                case "Chess Bishop":
                    shouldSplit = cup.SceneName() == "scene_level_chess_bishop" && cup.LevelComplete(Levels.ChessBishop); break;
                case "Chess Rook":
                    shouldSplit = cup.SceneName() == "scene_level_chess_rook" && cup.LevelComplete(Levels.ChessRook); break;
                case "Chess Queen":
                    shouldSplit = cup.SceneName() == "scene_level_chess_queen" && cup.LevelComplete(Levels.ChessQueen); break;
                default: shouldSplit = false; break;
            }
            return shouldSplit;
        }

        private void ElementToSplit()
        {
            var ElementToSplit = dataCuphead.GetElementToSplit();
            while (dataCuphead.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusCuphead && !_PracticeMode && !_ShowSettings)
                {
                    if (ElementToSplit != dataCuphead.GetElementToSplit()) ElementToSplit = dataCuphead.GetElementToSplit();
                    foreach (var element in ElementToSplit)
                    {
                        if (!element.IsSplited && ElementCase(element.Title))
                        {
                            splitterControl.SplitCheck($"SplitFlags is produced by: Cuphead -> {element.Title}");
                            element.IsSplited = splitterControl.GetSplitStatus();
                        }
                    }
                }
            }
        }
        #endregion
    }
}

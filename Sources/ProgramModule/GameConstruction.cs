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
using System.Linq;

namespace AutoSplitterCore
{
    public static class GameConstruction
    {
        public enum Game
        {
            None,          // 0
            Sekiro,        // 1
            DarkSouls1,    // 2
            DarkSouls2,    // 3
            DarkSouls3,    // 4
            EldenRing,     // 5
            HollowKnight,  // 6
            Celeste,       // 7
            Cuphead,       // 8
            Dishonored,    // 9
            ASLMethod      // 10
        }

        private static Dictionary<Game, string> AliasGames = new Dictionary<Game, string>()
        {
            { Game.None, "None" },
            { Game.Sekiro, "Sekiro: Shadows Die Twice" },
            { Game.DarkSouls1, "Dark Souls 1" },
            { Game.DarkSouls2, "Dark Souls 2" },
            { Game.DarkSouls3, "Dark Souls 3" },
            { Game.EldenRing, "Elden Ring" },
            { Game.HollowKnight, "Hollow Knight" },
            { Game.Celeste, "Celeste" },
            { Game.Cuphead, "Cuphead" },
            { Game.Dishonored, "Dishonored" },
            { Game.ASLMethod, "ASL Method" }
        };

        public static readonly List<string> GameList = AliasGames.Values.ToList();

        public static string GetGameName(int index)
        {
            return Enum.IsDefined(typeof(Game), index) ? ((Game)index).ToString() : "Unknown";
        }

        public static int GetGameIndex(string gameName)
        {
            return Enum.TryParse(gameName, out Game game) ? (int)game : -1;
        }

        public static bool IsValidGameIndex(int index)
        {
            return Enum.IsDefined(typeof(Game), index);
        }

        public static int GetGameIndex(Game game)
        {
            return (int)game;
        }

        public static Game GetGameEnum(string gameName)
        {
            return Enum.TryParse(gameName, out Game game) && Enum.IsDefined(typeof(Game), game)
                ? game
                : Game.None;
        }

        /*
         Instrucctions to Add a New Game:
        1- Add Game Enum on GameConstruction
        2- Set alias on Dictionary<Game, string> AliasGames on GameConstruction for add AutoSplitterList
        3- Create splitter class on Sources/AutoSplitters example sekiroSplitter.cs /higly recommend use singleton pattron
            Should Have Variables Like: bool _PracticeMode
                                        bool _ShowSettings
                                        bool _ActiveSplit
            Add Data Managment if you want save data between sesion on serialization.
            Add Dll Splitter of Livesplit on References proyects and class usings

        4- For class Conent you can use ISplitterControl to manage Splits on hcm example with 
            private ISplitterControl splitterControl = SplitterControl.GetControl();
            splitterControl.SplitCheck($"Message for Log");
          Read SplitterControl.cs to have more info
        5- In AutoSplitterMainModule:
            Create a static variable of SplitterClass ex:  public static SekiroSplitter sekiroSplitter = SekiroSplitter.GetIntance();
            
            public void SetPracticeMode(bool status) - For SetPracticeMode
            public void SetShowSettings(bool status) - When config is open
            Dictionary<GameConstruction.Game, Func<bool>> splitterEnablerCheckers - {GameConstruction.Game.ASLMethod, () => aslSplitter._AslActive } - Get if splitter is on
            private readonly Dictionary<int, Action<bool>> splitterActions - {GameConstruction.GetGameIndex(GameConstruction.Game.ASLMethod), status => aslSplitter.SetStatusSplitting(status)} - Get to set Splitters on
            ResetSplitterFlags() - to reset internal flag if you use DTmethods

        6- In IGTModule
            splitterMap = new Dictionary<int, Func<long>>
            {
                { (int)GameConstruction.Game.Sekiro, () => sekiroSplitter.GetTimeInGame() },
            } // To set GetInGameTime
                
        7 - In SaveModule considering add to local saving data of splitter
            Region DataAutoSplitter
                DataAutoSplitter - Contains All Splits Configurations of AutoSplitterCore to serializate
                GeneralAutoSplitter - Contains General User Settings of AutoSplitterCore to serializate
            Region SaveModule
                UpdateAutoSplitterData() - Update all Data for Serialization
                LoadAutoSplitterSettings() - Load user data in XML for AutoSplitter

        8- in AutoSplitter.cs design and management objects interface for user usations

        Check https://chatgpt.com/canvas/shared/67ab6fa1ee308191baf4ae671038ff62 - For More Information

         */
    }
}

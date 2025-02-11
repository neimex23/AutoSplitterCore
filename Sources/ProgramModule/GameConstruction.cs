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
using System.Text;
using System.Threading.Tasks;

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
            Skyrim,        // 10
            ASLMethod      // 11
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
            { Game.Skyrim, "Skyrim" },
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
    }
}

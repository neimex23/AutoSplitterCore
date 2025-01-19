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
    public class GameConstruction //Dont Use Intance of This reference by "GameConstruction.X"
    {
        //List All Games
        public static List<string> GameList = new List<string>()
        {
            "None", //NoneSplitterIndex
            "Sekiro",
            "Dark Souls 1",
            "Dark Souls 2",
            "Dark Souls 3",
            "Elden Ring",
            "Hollow Knight",
            "Celeste",
            "Cuphead",
            "Dishonored"
        };

        //SplitterIndex: Use For if Want Add a new Game change index it do more easier
        public const int NoneSplitterIndex = 0; //Dont Change, is Seted Always in HCM program
        public const int SekiroSplitterIndex = 1;
        public const int Ds1SplitterIndex = 2;
        public const int Ds2SplitterIndex = 3;
        public const int Ds3SplitterIndex = 4;
        public const int EldenSplitterIndex = 5;
        public const int HollowSplitterIndex = 6;
        public const int CelesteSplitterIndex = 7;
        public const int CupheadSplitterIndex = 8;
        public const int DishonoredSplitterIndex = 9; 
    }
}

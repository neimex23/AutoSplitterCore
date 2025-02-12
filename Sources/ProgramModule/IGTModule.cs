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

using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace AutoSplitterCore
{
    public class IGTModule
    {
        public int gameSelect { get; set; }

        private Dictionary<int, Func<long>> splitterMap;

        SekiroSplitter sekiroSplitter = SekiroSplitter.GetIntance();
        EldenSplitter eldenSplitter = EldenSplitter.GetIntance();
        Ds3Splitter ds3Splitter = Ds3Splitter.GetIntance();
        Ds1Splitter ds1Splitter = Ds1Splitter.GetIntance();
        CelesteSplitter celesteSplitter = CelesteSplitter.GetIntance();
        CupheadSplitter cupSplitter = CupheadSplitter.GetIntance();
        ASLSplitter aslSplitter = ASLSplitter.GetInstance();

        public IGTModule()
        {
            splitterMap = new Dictionary<int, Func<long>>
            {
                { (int)GameConstruction.Game.Sekiro, () => sekiroSplitter.GetTimeInGame() },
                { (int) GameConstruction.Game.DarkSouls1, () => ds1Splitter.GetTimeInGame() },
                { (int) GameConstruction.Game.DarkSouls3, () => ds3Splitter.GetTimeInGame() },
                { (int) GameConstruction.Game.EldenRing, () => eldenSplitter.GetTimeInGame() }, 
                { (int) GameConstruction.Game.Celeste, () => celesteSplitter.GetTimeInGame() },
                { (int) GameConstruction.Game.Cuphead , () => cupSplitter.GetTimeInGame() },
                { (int) GameConstruction.Game.ASLMethod, () =>  aslSplitter.GetIngameTime()}
            };
        }

        public long ReturnCurrentIGT() => splitterMap.ContainsKey(gameSelect)? splitterMap[gameSelect]() : -1;

    }
}

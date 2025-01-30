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
using System.Threading.Tasks;
using SoulMemory.EldenRing;
using System.Threading;
using System.Runtime.InteropServices.ComTypes;

namespace AutoSplitterCore
{
    public class EldenSplitter
    {
        private static EldenRing elden = new EldenRing();
        private DTElden dataElden;
        private DefinitionsElden defE = new DefinitionsElden();
        private ISplitterControl splitterControl = SplitterControl.GetControl();

        public bool _StatusElden = false;
        public bool _PracticeMode = false;
        public bool _ShowSettings = false;


        #region Control Management
        public DTElden GetDataElden() => dataElden;

        public void SetDataElden(DTElden data) => dataElden = data;

        public bool GetEldenStatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            try { 
                _StatusElden = elden.TryRefresh(); 
            }catch  (Exception) { _StatusElden = false; }
            return _StatusElden;
        }

        public void SetStatusSplitting(bool status)
        {
            dataElden.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); }
        }

        public void ResetSplited()
        {
            listPendingB.Clear();
            listPendingG.Clear();
            listPendingP.Clear();
            listPendingCf.Clear();

            if (dataElden.GetBossToSplit().Count > 0)
            {
                foreach (var b in dataElden.GetBossToSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataElden.GetGraceToSplit().Count > 0)
            {
                foreach (var g in dataElden.GetGraceToSplit())
                {
                    g.IsSplited = false;
                }
            }

            if (dataElden.GetPositionToSplit().Count > 0)
            {
                foreach (var p in dataElden.GetPositionToSplit())
                {
                    p.IsSplited = false;
                }
            }

            if (dataElden.GetFlagsToSplit().Count > 0)
            {
                foreach (var cf in dataElden.GetFlagsToSplit())
                {
                    cf.IsSplited = false;
                }
            }
        }
        #endregion
        #region Object Management
        public void AddBoss(string boss, string mode)
        {
            DefinitionsElden.BossER cBoss = defE.StringToEnumBoss(boss);
            cBoss.Mode = mode;
            dataElden.bossToSplit.Add(cBoss);
        }

        public void AddGrace (string grace, string mode)
        {
            DefinitionsElden.GraceER cGrace = defE.StringToGraceEnum(grace);
            cGrace.Mode = mode;
            dataElden.graceToSplit.Add(cGrace);
        }

        public void AddPosition(SoulMemory.EldenRing.Position vector, string mode, string title)
        {
            DefinitionsElden.PositionER cPosition = new DefinitionsElden.PositionER()
            { vector = vector, Mode = mode, Title = title };
            dataElden.positionToSplit.Add(cPosition);   
        }

        public void AddCustomFlag(uint id, string mode, string title)
        {
            DefinitionsElden.CustomFlagER cf = new DefinitionsElden.CustomFlagER()
            { Id = id, Mode = mode, Title = title };
            dataElden.flagsToSplit.Add(cf);
        }

        public void RemoveBoss(int position)
        {
            listPendingB.RemoveAll(iboss => iboss.Id == dataElden.bossToSplit[position].Id);
            dataElden.bossToSplit.RemoveAt(position);
        }
        public void RemoveGrace(int position)
        {
            listPendingG.RemoveAll(igrace => igrace.Id == dataElden.graceToSplit[position].Id);
            dataElden.graceToSplit.RemoveAt(position);
        }

        public void RemovePosition(int position)
        {
            listPendingP.RemoveAll(iposition => iposition.vector == dataElden.positionToSplit[position].vector);
            dataElden.positionToSplit.RemoveAt(position);
        }

        public void RemoveCustomFlag(int position)
        {
            listPendingCf.RemoveAll(iCf => iCf.Id == dataElden.flagsToSplit[position].Id);
            dataElden.flagsToSplit.RemoveAt(position);
        }
        public void ClearData()
        {
            listPendingB.Clear();
            listPendingG.Clear();
            listPendingP.Clear();
            listPendingCf.Clear();
            dataElden.positionMargin = 3;
            dataElden.bossToSplit.Clear();
            dataElden.graceToSplit.Clear();
            dataElden.positionToSplit.Clear();
            dataElden.flagsToSplit.Clear();
        }
        #endregion
        #region Checking
        public SoulMemory.EldenRing.Position GetCurrentPosition()
        {
            if (!_StatusElden) GetEldenStatusProcess(0);
            if (!_StatusElden)
            {
                SoulMemory.EldenRing.Position vector = new SoulMemory.EldenRing.Position() { X = 0, Y = 0, Z = 0 };
                return vector;
            }
            return elden.GetPosition();
        }

        public int GetTimeInGame()
        {
            if (!_StatusElden) GetEldenStatusProcess(0);
            return elden.GetInGameTimeMilliseconds();
        }

        public bool CheckFlag(uint id)
        {
            if(!_StatusElden) GetEldenStatusProcess(0);
            return _StatusElden && elden.ReadEventFlag(id);
        }
        #endregion
        #region Procedure
        public void LoadAutoSplitterProcedure()
        {
            Task.Run(() => RefreshElden());
            Task.Run(() => CheckLoad());
            Task.Run(() => BossToSplit());
            Task.Run(() => GraceToSplit());
            Task.Run(() => PositionToSplit());
            Task.Run(() => FlagsToSplit());
        }
        #endregion
        #region CheckFlag Init()   
        private bool _writeMemory = false;
        private void RefreshElden()
        {
            int delay = 2000;
            GetEldenStatusProcess(delay);
            while (dataElden.enableSplitting)
            {
                Thread.Sleep(10);
                GetEldenStatusProcess(2000);
                if (!_StatusElden)
                {
                    _writeMemory = false;
                    delay = 2000;
                }
                else
                {
                    delay = 5000;
                }

                if (_StatusElden && !_writeMemory)
                {
                    if (dataElden.ResetIGTNG && elden.GetInGameTimeMilliseconds() < 1) { 
                        elden.WriteInGameTimeMilliseconds(0);
                        _writeMemory = true;
                    }
                }
            }
        }

       
        List<DefinitionsElden.BossER> listPendingB = new List<DefinitionsElden.BossER>();
        List<DefinitionsElden.GraceER> listPendingG = new List<DefinitionsElden.GraceER>();
        List<DefinitionsElden.PositionER> listPendingP = new List<DefinitionsElden.PositionER>();
        List<DefinitionsElden.CustomFlagER> listPendingCf = new List<DefinitionsElden.CustomFlagER>();

      
        private void CheckLoad()
        {
            while (dataElden.enableSplitting)
            {
                Thread.Sleep(200);
                if (_StatusElden && !_PracticeMode && !_ShowSettings)
                {
                    if (listPendingB.Count > 0 || listPendingG.Count > 0 || listPendingP.Count > 0 || listPendingCf.Count > 0)
                    {
                        if (!elden.IsPlayerLoaded())
                        {
                            foreach (var boss in listPendingB)
                            {
                                var b = dataElden.bossToSplit.FindIndex(iboss => iboss.Id == boss.Id);
                                splitterControl.SplitCheck($"SplitFlags is produced by: ELDEN RING *After Login* BOSS -> {dataElden.bossToSplit[b].Title}");
                                dataElden.bossToSplit[b].IsSplited = true;
                            }

                            foreach (var grace in listPendingG)
                            {
                                var g = dataElden.graceToSplit.FindIndex(igrace => igrace.Id == grace.Id);
                                splitterControl.SplitCheck($"SplitFlags is produced by: ELDEN RING *After Login* GRACE -> {dataElden.graceToSplit[g].Title}");
                                dataElden.graceToSplit[g].IsSplited = true;
                            }

                            foreach (var position in listPendingP)
                            {
                                var p = dataElden.positionToSplit.FindIndex(iposition => iposition.vector == position.vector);
                                splitterControl.SplitCheck($"SplitFlags is produced by: ELDEN RING *After Login* POSITION -> {dataElden.positionToSplit[p].Title} - {dataElden.positionToSplit[p].vector.ToString()}");
                                dataElden.positionToSplit[p].IsSplited = true;
                            }

                            foreach (var cf in listPendingCf)
                            {
                                var c = dataElden.flagsToSplit.FindIndex(iflag => iflag.Id == cf.Id);
                                splitterControl.SplitCheck($"SplitFlags is produced by: ELDEN RING *After Login* CUSTOM_FLAGS -> {dataElden.flagsToSplit[c].Title} - {dataElden.flagsToSplit[c].Id}");
                                dataElden.flagsToSplit[c].IsSplited = true;
                            }

                            listPendingB.Clear();
                            listPendingG.Clear();
                            listPendingP.Clear();
                            listPendingCf.Clear();
                        }
                    }
                }
            }
        }

        private void BossToSplit()
        {
            var BossToSplit = dataElden.GetBossToSplit();
            while (dataElden.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusElden && !_PracticeMode && !_ShowSettings)
                {
                    if (BossToSplit != dataElden.GetBossToSplit()) BossToSplit = dataElden.GetBossToSplit();
                    foreach (var b in BossToSplit)
                    {
                        if (!b.IsSplited && elden.ReadEventFlag(b.Id))
                        {
                            if (b.Mode == "Loading game after")
                            {
                                if (!listPendingB.Contains(b))
                                {
                                    listPendingB.Add(b);
                                }
                            }
                            else
                            {
                                splitterControl.SplitCheck($"SplitFlags is produced by: ELDEN RING BOSS -> {b.Title}");
                                b.IsSplited = splitterControl.GetSplitStatus();
                            }
                        }
                    }
                }
            }
        }

        private void GraceToSplit()
        {
            var GraceToSplit = dataElden.GetGraceToSplit();
            while (dataElden.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusElden && !_PracticeMode && !_ShowSettings)
                {
                    if (GraceToSplit != dataElden.GetGraceToSplit()) GraceToSplit = dataElden.GetGraceToSplit();
                    foreach (var i in GraceToSplit)
                    {
                        if (!i.IsSplited && elden.ReadEventFlag(i.Id))
                        {
                            if (i.Mode == "Loading game after")
                            {
                                if (!listPendingG.Contains(i))
                                {
                                    listPendingG.Add(i);
                                }
                            }
                            else
                            {
                                splitterControl.SplitCheck($"SplitFlags is produced by: ELDEN RING GRACE -> {i.Title}");
                                i.IsSplited = splitterControl.GetSplitStatus();
                            }
                        }
                    }
                }
            }
        }

        private void FlagsToSplit()
        {
            var FlagsToSplit = dataElden.GetFlagsToSplit();
            while (dataElden.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusElden && !_PracticeMode && !_ShowSettings)
                {
                    if (FlagsToSplit != dataElden.GetFlagsToSplit()) FlagsToSplit = dataElden.GetFlagsToSplit();
                    foreach (var cf in FlagsToSplit)
                    {

                        if (!cf.IsSplited && elden.ReadEventFlag(cf.Id))
                        {
                            if (cf.Mode == "Loading game after")
                            {
                                if (!listPendingCf.Contains(cf))
                                {
                                    listPendingCf.Add(cf);
                                }
                            }
                            else
                            {
                                splitterControl.SplitCheck($"SplitFlags is produced by: ELDEN RING CUSTOM_FLAG -> {cf.Title} - {cf.Id}");
                                cf.IsSplited = splitterControl.GetSplitStatus();
                            }
                        }
                    }
                }
            }
        }

        private void PositionToSplit()
        {
            var PositionToSplit = dataElden.GetPositionToSplit();
            while (dataElden.enableSplitting)
            {
                Thread.Sleep(100);
                if (_StatusElden && !_PracticeMode && !_ShowSettings)
                {
                    if (PositionToSplit != dataElden.GetPositionToSplit()) PositionToSplit = dataElden.GetPositionToSplit();
                    foreach (var p in PositionToSplit)
                    {
                        if (!p.IsSplited)
                        {
                            var currentlyPosition = elden.GetPosition();
                            var rangeX = ((currentlyPosition.X - p.vector.X) <= dataElden.positionMargin) && ((currentlyPosition.X - p.vector.X) >= -dataElden.positionMargin);
                            var rangeY = ((currentlyPosition.Y - p.vector.Y) <= dataElden.positionMargin) && ((currentlyPosition.Y - p.vector.Y) >= -dataElden.positionMargin);
                            var rangeZ = ((currentlyPosition.Z - p.vector.Z) <= dataElden.positionMargin) && ((currentlyPosition.Z - p.vector.Z) >= -dataElden.positionMargin);
                            if (rangeX && rangeY && rangeZ)
                            {
                                if (p.Mode == "Loading game after")
                                {
                                    if (!listPendingP.Contains(p))
                                    {
                                        listPendingP.Add(p);
                                    }
                                }
                                else
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: ELDEN RING POSITION -> {p.Title} - {p.vector.ToString()}");
                                    p.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }
                        }
                    }
                }
            }
        }

        
    }
    #endregion
}
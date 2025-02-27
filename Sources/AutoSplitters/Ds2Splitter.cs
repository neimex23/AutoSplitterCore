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
using SoulMemory.DarkSouls2;
using SoulMemory;
using System.Threading;
using System.Runtime.InteropServices.ComTypes;

namespace AutoSplitterCore
{
    public class Ds2Splitter
    {
        private static DarkSouls2 Ds2 = new DarkSouls2();
        private DTDs2 dataDs2;
        private DefinitionsDs2 defD2 = new DefinitionsDs2();
        private ISplitterControl splitterControl = SplitterControl.GetControl();

        public bool _StatusDs2 = false;
        public bool _runStarted = false;
        public bool _PracticeMode = false;
        public bool _ShowSettings = false;

        #region SingletonFactory
        private static Ds2Splitter _intance = new Ds2Splitter();

        private Ds2Splitter() { }

        public static Ds2Splitter GetIntance() { return _intance; }
        #endregion

        #region Control Management
        public DTDs2 GetDataDs2() => dataDs2;

        public void SetDataDs2(DTDs2 data) => dataDs2 = data;
       
        public bool GetDs2StatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            try 
            { 
                _StatusDs2 = Ds2.TryRefresh(); 
            }
            catch (Exception) { _StatusDs2 = false; }
            return _StatusDs2;
        }

        public void SetStatusSplitting(bool status)
        {
            dataDs2.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); }
        }

        public void ResetSplited()
        {
            listPendingB.Clear();
            listPendingP.Clear();
            listPendingLvl.Clear();
            if (dataDs2.GetBossToSplit().Count > 0)
            {
                foreach (var b in dataDs2.GetBossToSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataDs2.GetLvlToSplit().Count > 0)
            {
                foreach (var l in dataDs2.GetLvlToSplit())
                {
                    l.IsSplited = false;
                }
            }
            _runStarted = false;
        }
        #endregion
        #region Object Management
        public void AddBoss(string boss, string mode)
        {
            DefinitionsDs2.BossDs2 cBoss = defD2.StringToEnumBoss(boss);
            cBoss.Mode = mode;
            dataDs2.bossToSplit.Add(cBoss);
        }

        public void RemoveBoss(int position)
        {
            dataDs2.bossToSplit.RemoveAt(position);
        }

        public void AddPosition(Vector3f vector, string mode, string title)
        {
            var position = new DefinitionsDs2.PositionDs2()
            {
                vector = vector, Mode = mode, Title = title
            };
            dataDs2.positionsToSplit.Add(position);
        }

        public void RemovePosition(int position)
        {
            listPendingP.RemoveAll(iposition => iposition.vector == dataDs2.positionsToSplit[position].vector);
            dataDs2.positionsToSplit.RemoveAt(position);
        }

        public void AddAttribute(string attribute, string mode, uint value)
        {
            DefinitionsDs2.LvlDs2 cLvl = new DefinitionsDs2.LvlDs2()
            {
                Attribute = defD2.StringToEnumAttribute(attribute),
                Mode = mode,
                Value = value
            };
            dataDs2.lvlToSplit.Add(cLvl);
        }

        public void RemoveAttribute(int position)
        {
            listPendingLvl.RemoveAll(ilvl => ilvl.Attribute == dataDs2.lvlToSplit[position].Attribute && ilvl.Value == dataDs2.lvlToSplit[position].Value);
            dataDs2.lvlToSplit.RemoveAt(position);
        }

        public void ClearData()
        {
            listPendingB.Clear();
            listPendingP.Clear();
            listPendingLvl.Clear();
            dataDs2.bossToSplit.Clear();
            dataDs2.positionsToSplit.Clear();
            dataDs2.lvlToSplit.Clear();
            dataDs2.positionMargin = 3;
            _runStarted = false;
        }
        #endregion
        #region Checking
        public Vector3f GetCurrentPosition()
        {
            if (!_StatusDs2) GetDs2StatusProcess(0);
            return _StatusDs2 ? Ds2.GetPosition() : new Vector3f() { X = 0, Y = 0, Z = 0 };
        }

        public bool CheckFlag(uint id)
        {
            if (!_StatusDs2) GetDs2StatusProcess(0);
            return _StatusDs2 && Ds2.ReadEventFlag(id);
        }

        public bool Ds2IsLoading()
        {
            if (!_StatusDs2) GetDs2StatusProcess(0);
            return _StatusDs2 ? Ds2.IsLoading() : false;
        }
        #endregion
        #region Procedure
        public void LoadAutoSplitterProcedure()
        {
            Task.Run(() => RefreshDs2());
            Task.Run(() => CheckLoad());
            Task.Run(() => CheckStart());
            Task.Run(() => BossToSplit());
            Task.Run(() => PositionToSplit());
            Task.Run(() => LvlToSplit());
        }
        #endregion
        #region CheckFlag Init()
        private void RefreshDs2()
        {
            int delay = 2000;
            GetDs2StatusProcess(delay);
            while (dataDs2.enableSplitting)
            {
                Thread.Sleep(10);
                GetDs2StatusProcess(delay);
                if (!_StatusDs2) { delay = 2000; }else { delay = 5000; }
            }
        }

        private void CheckStart()
        {
            while (dataDs2.enableSplitting)
            {
                Thread.Sleep(200);
                if (_StatusDs2 && !_PracticeMode && !_ShowSettings)
                {
                    var position = Ds2.GetPosition();
                    bool menu = position.X == 0.00 && position.Y == 0.00 && position.Z == 0.00;

                    if (!dataDs2.gameTimer) {
                        if (menu)
                            _runStarted = false;
                        else
                            _runStarted = true;
                    }else
                    {
                        if (!menu && Ds2.IsLoading())
                            _runStarted = false;

                        if (menu && !Ds2.IsLoading())
                            _runStarted = false;

                        if (!menu &&  !Ds2.IsLoading())
                            _runStarted = true;
                    }
                }
            }
        }

        List<DefinitionsDs2.BossDs2> listPendingB = new List<DefinitionsDs2.BossDs2>();
        List<DefinitionsDs2.PositionDs2> listPendingP = new List<DefinitionsDs2.PositionDs2>();
        List<DefinitionsDs2.LvlDs2> listPendingLvl = new List<DefinitionsDs2.LvlDs2>();

        private void CheckLoad()
        {
            while (dataDs2.enableSplitting)
            {
                Thread.Sleep(200);
                if (_StatusDs2 && !_PracticeMode && !_ShowSettings)
                {
                    if ((listPendingB.Count > 0 || listPendingP.Count > 0 || listPendingLvl.Count > 0))
                    {
                        if (Ds2.IsLoading())
                        {
                            foreach (var boss in listPendingB)
                            {
                                var b = dataDs2.bossToSplit.FindIndex(iboss => iboss.Id == boss.Id);
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS2 *After Login* BOSS -> {dataDs2.bossToSplit[b].Title}");
                                dataDs2.bossToSplit[b].IsSplited = true;
                            }

                            foreach (var position in listPendingP)
                            {
                                var p = dataDs2.positionsToSplit.FindIndex(fposition => fposition.vector == position.vector);
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS2 POSITION -> {dataDs2.positionsToSplit[p].Title} - {dataDs2.positionsToSplit[p].vector.ToString()}");
                                dataDs2.positionsToSplit[p].IsSplited = true;
                            }

                            foreach (var lvl in listPendingLvl)
                            {
                                var l = dataDs2.lvlToSplit.FindIndex(Ilvl => Ilvl.Attribute == lvl.Attribute && Ilvl.Value == lvl.Value);
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS2 *After Login* LEVEL -> {dataDs2.lvlToSplit[l].Attribute} - {dataDs2.lvlToSplit[l].Value}");
                                dataDs2.lvlToSplit[l].IsSplited = true;
                            }

                            listPendingB.Clear();
                            listPendingP.Clear();
                            listPendingLvl.Clear();
                        }
                    }
                }
            }
        }

        private void BossToSplit()
        {
            var BossToSplit = dataDs2.GetBossToSplit();
            while (dataDs2.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusDs2 && !_PracticeMode && !_ShowSettings)
                {
                    if (BossToSplit != dataDs2.GetBossToSplit()) BossToSplit = dataDs2.GetBossToSplit();
                    foreach (var b in BossToSplit)
                    {
                        if (!b.IsSplited && Ds2.GetBossKillCount(b.Id) > 0)
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
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS2 BOSS -> {b.Title}");
                                b.IsSplited = splitterControl.GetSplitStatus();                               
                            }
                        }
                    }
                }
            }
        }

        private void LvlToSplit()
        {
            var LvlToSplit = dataDs2.GetLvlToSplit();
            while (dataDs2.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusDs2 && !_PracticeMode && !_ShowSettings)
                {
                    if (LvlToSplit != dataDs2.GetLvlToSplit()) LvlToSplit = dataDs2.GetLvlToSplit();
                    foreach (var lvl in LvlToSplit)
                    {
                        if (!lvl.IsSplited && Ds2.GetAttribute(lvl.Attribute) >= lvl.Value)
                        {
                            if (lvl.Mode == "Loading game after")
                            {
                                if (!listPendingLvl.Contains(lvl))
                                {
                                    listPendingLvl.Add(lvl);
                                }
                            }
                            else
                            {
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS2 LEVEL -> {lvl.Attribute} - {lvl.Value}");
                                lvl.IsSplited = splitterControl.GetSplitStatus();
                            }
                        }
                    }
                }
            }
        }

        private void PositionToSplit()
        {
            var PositionsToSplit = dataDs2.GetPositionsToSplit();
            while (dataDs2.enableSplitting)
            {
                Thread.Sleep(100);
                if (_StatusDs2 && !_PracticeMode && !_ShowSettings)
                {
                    if (PositionsToSplit != dataDs2.GetPositionsToSplit()) PositionsToSplit = dataDs2.GetPositionsToSplit();
                    foreach (var p in PositionsToSplit)
                    {
                        if (!p.IsSplited)
                        {
                            var currentlyPosition = Ds2.GetPosition();
                            var rangeX = ((currentlyPosition.X - p.vector.X) <= dataDs2.positionMargin) && ((currentlyPosition.X - p.vector.X) >= -dataDs2.positionMargin);
                            var rangeY = ((currentlyPosition.Y - p.vector.Y) <= dataDs2.positionMargin) && ((currentlyPosition.Y - p.vector.Y) >= -dataDs2.positionMargin);
                            var rangeZ = ((currentlyPosition.Z - p.vector.Z) <= dataDs2.positionMargin) && ((currentlyPosition.Z - p.vector.Z) >= -dataDs2.positionMargin);
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
                                    splitterControl.SplitCheck($"SplitFlags is produced by: DS2 POSITION -> {p.Title} - {p.vector.ToString()}");
                                    p.IsSplited = splitterControl.GetSplitStatus();
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
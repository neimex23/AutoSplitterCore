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
using System.Threading;
using SoulMemory.DarkSouls3;
using SoulMemory;
using System.Runtime.InteropServices.ComTypes;

namespace AutoSplitterCore
{
    public class Ds3Splitter
    {
        private static DarkSouls3 Ds3 = new DarkSouls3();
        private DTDs3 dataDs3;
        private DefinitionsDs3 defD3 = new DefinitionsDs3();
        private ISplitterControl splitterControl = SplitterControl.GetControl();

        public bool _StatusDs3 = false;
        public bool _PracticeMode = false;
        public bool _ShowSettings = false;


        #region SingletonFactory
        private static Ds3Splitter _intance = new Ds3Splitter();

        private Ds3Splitter() { }

        public static Ds3Splitter GetIntance() { return _intance; }
        #endregion

        #region Control Management
        public DTDs3 GetDataDs3() => dataDs3;

        public void SetDataDs3(DTDs3 data) => dataDs3 = data;

        public bool GetDs3StatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            try {
                _StatusDs3 = Ds3.TryRefresh();
            } catch (Exception) { _StatusDs3 = false; }
            return _StatusDs3;
        }

        public void SetStatusSplitting(bool status)
        {
            dataDs3.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); }
        }

        public void ResetSplited()
        {
            listPendingB.Clear();
            listPendingBon.Clear();
            listPendingLvl.Clear();
            listPendingCf.Clear();
            listPendingP.Clear();

            if (dataDs3.GetBossToSplit().Count > 0)
            {
                foreach (var b in dataDs3.GetBossToSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataDs3.GetBonfireToSplit().Count > 0)
            {
                foreach (var bf in dataDs3.GetBonfireToSplit())
                {
                    bf.IsSplited = false;
                }
            }

            if (dataDs3.GetLvlToSplit().Count > 0)
            {
                foreach (var l in dataDs3.GetLvlToSplit())
                {
                    l.IsSplited = false;
                }
            }

            if (dataDs3.GetFlagToSplit().Count > 0)
            {
                foreach (var cf in dataDs3.GetFlagToSplit())
                {
                    cf.IsSplited = false;
                }
            }

            if (dataDs3.GetPositionsToSplit().Count > 0)
            {
                foreach (var p in dataDs3.GetPositionsToSplit())
                {
                    p.IsSplited = false;
                }
            }
        }
        #endregion
        #region Object Management
        public void AddBoss(string boss, string mode)
        {
            DefinitionsDs3.BossDs3 cBoss = defD3.StringToEnumBoss(boss);
            cBoss.Mode = mode;
            dataDs3.bossToSplit.Add(cBoss);
        }

        public void RemoveBoss(int position)
        {
            listPendingB.RemoveAll(iboss => iboss.Id == dataDs3.bossToSplit[position].Id);
            dataDs3.bossToSplit.RemoveAt(position);
        }

        public void AddBonfire(string Bonfire, string mode)
        {
            DefinitionsDs3.BonfireDs3 cBonfire = defD3.StringToEnumBonfire(Bonfire);
            cBonfire.Mode = mode;
            dataDs3.bonfireToSplit.Add(cBonfire);
        }

        public void RemoveBonfire(int position)
        {
            listPendingBon.RemoveAll(iposition => iposition.Id == dataDs3.bonfireToSplit[position].Id);
            dataDs3.bonfireToSplit.RemoveAt(position);
        }

        public void AddAttribute(string attribute, string mode, uint value)
        {
            DefinitionsDs3.LvlDs3 cLvl = new DefinitionsDs3.LvlDs3()
            {
                Attribute = defD3.StringToEnumAttribute(attribute),
                Mode = mode,
                Value = value
            };
            dataDs3.lvlToSplit.Add(cLvl);
        }

        public void RemoveAttribute(int position)
        {
            listPendingLvl.RemoveAll(ilvl => ilvl.Attribute == dataDs3.lvlToSplit[position].Attribute && ilvl.Value == dataDs3.lvlToSplit[position].Value);
            dataDs3.lvlToSplit.RemoveAt(position);
        }

        public void AddCustomFlag(uint id, string mode, string title)
        {
            DefinitionsDs3.CfDs3 cf = new DefinitionsDs3.CfDs3()
            { Id = id, Mode = mode, Title = title };
            dataDs3.flagToSplit.Add(cf);
        }

        public void RemoveCustomFlag(int position)
        {
            listPendingCf.RemoveAll(iflag => iflag.Id == dataDs3.flagToSplit[position].Id);
            dataDs3.flagToSplit.RemoveAt(position);
        }

        public void AddPosition(Vector3f vector, string mode, string title)
        {
            var position = new DefinitionsDs3.PositionDs3()
            {
                vector = vector,
                Mode = mode,
                Title = title
            };
            dataDs3.positionsToSplit.Add(position);
        }

        public void RemovePosition(int position)
        {
            listPendingP.RemoveAll(iposition => iposition.vector == dataDs3.positionsToSplit[position].vector);
            dataDs3.positionsToSplit.RemoveAt(position);
        }
        public void ClearData()
        {
            listPendingB.Clear();
            listPendingBon.Clear();
            listPendingLvl.Clear();
            listPendingCf.Clear();
            listPendingP.Clear();
            dataDs3.bossToSplit.Clear();
            dataDs3.bonfireToSplit.Clear();
            dataDs3.lvlToSplit.Clear();
            dataDs3.flagToSplit.Clear();
            dataDs3.positionsToSplit.Clear();
        }
        #endregion
        #region Checking
        public bool CheckFlag(uint id)
        {
            if (!_StatusDs3) GetDs3StatusProcess(0);
            return _StatusDs3 && Ds3.ReadEventFlag(id);
        }

        public int GetTimeInGame()
        {
            if (!_StatusDs3) GetDs3StatusProcess(0);
            return Ds3.GetInGameTimeMilliseconds();
        }

        public Vector3f GetCurrentPosition()
        {
            if(!_StatusDs3) GetDs3StatusProcess(0);
            if (!_StatusDs3)
            {
                Vector3f vector = new Vector3f() { X = 0, Y = 0, Z = 0 };
                return vector;
            }
            return Ds3.GetPosition();
        }
        #endregion
        #region Procedure
        public void LoadAutoSplitterProcedure()
        {
            Task.Run(() => RefreshDs3());
            Task.Run(() => CheckLoad());
            Task.Run(() => BossToSplit());
            Task.Run(() => BonfireToSplit());
            Task.Run(() => LvlToSplit());
            Task.Run(() => CustomFlagToSplit());
            Task.Run(() => PositionToSplit());
        }
        #endregion
        #region CheckFlag Init()   

        private bool _writeMemory = false;
        private void RefreshDs3()
        {           
            int delay = 2000;
            _StatusDs3 = GetDs3StatusProcess(delay);
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(10);
                GetDs3StatusProcess(delay);
                if (!_StatusDs3)
                {
                    _writeMemory = false;
                    delay = 2000;
                }
                else
                {
                    delay = 5000;
                }

                if (_StatusDs3 && !_writeMemory)
                {
                    if (dataDs3.ResetIGTNG && Ds3.GetInGameTimeMilliseconds() < 1)
                    {
                        Ds3.WriteInGameTimeMilliseconds(0);
                        _writeMemory = true;
                    }          
                }
            }
        }

        List<DefinitionsDs3.BossDs3> listPendingB = new List<DefinitionsDs3.BossDs3>();
        List<DefinitionsDs3.BonfireDs3> listPendingBon = new List<DefinitionsDs3.BonfireDs3>();
        List<DefinitionsDs3.LvlDs3> listPendingLvl = new List<DefinitionsDs3.LvlDs3>();
        List<DefinitionsDs3.CfDs3> listPendingCf = new List<DefinitionsDs3.CfDs3>();
        List<DefinitionsDs3.PositionDs3> listPendingP = new List<DefinitionsDs3.PositionDs3>();

        private void CheckLoad()
        {
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(200);
                if (_StatusDs3 && !_PracticeMode && !_ShowSettings)
                {
                    if (listPendingB.Count > 0 || listPendingBon.Count > 0 || listPendingLvl.Count > 0 || listPendingCf.Count > 0 || listPendingP.Count > 0)
                    {
                        if (!Ds3.IsPlayerLoaded())
                        {
                            foreach (var boss in listPendingB)
                            {
                                var b = dataDs3.bossToSplit.FindIndex(iboss => iboss.Id == boss.Id);
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS3 *After Login* BOSS -> {dataDs3.bossToSplit[b].Title}");
                                dataDs3.bossToSplit[b].IsSplited = true;
                            }

                            foreach (var bone in listPendingBon)
                            {
                                var bo = dataDs3.bonfireToSplit.FindIndex(Ibone => Ibone.Id == bone.Id);
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS3 *After Login* BONFIRE -> {dataDs3.bonfireToSplit[bo].Title}");
                                dataDs3.bonfireToSplit[bo].IsSplited = true;
                            }

                            foreach (var lvl in listPendingLvl)
                            {
                                var l = dataDs3.lvlToSplit.FindIndex(Ilvl => Ilvl.Attribute == lvl.Attribute && Ilvl.Value == lvl.Value);
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS3 *After Login* LEVEL -> {dataDs3.lvlToSplit[l].Attribute} - {dataDs3.lvlToSplit[l].Value}");
                                dataDs3.lvlToSplit[l].IsSplited = true;
                            }

                            foreach (var cf in listPendingCf)
                            {
                                var c = dataDs3.flagToSplit.FindIndex(icf => icf.Id == cf.Id);
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS3 *After Login*  CUSOM_FLAGS -> {cf.Title} - {cf.Id}");
                                dataDs3.flagToSplit[c].IsSplited = true;
                            }

                            foreach (var position in listPendingP)
                            {
                                var p = dataDs3.positionsToSplit.FindIndex(fposition => fposition.vector == position.vector);
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS3 POSITION -> {dataDs3.positionsToSplit[p].Title} - {dataDs3.positionsToSplit[p].vector.ToString()}");
                                dataDs3.positionsToSplit[p].IsSplited = true;
                            }

                            listPendingB.Clear();
                            listPendingBon.Clear();
                            listPendingLvl.Clear();
                            listPendingCf.Clear();
                            listPendingP.Clear();

                        }
                    }
                }
            }
        }
       
        private void BossToSplit()
        {
            var BossToSplit = dataDs3.GetBossToSplit();
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusDs3 && !_PracticeMode && !_ShowSettings)
                {
                    if (BossToSplit != dataDs3.GetBossToSplit()) BossToSplit = dataDs3.GetBossToSplit();
                    foreach (var b in BossToSplit)
                    {
                        if (!b.IsSplited && Ds3.ReadEventFlag(b.Id))
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
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS3 BOSS -> {b.Title}");
                                b.IsSplited = splitterControl.GetSplitStatus();
                            }
                        }
                    }
                }
            }
        }


        private void BonfireToSplit()
        {
            var BonfireToSplit = dataDs3.GetBonfireToSplit();
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusDs3 && !_PracticeMode && !_ShowSettings)
                {
                    if (BonfireToSplit != dataDs3.GetBonfireToSplit()) BonfireToSplit = dataDs3.GetBonfireToSplit();
                    foreach (var bonfire in BonfireToSplit)
                    {
                        if (!bonfire.IsSplited && Ds3.ReadEventFlag(bonfire.Id))
                        {
                            if (bonfire.Mode == "Loading game after")
                            {
                                if (!listPendingBon.Contains(bonfire))
                                {
                                    listPendingBon.Add(bonfire);
                                }
                            }
                            else
                            {
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS3 BONEFIRE -> {bonfire.Title}");
                                bonfire.IsSplited = splitterControl.GetSplitStatus();
                            }
                        }
                    }
                }
            }
        }

        private void LvlToSplit()
        {
            var LvlToSplit = dataDs3.GetLvlToSplit();
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusDs3 && !_PracticeMode && !_ShowSettings)
                {
                    if (LvlToSplit != dataDs3.GetLvlToSplit()) LvlToSplit = dataDs3.GetLvlToSplit();
                    foreach (var lvl in LvlToSplit)
                    {
                        if (!lvl.IsSplited && Ds3.ReadAttribute(lvl.Attribute) >= lvl.Value)
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
                                splitterControl.SplitCheck($"SplitFlags is produced by: DS3 LEVEL -> {lvl.Attribute} - {lvl.Value}");
                                lvl.IsSplited = splitterControl.GetSplitStatus();
                            }
                        }
                    }
                }
            }
        }

        private void CustomFlagToSplit()
        {
            var FlagToSplit = dataDs3.GetFlagToSplit();
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusDs3 && !_PracticeMode && !_ShowSettings)
                {
                    if (FlagToSplit != dataDs3.GetFlagToSplit()) FlagToSplit = dataDs3.GetFlagToSplit();
                    foreach (var cf in FlagToSplit)
                    {
                        if (!cf.IsSplited && Ds3.ReadEventFlag(cf.Id))
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

                                splitterControl.SplitCheck($"SplitFlags is produced by: DS3 CUSTOM_FLAGS -> {cf.Title} - {cf.Id}");
                                cf.IsSplited = splitterControl.GetSplitStatus();
                            }
                        }
                    }
                }
            }
        }

        private void PositionToSplit()
        {
            var PositionsToSplit = dataDs3.GetPositionsToSplit();
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(100);
                if (_StatusDs3 && !_PracticeMode && !_ShowSettings)
                {
                    if (PositionsToSplit != dataDs3.GetPositionsToSplit()) PositionsToSplit = dataDs3.GetPositionsToSplit();
                    foreach (var p in PositionsToSplit)
                    {
                        if (!p.IsSplited)
                        {
                            var currentlyPosition = Ds3.GetPosition();
                            var rangeX = ((currentlyPosition.X - p.vector.X) <= dataDs3.positionMargin) && ((currentlyPosition.X - p.vector.X) >= -dataDs3.positionMargin);
                            var rangeY = ((currentlyPosition.Y - p.vector.Y) <= dataDs3.positionMargin) && ((currentlyPosition.Y - p.vector.Y) >= -dataDs3.positionMargin);
                            var rangeZ = ((currentlyPosition.Z - p.vector.Z) <= dataDs3.positionMargin) && ((currentlyPosition.Z - p.vector.Z) >= -dataDs3.positionMargin);
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
                                    splitterControl.SplitCheck($"SplitFlags is produced by: DS3 POSITION -> {p.Title} - {p.vector.ToString()}");
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
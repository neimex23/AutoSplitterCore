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
using System.Threading.Tasks;
using System.Threading;
using SoulMemory.DarkSouls3;
using HitCounterManager;
using SoulMemory;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;

namespace AutoSplitterCore
{
    public class Ds3Splitter
    {
        public static DarkSouls3 Ds3 = new DarkSouls3();
        public bool _StatusDs3 = false;
        public bool _SplitGo = false;
        public bool _PracticeMode = false;
        public bool _ShowSettings = false;
        public DTDs3 dataDs3;
        public DefinitionsDs3 defD3 = new DefinitionsDs3();
        public ProfilesControl _profile;
        private bool _writeMemory = false;      
        private static readonly object _object = new object();
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };
        public bool DebugMode = false;

        #region Control Management
        public DTDs3 getDataDs3()
        {
            return this.dataDs3;
        }

        public void setDataDs3(DTDs3 data, ProfilesControl profile)
        {
            this.dataDs3 = data;
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

        public bool getDs3StatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            return _StatusDs3 = Ds3.TryRefresh(out Exception e);
        }

        public void setStatusSplitting(bool status)
        {
            dataDs3.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }

        public void resetSplited()
        {
            listPendingB.Clear();
            listPendingBon.Clear();
            listPendingLvl.Clear();
            listPendingCf.Clear();
            listPendingP.Clear();

            if (dataDs3.getBossToSplit().Count > 0)
            {
                foreach (var b in dataDs3.getBossToSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataDs3.getBonfireToSplit().Count > 0)
            {
                foreach (var bf in dataDs3.getBonfireToSplit())
                {
                    bf.IsSplited = false;
                }
            }

            if (dataDs3.getLvlToSplit().Count > 0)
            {
                foreach (var l in dataDs3.getLvlToSplit())
                {
                    l.IsSplited = false;
                }
            }

            if (dataDs3.getFlagToSplit().Count > 0)
            {
                foreach (var cf in dataDs3.getFlagToSplit())
                {
                    cf.IsSplited = false;
                }
            }

            if (dataDs3.getPositionsToSplit().Count > 0)
            {
                foreach (var p in dataDs3.getPositionsToSplit())
                {
                    p.IsSplited = false;
                }
            }
        }
        #endregion
        #region Object Management
        public void AddBoss(string boss, string mode)
        {
            DefinitionsDs3.BossDs3 cBoss = defD3.stringToEnumBoss(boss);
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
            DefinitionsDs3.BonfireDs3 cBonfire = defD3.stringToEnumBonfire(Bonfire);
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
                Attribute = defD3.stringToEnumAttribute(attribute),
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

        public void AddCustomFlag(uint id, string mode)
        {
            DefinitionsDs3.CfDs3 cf = new DefinitionsDs3.CfDs3()
            { Id = id, Mode = mode };
            dataDs3.flagToSplit.Add(cf);
        }

        public void RemoveCustomFlag(int position)
        {
            listPendingCf.RemoveAll(iflag => iflag.Id == dataDs3.flagToSplit[position].Id);
            dataDs3.flagToSplit.RemoveAt(position);
        }

        public void AddPosition(Vector3f vector, string mode)
        {
            var position = new DefinitionsDs3.PositionDs3()
            {
                vector = vector,
                Mode = mode
            };
            dataDs3.positionsToSplit.Add(position);
        }

        public void RemovePosition(int position)
        {
            listPendingP.RemoveAll(iposition => iposition.vector == dataDs3.positionsToSplit[position].vector);
            dataDs3.positionsToSplit.RemoveAt(position);
        }
        public void clearData()
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
            return Ds3.ReadEventFlag(id);
        }

        public int getTimeInGame()
        {
            return Ds3.GetInGameTimeMilliseconds();
        }

        public Vector3f getCurrentPosition()
        {
            getDs3StatusProcess(0);
            return Ds3.GetPosition();
        }
        #endregion
        #region Procedure
        public void LoadAutoSplitterProcedure()
        {         
            var taskRefresh = new Task(() =>
            {
                RefreshDs3();
            });
            var taskCheckload = new Task(() =>
            {
                checkLoad();
            });
            var task1 = new Task(() =>
            {
                bossToSplit();
            });
            var task2 = new Task(() =>
            {
                bonfireToSplit();
            });

            var task3 = new Task(() =>
            {
                lvlToSplit();
            });

            var task4 = new Task(() =>
            {
                customFlagToSplit();
            });

            var task5 = new Task(() =>
            {
                positionToSplit();
            });

            taskRefresh.Start();
            taskCheckload.Start();
            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();
            task5.Start();
        }
        #endregion
        #region CheckFlag Init()   
        private void RefreshDs3()
        {           
            int delay = 2000;
            _StatusDs3 = getDs3StatusProcess(delay);
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(10);
                getDs3StatusProcess(delay);
                if (!_StatusDs3)
                {
                    _writeMemory = false;
                    delay = 2000;
                }
                else
                {
                    delay = 20000;
                }

                if (!_writeMemory)
                {
                    if (Ds3.GetInGameTimeMilliseconds() < 1)
                    {
                        Ds3.WriteInGameTimeMilliseconds(0);                      
                    }
                    _writeMemory = true;
                }
            }
        }

        List<DefinitionsDs3.BossDs3> listPendingB = new List<DefinitionsDs3.BossDs3>();
        List<DefinitionsDs3.BonfireDs3> listPendingBon = new List<DefinitionsDs3.BonfireDs3>();
        List<DefinitionsDs3.LvlDs3> listPendingLvl = new List<DefinitionsDs3.LvlDs3>();
        List<DefinitionsDs3.CfDs3> listPendingCf = new List<DefinitionsDs3.CfDs3>();
        List<DefinitionsDs3.PositionDs3> listPendingP = new List<DefinitionsDs3.PositionDs3>();


        private void checkLoad()
        {
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(200);
                if ((listPendingB.Count > 0 || listPendingBon.Count > 0 || listPendingLvl.Count > 0 || listPendingCf.Count > 0 || listPendingP.Count > 0) && _StatusDs3)
                {
                    if (!Ds3.IsPlayerLoaded())
                    {
                        foreach (var boss in listPendingB)
                        {
                            SplitCheck();
                            var b = dataDs3.bossToSplit.FindIndex(iboss => iboss.Id == boss.Id);
                            dataDs3.bossToSplit[b].IsSplited = true;
                        }

                        foreach (var bone in listPendingBon)
                        {
                            SplitCheck();
                            var bo = dataDs3.bonfireToSplit.FindIndex(Ibone => Ibone.Id == bone.Id);
                            dataDs3.bonfireToSplit[bo].IsSplited = true;
                        }

                        foreach (var lvl in listPendingLvl)
                        {
                            SplitCheck();
                            var l = dataDs3.lvlToSplit.FindIndex(Ilvl => Ilvl.Attribute == lvl.Attribute && Ilvl.Value == lvl.Value);
                            dataDs3.lvlToSplit[l].IsSplited = true;
                        }

                        foreach (var cf in listPendingCf)
                        {
                            SplitCheck();
                            var c = dataDs3.flagToSplit.FindIndex(icf => icf.Id == cf.Id);
                            dataDs3.flagToSplit[c].IsSplited = true;
                        }

                        foreach (var position in listPendingP)
                        {
                            SplitCheck();
                            var p = dataDs3.positionsToSplit.FindIndex(fposition => fposition.vector == position.vector);
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

       
        private void bossToSplit()
        {
            var BossToSplit = dataDs3.getBossToSplit();
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(3000);
                if (_StatusDs3 && !_PracticeMode && !_ShowSettings)
                {
                    if (BossToSplit != dataDs3.getBossToSplit()) BossToSplit = dataDs3.getBossToSplit();
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
                                b.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }


        private void bonfireToSplit()
        {
            var BonfireToSplit = dataDs3.getBonfireToSplit();
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(3000);
                if (_StatusDs3 && !_PracticeMode && !_ShowSettings)
                {
                    if (BonfireToSplit != dataDs3.getBonfireToSplit()) BonfireToSplit = dataDs3.getBonfireToSplit();
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
                                bonfire.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }

        private void lvlToSplit()
        {
            var LvlToSplit = dataDs3.getLvlToSplit();
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(3000);
                if (_StatusDs3 && !_PracticeMode && !_ShowSettings)
                {
                    if (LvlToSplit != dataDs3.getLvlToSplit()) LvlToSplit = dataDs3.getLvlToSplit();
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
                                lvl.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }

        private void customFlagToSplit()
        {
            var FlagToSplit = dataDs3.getFlagToSplit();
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(3000);
                if (_StatusDs3 && !_PracticeMode && !_ShowSettings)
                {
                    if (FlagToSplit != dataDs3.getFlagToSplit()) FlagToSplit = dataDs3.getFlagToSplit();
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
                                cf.IsSplited = true;
                                SplitCheck();
                            }
                        }
                    }
                }
            }
        }

        private void positionToSplit()
        {
            var PositionsToSplit = dataDs3.getPositionsToSplit();
            while (dataDs3.enableSplitting)
            {
                Thread.Sleep(100);
                if (_StatusDs3 && !_PracticeMode && !_ShowSettings)
                {
                    if (PositionsToSplit != dataDs3.getPositionsToSplit()) PositionsToSplit = dataDs3.getPositionsToSplit();
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
                                    p.IsSplited = true;
                                    SplitCheck();
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
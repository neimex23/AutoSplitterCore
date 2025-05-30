﻿//MIT License

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

using Irony;
using SoulMemory;
using SoulMemory.DarkSouls1;
using SoulMemory.Sekiro;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSplitterCore
{
    public class Ds1Splitter
    {
        private static DarkSouls1 Ds1 = new DarkSouls1();
        private DTDs1 dataDs1;
        private DefinitionsDs1 defDs1 = new DefinitionsDs1();
        private ISplitterControl splitterControl = SplitterControl.GetControl();

        public bool _StatusDs1 = false;
        public bool _PracticeMode = false;
        public bool _ShowSettings = false;

        #region SingletonFactory
        private static Ds1Splitter _intance = new Ds1Splitter();

        private Ds1Splitter() { }

        public static Ds1Splitter GetIntance() { return _intance; }
        #endregion

        #region Control Management
        public DTDs1 GetDataDs1() => dataDs1;

        public void SetDataDs1(DTDs1 data) => dataDs1 = data;

        public bool GetDs1StatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            try
            {
                _StatusDs1 = Ds1.TryRefresh();
            }
            catch (Exception) { _StatusDs1 = false; }
            return _StatusDs1;
        }

        public void SetStatusSplitting(bool status)
        {
            dataDs1.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); }
        }

        public void ResetSplited()
        {
            listPendingB.Clear();
            listPendingBon.Clear();
            listPendingLvl.Clear();
            listPendingP.Clear();
            listPendingItem.Clear();

            if (dataDs1.GetBossToSplit().Count > 0)
            {
                foreach (var b in dataDs1.GetBossToSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataDs1.GetBonfireToSplit().Count > 0)
            {
                foreach (var bf in dataDs1.GetBonfireToSplit())
                {
                    bf.IsSplited = false;
                }
            }

            if (dataDs1.GetLvlToSplit().Count > 0)
            {
                foreach (var l in dataDs1.GetLvlToSplit())
                {
                    l.IsSplited = false;
                }
            }

            if (dataDs1.GetPositionsToSplit().Count > 0)
            {
                foreach (var p in dataDs1.GetPositionsToSplit())
                {
                    p.IsSplited = false;
                }
            }

            if (dataDs1.GetItemsToSplit().Count > 0)
            {
                foreach (var i in dataDs1.GetItemsToSplit())
                {
                    i.IsSplited = false;
                }
            }
        }
        #endregion
        #region Object Management
        public void AddBoss(string boss, string mode)
        {
            DefinitionsDs1.BossDs1 cBoss = defDs1.StringToEnumBoss(boss);
            cBoss.Mode = mode;
            dataDs1.bossToSplit.Add(cBoss);
        }

        public void RemoveBoss(int position)
        {
            listPendingB.RemoveAll(iboss => iboss.Id == dataDs1.bossToSplit[position].Id);
            dataDs1.bossToSplit.RemoveAt(position);
        }

        public void AddBonfire(string Bonfire, string mode, string state)
        {
            DefinitionsDs1.BonfireDs1 cBonfire = defDs1.StringToEnumBonfire(Bonfire);
            cBonfire.Mode = mode;
            cBonfire.Value = defDs1.StringtoEnumBonfireState(state);
            dataDs1.bonfireToSplit.Add(cBonfire);
        }

        public BonfireState ConvertStringToState(string state)
        {
            return defDs1.StringtoEnumBonfireState(state);
        }

        public void RemoveBonfire(int position)
        {
            listPendingBon.RemoveAll(iposition => iposition.Id == dataDs1.bonfireToSplit[position].Id);
            dataDs1.bonfireToSplit.RemoveAt(position);
        }

        public void AddAttribute(string attribute, string mode, uint value)
        {
            DefinitionsDs1.LvlDs1 cLvl = new DefinitionsDs1.LvlDs1()
            {
                Attribute = defDs1.StringToEnumAttribute(attribute),
                Mode = mode,
                Value = value
            };
            dataDs1.lvlToSplit.Add(cLvl);
        }

        public void RemoveAttribute(int position)
        {
            listPendingLvl.RemoveAll(ilvl => ilvl.Attribute == dataDs1.lvlToSplit[position].Attribute && ilvl.Value == dataDs1.lvlToSplit[position].Value);
            dataDs1.lvlToSplit.RemoveAt(position);
        }

        public void AddPosition(Vector3f vector, string mode, string title)
        {
            var position = new DefinitionsDs1.PositionDs1()
            {
                vector = vector,
                Mode = mode,
                Title = title
            };
            dataDs1.positionsToSplit.Add(position);
        }

        public void RemovePosition(int position)
        {
            listPendingP.RemoveAll(iposition => iposition.vector == dataDs1.positionsToSplit[position].vector);
            dataDs1.positionsToSplit.RemoveAt(position);
        }

        public void AddItem(string item, string mode)
        {
            DefinitionsDs1.ItemDs1 cItem = defDs1.StringToEnumItem(item);
            cItem.Mode = mode;
            dataDs1.itemToSplit.Add(cItem);
        }

        public void RemoveItem(int position)
        {
            listPendingItem.RemoveAll(iboss => iboss.Id == dataDs1.itemToSplit[position].Id);
            dataDs1.itemToSplit.RemoveAt(position);
        }

        public void ClearData()
        {
            listPendingB.Clear();
            listPendingBon.Clear();
            listPendingLvl.Clear();
            listPendingP.Clear();
            listPendingItem.Clear();

            dataDs1.bossToSplit.Clear();
            dataDs1.bonfireToSplit.Clear();
            dataDs1.lvlToSplit.Clear();
            dataDs1.positionsToSplit.Clear();
            dataDs1.itemToSplit.Clear();
            dataDs1.positionMargin = 3;
        }
        #endregion
        #region Checking
        public Vector3f GetCurrentPosition()
        {
            if (!_StatusDs1) GetDs1StatusProcess(0);
            return _StatusDs1 ? Ds1.GetPosition() : new Vector3f() { X = 0, Y = 0, Z = 0 };
        }

        public int GetTimeInGame()
        {
            if (!_StatusDs1)
                GetDs1StatusProcess(0);

            if (!_StatusDs1)
                return -1;

            try
            {
                int time = Ds1.GetInGameTimeMilliseconds();
                return time >= 0 ? time : -1;
            }
            catch (Exception ex)
            {
                DebugLog.LogMessage($"DS1 GetTimeInGame Ex: {ex.Message}", ex);
                return -1;
            }
        }
        #endregion
        #region Procedure
        public void LoadAutoSplitterProcedure()
        {
            var actions = new Action[]
            {
                () => RefreshDs1(),
                () => CheckLoad(),
                () => InventorySee(),
                () => BossToSplit(),
                () => BonfireToSplit(),
                () => LvlToSplit(),
                () => PositionToSplit(),
                () => ItemToSplit()
            };

            foreach (var action in actions)
            {
                Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Current);
            }

        }
        #endregion
        #region CheckFlag Init()
        private void RefreshDs1()
        {
            int delay = 2000;
            GetDs1StatusProcess(delay);
            while (dataDs1.enableSplitting)
            {
                Thread.Sleep(10);
                GetDs1StatusProcess(delay);
                if (!_StatusDs1)
                {
                    delay = 2000;
                }
                else
                {
                    delay = 5000;
                }
            }
        }

        private List<Item> inventory = null;

        public void InventorySee()
        {
            while (dataDs1.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusDs1) try { inventory = Ds1.GetInventory(); } catch (Exception ex) { DebugLog.LogMessage($"DS1 InventorySee Thread Ex: {ex.Message}",ex); }
            }
        }

        List<DefinitionsDs1.BossDs1> listPendingB = new List<DefinitionsDs1.BossDs1>();
        List<DefinitionsDs1.BonfireDs1> listPendingBon = new List<DefinitionsDs1.BonfireDs1>();
        List<DefinitionsDs1.LvlDs1> listPendingLvl = new List<DefinitionsDs1.LvlDs1>();
        List<DefinitionsDs1.PositionDs1> listPendingP = new List<DefinitionsDs1.PositionDs1>();
        List<DefinitionsDs1.ItemDs1> listPendingItem = new List<DefinitionsDs1.ItemDs1>();

        private void CheckLoad()
        {
            while (dataDs1.enableSplitting)
            {
                Thread.Sleep(200);
                if (_StatusDs1 && !_PracticeMode && !_ShowSettings)
                {
                    try
                    {
                        if ((listPendingB.Count > 0 || listPendingBon.Count > 0 || listPendingLvl.Count > 0 || listPendingP.Count > 0 || listPendingItem.Count > 0))
                        {
                            if (!Ds1.IsPlayerLoaded())
                            {
                                foreach (var boss in listPendingB)
                                {
                                    var b = dataDs1.bossToSplit.FindIndex(iboss => iboss.Id == boss.Id);
                                    splitterControl.SplitCheck($"SplitFlags is produced by: DS1 *After Login* BOSS -> {dataDs1.bossToSplit[b].Title}");
                                    dataDs1.bossToSplit[b].IsSplited = true;
                                }

                                foreach (var bone in listPendingBon)
                                {
                                    var bo = dataDs1.bonfireToSplit.FindIndex(Ibone => Ibone.Id == bone.Id);
                                    splitterControl.SplitCheck($"SplitFlags is produced by: DS1 *After Login* BONFIRE -> {dataDs1.bonfireToSplit[bo].Title}");
                                    dataDs1.bonfireToSplit[bo].IsSplited = true;
                                }

                                foreach (var lvl in listPendingLvl)
                                {
                                    var l = dataDs1.lvlToSplit.FindIndex(Ilvl => Ilvl.Attribute == lvl.Attribute && Ilvl.Value == lvl.Value);
                                    splitterControl.SplitCheck($"SplitFlags is produced by: DS1 *After Login* LEVEL -> {dataDs1.lvlToSplit[l].Attribute.ToString()} - {dataDs1.lvlToSplit[l].Value.ToString()}");
                                    dataDs1.lvlToSplit[l].IsSplited = true;
                                }

                                foreach (var position in listPendingP)
                                {
                                    var p = dataDs1.positionsToSplit.FindIndex(fposition => fposition.vector == position.vector);
                                    splitterControl.SplitCheck($"SplitFlags is produced by: DS1 *After Login* POSITION -> {dataDs1.positionsToSplit[p].Title} - {dataDs1.positionsToSplit[p].vector.ToString()}");
                                    dataDs1.positionsToSplit[p].IsSplited = true;
                                }

                                foreach (var cf in listPendingItem)
                                {
                                    var c = dataDs1.itemToSplit.FindIndex(icf => icf.Id == cf.Id);
                                    splitterControl.SplitCheck($"SplitFlags is produced by: DS1 *After Login* ITEM -> {dataDs1.itemToSplit[c].Title}");
                                    dataDs1.itemToSplit[c].IsSplited = true;
                                }

                                listPendingB.Clear();
                                listPendingBon.Clear();
                                listPendingLvl.Clear();
                                listPendingP.Clear();
                                listPendingItem.Clear();
                            }
                        }
                    }
                    catch (Exception ex) { DebugLog.LogMessage($"DS1 CheckLoad Thread Ex: {ex.Message}", ex); }
                }
            }
        }

        private void BossToSplit()
        {
            var BossToSplit = dataDs1.GetBossToSplit();
            while (dataDs1.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusDs1 && !_PracticeMode && !_ShowSettings)
                {
                    try
                    {
                        if (BossToSplit != dataDs1.GetBossToSplit()) BossToSplit = dataDs1.GetBossToSplit();
                        foreach (var b in BossToSplit)
                        {
                            if (!b.IsSplited && Ds1.ReadEventFlag(b.Id))
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
                                    splitterControl.SplitCheck($"SplitFlags is produced by: DS1 BOSS -> {b.Title}");
                                    b.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DebugLog.LogMessage($"DS1 BossToSplit Thread Ex: {ex.Message}", ex);
                    }
                }
            }
        }

        private void BonfireToSplit()
        {
            var BonfireToSplit = dataDs1.GetBonfireToSplit();
            while (dataDs1.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusDs1 && !_PracticeMode && !_ShowSettings)
                {
                    try
                    {
                        if (BonfireToSplit != dataDs1.GetBonfireToSplit()) BonfireToSplit = dataDs1.GetBonfireToSplit();
                        foreach (var bonfire in BonfireToSplit)
                        {
                            Bonfire aux = bonfire.Id;
                            if (!bonfire.IsSplited && Ds1.GetBonfireState(bonfire.Id) == bonfire.Value)
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
                                    splitterControl.SplitCheck($"SplitFlags is produced by: DS1 BONFIRE -> {bonfire.Title}");
                                    bonfire.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DebugLog.LogMessage($"DS1 BonfireToSplit Thread Ex: {ex.Message}", ex);
                    }
                }
            }
        }

        private void LvlToSplit()
        {
            var LvlToSplit = dataDs1.GetLvlToSplit();
            while (dataDs1.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusDs1 && !_PracticeMode && !_ShowSettings)
                {
                    try
                    {
                        if (LvlToSplit != dataDs1.GetLvlToSplit()) LvlToSplit = dataDs1.GetLvlToSplit();
                        foreach (var lvl in LvlToSplit)
                        {
                            if (!lvl.IsSplited && Ds1.GetAttribute(lvl.Attribute) >= lvl.Value)
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
                                    splitterControl.SplitCheck($"SplitFlags is produced by: DS1 LEVEL -> {lvl.Attribute} - {lvl.Value}");
                                    lvl.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DebugLog.LogMessage($"DS1 LvlToSplit Thread Ex: {ex.Message}", ex);
                    }
                }
            }
        }

        private void PositionToSplit()
        {
            var PositionsToSplit = dataDs1.GetPositionsToSplit();
            while (dataDs1.enableSplitting)
            {
                Thread.Sleep(100);
                if (_StatusDs1 && !_PracticeMode && !_ShowSettings)
                {
                    try
                    {
                        if (PositionsToSplit != dataDs1.GetPositionsToSplit()) PositionsToSplit = dataDs1.GetPositionsToSplit();
                        foreach (var p in PositionsToSplit)
                        {
                            if (!p.IsSplited)
                            {
                                var currentlyPosition = Ds1.GetPosition();
                                var rangeX = ((currentlyPosition.X - p.vector.X) <= dataDs1.positionMargin) && ((currentlyPosition.X - p.vector.X) >= -dataDs1.positionMargin);
                                var rangeY = ((currentlyPosition.Y - p.vector.Y) <= dataDs1.positionMargin) && ((currentlyPosition.Y - p.vector.Y) >= -dataDs1.positionMargin);
                                var rangeZ = ((currentlyPosition.Z - p.vector.Z) <= dataDs1.positionMargin) && ((currentlyPosition.Z - p.vector.Z) >= -dataDs1.positionMargin);
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
                                        splitterControl.SplitCheck($"SplitFlags is produced by: DS1 POSITION -> {p.Title} - {p.vector.ToString()}");
                                        p.IsSplited = splitterControl.GetSplitStatus();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DebugLog.LogMessage($"DS1 PositionToSplit Thread Ex: {ex.Message}", ex);
                    }
                }
            }
        }

        #region AllItems List
        private readonly List<Item> allItems = new List<Item>()
        {
            new Item("Catarina Helm"                                    ,   10000, ItemType.CatarinaHelm                           , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Catarina Armor"                                   ,   11000, ItemType.CatarinaArmor                          , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Catarina Gauntlets"                               ,   12000, ItemType.CatarinaGauntlets                      , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Catarina Leggings"                                ,   13000, ItemType.CatarinaLeggings                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Paladin Helm"                                     ,   20000, ItemType.PaladinHelm                            , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Paladin Armor"                                    ,   21000, ItemType.PaladinArmor                           , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Paladin Gauntlets"                                ,   22000, ItemType.PaladinGauntlets                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Paladin Leggings"                                 ,   23000, ItemType.PaladinLeggings                        , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Dark Mask"                                        ,   40000, ItemType.DarkMask                               , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Dark Armor"                                       ,   41000, ItemType.DarkArmor                              , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Dark Gauntlets"                                   ,   42000, ItemType.DarkGauntlets                          , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Dark Leggings"                                    ,   43000, ItemType.DarkLeggings                           , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Brigand Hood"                                     ,   50000, ItemType.BrigandHood                            , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Brigand Armor"                                    ,   51000, ItemType.BrigandArmor                           , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Brigand Gauntlets"                                ,   52000, ItemType.BrigandGauntlets                       , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Brigand Trousers"                                 ,   53000, ItemType.BrigandTrousers                        , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Shadow Mask"                                      ,   60000, ItemType.ShadowMask                             , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Shadow Garb"                                      ,   61000, ItemType.ShadowGarb                             , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Shadow Gauntlets"                                 ,   62000, ItemType.ShadowGauntlets                        , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Shadow Leggings"                                  ,   63000, ItemType.ShadowLeggings                         , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Black Iron Helm"                                  ,   70000, ItemType.BlackIronHelm                          , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Black Iron Armor"                                 ,   71000, ItemType.BlackIronArmor                         , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Black Iron Gauntlets"                             ,   72000, ItemType.BlackIronGauntlets                     , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Black Iron Leggings"                              ,   73000, ItemType.BlackIronLeggings                      , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Smough's Helm"                                    ,   80000, ItemType.SmoughsHelm                            , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Smough's Armor"                                   ,   81000, ItemType.SmoughsArmor                           , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Smough's Gauntlets"                               ,   82000, ItemType.SmoughsGauntlets                       , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Smough's Leggings"                                ,   83000, ItemType.SmoughsLeggings                        , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Six-Eyed Helm of the Channelers"                  ,   90000, ItemType.SixEyedHelmoftheChannelers             , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Robe of the Channelers"                           ,   91000, ItemType.RobeoftheChannelers                    , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Gauntlets of the Channelers"                      ,   92000, ItemType.GauntletsoftheChannelers               , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Waistcloth of the Channelers"                     ,   93000, ItemType.WaistclothoftheChannelers              , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Helm of Favor"                                    ,  100000, ItemType.HelmofFavor                            , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Embraced Armor of Favor"                          ,  101000, ItemType.EmbracedArmorofFavor                   , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Gauntlets of Favor"                               ,  102000, ItemType.GauntletsofFavor                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Leggings of Favor"                                ,  103000, ItemType.LeggingsofFavor                        , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Helm of the Wise"                                 ,  110000, ItemType.HelmoftheWise                          , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Armor of the Glorious"                            ,  111000, ItemType.ArmoroftheGlorious                     , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Gauntlets of the Vanquisher"                      ,  112000, ItemType.GauntletsoftheVanquisher               , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Boots of the Explorer"                            ,  113000, ItemType.BootsoftheExplorer                     , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Stone Helm"                                       ,  120000, ItemType.StoneHelm                              , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Stone Armor"                                      ,  121000, ItemType.StoneArmor                             , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Stone Gauntlets"                                  ,  122000, ItemType.StoneGauntlets                         , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Stone Leggings"                                   ,  123000, ItemType.StoneLeggings                          , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Crystalline Helm"                                 ,  130000, ItemType.CrystallineHelm                        , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Crystalline Armor"                                ,  131000, ItemType.CrystallineArmor                       , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Crystalline Gauntlets"                            ,  132000, ItemType.CrystallineGauntlets                   , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Crystalline Leggings"                             ,  133000, ItemType.CrystallineLeggings                    , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Mask of the Sealer"                               ,  140000, ItemType.MaskoftheSealer                        , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Crimson Robe"                                     ,  141000, ItemType.CrimsonRobe                            , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Crimson Gloves"                                   ,  142000, ItemType.CrimsonGloves                          , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Crimson Waistcloth"                               ,  143000, ItemType.CrimsonWaistcloth                      , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Mask of Velka"                                    ,  150000, ItemType.MaskofVelka                            , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Black Cleric Robe"                                ,  151000, ItemType.BlackClericRobe                        , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Black Manchette"                                  ,  152000, ItemType.BlackManchette                         , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Black Tights"                                     ,  153000, ItemType.BlackTights                            , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Iron Helm"                                        ,  160000, ItemType.IronHelm                               , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Armor of the Sun"                                 ,  161000, ItemType.ArmoroftheSun                          , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Iron Bracelet"                                    ,  162000, ItemType.IronBracelet                           , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Iron Leggings"                                    ,  163000, ItemType.IronLeggings                           , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Chain Helm"                                       ,  170000, ItemType.ChainHelm                              , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Chain Armor"                                      ,  171000, ItemType.ChainArmor                             , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Leather Gauntlets"                                ,  172000, ItemType.LeatherGauntlets                       , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Chain Leggings"                                   ,  173000, ItemType.ChainLeggings                          , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Cleric Helm"                                      ,  180000, ItemType.ClericHelm                             , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Cleric Armor"                                     ,  181000, ItemType.ClericArmor                            , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Cleric Gauntlets"                                 ,  182000, ItemType.ClericGauntlets                        , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Cleric Leggings"                                  ,  183000, ItemType.ClericLeggings                         , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Sunlight Maggot"                                  ,  190000, ItemType.SunlightMaggot                         , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Helm of Thorns"                                   ,  200000, ItemType.HelmofThorns                           , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Armor of Thorns"                                  ,  201000, ItemType.ArmorofThorns                          , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Gauntlets of Thorns"                              ,  202000, ItemType.GauntletsofThorns                      , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Leggings of Thorns"                               ,  203000, ItemType.LeggingsofThorns                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Standard Helm"                                    ,  210000, ItemType.StandardHelm                           , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Hard Leather Armor"                               ,  211000, ItemType.HardLeatherArmor                       , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Hard Leather Gauntlets"                           ,  212000, ItemType.HardLeatherGauntlets                   , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Hard Leather Boots"                               ,  213000, ItemType.HardLeatherBoots                       , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Sorcerer Hat"                                     ,  220000, ItemType.SorcererHat                            , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Sorcerer Cloak"                                   ,  221000, ItemType.SorcererCloak                          , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Sorcerer Gauntlets"                               ,  222000, ItemType.SorcererGauntlets                      , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Sorcerer Boots"                                   ,  223000, ItemType.SorcererBoots                          , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Tattered Cloth Hood"                              ,  230000, ItemType.TatteredClothHood                      , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Tattered Cloth Robe"                              ,  231000, ItemType.TatteredClothRobe                      , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Tattered Cloth Manchette"                         ,  232000, ItemType.TatteredClothManchette                 , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Heavy Boots"                                      ,  233000, ItemType.HeavyBoots                             , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Pharis's Hat"                                     ,  240000, ItemType.PharissHat                             , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Leather Armor"                                    ,  241000, ItemType.LeatherArmor                           , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Leather Gloves"                                   ,  242000, ItemType.LeatherGloves                          , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Leather Boots"                                    ,  243000, ItemType.LeatherBoots                           , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Painting Guardian Hood"                           ,  250000, ItemType.PaintingGuardianHood                   , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Painting Guardian Robe"                           ,  251000, ItemType.PaintingGuardianRobe                   , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Painting Guardian Gloves"                         ,  252000, ItemType.PaintingGuardianGloves                 , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Painting Guardian Waistcloth"                     ,  253000, ItemType.PaintingGuardianWaistcloth             , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Ornstein's Helm"                                  ,  270000, ItemType.OrnsteinsHelm                          , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Ornstein's Armor"                                 ,  271000, ItemType.OrnsteinsArmor                         , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Ornstein's Gauntlets"                             ,  272000, ItemType.OrnsteinsGauntlets                     , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Ornstein's Leggings"                              ,  273000, ItemType.OrnsteinsLeggings                      , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Eastern Helm"                                     ,  280000, ItemType.EasternHelm                            , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Eastern Armor"                                    ,  281000, ItemType.EasternArmor                           , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Eastern Gauntlets"                                ,  282000, ItemType.EasternGauntlets                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Eastern Leggings"                                 ,  283000, ItemType.EasternLeggings                        , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Xanthous Crown"                                   ,  290000, ItemType.XanthousCrown                          , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Xanthous Overcoat"                                ,  291000, ItemType.XanthousOvercoat                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Xanthous Gloves"                                  ,  292000, ItemType.XanthousGloves                         , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Xanthous Waistcloth"                              ,  293000, ItemType.XanthousWaistcloth                     , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Thief Mask"                                       ,  300000, ItemType.ThiefMask                              , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Black Leather Armor"                              ,  301000, ItemType.BlackLeatherArmor                      , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Black Leather Gloves"                             ,  302000, ItemType.BlackLeatherGloves                     , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Black Leather Boots"                              ,  303000, ItemType.BlackLeatherBoots                      , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Priest's Hat"                                     ,  310000, ItemType.PriestsHat                             , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Holy Robe"                                        ,  311000, ItemType.HolyRobe                               , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Traveling Gloves (Holy)"                          ,  312000, ItemType.TravelingGlovesHoly                    , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Holy Trousers"                                    ,  313000, ItemType.HolyTrousers                           , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Black Knight Helm"                                ,  320000, ItemType.BlackKnightHelm                        , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Black Knight Armor"                               ,  321000, ItemType.BlackKnightArmor                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Black Knight Gauntlets"                           ,  322000, ItemType.BlackKnightGauntlets                   , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Black Knight Leggings"                            ,  323000, ItemType.BlackKnightLeggings                    , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Crown of Dusk"                                    ,  330000, ItemType.CrownofDusk                            , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Antiquated Dress"                                 ,  331000, ItemType.AntiquatedDress                        , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Antiquated Gloves"                                ,  332000, ItemType.AntiquatedGloves                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Antiquated Skirt"                                 ,  333000, ItemType.AntiquatedSkirt                        , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Witch Hat"                                        ,  340000, ItemType.WitchHat                               , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Witch Cloak"                                      ,  341000, ItemType.WitchCloak                             , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Witch Gloves"                                     ,  342000, ItemType.WitchGloves                            , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Witch Skirt"                                      ,  343000, ItemType.WitchSkirt                             , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Elite Knight Helm"                                ,  350000, ItemType.EliteKnightHelm                        , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Elite Knight Armor"                               ,  351000, ItemType.EliteKnightArmor                       , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Elite Knight Gauntlets"                           ,  352000, ItemType.EliteKnightGauntlets                   , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Elite Knight Leggings"                            ,  353000, ItemType.EliteKnightLeggings                    , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Wanderer Hood"                                    ,  360000, ItemType.WandererHood                           , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Wanderer Coat"                                    ,  361000, ItemType.WandererCoat                           , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Wanderer Manchette"                               ,  362000, ItemType.WandererManchette                      , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Wanderer Boots"                                   ,  363000, ItemType.WandererBoots                          , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Big Hat"                                          ,  380000, ItemType.BigHat                                 , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Sage Robe"                                        ,  381000, ItemType.SageRobe                               , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Traveling Gloves (Sage)"                          ,  382000, ItemType.TravelingGlovesSage                    , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Traveling Boots"                                  ,  383000, ItemType.TravelingBoots                         , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Knight Helm"                                      ,  390000, ItemType.KnightHelm                             , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Knight Armor"                                     ,  391000, ItemType.KnightArmor                            , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Knight Gauntlets"                                 ,  392000, ItemType.KnightGauntlets                        , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Knight Leggings"                                  ,  393000, ItemType.KnightLeggings                         , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Dingy Hood"                                       ,  400000, ItemType.DingyHood                              , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Dingy Robe"                                       ,  401000, ItemType.DingyRobe                              , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Dingy Gloves"                                     ,  402000, ItemType.DingyGloves                            , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Blood-Stained Skirt"                              ,  403000, ItemType.BloodStainedSkirt                      , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Maiden Hood"                                      ,  410000, ItemType.MaidenHood                             , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Maiden Robe"                                      ,  411000, ItemType.MaidenRobe                             , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Maiden Gloves"                                    ,  412000, ItemType.MaidenGloves                           , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Maiden Skirt"                                     ,  413000, ItemType.MaidenSkirt                            , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Silver Knight Helm"                               ,  420000, ItemType.SilverKnightHelm                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Silver Knight Armor"                              ,  421000, ItemType.SilverKnightArmor                      , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Silver Knight Gauntlets"                          ,  422000, ItemType.SilverKnightGauntlets                  , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Silver Knight Leggings"                           ,  423000, ItemType.SilverKnightLeggings                   , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Havel's Helm"                                     ,  440000, ItemType.HavelsHelm                             , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Havel's Armor"                                    ,  441000, ItemType.HavelsArmor                            , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Havel's Gauntlets"                                ,  442000, ItemType.HavelsGauntlets                        , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Havel's Leggings"                                 ,  443000, ItemType.HavelsLeggings                         , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Brass Helm"                                       ,  450000, ItemType.BrassHelm                              , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Brass Armor"                                      ,  451000, ItemType.BrassArmor                             , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Brass Gauntlets"                                  ,  452000, ItemType.BrassGauntlets                         , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Brass Leggings"                                   ,  453000, ItemType.BrassLeggings                          , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Gold-Hemmed Black Hood"                           ,  460000, ItemType.GoldHemmedBlackHood                    , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Gold-Hemmed Black Cloak"                          ,  461000, ItemType.GoldHemmedBlackCloak                   , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Gold-Hemmed Black Gloves"                         ,  462000, ItemType.GoldHemmedBlackGloves                  , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Gold-Hemmed Black Skirt"                          ,  463000, ItemType.GoldHemmedBlackSkirt                   , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Golem Helm"                                       ,  470000, ItemType.GolemHelm                              , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Golem Armor"                                      ,  471000, ItemType.GolemArmor                             , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Golem Gauntlets"                                  ,  472000, ItemType.GolemGauntlets                         , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Golem Leggings"                                   ,  473000, ItemType.GolemLeggings                          , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Hollow Soldier Helm"                              ,  480000, ItemType.HollowSoldierHelm                      , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Hollow Soldier Armor"                             ,  481000, ItemType.HollowSoldierArmor                     , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Hollow Soldier Waistcloth"                        ,  483000, ItemType.HollowSoldierWaistcloth                , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Steel Helm"                                       ,  490000, ItemType.SteelHelm                              , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Steel Armor"                                      ,  491000, ItemType.SteelArmor                             , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Steel Gauntlets"                                  ,  492000, ItemType.SteelGauntlets                         , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Steel Leggings"                                   ,  493000, ItemType.SteelLeggings                          , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Hollow Thief's Hood"                              ,  500000, ItemType.HollowThiefsHood                       , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Hollow Thief's Leather Armor"                     ,  501000, ItemType.HollowThiefsLeatherArmor               , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Hollow Thief's Tights"                            ,  503000, ItemType.HollowThiefsTights                     , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Balder Helm"                                      ,  510000, ItemType.BalderHelm                             , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Balder Armor"                                     ,  511000, ItemType.BalderArmor                            , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Balder Gauntlets"                                 ,  512000, ItemType.BalderGauntlets                        , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Balder Leggings"                                  ,  513000, ItemType.BalderLeggings                         , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Hollow Warrior Helm"                              ,  520000, ItemType.HollowWarriorHelm                      , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Hollow Warrior Armor"                             ,  521000, ItemType.HollowWarriorArmor                     , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Hollow Warrior Waistcloth"                        ,  523000, ItemType.HollowWarriorWaistcloth                , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Giant Helm"                                       ,  530000, ItemType.GiantHelm                              , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Giant Armor"                                      ,  531000, ItemType.GiantArmor                             , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Giant Gauntlets"                                  ,  532000, ItemType.GiantGauntlets                         , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Giant Leggings"                                   ,  533000, ItemType.GiantLeggings                          , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Crown of the Dark Sun"                            ,  540000, ItemType.CrownoftheDarkSun                      , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Moonlight Robe"                                   ,  541000, ItemType.MoonlightRobe                          , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Moonlight Gloves"                                 ,  542000, ItemType.MoonlightGloves                        , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Moonlight Waistcloth"                             ,  543000, ItemType.MoonlightWaistcloth                    , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Crown of the Great Lord"                          ,  550000, ItemType.CrownoftheGreatLord                    , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Robe of the Great Lord"                           ,  551000, ItemType.RobeoftheGreatLord                     , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Bracelet of the Great Lord"                       ,  552000, ItemType.BraceletoftheGreatLord                 , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Anklet of the Great Lord"                         ,  553000, ItemType.AnkletoftheGreatLord                   , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Sack"                                             ,  560000, ItemType.Sack                                   , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Symbol of Avarice"                                ,  570000, ItemType.SymbolofAvarice                        , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Royal Helm"                                       ,  580000, ItemType.RoyalHelm                              , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Mask of the Father"                               ,  590000, ItemType.MaskoftheFather                        , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Mask of the Mother"                               ,  600000, ItemType.MaskoftheMother                        , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Mask of the Child"                                ,  610000, ItemType.MaskoftheChild                         , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Fang Boar Helm"                                   ,  620000, ItemType.FangBoarHelm                           , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Gargoyle Helm"                                    ,  630000, ItemType.GargoyleHelm                           , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Black Sorcerer Hat"                               ,  640000, ItemType.BlackSorcererHat                       , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Black Sorcerer Cloak"                             ,  641000, ItemType.BlackSorcererCloak                     , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Black Sorcerer Gauntlets"                         ,  642000, ItemType.BlackSorcererGauntlets                 , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Black Sorcerer Boots"                             ,  643000, ItemType.BlackSorcererBoots                     , ItemCategory.Armor          ,   1, ItemUpgrade.Armor              ),
            new Item("Helm of Artorias"                                 ,  660000, ItemType.HelmofArtorias                         , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Armor of Artorias"                                ,  661000, ItemType.ArmorofArtorias                        , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Gauntlets of Artorias"                            ,  662000, ItemType.GauntletsofArtorias                    , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Leggings of Artorias"                             ,  663000, ItemType.LeggingsofArtorias                     , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Porcelain Mask"                                   ,  670000, ItemType.PorcelainMask                          , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Lord's Blade Robe"                                ,  671000, ItemType.LordsBladeRobe                         , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Lord's Blade Gloves"                              ,  672000, ItemType.LordsBladeGloves                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Lord's Blade Waistcloth"                          ,  673000, ItemType.LordsBladeWaistcloth                   , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Gough's Helm"                                     ,  680000, ItemType.GoughsHelm                             , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Gough's Armor"                                    ,  681000, ItemType.GoughsArmor                            , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Gough's Gauntlets"                                ,  682000, ItemType.GoughsGauntlets                        , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Gough's Leggings"                                 ,  683000, ItemType.GoughsLeggings                         , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Guardian Helm"                                    ,  690000, ItemType.GuardianHelm                           , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Guardian Armor"                                   ,  691000, ItemType.GuardianArmor                          , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Guardian Gauntlets"                               ,  692000, ItemType.GuardianGauntlets                      , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Guardian Leggings"                                ,  693000, ItemType.GuardianLeggings                       , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Snickering Top Hat"                               ,  700000, ItemType.SnickeringTopHat                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Chester's Long Coat"                              ,  701000, ItemType.ChestersLongCoat                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Chester's Gloves"                                 ,  702000, ItemType.ChestersGloves                         , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Chester's Trousers"                               ,  703000, ItemType.ChestersTrousers                       , ItemCategory.Armor          ,   1, ItemUpgrade.Unique             ),
            new Item("Bloated Head"                                     ,  710000, ItemType.BloatedHead                            , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),
            new Item("Bloated Sorcerer Head"                            ,  720000, ItemType.BloatedSorcererHead                    , ItemCategory.Armor          ,   1, ItemUpgrade.None               ),

            new Item("Eye of Death"                                     ,      109, ItemType.EyeofDeath                             , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Cracked Red Eye Orb"                              ,      111, ItemType.CrackedRedEyeOrb                       , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Estus Flask"                                      ,      200, ItemType.EstusFlask                             , ItemCategory.Consumables    ,   1, ItemUpgrade.None               ),
            new Item("Elizabeth's Mushroom"                             ,      230, ItemType.ElizabethsMushroom                     , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Divine Blessing"                                  ,      240, ItemType.DivineBlessing                         , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Green Blossom"                                    ,      260, ItemType.GreenBlossom                           , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Bloodred Moss Clump"                              ,      270, ItemType.BloodredMossClump                      , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Purple Moss Clump"                                ,      271, ItemType.PurpleMossClump                        , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Blooming Purple Moss Clump"                       ,      272, ItemType.BloomingPurpleMossClump                , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Purging Stone"                                    ,      274, ItemType.PurgingStone                           , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Egg Vermifuge"                                    ,      275, ItemType.EggVermifuge                           , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Repair Powder"                                    ,      280, ItemType.RepairPowder                           , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Throwing Knife"                                   ,      290, ItemType.ThrowingKnife                          , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Poison Throwing Knife"                            ,      291, ItemType.PoisonThrowingKnife                    , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Firebomb"                                         ,      292, ItemType.Firebomb                               , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Dung Pie"                                         ,      293, ItemType.DungPie                                , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Alluring Skull"                                   ,      294, ItemType.AlluringSkull                          , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Lloyd's Talisman"                                 ,      296, ItemType.LloydsTalisman                         , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Black Firebomb"                                   ,      297, ItemType.BlackFirebomb                          , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Charcoal Pine Resin"                              ,      310, ItemType.CharcoalPineResin                      , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Gold Pine Resin"                                  ,      311, ItemType.GoldPineResin                          , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Transient Curse"                                  ,      312, ItemType.TransientCurse                         , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Rotten Pine Resin"                                ,      313, ItemType.RottenPineResin                        , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Homeward Bone"                                    ,      330, ItemType.HomewardBone                           , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Prism Stone"                                      ,      370, ItemType.PrismStone                             , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Indictment"                                       ,      373, ItemType.Indictment                             , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Souvenir of Reprisal"                             ,      374, ItemType.SouvenirofReprisal                     , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Sunlight Medal"                                   ,      375, ItemType.SunlightMedal                          , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Pendant"                                          ,      376, ItemType.Pendant                                , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Rubbish"                                          ,      380, ItemType.Rubbish                                , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Copper Coin"                                      ,      381, ItemType.CopperCoin                             , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Silver Coin"                                      ,      382, ItemType.SilverCoin                             , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Gold Coin"                                        ,      383, ItemType.GoldCoin                               , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Fire Keeper Soul (Anastacia of Astora)"           ,      390, ItemType.FireKeeperSoulAnastaciaofAstora        , ItemCategory.Consumables    ,   1, ItemUpgrade.None               ),
            new Item("Fire Keeper Soul (Darkmoon Knightess)"            ,      391, ItemType.FireKeeperSoulDarkmoonKnightess        , ItemCategory.Consumables    ,   1, ItemUpgrade.None               ),
            new Item("Fire Keeper Soul (Daughter of Chaos)"             ,      392, ItemType.FireKeeperSoulDaughterofChaos          , ItemCategory.Consumables    ,   1, ItemUpgrade.None               ),
            new Item("Fire Keeper Soul (New Londo)"                     ,      393, ItemType.FireKeeperSoulNewLondo                 , ItemCategory.Consumables    ,   1, ItemUpgrade.None               ),
            new Item("Fire Keeper Soul (Blighttown)"                    ,      394, ItemType.FireKeeperSoulBlighttown               , ItemCategory.Consumables    ,   1, ItemUpgrade.None               ),
            new Item("Fire Keeper Soul (Duke's Archives)"               ,      395, ItemType.FireKeeperSoulDukesArchives            , ItemCategory.Consumables    ,   1, ItemUpgrade.None               ),
            new Item("Fire Keeper Soul (Undead Parish)"                 ,      396, ItemType.FireKeeperSoulUndeadParish             , ItemCategory.Consumables    ,   1, ItemUpgrade.None               ),
            new Item("Soul of a Lost Undead"                            ,      400, ItemType.SoulofaLostUndead                      , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Large Soul of a Lost Undead"                      ,      401, ItemType.LargeSoulofaLostUndead                 , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of a Nameless Soldier"                       ,      402, ItemType.SoulofaNamelessSoldier                 , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Large Soul of a Nameless Soldier"                 ,      403, ItemType.LargeSoulofaNamelessSoldier            , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of a Proud Knight"                           ,      404, ItemType.SoulofaProudKnight                     , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Large Soul of a Proud Knight"                     ,      405, ItemType.LargeSoulofaProudKnight                , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of a Brave Warrior"                          ,      406, ItemType.SoulofaBraveWarrior                    , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Large Soul of a Brave Warrior"                    ,      407, ItemType.LargeSoulofaBraveWarrior               , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of a Hero"                                   ,      408, ItemType.SoulofaHero                            , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of a Great Hero"                             ,      409, ItemType.SoulofaGreatHero                       , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Humanity"                                         ,      500, ItemType.Humanity                               , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Twin Humanities"                                  ,      501, ItemType.TwinHumanities                         , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of Quelaag"                                  ,      700, ItemType.SoulofQuelaag                          , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of Sif"                                      ,      701, ItemType.SoulofSif                              , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of Gwyn, Lord of Cinder"                     ,      702, ItemType.SoulofGwynLordofCinder                 , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Core of an Iron Golem"                            ,      703, ItemType.CoreofanIronGolem                      , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of Ornstein"                                 ,      704, ItemType.SoulofOrnstein                         , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of the Moonlight Butterfly"                  ,      705, ItemType.SouloftheMoonlightButterfly            , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of Smough"                                   ,      706, ItemType.SoulofSmough                           , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of Priscilla"                                ,      707, ItemType.SoulofPriscilla                        , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of Gwyndolin"                                ,      708, ItemType.SoulofGwyndolin                        , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Guardian Soul"                                    ,      709, ItemType.GuardianSoul                           , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of Artorias"                                 ,      710, ItemType.SoulofArtorias                         , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),
            new Item("Soul of Manus"                                    ,      711, ItemType.SoulofManus                            , ItemCategory.Consumables    ,  99, ItemUpgrade.None               ),

            new Item("Peculiar Doll"                                    ,      384, ItemType.PeculiarDoll                           , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Basement Key"                                     ,     2001, ItemType.BasementKey                            , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Crest of Artorias"                                ,     2002, ItemType.CrestofArtorias                        , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Cage Key"                                         ,     2003, ItemType.CageKey                                , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Archive Tower Cell Key"                           ,     2004, ItemType.ArchiveTowerCellKey                    , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Archive Tower Giant Door Key"                     ,     2005, ItemType.ArchiveTowerGiantDoorKey               , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Archive Tower Giant Cell Key"                     ,     2006, ItemType.ArchiveTowerGiantCellKey               , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Blighttown Key"                                   ,     2007, ItemType.BlighttownKey                          , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Key to New Londo Ruins"                           ,     2008, ItemType.KeytoNewLondoRuins                     , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Annex Key"                                        ,     2009, ItemType.AnnexKey                               , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Dungeon Cell Key"                                 ,     2010, ItemType.DungeonCellKey                         , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Big Pilgrim's Key"                                ,     2011, ItemType.BigPilgrimsKey                         , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Undead Asylum F2 East Key"                        ,     2012, ItemType.UndeadAsylumF2EastKey                  , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Key to the Seal"                                  ,     2013, ItemType.KeytotheSeal                           , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Key to Depths"                                    ,     2014, ItemType.KeytoDepths                            , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Undead Asylum F2 West Key"                        ,     2016, ItemType.UndeadAsylumF2WestKey                  , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Mystery Key"                                      ,     2017, ItemType.MysteryKey                             , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Sewer Chamber Key"                                ,     2018, ItemType.SewerChamberKey                        , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Watchtower Basement Key"                          ,     2019, ItemType.WatchtowerBasementKey                  , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Archive Prison Extra Key"                         ,     2020, ItemType.ArchivePrisonExtraKey                  , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Residence Key"                                    ,     2021, ItemType.ResidenceKey                           , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Crest Key"                                        ,     2022, ItemType.CrestKey                               , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Master Key"                                       ,     2100, ItemType.MasterKey                              , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Lord Soul (Nito)"                                 ,     2500, ItemType.LordSoulNito                           , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Lord Soul (Bed of Chaos)"                         ,     2501, ItemType.LordSoulBedofChaos                     , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Bequeathed Lord Soul Shard (Four Kings)"          ,     2502, ItemType.BequeathedLordSoulShardFourKings       , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Bequeathed Lord Soul Shard (Seath)"               ,     2503, ItemType.BequeathedLordSoulShardSeath           , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Lordvessel"                                       ,     2510, ItemType.Lordvessel                             , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Broken Pendant"                                   ,     2520, ItemType.BrokenPendant                          , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Weapon Smithbox"                                  ,     2600, ItemType.WeaponSmithbox                         , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Armor Smithbox"                                   ,     2601, ItemType.ArmorSmithbox                          , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Repairbox"                                        ,     2602, ItemType.Repairbox                              , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Rite of Kindling"                                 ,     2607, ItemType.RiteofKindling                         , ItemCategory.Key            ,   1, ItemUpgrade.None               ),
            new Item("Bottomless Box"                                   ,     2608, ItemType.BottomlessBox                          , ItemCategory.Key            ,   1, ItemUpgrade.None               ),

            new Item("Dagger"                                           ,   100000, ItemType.Dagger                                 , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Parrying Dagger"                                  ,   101000, ItemType.ParryingDagger                         , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Ghost Blade"                                      ,   102000, ItemType.GhostBlade                             , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Bandit's Knife"                                   ,   103000, ItemType.BanditsKnife                           , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Priscilla's Dagger"                               ,   104000, ItemType.PriscillasDagger                       , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Shortsword"                                       ,   200000, ItemType.Shortsword                             , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Longsword"                                        ,   201000, ItemType.Longsword                              , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Broadsword"                                       ,   202000, ItemType.Broadsword                             , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Broken Straight Sword"                            ,   203000, ItemType.BrokenStraightSword                    , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Balder Side Sword"                                ,   204000, ItemType.BalderSideSword                        , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Crystal Straight Sword"                           ,   205000, ItemType.CrystalStraightSword                   , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.None               ),
            new Item("Sunlight Straight Sword"                          ,   206000, ItemType.SunlightStraightSword                  , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Barbed Straight Sword"                            ,   207000, ItemType.BarbedStraightSword                    , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Silver Knight Straight Sword"                     ,   208000, ItemType.SilverKnightStraightSword              , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Astora's Straight Sword"                          ,   209000, ItemType.AstorasStraightSword                   , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Darksword"                                        ,   210000, ItemType.Darksword                              , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Drake Sword"                                      ,   211000, ItemType.DrakeSword                             , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Straight Sword Hilt"                              ,   212000, ItemType.StraightSwordHilt                      , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Bastard Sword"                                    ,   300000, ItemType.BastardSword                           , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Claymore"                                         ,   301000, ItemType.Claymore                               , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Man-serpent Greatsword"                           ,   302000, ItemType.ManserpentGreatsword                   , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Flamberge"                                        ,   303000, ItemType.Flamberge                              , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Crystal Greatsword"                               ,   304000, ItemType.CrystalGreatsword                      , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.None               ),
            new Item("Stone Greatsword"                                 ,   306000, ItemType.StoneGreatsword                        , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Greatsword of Artorias"                           ,   307000, ItemType.GreatswordofArtorias                   , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Moonlight Greatsword"                             ,   309000, ItemType.MoonlightGreatsword                    , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Black Knight Sword"                               ,   310000, ItemType.BlackKnightSword                       , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Greatsword of Artorias (Cursed)"                  ,   311000, ItemType.GreatswordofArtoriasCursed             , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Great Lord Greatsword"                            ,   314000, ItemType.GreatLordGreatsword                    , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Zweihander"                                       ,   350000, ItemType.Zweihander                             , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Greatsword"                                       ,   351000, ItemType.Greatsword                             , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Demon Great Machete"                              ,   352000, ItemType.DemonGreatMachete                      , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Dragon Greatsword"                                ,   354000, ItemType.DragonGreatsword                       , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Black Knight Greatsword"                          ,   355000, ItemType.BlackKnightGreatsword                  , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Scimitar"                                         ,   400000, ItemType.Scimitar                               , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Falchion"                                         ,   401000, ItemType.Falchion                               , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Shotel"                                           ,   402000, ItemType.Shotel                                 , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Jagged Ghost Blade"                               ,   403000, ItemType.JaggedGhostBlade                       , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Painting Guardian Sword"                          ,   405000, ItemType.PaintingGuardianSword                  , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Quelaag's Furysword"                              ,   406000, ItemType.QuelaagsFurysword                      , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Server"                                           ,   450000, ItemType.Server                                 , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Murakumo"                                         ,   451000, ItemType.Murakumo                               , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Gravelord Sword"                                  ,   453000, ItemType.GravelordSword                         , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Uchigatana"                                       ,   500000, ItemType.Uchigatana                             , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Washing Pole"                                     ,   501000, ItemType.WashingPole                            , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Iaito"                                            ,   502000, ItemType.Iaito                                  , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Chaos Blade"                                      ,   503000, ItemType.ChaosBlade                             , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Mail Breaker"                                     ,   600000, ItemType.MailBreaker                            , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Rapier"                                           ,   601000, ItemType.Rapier                                 , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Estoc"                                            ,   602000, ItemType.Estoc                                  , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Velka's Rapier"                                   ,   603000, ItemType.VelkasRapier                           , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Ricard's Rapier"                                  ,   604000, ItemType.RicardsRapier                          , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Hand Axe"                                         ,   700000, ItemType.HandAxe                                , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Battle Axe"                                       ,   701000, ItemType.BattleAxe                              , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Crescent Axe"                                     ,   702000, ItemType.CrescentAxe                            , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Butcher Knife"                                    ,   703000, ItemType.ButcherKnife                           , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Golem Axe"                                        ,   704000, ItemType.GolemAxe                               , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Gargoyle Tail Axe"                                ,   705000, ItemType.GargoyleTailAxe                        , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Greataxe"                                         ,   750000, ItemType.Greataxe                               , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Demon's Greataxe"                                 ,   751000, ItemType.DemonsGreataxe                         , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Dragon King Greataxe"                             ,   752000, ItemType.DragonKingGreataxe                     , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Black Knight Greataxe"                            ,   753000, ItemType.BlackKnightGreataxe                    , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Club"                                             ,   800000, ItemType.Club                                   , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Mace"                                             ,   801000, ItemType.Mace                                   , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Morning Star"                                     ,   802000, ItemType.MorningStar                            , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Warpick"                                          ,   803000, ItemType.Warpick                                , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Pickaxe"                                          ,   804000, ItemType.Pickaxe                                , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Reinforced Club"                                  ,   809000, ItemType.ReinforcedClub                         , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Blacksmith Hammer"                                ,   810000, ItemType.BlacksmithHammer                       , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Blacksmith Giant Hammer"                          ,   811000, ItemType.BlacksmithGiantHammer                  , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Hammer of Vamos"                                  ,   812000, ItemType.HammerofVamos                          , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Great Club"                                       ,   850000, ItemType.GreatClub                              , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Grant"                                            ,   851000, ItemType.Grant                                  , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Demon's Great Hammer"                             ,   852000, ItemType.DemonsGreatHammer                      , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Dragon Tooth"                                     ,   854000, ItemType.DragonTooth                            , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Large Club"                                       ,   855000, ItemType.LargeClub                              , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Smough's Hammer"                                  ,   856000, ItemType.SmoughsHammer                          , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Caestus"                                          ,   901000, ItemType.Caestus                                , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Claw"                                             ,   902000, ItemType.Claw                                   , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Dragon Bone Fist"                                 ,   903000, ItemType.DragonBoneFist                         , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Dark Hand"                                        ,   904000, ItemType.DarkHand                               , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.None               ),
            new Item("Spear"                                            ,  1000000, ItemType.Spear                                  , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Winged Spear"                                     ,  1001000, ItemType.WingedSpear                            , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Partizan"                                         ,  1002000, ItemType.Partizan                               , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Demon's Spear"                                    ,  1003000, ItemType.DemonsSpear                            , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Channeler's Trident"                              ,  1004000, ItemType.ChannelersTrident                      , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Silver Knight Spear"                              ,  1006000, ItemType.SilverKnightSpear                      , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Pike"                                             ,  1050000, ItemType.Pike                                   , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Dragonslayer Spear"                               ,  1051000, ItemType.DragonslayerSpear                      , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Moonlight Butterfly Horn"                         ,  1052000, ItemType.MoonlightButterflyHorn                 , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Halberd"                                          ,  1100000, ItemType.Halberd                                , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Giant's Halberd"                                  ,  1101000, ItemType.GiantsHalberd                          , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Titanite Catch Pole"                              ,  1102000, ItemType.TitaniteCatchPole                      , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Gargoyle's Halberd"                               ,  1103000, ItemType.GargoylesHalberd                       , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Black Knight Halberd"                             ,  1105000, ItemType.BlackKnightHalberd                     , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Lucerne"                                          ,  1106000, ItemType.Lucerne                                , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Scythe"                                           ,  1107000, ItemType.Scythe                                 , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Great Scythe"                                     ,  1150000, ItemType.GreatScythe                            , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Lifehunt Scythe"                                  ,  1151000, ItemType.LifehuntScythe                         , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Whip"                                             ,  1600000, ItemType.Whip                                   , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Notched Whip"                                     ,  1601000, ItemType.NotchedWhip                            , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Gold Tracer"                                      ,  9010000, ItemType.GoldTracer                             , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Dark Silver Tracer"                               ,  9011000, ItemType.DarkSilverTracer                       , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Abyss Greatsword"                                 ,  9012000, ItemType.AbyssGreatsword                        , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Stone Greataxe"                                   ,  9015000, ItemType.StoneGreataxe                          , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),
            new Item("Four-pronged Plow"                                ,  9016000, ItemType.FourprongedPlow                        , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Guardian Tail"                                    ,  9019000, ItemType.GuardianTail                           , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Infusable          ),
            new Item("Obsidian Greatsword"                              ,  9020000, ItemType.ObsidianGreatsword                     , ItemCategory.MeleeWeapons   ,   1, ItemUpgrade.Unique             ),

            new Item("Short Bow"                                        ,  1200000, ItemType.ShortBow                               , ItemCategory.RangedWeapons  ,   1, ItemUpgrade.Infusable          ),
            new Item("Longbow"                                          ,  1201000, ItemType.Longbow                                , ItemCategory.RangedWeapons  ,   1, ItemUpgrade.Infusable          ),
            new Item("Black Bow of Pharis"                              ,  1202000, ItemType.BlackBowofPharis                       , ItemCategory.RangedWeapons  ,   1, ItemUpgrade.Infusable          ),
            new Item("Dragonslayer Greatbow"                            ,  1203000, ItemType.DragonslayerGreatbow                   , ItemCategory.RangedWeapons  ,   1, ItemUpgrade.Unique             ),
            new Item("Composite Bow"                                    ,  1204000, ItemType.CompositeBow                           , ItemCategory.RangedWeapons  ,   1, ItemUpgrade.Infusable          ),
            new Item("Darkmoon Bow"                                     ,  1205000, ItemType.DarkmoonBow                            , ItemCategory.RangedWeapons  ,   1, ItemUpgrade.Unique             ),
            new Item("Light Crossbow"                                   ,  1250000, ItemType.LightCrossbow                          , ItemCategory.RangedWeapons  ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Heavy Crossbow"                                   ,  1251000, ItemType.HeavyCrossbow                          , ItemCategory.RangedWeapons  ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Avelyn"                                           ,  1252000, ItemType.Avelyn                                 , ItemCategory.RangedWeapons  ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Sniper Crossbow"                                  ,  1253000, ItemType.SniperCrossbow                         , ItemCategory.RangedWeapons  ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Gough's Greatbow"                                 ,  9021000, ItemType.GoughsGreatbow                         , ItemCategory.RangedWeapons  ,   1, ItemUpgrade.Unique             ),

            new Item("Standard Arrow"                                   ,  2000000, ItemType.StandardArrow                          , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Large Arrow"                                      ,  2001000, ItemType.LargeArrow                             , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Feather Arrow"                                    ,  2002000, ItemType.FeatherArrow                           , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Fire Arrow"                                       ,  2003000, ItemType.FireArrow                              , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Poison Arrow"                                     ,  2004000, ItemType.PoisonArrow                            , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Moonlight Arrow"                                  ,  2005000, ItemType.MoonlightArrow                         , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Wooden Arrow"                                     ,  2006000, ItemType.WoodenArrow                            , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Dragonslayer Arrow"                               ,  2007000, ItemType.DragonslayerArrow                      , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Gough's Great Arrow"                              ,  2008000, ItemType.GoughsGreatArrow                       , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Standard Bolt"                                    ,  2100000, ItemType.StandardBolt                           , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Heavy Bolt"                                       ,  2101000, ItemType.HeavyBolt                              , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Sniper Bolt"                                      ,  2102000, ItemType.SniperBolt                             , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Wood Bolt"                                        ,  2103000, ItemType.WoodBolt                               , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),
            new Item("Lightning Bolt"                                   ,  2104000, ItemType.LightningBolt                          , ItemCategory.Ammo           , 999, ItemUpgrade.None               ),

            new Item("Havel's Ring"                                     ,      100, ItemType.HavelsRing                            , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Red Tearstone Ring"                               ,      101, ItemType.RedTearstoneRing                      , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Darkmoon Blade Covenant Ring"                     ,      102, ItemType.DarkmoonBladeCovenantRing             , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Cat Covenant Ring"                                ,      103, ItemType.CatCovenantRing                       , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Cloranthy Ring"                                   ,      104, ItemType.CloranthyRing                         , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Flame Stoneplate Ring"                            ,      105, ItemType.FlameStoneplateRing                   , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Thunder Stoneplate Ring"                          ,      106, ItemType.ThunderStoneplateRing                 , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Spell Stoneplate Ring"                            ,      107, ItemType.SpellStoneplateRing                   , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Speckled Stoneplate Ring"                         ,      108, ItemType.SpeckledStoneplateRing                , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Bloodbite Ring"                                   ,      109, ItemType.BloodbiteRing                         , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Poisonbite Ring"                                  ,      110, ItemType.PoisonbiteRing                        , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Tiny Being's Ring"                                ,      111, ItemType.TinyBeingsRing                        , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Cursebite Ring"                                   ,      113, ItemType.CursebiteRing                         , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("White Seance Ring"                                ,      114, ItemType.WhiteSeanceRing                       , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Bellowing Dragoncrest Ring"                       ,      115, ItemType.BellowingDragoncrestRing              , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Dusk Crown Ring"                                  ,      116, ItemType.DuskCrownRing                         , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Hornet Ring"                                      ,      117, ItemType.HornetRing                            , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Hawk Ring"                                        ,      119, ItemType.HawkRing                              , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Ring of Steel Protection"                         ,      120, ItemType.RingofSteelProtection                 , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Covetous Gold Serpent Ring"                       ,      121, ItemType.CovetousGoldSerpentRing               , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Covetous Silver Serpent Ring"                     ,      122, ItemType.CovetousSilverSerpentRing             , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Slumbering Dragoncrest Ring"                      ,      123, ItemType.SlumberingDragoncrestRing             , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Ring of Fog"                                      ,      124, ItemType.RingofFog                             , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Rusted Iron Ring"                                 ,      125, ItemType.RustedIronRing                        , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Ring of Sacrifice"                                ,      126, ItemType.RingofSacrifice                       , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Rare Ring of Sacrifice"                           ,      127, ItemType.RareRingofSacrifice                   , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Dark Wood Grain Ring"                             ,      128, ItemType.DarkWoodGrainRing                     , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Ring of the Sun Princess"                         ,      130, ItemType.RingoftheSunPrincess                  , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Old Witch's Ring"                                 ,      137, ItemType.OldWitchsRing                         , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Covenant of Artorias"                             ,      138, ItemType.CovenantofArtorias                    , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Orange Charred Ring"                              ,      139, ItemType.OrangeCharredRing                     , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Lingering Dragoncrest Ring"                       ,      141, ItemType.LingeringDragoncrestRing              , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Ring of the Evil Eye"                             ,      142, ItemType.RingoftheEvilEye                      , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Ring of Favor and Protection"                     ,      143, ItemType.RingofFavorandProtection              , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Leo Ring"                                         ,      144, ItemType.LeoRing                               , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("East Wood Grain Ring"                             ,      145, ItemType.EastWoodGrainRing                     , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Wolf Ring"                                        ,      146, ItemType.WolfRing                              , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Blue Tearstone Ring"                              ,      147, ItemType.BlueTearstoneRing                     , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Ring of the Sun's Firstborn"                      ,      148, ItemType.RingoftheSunsFirstborn                , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Darkmoon Seance Ring"                             ,      149, ItemType.DarkmoonSeanceRing                    , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),
            new Item("Calamity Ring"                                    ,      150, ItemType.CalamityRing                          , ItemCategory.Rings           ,   1, ItemUpgrade.None               ),

            new Item("Skull Lantern"                                    ,  1396000, ItemType.SkullLantern                          , ItemCategory.Shields         ,   1, ItemUpgrade.None               ),
            new Item("East-West Shield"                                 ,  1400000, ItemType.EastWestShield                        , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Wooden Shield"                                    ,  1401000, ItemType.WoodenShield                          , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Large Leather Shield"                             ,  1402000, ItemType.LargeLeatherShield                    , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Small Leather Shield"                             ,  1403000, ItemType.SmallLeatherShield                    , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Target Shield"                                    ,  1404000, ItemType.TargetShield                          , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Buckler"                                          ,  1405000, ItemType.Buckler                               , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Cracked Round Shield"                             ,  1406000, ItemType.CrackedRoundShield                    , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Leather Shield"                                   ,  1408000, ItemType.LeatherShield                         , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Plank Shield"                                     ,  1409000, ItemType.PlankShield                           , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Caduceus Round Shield"                            ,  1410000, ItemType.CaduceusRoundShield                   , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Crystal Ring Shield"                              ,  1411000, ItemType.CrystalRingShield                     , ItemCategory.Shields         ,   1, ItemUpgrade.Unique             ),
            new Item("Heater Shield"                                    ,  1450000, ItemType.HeaterShield                          , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Knight Shield"                                    ,  1451000, ItemType.KnightShield                          , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Tower Kite Shield"                                ,  1452000, ItemType.TowerKiteShield                       , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Grass Crest Shield"                               ,  1453000, ItemType.GrassCrestShield                      , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Hollow Soldier Shield"                            ,  1454000, ItemType.HollowSoldierShield                   , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Balder Shield"                                    ,  1455000, ItemType.BalderShield                          , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Crest Shield"                                     ,  1456000, ItemType.CrestShield                           , ItemCategory.Shields         ,   1, ItemUpgrade.Unique             ),
            new Item("Dragon Crest Shield"                              ,  1457000, ItemType.DragonCrestShield                     , ItemCategory.Shields         ,   1, ItemUpgrade.Unique             ),
            new Item("Warrior's Round Shield"                           ,  1460000, ItemType.WarriorsRoundShield                   , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Iron Round Shield"                                ,  1461000, ItemType.IronRoundShield                       , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Spider Shield"                                    ,  1462000, ItemType.SpiderShield                          , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Spiked Shield"                                    ,  1470000, ItemType.SpikedShield                          , ItemCategory.Shields         ,   1, ItemUpgrade.Infusable          ),
            new Item("Crystal Shield"                                   ,  1471000, ItemType.CrystalShield                         , ItemCategory.Shields         ,   1, ItemUpgrade.None               ),
            new Item("Sunlight Shield"                                  ,  1472000, ItemType.SunlightShield                        , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Silver Knight Shield"                             ,  1473000, ItemType.SilverKnightShield                    , ItemCategory.Shields         ,   1, ItemUpgrade.Unique             ),
            new Item("Black Knight Shield"                              ,  1474000, ItemType.BlackKnightShield                     , ItemCategory.Shields         ,   1, ItemUpgrade.Unique             ),
            new Item("Pierce Shield"                                    ,  1475000, ItemType.PierceShield                          , ItemCategory.Shields         ,   1, ItemUpgrade.Infusable          ),
            new Item("Red and White Round Shield"                       ,  1476000, ItemType.RedandWhiteRoundShield                , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Caduceus Kite Shield"                             ,  1477000, ItemType.CaduceusKiteShield                    , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Gargoyle's Shield"                                ,  1478000, ItemType.GargoylesShield                       , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Eagle Shield"                                     ,  1500000, ItemType.EagleShield                           , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Tower Shield"                                     ,  1501000, ItemType.TowerShield                           , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Giant Shield"                                     ,  1502000, ItemType.GiantShield                           , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Stone Greatshield"                                ,  1503000, ItemType.StoneGreatshield                      , ItemCategory.Shields         ,   1, ItemUpgrade.Unique             ),
            new Item("Havel's Greatshield"                              ,  1505000, ItemType.HavelsGreatshield                     , ItemCategory.Shields         ,   1, ItemUpgrade.Unique             ),
            new Item("Bonewheel Shield"                                 ,  1506000, ItemType.BonewheelShield                       , ItemCategory.Shields         ,   1, ItemUpgrade.Infusable          ),
            new Item("Greatshield of Artorias"                          ,  1507000, ItemType.GreatshieldofArtorias                 , ItemCategory.Shields         ,   1, ItemUpgrade.Unique             ),
            new Item("Effigy Shield"                                    ,  9000000, ItemType.EffigyShield                          , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Sanctus"                                          ,  9001000, ItemType.Sanctus                               , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Bloodshield"                                      ,  9002000, ItemType.Bloodshield                           , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Black Iron Greatshield"                           ,  9003000, ItemType.BlackIronGreatshield                  , ItemCategory.Shields         ,   1, ItemUpgrade.InfusableRestricted),
            new Item("Cleansing Greatshield"                            ,  9014000, ItemType.CleansingGreatshield                  , ItemCategory.Shields         ,   1, ItemUpgrade.Unique             ),

            new Item("Sorcery: Soul Arrow"                              ,     3000, ItemType.SorcerySoulArrow                      , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Great Soul Arrow"                        ,     3010, ItemType.SorceryGreatSoulArrow                 , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Heavy Soul Arrow"                        ,     3020, ItemType.SorceryHeavySoulArrow                 , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Great Heavy Soul Arrow"                  ,     3030, ItemType.SorceryGreatHeavySoulArrow            , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Homing Soulmass"                         ,     3040, ItemType.SorceryHomingSoulmass                 , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Homing Crystal Soulmass"                 ,     3050, ItemType.SorceryHomingCrystalSoulmass          , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Soul Spear"                              ,     3060, ItemType.SorcerySoulSpear                      , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Crystal Soul Spear"                      ,     3070, ItemType.SorceryCrystalSoulSpear               , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Magic Weapon"                            ,     3100, ItemType.SorceryMagicWeapon                    , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Great Magic Weapon"                      ,     3110, ItemType.SorceryGreatMagicWeapon               , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Crystal Magic Weapon"                    ,     3120, ItemType.SorceryCrystalMagicWeapon             , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Magic Shield"                            ,     3300, ItemType.SorceryMagicShield                    , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Strong Magic Shield"                     ,     3310, ItemType.SorceryStrongMagicShield              , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Hidden Weapon"                           ,     3400, ItemType.SorceryHiddenWeapon                   , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Hidden Body"                             ,     3410, ItemType.SorceryHiddenBody                     , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Cast Light"                              ,     3500, ItemType.SorceryCastLight                      , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Hush"                                    ,     3510, ItemType.SorceryHush                           , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Aural Decoy"                             ,     3520, ItemType.SorceryAuralDecoy                     , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Repair"                                  ,     3530, ItemType.SorceryRepair                         , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Fall Control"                            ,     3540, ItemType.SorceryFallControl                    , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Chameleon"                               ,     3550, ItemType.SorceryChameleon                      , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Resist Curse"                            ,     3600, ItemType.SorceryResistCurse                    , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Remedy"                                  ,     3610, ItemType.SorceryRemedy                         , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: White Dragon Breath"                     ,     3700, ItemType.SorceryWhiteDragonBreath              , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Dark Orb"                                ,     3710, ItemType.SorceryDarkOrb                        , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Dark Bead"                               ,     3720, ItemType.SorceryDarkBead                       , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Dark Fog"                                ,     3730, ItemType.SorceryDarkFog                        , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Sorcery: Pursuers"                                ,     3740, ItemType.SorceryPursuers                       , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Fireball"                              ,     4000, ItemType.PyromancyFireball                     , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Fire Orb"                              ,     4010, ItemType.PyromancyFireOrb                      , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Great Fireball"                        ,     4020, ItemType.PyromancyGreatFireball                , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Firestorm"                             ,     4030, ItemType.PyromancyFirestorm                    , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Fire Tempest"                          ,     4040, ItemType.PyromancyFireTempest                  , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Fire Surge"                            ,     4050, ItemType.PyromancyFireSurge                    , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Fire Whip"                             ,     4060, ItemType.PyromancyFireWhip                     , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Combustion"                            ,     4100, ItemType.PyromancyCombustion                   , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Great Combustion"                      ,     4110, ItemType.PyromancyGreatCombustion              , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Poison Mist"                           ,     4200, ItemType.PyromancyPoisonMist                   , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Toxic Mist"                            ,     4210, ItemType.PyromancyToxicMist                    , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Acid Surge"                            ,     4220, ItemType.PyromancyAcidSurge                    , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Iron Flesh"                            ,     4300, ItemType.PyromancyIronFlesh                    , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Flash Sweat"                           ,     4310, ItemType.PyromancyFlashSweat                   , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Undead Rapport"                        ,     4360, ItemType.PyromancyUndeadRapport                , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Power Within"                          ,     4400, ItemType.PyromancyPowerWithin                  , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Great Chaos Fireball"                  ,     4500, ItemType.PyromancyGreatChaosFireball           , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Chaos Storm"                           ,     4510, ItemType.PyromancyChaosStorm                   , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Chaos Fire Whip"                       ,     4520, ItemType.PyromancyChaosFireWhip                , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Pyromancy: Black Flame"                           ,     4530, ItemType.PyromancyBlackFlame                   , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Heal"                                    ,     5000, ItemType.MiracleHeal                           , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Great Heal"                              ,     5010, ItemType.MiracleGreatHeal                      , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Great Heal Excerpt"                      ,     5020, ItemType.MiracleGreatHealExcerpt               , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Soothing Sunlight"                       ,     5030, ItemType.MiracleSoothingSunlight               , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Replenishment"                           ,     5040, ItemType.MiracleReplenishment                  , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Bountiful Sunlight"                      ,     5050, ItemType.MiracleBountifulSunlight              , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Gravelord Sword Dance"                   ,     5100, ItemType.MiracleGravelordSwordDance            , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Gravelord Greatsword Dance"              ,     5110, ItemType.MiracleGravelordGreatswordDance       , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Homeward"                                ,     5210, ItemType.MiracleHomeward                       , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Force"                                   ,     5300, ItemType.MiracleForce                          , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Wrath of the Gods"                       ,     5310, ItemType.MiracleWrathoftheGods                 , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Emit Force"                              ,     5320, ItemType.MiracleEmitForce                      , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Seek Guidance"                           ,     5400, ItemType.MiracleSeekGuidance                   , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Lightning Spear"                         ,     5500, ItemType.MiracleLightningSpear                 , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Great Lightning Spear"                   ,     5510, ItemType.MiracleGreatLightningSpear            , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Sunlight Spear"                          ,     5520, ItemType.MiracleSunlightSpear                  , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Magic Barrier"                           ,     5600, ItemType.MiracleMagicBarrier                   , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Great Magic Barrier"                     ,     5610, ItemType.MiracleGreatMagicBarrier              , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Karmic Justice"                          ,     5700, ItemType.MiracleKarmicJustice                  , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Tranquil Walk of Peace"                  ,     5800, ItemType.MiracleTranquilWalkofPeace            , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Vow of Silence"                          ,     5810, ItemType.MiracleVowofSilence                   , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Sunlight Blade"                          ,     5900, ItemType.MiracleSunlightBlade                  , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),
            new Item("Miracle: Darkmoon Blade"                          ,     5910, ItemType.MiracleDarkmoonBlade                  , ItemCategory.Spells          ,  99, ItemUpgrade.None               ),

            new Item("Sorcerer's Catalyst"                              ,  1300000, ItemType.SorcerersCatalyst                     , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Beatrice's Catalyst"                              ,  1301000, ItemType.BeatricesCatalyst                     , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Tin Banishment Catalyst"                          ,  1302000, ItemType.TinBanishmentCatalyst                 , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Logan's Catalyst"                                 ,  1303000, ItemType.LogansCatalyst                        , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Tin Darkmoon Catalyst"                            ,  1304000, ItemType.TinDarkmoonCatalyst                   , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Oolacile Ivory Catalyst"                          ,  1305000, ItemType.OolacileIvoryCatalyst                 , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Tin Crystallization Catalyst"                     ,  1306000, ItemType.TinCrystallizationCatalyst            , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Demon's Catalyst"                                 ,  1307000, ItemType.DemonsCatalyst                        , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Izalith Catalyst"                                 ,  1308000, ItemType.IzalithCatalyst                       , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Pyromancy Flame"                                  ,  1330000, ItemType.PyromancyFlame                        , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Pyromancy Flame (Ascended)"                       ,  1332000, ItemType.PyromancyFlameAscended                , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Talisman"                                         ,  1360000, ItemType.Talisman                              , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Canvas Talisman"                                  ,  1361000, ItemType.CanvasTalisman                        , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Thorolund Talisman"                               ,  1362000, ItemType.ThorolundTalisman                     , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Ivory Talisman"                                   ,  1363000, ItemType.IvoryTalisman                         , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Sunlight Talisman"                                ,  1365000, ItemType.SunlightTalisman                      , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Darkmoon Talisman"                                ,  1366000, ItemType.DarkmoonTalisman                      , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Velka's Talisman"                                 ,  1367000, ItemType.VelkasTalisman                        , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Manus Catalyst"                                   ,  9017000, ItemType.ManusCatalyst                         , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),
            new Item("Oolacile Catalyst"                                ,  9018000, ItemType.OolacileCatalyst                      , ItemCategory.SpellTools      ,   1, ItemUpgrade.None               ),

            new Item("Large Ember"                                      ,     800 , ItemType.LargeEmber                            , ItemCategory.UpgradeMaterials,   1, ItemUpgrade.None               ),
            new Item("Very Large Ember"                                 ,     801 , ItemType.VeryLargeEmber                        , ItemCategory.UpgradeMaterials,   1, ItemUpgrade.None               ),
            new Item("Crystal Ember"                                    ,     802 , ItemType.CrystalEmber                          , ItemCategory.UpgradeMaterials,   1, ItemUpgrade.None               ),
            new Item("Large Magic Ember"                                ,     806 , ItemType.LargeMagicEmber                       , ItemCategory.UpgradeMaterials,   1, ItemUpgrade.None               ),
            new Item("Enchanted Ember"                                  ,     807 , ItemType.EnchantedEmber                        , ItemCategory.UpgradeMaterials,   1, ItemUpgrade.None               ),
            new Item("Divine Ember"                                     ,     808 , ItemType.DivineEmber                           , ItemCategory.UpgradeMaterials,   1, ItemUpgrade.None               ),
            new Item("Large Divine Ember"                               ,     809 , ItemType.LargeDivineEmber                      , ItemCategory.UpgradeMaterials,   1, ItemUpgrade.None               ),
            new Item("Dark Ember"                                       ,     810 , ItemType.DarkEmber                             , ItemCategory.UpgradeMaterials,   1, ItemUpgrade.None               ),
            new Item("Large Flame Ember"                                ,     812 , ItemType.LargeFlameEmber                       , ItemCategory.UpgradeMaterials,   1, ItemUpgrade.None               ),
            new Item("Chaos Flame Ember"                                ,     813 , ItemType.ChaosFlameEmber                       , ItemCategory.UpgradeMaterials,   1, ItemUpgrade.None               ),
            new Item("Titanite Shard"                                   ,     1000, ItemType.TitaniteShard                         , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("Large Titanite Shard"                             ,     1010, ItemType.LargeTitaniteShard                    , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("Green Titanite Shard"                             ,     1020, ItemType.GreenTitaniteShard                    , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("Titanite Chunk"                                   ,     1030, ItemType.TitaniteChunk                         , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("Blue Titanite Chunk"                              ,     1040, ItemType.BlueTitaniteChunk                     , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("White Titanite Chunk"                             ,     1050, ItemType.WhiteTitaniteChunk                    , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("Red Titanite Chunk"                               ,     1060, ItemType.RedTitaniteChunk                      , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("Titanite Slab"                                    ,     1070, ItemType.TitaniteSlab                          , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("Blue Titanite Slab"                               ,     1080, ItemType.BlueTitaniteSlab                      , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("White Titanite Slab"                              ,     1090, ItemType.WhiteTitaniteSlab                     , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("Red Titanite Slab"                                ,     1100, ItemType.RedTitaniteSlab                       , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("Dragon Scale"                                     ,     1110, ItemType.DragonScale                           , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("Demon Titanite"                                   ,     1120, ItemType.DemonTitanite                         , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),
            new Item("Twinkling Titanite"                               ,     1130, ItemType.TwinklingTitanite                     , ItemCategory.UpgradeMaterials,  99, ItemUpgrade.None               ),

            new Item("White Sign Soapstone"                             ,      100, ItemType.WhiteSignSoapstone                    , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Red Sign Soapstone"                               ,      101, ItemType.RedSignSoapstone                      , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Red Eye Orb"                                      ,      102, ItemType.RedEyeOrb                             , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Black Separation Crystal"                         ,      103, ItemType.BlackSeparationCrystal                , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Orange Guidance Soapstone"                        ,      106, ItemType.OrangeGuidanceSoapstone               , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Book of the Guilty"                               ,      108, ItemType.BookoftheGuilty                       , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Servant Roster"                                   ,      112, ItemType.ServantRoster                         , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Blue Eye Orb"                                     ,      113, ItemType.BlueEyeOrb                            , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Dragon Eye"                                       ,      114, ItemType.DragonEye                             , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Black Eye Orb"                                    ,      115, ItemType.BlackEyeOrb                           , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Darksign"                                         ,      117, ItemType.Darksign                              , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Purple Coward's Crystal"                          ,      118, ItemType.PurpleCowardsCrystal                  , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Silver Pendant"                                   ,      220, ItemType.SilverPendant                         , ItemCategory.UsableItems     ,  99, ItemUpgrade.None               ),
            new Item("Binoculars"                                       ,      371, ItemType.Binoculars                            , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Dragon Head Stone"                                ,      377, ItemType.DragonHeadStone                       , ItemCategory.UsableItems     ,  99, ItemUpgrade.None               ),
            new Item("Dragon Torso Stone"                               ,      378, ItemType.DragonTorsoStone                      , ItemCategory.UsableItems     ,  99, ItemUpgrade.None               ),
            new Item("Dried Finger"                                     ,      385, ItemType.DriedFinger                           , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Hello Carving"                                    ,      510, ItemType.HelloCarving                          , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Thank you Carving"                                ,      511, ItemType.ThankyouCarving                       , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Very good! Carving"                               ,      512, ItemType.VerygoodCarving                       , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("I'm sorry Carving"                                ,      513, ItemType.ImsorryCarving                        , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               ),
            new Item("Help me! Carving"                                 ,      514, ItemType.HelpmeCarving                         , ItemCategory.UsableItems     ,   1, ItemUpgrade.None               )
        };
        #endregion
        private void ItemToSplit()
        {
            var ItemsToSplit = dataDs1.GetItemsToSplit();
            while (dataDs1.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusDs1 && !_PracticeMode && !_ShowSettings)
                {
                    try
                    {
                        if (ItemsToSplit != dataDs1.GetItemsToSplit()) ItemsToSplit = dataDs1.GetItemsToSplit();
                        foreach (var item in ItemsToSplit)
                        {
                            Item aux = allItems.Find(i => i.Id == item.Id);
                            if (!item.IsSplited && inventory.Exists(i => i.ItemType == aux.ItemType))
                            {
                                if (item.Mode == "Loading game after")
                                {
                                    if (!listPendingItem.Contains(item))
                                    {
                                        listPendingItem.Add(item);
                                    }
                                }
                                else
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: DS1 ITEM -> {item.Title}");
                                    item.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DebugLog.LogMessage($"Error in ItemToSplit: {ex.Message}");
                    }
                }
            }
        }
        #endregion
    }
}
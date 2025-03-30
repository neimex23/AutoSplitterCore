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

using SoulMemory;
using SoulMemory.Sekiro;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSplitterCore
{
    public class SekiroSplitter
    {
        private DTSekiro dataSekiro;
        private static Sekiro sekiro = new Sekiro();
        private DefinitionsSekiro defS = new DefinitionsSekiro();
        private ISplitterControl splitterControl = SplitterControl.GetControl();

        public bool _StatusSekiro = false;
        public bool _PracticeMode = false;
        public bool _ShowSettings = false;

        #region SingletonFactory
        private static SekiroSplitter _intance = new SekiroSplitter();

        private SekiroSplitter() { }

        public static SekiroSplitter GetIntance() { return _intance; }
        #endregion

        #region Control Management
        public DTSekiro GetDataSekiro() => dataSekiro;

        public void SetDataSekiro(DTSekiro data) => dataSekiro = data;

        public bool GetSekiroStatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            try
            {
                _StatusSekiro = sekiro.TryRefresh();
            }
            catch (Exception) { _StatusSekiro = false; }
            ;
            return _StatusSekiro;
        }

        public void SetStatusSplitting(bool status)
        {
            dataSekiro.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); }
        }

        public void ResetSplited()
        {
            listPendingB.Clear();
            listPendingI.Clear();
            listPendingP.Clear();
            listPendingCf.Clear();
            listPendingMb.Clear();

            if (dataSekiro.GetBossToSplit().Count > 0)
            {
                foreach (var b in dataSekiro.GetBossToSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataSekiro.GetidolsTosplit().Count > 0)
            {
                foreach (var b in dataSekiro.GetidolsTosplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataSekiro.GetPositionsToSplit().Count > 0)
            {
                foreach (var p in dataSekiro.GetPositionsToSplit())
                {
                    p.IsSplited = false;
                }
            }

            if (dataSekiro.GetFlagToSplit().Count > 0)
            {
                foreach (var cf in dataSekiro.GetFlagToSplit())
                {
                    cf.IsSplited = false;
                }
            }

            if (dataSekiro.GetMiniBossToSplit().Count > 0)
            {
                foreach (var mb in dataSekiro.GetMiniBossToSplit())
                {
                    mb.IsSplited = false;
                }
            }
            index = 0;
            NotSplited(ref MortalJourneyData);
            PendingMortal.Clear();
        }
        #endregion
        #region Object Management
        public void AddIdol(string idol, string mode)
        {
            DefinitionsSekiro.Idol cIdol = defS.IdolToEnum(idol);
            cIdol.Mode = mode;
            if (!dataSekiro.idolsTosplit.Exists(x => x.Id == cIdol.Id))
            {
                dataSekiro.idolsTosplit.Add(cIdol);
            }
        }

        public List<String> GetAllIdols() => defS.GetAllIdols();

        public void AddBoss(string boss, string mode)
        {
            DefinitionsSekiro.BossS cBoss = defS.BossToEnum(boss);
            cBoss.Mode = mode;
            dataSekiro.bossToSplit.Add(cBoss);
        }

        public void AddPosition(float X, float Y, float Z, string mode, string title)
        {
            var position = new DefinitionsSekiro.PositionS();
            position.SetVector(new Vector3f(X, Y, Z));
            position.Mode = mode;
            position.Title = title;
            dataSekiro.positionsToSplit.Add(position);
        }

        public void AddCustomFlag(uint id, string mode, string title)
        {
            DefinitionsSekiro.CfSk cFlag = new DefinitionsSekiro.CfSk()
            {
                Id = id,
                Mode = mode,
                Title = title
            };
            dataSekiro.flagToSplit.Add(cFlag);
        }

        public void AddAttribute(string attribute, string mode, uint value)
        {
            DefinitionsSekiro.LevelS cLvl = new DefinitionsSekiro.LevelS()
            {
                Attribute = defS.StringToEnumAttribute(attribute),
                Mode = mode,
                Value = value
            };
            dataSekiro.lvlToSplit.Add(cLvl);
        }

        public void RemoveAttribute(int position)
        {
            listPendingLevel.RemoveAll(ilvl => ilvl.Attribute == dataSekiro.lvlToSplit[position].Attribute && ilvl.Value == dataSekiro.lvlToSplit[position].Value);
            dataSekiro.lvlToSplit.RemoveAt(position);
        }


        public void AddMiniBoss(string miniboss, string mode)
        {
            DefinitionsSekiro.MiniBossS mBoss = new DefinitionsSekiro.MiniBossS();
            switch (miniboss)
            {
                case "Leader Shigenori Yamauchi":
                    mBoss.Id = 51120150;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "General Naomori Kawarada":
                    mBoss.Id = 714;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Ogre - Ashina Outskirts":
                    mBoss.vector = new Vector3f((float)124.60, (float)-35.50, (float)140.20);
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.Position;
                    break;
                case "General Tenzen Yamauchi":
                    mBoss.vector = new Vector3f((float)163.30, (float)-72.40, (float)220.50);
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.Position;
                    break;
                case "Headless Ako":
                    mBoss.Id = 11100330;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Blazing Bull":
                    mBoss.Id = defS.IdolToEnum("Ashina Castle").Id;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Shigekichi of the Red Guard":
                    mBoss.Id = defS.IdolToEnum("Flames of Hatred").Id;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Shinobi Hunter Enshin of Misen":
                    mBoss.Id = 50006120;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Juzou the Drunkard":
                    mBoss.Id = defS.IdolToEnum("Hirata Audience Chamber").Id;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Lone Shadow Masanaga the Spear-Bearer":
                    mBoss.vector = new Vector3f((float)74.50, (float)-37.65, (float)385.25);
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.Position;
                    break;
                case "Seven Ashina Spears - Shume Masaji Oniwa":
                    mBoss.Id = 61120711;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Juzou the Drunkard 2":
                    mBoss.Id = 11000301;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "General Kuranosuke Matsumoto":
                    mBoss.Id = 11110410;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Seven Achina Spears – Shikibu Toshikatsu Yamauchi":
                    mBoss.Id = 11120530;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Lone Shadow Longswordsman":
                    mBoss.vector = new Vector3f((float)-323.20, (float)-48.30, (float)344.35);
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.Position;
                    break;
                case "Headless Ungo":
                    mBoss.vector = new Vector3f((float)-27.80, (float)0.25, (float)252.0);
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.Position;
                    break;
                case "Ashina Elite – Jinsuke Saze":
                    mBoss.Id = 11110504;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Ogre - Ashina Castle":
                    mBoss.vector = new Vector3f((float)-111.20, (float)25.00, (float)237.90);
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.Position;
                    break;
                case "Lone Shadow Vilehand": //Check
                    mBoss.Id = 51110330;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Ashina Elite - Ujinari Mizuo":
                    mBoss.Id = 51110650;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Shichimen Warrior - Abandoned Dungeon":
                    mBoss.Id = defS.IdolToEnum("Bottomless Hole").Id;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Armored Warrior":
                    mBoss.Id = 12000400;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Long-arm Centipede Sen’un":
                    mBoss.Id = 12000279;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Headless Gokan":
                    mBoss.Id = 11700500;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Long-arm Centipede Giraffe":
                    mBoss.Id = 11705202;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Snake Eyes Shirahagi":
                    mBoss.Id = defS.IdolToEnum("Hidden Forest").Id;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Shichimen Warrior - Ashina Depths":
                    mBoss.Id = 11700520;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Headless Gacchin":
                    mBoss.Id = 11500680;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Tokujiro the Glutton":
                    mBoss.Id = 11500490;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Mist Noble":
                    mBoss.Id = 11505200;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "O'rin of the Water":
                    mBoss.Id = 11500690;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Sakura Bull of the Palace":
                    mBoss.Id = 12500570;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Leader Okami":
                    mBoss.Id = 12500406;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Headless Yashariku":
                    mBoss.Id = defS.IdolToEnum("Flower Viewing Stage").Id;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Shichimen Warrior - Fountainhead Palace":
                    mBoss.Id = 12500580;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                default: return;
            }
            mBoss.Title = miniboss;
            mBoss.Mode = mode;
            dataSekiro.miniBossToSplit.Add(mBoss);
        }

        public string GetMiniBossDescription(string mBoss) => defS.GetMiniBossDescription(mBoss);

        public void RemoveMiniBoss(int position)
        {
            listPendingMb.RemoveAll(imb => imb.Title == dataSekiro.miniBossToSplit[position].Title);
            dataSekiro.miniBossToSplit.RemoveAt(position);
        }

        public void RemoveCustomFlag(int position)
        {
            listPendingCf.RemoveAll(iflag => iflag.Id == dataSekiro.flagToSplit[position].Id);
            dataSekiro.flagToSplit.RemoveAt(position);
        }

        public void RemoveBoss(int position)
        {
            listPendingB.RemoveAll(iboss => iboss.Id == dataSekiro.bossToSplit[position].Id);
            dataSekiro.bossToSplit.RemoveAt(position);

        }

        public void RemovePosition(int position)
        {
            listPendingP.RemoveAll(iposition => iposition.vector == dataSekiro.positionsToSplit[position].vector);
            dataSekiro.positionsToSplit.RemoveAt(position);
        }

        public void RemoveIdol(string fidol)
        {
            DefinitionsSekiro.Idol cIdol = defS.IdolToEnum(fidol);
            listPendingI.RemoveAll(idol => idol.Id == cIdol.Id);
            dataSekiro.idolsTosplit.RemoveAll(idol => idol.Id == cIdol.Id);

        }

        public string FindIdol(string fidol)
        {
            DefinitionsSekiro.Idol cIdol = defS.IdolToEnum(fidol);
            var idolReturn = dataSekiro.idolsTosplit.Find(idol => idol.Id == cIdol.Id);
            if (idolReturn == null)
            {
                return "None";
            }
            else { return idolReturn.Mode; }
        }

        public void SetPositionMargin(int select)
        {
            dataSekiro.positionMargin = select;
        }

        public void ClearData()
        {
            listPendingB.Clear();
            listPendingI.Clear();
            listPendingP.Clear();
            listPendingCf.Clear();
            listPendingMb.Clear();
            listPendingLevel.Clear();
            PendingMortal.Clear();
            dataSekiro.bossToSplit.Clear();
            dataSekiro.idolsTosplit.Clear();
            dataSekiro.positionsToSplit.Clear();
            dataSekiro.flagToSplit.Clear();
            dataSekiro.miniBossToSplit.Clear();
            dataSekiro.lvlToSplit.Clear();
            NotSplited(ref MortalJourneyData);
            dataSekiro.positionMargin = 3;
            dataSekiro.mortalJourneyRun = false;
            index = 0;
        }
        #endregion
        #region Checking
        public Vector3f GetCurrentPosition()
        {
            if (!_StatusSekiro) GetSekiroStatusProcess(0);
            return _StatusSekiro ? sekiro.GetPlayerPosition() : new Vector3f() { X = 0, Y = 0, Z = 0 };
        }

        public int GetTimeInGame()
        {
            if (!_StatusSekiro) GetSekiroStatusProcess(0);
            return _StatusSekiro ? sekiro.GetInGameTimeMilliseconds() : -1;
        }

        public bool CheckFlag(uint id)
        {
            if (!_StatusSekiro) GetSekiroStatusProcess(0);
            return _StatusSekiro && sekiro.ReadEventFlag(id);
        }
        #endregion
        #region Procedure
        public void LoadAutoSplitterProcedure()
        {
            Task.Run(() => RefreshSekiro());
            Task.Run(() => CheckLoad());
            Task.Run(() => BossSplit());
            Task.Run(() => IdolSplit());
            Task.Run(() => PositionSplit());
            Task.Run(() => CustomFlagToSplit());
            Task.Run(() => MortalJourney());
            Task.Run(() => MiniBossSplit());
            Task.Run(() => LevelSplit());
        }
        #endregion
        #region CheckFlag Init()   
        private bool _writeMemory = false;
        private void RefreshSekiro()
        {
            int delay = 2000;
            GetSekiroStatusProcess(delay);
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(10);
                GetSekiroStatusProcess(delay);
                if (!_StatusSekiro)
                {
                    _writeMemory = false;
                    delay = 2000;
                }
                else
                {
                    delay = 5000;
                }

                if (!_writeMemory && _StatusSekiro)
                {
                    if (dataSekiro.ResetIGTNG && sekiro.GetInGameTimeMilliseconds() < 1)
                    {
                        sekiro.WriteInGameTimeMilliseconds(0);
                        _writeMemory = true;
                    }
                }
            }
        }

        List<DefinitionsSekiro.PositionS> listPendingP = new List<DefinitionsSekiro.PositionS>();
        List<DefinitionsSekiro.BossS> listPendingB = new List<DefinitionsSekiro.BossS>();
        List<DefinitionsSekiro.Idol> listPendingI = new List<DefinitionsSekiro.Idol>();
        List<DefinitionsSekiro.CfSk> listPendingCf = new List<DefinitionsSekiro.CfSk>();
        List<DefinitionsSekiro.MiniBossS> listPendingMb = new List<DefinitionsSekiro.MiniBossS>();
        List<DefinitionsSekiro.LevelS> listPendingLevel = new List<DefinitionsSekiro.LevelS>();


        private void CheckLoad()
        {
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(200);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if ((listPendingI.Count > 0 || listPendingB.Count > 0 || listPendingP.Count > 0 || listPendingCf.Count > 0 || listPendingMb.Count > 0))
                    {
                        if (!sekiro.IsPlayerLoaded())
                        {
                            foreach (var idol in listPendingI)
                            {
                                var i = dataSekiro.idolsTosplit.FindIndex(fidol => fidol.Id == idol.Id);
                                splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO *After Login* IDOL -> {dataSekiro.idolsTosplit[i].Title}");
                                dataSekiro.idolsTosplit[i].IsSplited = true;
                            }

                            foreach (var boss in listPendingB)
                            {
                                var b = dataSekiro.bossToSplit.FindIndex(fboss => fboss.Id == boss.Id);
                                splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO *After Login* BOSS -> {dataSekiro.bossToSplit[b].Title}");
                                dataSekiro.bossToSplit[b].IsSplited = true;

                            }

                            foreach (var position in listPendingP)
                            {
                                var p = dataSekiro.positionsToSplit.FindIndex(fposition => fposition.vector == position.vector);
                                splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO *After Login* POSITION -> {dataSekiro.positionsToSplit[p].Title} - {dataSekiro.positionsToSplit[p].vector.ToString()}");
                                dataSekiro.positionsToSplit[p].IsSplited = true;
                            }

                            foreach (var cf in listPendingCf)
                            {
                                var c = dataSekiro.flagToSplit.FindIndex(icf => icf.Id == cf.Id);
                                splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO *After Login* CUSTOM_FLAG -> {dataSekiro.flagToSplit[c].Title} - {dataSekiro.flagToSplit[c].Id}");
                                dataSekiro.flagToSplit[c].IsSplited = true;
                            }

                            foreach (var mb in listPendingMb)
                            {
                                var mbo = dataSekiro.miniBossToSplit.FindIndex(fmb => fmb.Title == mb.Title);
                                splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO *After Login* MINIBOSS -> {dataSekiro.miniBossToSplit[mbo].Title}");
                                dataSekiro.miniBossToSplit[mbo].IsSplited = true;
                            }

                            foreach (var lvl in listPendingLevel)
                            {
                                var l = dataSekiro.lvlToSplit.FindIndex(Ilvl => Ilvl.Attribute == lvl.Attribute && Ilvl.Value == lvl.Value);
                                splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO *After Login* LEVEL -> {dataSekiro.lvlToSplit[l].Attribute} - {dataSekiro.lvlToSplit[l].Value}");
                                dataSekiro.lvlToSplit[l].IsSplited = true;
                            }

                            listPendingB.Clear();
                            listPendingI.Clear();
                            listPendingP.Clear();
                            listPendingCf.Clear();
                            listPendingLevel.Clear();
                        }
                    }
                }
            }
        }

        private void BossSplit()
        {
            var BossToSplit = dataSekiro.GetBossToSplit();
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if (BossToSplit != dataSekiro.GetBossToSplit()) BossToSplit = dataSekiro.GetBossToSplit();
                    foreach (var b in BossToSplit)
                    {
                        if (!b.IsSplited && sekiro.ReadEventFlag(b.Id))
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
                                splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO BOSS -> {b.Title}");
                                b.IsSplited = splitterControl.GetSplitStatus();
                            }
                        }
                    }
                }
            }
        }

        private void MiniBossSplit()
        {
            var MiniBossToSplit = dataSekiro.GetMiniBossToSplit();
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(100);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if (MiniBossToSplit != dataSekiro.GetMiniBossToSplit()) MiniBossToSplit = dataSekiro.GetMiniBossToSplit();
                    foreach (var mb in MiniBossToSplit)
                    {
                        if (!mb.IsSplited)
                        {
                            if (mb.kindSplit == DefinitionsSekiro.KindSplit.ID && sekiro.ReadEventFlag(mb.Id))
                            {
                                if (mb.Mode == "Loading game after")
                                {
                                    if (!listPendingMb.Contains(mb))
                                    {
                                        listPendingMb.Add(mb);
                                    }
                                }
                                else
                                {
                                    splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO MINIBOSS -> {mb.Title}");
                                    mb.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }

                            if (mb.kindSplit == DefinitionsSekiro.KindSplit.Position)
                            {
                                double positionMargin = 2.5;
                                var currentlyPosition = sekiro.GetPlayerPosition();
                                var rangeX = ((currentlyPosition.X - mb.vector.X) <= positionMargin) && ((currentlyPosition.X - mb.vector.X) >= -positionMargin);
                                var rangeY = ((currentlyPosition.Y - mb.vector.Y) <= positionMargin) && ((currentlyPosition.Y - mb.vector.Y) >= -positionMargin);
                                var rangeZ = ((currentlyPosition.Z - mb.vector.Z) <= positionMargin) && ((currentlyPosition.Z - mb.vector.Z) >= -positionMargin);
                                if (rangeX && rangeY && rangeZ)
                                {
                                    if (mb.Mode == "Loading game after")
                                    {
                                        if (!listPendingMb.Contains(mb))
                                        {
                                            listPendingMb.Add(mb);
                                        }
                                    }
                                    else
                                    {
                                        splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO MINIBOSS POSITION -> {mb.Title} - {mb.vector}");
                                        mb.IsSplited = splitterControl.GetSplitStatus();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        List<DefinitionsSekiro.PositionS> MortalJourneyData = new List<DefinitionsSekiro.PositionS>()
        {
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-100.3826, (float)-69.91195, (float)38.01242)}, //Gyobu
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-217, (float)-787.8057, (float)569.8)},//Lady Butterfly - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-102.9621, (float)53.90246, (float)243.125)}, //Genichiro - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-626.7878, (float)-296.0899, (float)757.6398)}, //Guardian Ape - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-180.1189, (float)-397.2622, (float)1406.365)}, //Corrupted Monk - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-117.93, (float)54.07684, (float)231.449)},  //Owl - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-197.3754,(float) -377.826, (float)632.1486)},//Double Apes - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-103.509,(float) 53.90246,(float) 243.125)}, //Emma - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-103.508,(float) 53.90246,(float) 243.125)}, //Isshin - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)91.34,(float) 154.8877,(float) 69.23)}, //True Monk - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-206.346, (float)-787.7436,(float) 565.117)}, //Owl Father - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-122.3998,(float) -71.81956, (float)-1.056416)}, //Demon of Hatred - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-372.1678,(float) -46.28377, (float)184.5305)}, //Genichiro Way of Tomoe - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-372.1679,(float) -46.28377,(float) 184.5305)}, //Sword Saint Isshin - MJG
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-102.9759,(float) 53.90246, (float)243.125)}, //Inner Genichiro
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-206.347, (float)-787.7436, (float)565.117)}, //Inner Father
            new DefinitionsSekiro.PositionS(){vector = new Vector3f((float)-372.1677, (float)-46.28377, (float)184.5305)}, //Inner Isshin
        };

        private void NotSplited(ref List<DefinitionsSekiro.PositionS> p)
        {
            foreach (var i in p)
            {
                i.IsSplited = false;
            }
        }

        int index = 0;
        List<DefinitionsSekiro.PositionS> PendingMortal = new List<DefinitionsSekiro.PositionS>();
        private void MortalJourney()
        {
            DefinitionsSekiro.PositionS p;
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(100);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if (dataSekiro.mortalJourneyRun && index < MortalJourneyData.Count)
                    {
                        p = MortalJourneyData[index];
                        if (PendingMortal.Count == 0)
                        {
                            if (!p.IsSplited)
                            {
                                int positionMargin = 10;
                                var currentlyPosition = sekiro.GetPlayerPosition();
                                var rangeX = ((currentlyPosition.X - p.vector.X) <= positionMargin) && ((currentlyPosition.X - p.vector.X) >= -positionMargin);
                                var rangeY = ((currentlyPosition.Y - p.vector.Y) <= positionMargin) && ((currentlyPosition.Y - p.vector.Y) >= -positionMargin);
                                var rangeZ = ((currentlyPosition.Z - p.vector.Z) <= positionMargin) && ((currentlyPosition.Z - p.vector.Z) >= -positionMargin);
                                if (rangeX && rangeY && rangeZ)
                                {
                                    if (!PendingMortal.Contains(p))
                                    {
                                        PendingMortal.Add(p);
                                    }
                                }
                            }
                            else
                            {
                                index++;
                            }
                        }
                        else
                        {
                            if (!sekiro.IsPlayerLoaded())
                            {
                                splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO MORTAL JOURNEY -> {p.vector}");
                                if (splitterControl.GetSplitStatus())
                                {
                                    p.IsSplited = true;
                                    PendingMortal.Clear();
                                }
                            }
                        }
                    }
                    else
                    {
                        index = 0;
                        NotSplited(ref MortalJourneyData);
                    }
                }
            }
        }

        private void IdolSplit()
        {
            var IdolsToSplit = dataSekiro.GetidolsTosplit();
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if (IdolsToSplit != dataSekiro.GetidolsTosplit()) IdolsToSplit = dataSekiro.GetidolsTosplit();
                    foreach (var i in IdolsToSplit)
                    {
                        if (!i.IsSplited && sekiro.ReadEventFlag(i.Id))
                        {
                            if (i.Mode == "Loading game after")
                            {
                                if (!listPendingI.Contains(i))
                                {
                                    listPendingI.Add(i);
                                }
                            }
                            else
                            {
                                splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO IDOL -> {i.Title}");
                                i.IsSplited = splitterControl.GetSplitStatus();
                            }
                        }
                    }
                }
            }
        }

        private void PositionSplit()
        {
            var PositionsToSplit = dataSekiro.GetPositionsToSplit();
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(100);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if (PositionsToSplit != dataSekiro.GetPositionsToSplit()) PositionsToSplit = dataSekiro.GetPositionsToSplit();
                    foreach (var p in PositionsToSplit)
                    {
                        if (!p.IsSplited)
                        {
                            var currentlyPosition = sekiro.GetPlayerPosition();
                            var rangeX = ((currentlyPosition.X - p.vector.X) <= dataSekiro.positionMargin) && ((currentlyPosition.X - p.vector.X) >= -dataSekiro.positionMargin);
                            var rangeY = ((currentlyPosition.Y - p.vector.Y) <= dataSekiro.positionMargin) && ((currentlyPosition.Y - p.vector.Y) >= -dataSekiro.positionMargin);
                            var rangeZ = ((currentlyPosition.Z - p.vector.Z) <= dataSekiro.positionMargin) && ((currentlyPosition.Z - p.vector.Z) >= -dataSekiro.positionMargin);
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
                                    splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO POSITION -> {p.Title} - {p.vector.ToString()}");
                                    p.IsSplited = splitterControl.GetSplitStatus();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LevelSplit()
        {
            var LvlToSplit = dataSekiro.GetLvlToSplit();
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if (LvlToSplit != dataSekiro.GetLvlToSplit()) LvlToSplit = dataSekiro.GetLvlToSplit();
                    foreach (var lvl in LvlToSplit)
                    {
                        if (!lvl.IsSplited && sekiro.GetAttribute(lvl.Attribute) >= lvl.Value)
                        {
                            if (lvl.Mode == "Loading game after")
                            {
                                if (!listPendingLevel.Contains(lvl))
                                {
                                    listPendingLevel.Add(lvl);
                                }
                            }
                            else
                            {
                                splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO LEVEL -> {lvl.Attribute} - {lvl.Value}");
                                lvl.IsSplited = splitterControl.GetSplitStatus();
                            }
                        }
                    }
                }
            }
        }

        private void CustomFlagToSplit()
        {
            var FlagToSplit = dataSekiro.GetFlagToSplit();
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if (FlagToSplit != dataSekiro.GetFlagToSplit()) FlagToSplit = dataSekiro.GetFlagToSplit();
                    foreach (var cf in FlagToSplit)
                    {
                        if (!cf.IsSplited && sekiro.ReadEventFlag(cf.Id))
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
                                splitterControl.SplitCheck($"SplitFlags is produced by: SEKIRO CUSTOM_FLAG -> {cf.Title} - {cf.Id}");
                                cf.IsSplited = splitterControl.GetSplitStatus();
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
//MIT License

//Copyright (c) 2022-2024 Ezequiel Medina

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

using HitCounterManager;
using SoulMemory;
using SoulMemory.Sekiro;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSplitterCore
{
    public class SekiroSplitter
    {
        public static Sekiro sekiro = new Sekiro();
        public bool _StatusSekiro = false;
        public bool _SplitGo = false;
        public bool _PracticeMode = false;
        private bool PK = true;
        public bool _ShowSettings = false;
        public DTSekiro dataSekiro;
        public DefinitionsSekiro defS = new DefinitionsSekiro();
        public IAutoSplitterCoreInterface _profile;
        private bool _writeMemory = false;
        private static readonly object _object = new object();
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };
        public bool DebugMode = false;

        #region Control Management
        public DTSekiro getDataSekiro()
        {
            return this.dataSekiro;
        }

        public void setDataSekiro(DTSekiro data, IAutoSplitterCoreInterface profile)
        {
            this.dataSekiro = data;
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

        private void SplitCheck() //PK is seted false if user set Practice mode after a flagcheck is produced 
        {
            lock (_object)
            {
                if (_PracticeMode)
                    PK = false;
                else
                {
                    if (_SplitGo) { Thread.Sleep(2000); }
                    _SplitGo = true;
                    PK = true;
                }              
            }
        }

        public bool getSekiroStatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            try
            {
                _StatusSekiro = sekiro.TryRefresh();
            } catch (Exception) { _StatusSekiro = false; };
            return _StatusSekiro;
        }

        public void setStatusSplitting(bool status)
        {
            dataSekiro.enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }

        public void resetSplited()
        {
            listPendingB.Clear();
            listPendingI.Clear();
            listPendingP.Clear();
            listPendingCf.Clear();
            listPendingMb.Clear();

            if (dataSekiro.getBossToSplit().Count > 0)
            {
                foreach (var b in dataSekiro.getBossToSplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataSekiro.getidolsTosplit().Count > 0)
            {
                foreach (var b in dataSekiro.getidolsTosplit())
                {
                    b.IsSplited = false;
                }
            }

            if (dataSekiro.getPositionsToSplit().Count > 0)
            {
                foreach (var p in dataSekiro.getPositionsToSplit())
                {
                    p.IsSplited = false;
                }
            }

            if (dataSekiro.getFlagToSplit().Count > 0)
            {
                foreach (var cf in dataSekiro.getFlagToSplit())
                {
                    cf.IsSplited = false;
                }
            }

            if (dataSekiro.getMiniBossToSplit().Count > 0)
            {
                foreach (var mb in dataSekiro.getMiniBossToSplit())
                {
                    mb.IsSplited = false;
                }
            }
            index = 0;
            notSplited(ref MortalJourneyData);
            PendingMortal.Clear();
        }
        #endregion
        #region Object Management
        public void AddIdol(string idol,string mode)
        {
            DefinitionsSekiro.Idol cIdol = defS.idolToEnum(idol);
            cIdol.Mode = mode;
            dataSekiro.idolsTosplit.Add(cIdol);
        }
            

        public void AddBoss(string boss,string mode)
        {
            DefinitionsSekiro.BossS cBoss = defS.BossToEnum(boss);
            cBoss.Mode = mode;
            dataSekiro.bossToSplit.Add(cBoss);
        }

        public void AddPosition(float X, float Y, float Z , string mode, string title)
        {
            var position = new DefinitionsSekiro.PositionS();
            position.setVector(new Vector3f(X,Y,Z));
            position.Mode = mode;
            position.Title = title;
            dataSekiro.positionsToSplit.Add(position);
        }

        public void AddCustomFlag(uint id,string mode, string title)
        {
            DefinitionsSekiro.CfSk cFlag = new DefinitionsSekiro.CfSk()
            {
                Id = id,
                Mode = mode,
                Title = title
            };
            dataSekiro.flagToSplit.Add(cFlag);
        }

        public void AddMiniBoss (string miniboss, string mode)
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
                    mBoss.Id = defS.idolToEnum("Ashina Castle").Id;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Shigekichi of the Red Guard":
                    mBoss.Id = defS.idolToEnum("Flames of Hatred").Id;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Shinobi Hunter Enshin of Misen":
                    mBoss.Id = 50006120;
                    mBoss.kindSplit = DefinitionsSekiro.KindSplit.ID;
                    break;
                case "Juzou the Drunkard":
                    mBoss.Id = defS.idolToEnum("Hirata Audience Chamber").Id;
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
                    mBoss.Id = defS.idolToEnum("Bottomless Hole").Id;
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
                    mBoss.Id = defS.idolToEnum("Hidden Forest").Id;
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
                    mBoss.Id = defS.idolToEnum("Flower Viewing Stage").Id;
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

        public string GetMiniBossDescription(string mBoss)
        {
            var mappingMiniboss = new Dictionary<string, string>()
            {
                { "Leader Shigenori Yamauchi","Split when obtain Fistful of Ash in Genichiro path" },
                { "General Naomori Kawarada", "Split when kill General Naomori Kawarada" },     
                { "Ogre - Ashina Outskirts", "Split when trigger window position after kill Ogre\r\n X= 124.60, Y= -35.50, Z= 140.20" },
                { "General Tenzen Yamauchi", "Split shen trigger position near mini house\r\nPath of Headless Ako \r\n X= 1134.60, Y= -57.50, Z= 220.10" },
                { "Headless Ako", "Split when kill Headless Ako" },
                { "Blazing Bull", "Split when idol: Ashina Castle is automatically activate after kill Blazing Bull" },
                { "Shigekichi of the Red Guard", "Split when idol: Flames of Hatred is manual activated" },
                { "Shinobi Hunter Enshin of Misen", "Split when obtain Hidden Temple Key with Owl" },
                { "Juzou the Drunkard", "Split when idol: Hirata Audience Chamber is manual activated" },
                { "Lone Shadow Masanaga the Spear-Bearer", "Split when trigger position near Gokan path grapple\r\n X=74.50, Y=-37.65, Z=385.25" },
                { "Juzou the Drunkard 2", "Split when kill Juzou the Drunkard 2" },
                { "General Kuranosuke Matsumoto", "Split when kill General Kuranosuke Matsumoto" },
                { "Seven Achina Spears â€“ Shikibu Toshikatsu Yamauchi", "Split when kill Shikibu Toshikatsu Yamauchi" },
                { "Lone Shadow Longswordsman", "Split when trigger position after water grapple\r\nPath to Shichimen Warrior\r\n X= -323.20, Y= -48.30, Z= 344.35" },
                { "Headless Ungo", "Split when trigger position grapple\r\nExit of Ungo Boss Fight arena near of Bridge \r\n X= -27.80, Y= 0.25, Z= 252.0" },
                { "Ashina Elite â€“ Jinsuke Saze", "Split when kill Jinsuke Saze" },
                { "Ogre - Ashina Castle", "Split when trigger position on the beam above ogre\r\n X= -111.20, Y= 25.00 237.90 \r\nRecommend: Loading Game After" },
                { "Lone Shadow Vilehand", "Splits when opening the door leading to Ogre 2" },
                { "Seven Ashina Spears - Shume Masaji Oniwa", "Split when open SSIshin door" },
                { "Ashina Elite - Ujinari Mizuo", "Split when kill Ashina Elite Ujinari Mizuo" },
                { "Shichimen Warrior - Abandoned Dungeon", "Split when idol: Bottomless Hole is manual activated" },
                { "Armored Warrior", "Split when kill Armored Warrior" },
                { "Long-arm Centipede Senâ€™un", "Split when kill Long-arm Centipede Senâ€™un" },
                { "Headless Gokan", "Split when kill Headless Gokan" },
                { "Long-arm Centipede Giraffe", "Split when use key on door after Centipede" },
                { "Snake Eyes Shirahagi", "Split when idol: Hidden Forest is manual activated" },
                { "Shichimen Warrior - Ashina Depths", "Split when kill Shichimen Warrior" },
                { "Headless Gacchin", "Split when kill Headless Gacchin" },
                { "Tokujiro the Glutton", "Split when kill Tokujiro the Glutton" },
                { "Mist Noble", "Split when kill Mist Noble" },
                { "O'rin of the Water", "Split when kill O'rin" },
                { "Sakura Bull of the Palace", "Split when kill Sakura Bull" },
                { "Leader Okami", "Split when kill Leader Okami" },
                { "Headless Yashariku", "Split when idol: Flower Viewing Stage is manually activated\r\nRoute Recommended: Okami, Yashariku, go to idol" },
                { "Shichimen Warrior - Fountainhead Palace", "Split when kill Shichimen Warrior" }
             };

            try
            {
                mappingMiniboss.TryGetValue(mBoss, out string description);
                return description;
            }catch (Exception) { MessageBox.Show("Error miniboss String"); return string.Empty; }
     
        }

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
        
        public void RemoveIdol (string fidol)
        {
            DefinitionsSekiro.Idol cIdol = defS.idolToEnum(fidol);
            listPendingI.RemoveAll(idol => idol.Id == cIdol.Id);
            dataSekiro.idolsTosplit.RemoveAll(idol => idol.Id == cIdol.Id);
                      
        }

        public string FindIdol(string fidol)
        {
            DefinitionsSekiro.Idol cIdol = defS.idolToEnum(fidol);
            var idolReturn = dataSekiro.idolsTosplit.Find(idol => idol.Id == cIdol.Id);
            if (idolReturn == null)
            {
                return "None";
            }
            else { return idolReturn.Mode; }         
        }

        public void setPositionMargin(int select)
        {
            dataSekiro.positionMargin = select;
        }

        public void clearData()
        {
            listPendingB.Clear();
            listPendingI.Clear();
            listPendingP.Clear();
            listPendingCf.Clear();
            listPendingMb.Clear();
            PendingMortal.Clear();
            dataSekiro.bossToSplit.Clear();
            dataSekiro.idolsTosplit.Clear();
            dataSekiro.positionsToSplit.Clear();
            dataSekiro.flagToSplit.Clear();
            dataSekiro.miniBossToSplit.Clear();
            notSplited(ref MortalJourneyData);
            dataSekiro.positionMargin = 3;
            dataSekiro.mortalJourneyRun = false;
            index = 0;
        }
        #endregion
        #region Checking
        public Vector3f getCurrentPosition()
        {
            if (!_StatusSekiro) getSekiroStatusProcess(0);
            if (!_StatusSekiro)
            {
                Vector3f vector = new Vector3f() { X = 0, Y = 0, Z = 0 };
                return vector;
            }
            return sekiro.GetPlayerPosition();
        }

        public int getTimeInGame()
        {
            if (!_StatusSekiro) getSekiroStatusProcess(0);
            return sekiro.GetInGameTimeMilliseconds();
        }

        public bool CheckFlag(uint id)
        {
            if(!_StatusSekiro) getSekiroStatusProcess(0);
            return _StatusSekiro && sekiro.ReadEventFlag(id);
        }
        #endregion
        #region Procedure
        public void LoadAutoSplitterProcedure()
        {
            var taskRefresh = new Task(() =>
            {
                RefreshSekiro();
            });
            var taskCheckload = new Task(() =>
            {
                checkLoad();
            });
            var task1 = new Task(() =>
            {
                BossSplit();
            });
            var task2 = new Task(() =>
            {
                IdolSplit();
            });
            var task3 = new Task(() =>
            {
                PositionSplit();
            });
            var task4 = new Task(() =>
            {
                customFlagToSplit();
            });
            var task5 = new Task(() =>
            {
                MortalJourney();
            });
            var task6 = new Task(() =>
            {
                MiniBossSplit();
            });

            taskRefresh.Start();
            taskCheckload.Start();
            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();
            task5.Start();
            task6.Start();
        }
        #endregion
        #region CheckFlag Init()   
        private void RefreshSekiro()
        {
            int delay = 2000;
            getSekiroStatusProcess(delay);
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(10);
               getSekiroStatusProcess(delay);
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


        private void checkLoad()
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
                                SplitCheck();
                                var i = dataSekiro.idolsTosplit.FindIndex(fidol => fidol.Id == idol.Id);
                                dataSekiro.idolsTosplit[i].IsSplited = true;
                            }

                            foreach (var boss in listPendingB)
                            {
                                SplitCheck();
                                var b = dataSekiro.bossToSplit.FindIndex(fboss => fboss.Id == boss.Id);
                                dataSekiro.bossToSplit[b].IsSplited = true;

                            }

                            foreach (var position in listPendingP)
                            {
                                SplitCheck();
                                var p = dataSekiro.positionsToSplit.FindIndex(fposition => fposition.vector == position.vector);
                                dataSekiro.positionsToSplit[p].IsSplited = true;
                            }

                            foreach (var cf in listPendingCf)
                            {
                                SplitCheck();
                                var c = dataSekiro.flagToSplit.FindIndex(icf => icf.Id == cf.Id);
                                dataSekiro.flagToSplit[c].IsSplited = true;
                            }

                            foreach (var mb in listPendingMb)
                            {
                                SplitCheck();
                                var mbo = dataSekiro.miniBossToSplit.FindIndex(fmb => fmb.Title == mb.Title);
                                dataSekiro.miniBossToSplit[mbo].IsSplited = true;
                            }

                            listPendingB.Clear();
                            listPendingI.Clear();
                            listPendingP.Clear();
                            listPendingCf.Clear();
                        }
                    }
                }
            }
        }

        private void BossSplit()
        {
            var BossToSplit = dataSekiro.getBossToSplit();
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if(BossToSplit != dataSekiro.getBossToSplit()) BossToSplit = dataSekiro.getBossToSplit();
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
                                SplitCheck();
                                b.IsSplited = PK;
                            }
                        }
                    }
                }
            }
        }

        private void MiniBossSplit()
        {
            var MiniBossToSplit = dataSekiro.getMiniBossToSplit();
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(100);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if (MiniBossToSplit != dataSekiro.getMiniBossToSplit()) MiniBossToSplit = dataSekiro.getMiniBossToSplit();
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
                                    SplitCheck();
                                    mb.IsSplited = PK;
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
                                        SplitCheck();
                                        mb.IsSplited = PK;
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

        private void notSplited(ref List<DefinitionsSekiro.PositionS> p)
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
                                SplitCheck();
                                if (PK)
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
                        notSplited(ref MortalJourneyData);
                    }
                }
            }
        }

        private void IdolSplit()
        {
            var IdolsToSplit = dataSekiro.getidolsTosplit();
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if (IdolsToSplit != dataSekiro.getidolsTosplit()) IdolsToSplit = dataSekiro.getidolsTosplit();
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
                                SplitCheck();
                                i.IsSplited = PK;
                            }
                        }
                    }
                }
            }
        }

        private void PositionSplit() {
            var PositionsToSplit = dataSekiro.getPositionsToSplit();
            while (dataSekiro.enableSplitting)
            {              
                Thread.Sleep(100);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if (PositionsToSplit != dataSekiro.getPositionsToSplit()) PositionsToSplit = dataSekiro.getPositionsToSplit();
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
                                    SplitCheck();
                                    p.IsSplited = PK;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void customFlagToSplit()
        {
            var FlagToSplit = dataSekiro.getFlagToSplit();
            while (dataSekiro.enableSplitting)
            {
                Thread.Sleep(1000);
                if (_StatusSekiro && !_PracticeMode && !_ShowSettings)
                {
                    if (FlagToSplit != dataSekiro.getFlagToSplit()) FlagToSplit = dataSekiro.getFlagToSplit();
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
                                SplitCheck();
                                cf.IsSplited = PK;
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }        
}
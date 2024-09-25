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
using System.Drawing;
using System.Linq;
using SoulMemory;


namespace AutoSplitterCore
{
    public class DefinitionsSekiro
    {

        #region Boss.Sekiro
        [Serializable]
        public class BossS
        {
            public string Title;
            public uint Id;
            public bool IsSplited;
            public string Mode;

            public void BossDate(string title, uint id)
            {
                this.Title = title;
                this.Id = id;
                this.IsSplited = false;
            }

        }

        public BossS BossToEnum(string Nboss)
        {
            BossS boss = new BossS();
            switch (Nboss)
            {
                case "Genichiro Ashina - Tutorial":
                    boss.BossDate("Genichiro Ashina - Tutorial", 11120803); break;
                case "Gyoubu Masataka Oniwa":
                    boss.BossDate("Gyoubu Masataka Oniwa", 9301);
                    break;
                case "Lady Butterfly":
                    boss.BossDate("Lady Butterfly", 9302);
                    break;
                case "Genichiro Ashina":
                    boss.BossDate("Genichiro Ashina", 9303);
                    break;
                case "Folding Screen Monkeys":
                    boss.BossDate("Folding Screen Monkeys", 9305);
                    break;
                case "Guardian Ape":
                    boss.BossDate("Guardian Ape", 9304);
                    break;
                case "Headless Ape":
                    boss.BossDate("Headless Ape", 9307);
                    break;
                case "Corrupted Monk (ghost)":
                    boss.BossDate("Corrupted Monk (ghost)", 9306);
                    break;
                case "Emma, the Gentle Blade":
                    boss.BossDate("Emma, the Gentle Blade", 9315);
                    break;
                case "Isshin Ashina":
                    boss.BossDate("Isshin Ashina", 9316);
                    break;
                case "Great Shinobi Owl":
                    boss.BossDate("Great Shinobi Owl", 9308);
                    break;
                case "True Corrupted Monk":
                    boss.BossDate("True Corrupted Monk", 9309);
                    break;
                case "Divine Dragon":
                    boss.BossDate("Divine Dragon", 9310);
                    break;
                case "Owl (Father)":
                    boss.BossDate("Owl (Father)", 9317);
                    break;
                case "Demon of Hatred":
                    boss.BossDate("Demon of Hatred", 9313);
                    break;
                case "Isshin, the Sword Saint":
                    boss.BossDate("Isshin, the Sword Saint", 9312);
                    break;
            }
            return boss;
        }
        #endregion
        #region Idol.Sekiro
        [Serializable]
        public class Idol
        {
            public string Title;
            public string Location;
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
            public Idol(string Title, string Location, uint Id)
            {
                this.Title = Title;
                this.Location = Location;
                this.Id = Id;
                this.IsSplited = false;
            }
        }


        Dictionary<string, Idol> idolMap = new Dictionary<string, Idol>
        {
            // Ashina Outskirts
            { "Dilapidated Temple", new Idol("Dilapidated Temple", "Ashina Outskirts", 11100000) },
            { "Ashina Outskirts", new Idol("Ashina Outskirts", "Ashina Outskirts", 11100006) },
            { "Outskirts Wall - Gate Path", new Idol("Outskirts Wall - Gate Path", "Ashina Outskirts", 11100001) },
            { "Outskirts Wall - Stairway", new Idol("Outskirts Wall - Stairway", "Ashina Outskirts", 11100002) },
            { "Underbridge Valley", new Idol("Underbridge Valley", "Ashina Outskirts", 11100003) },
            { "Ashina Castle Fortress", new Idol("Ashina Castle Fortress", "Ashina Outskirts", 11100004) },
            { "Ashina Castle Gate", new Idol("Ashina Castle Gate", "Ashina Outskirts", 11100005) },
            { "Flames of Hatred", new Idol("Flames of Hatred", "Ashina Outskirts", 11100007) },

            // Hirata Estate
            { "Dragonspring - Hirata Estate", new Idol("Dragonspring - Hirata Estate", "Hirata Estate", 11000000) },
            { "Estate Path", new Idol("Estate Path", "Hirata Estate", 11000001) },
            { "Bamboo Thicket Slope", new Idol("Bamboo Thicket Slope", "Hirata Estate", 11000002) },
            { "Hirata Estate - Main Hal", new Idol("Hirata Estate - Main Hal", "Hirata Estate", 11000003) },
            { "Hirata Estate - Hidden Temple", new Idol("Hirata Estate - Hidden Temple", "Hirata Estate", 11000004) },
            { "Hirata Audience Chamber", new Idol("Hirata Audience Chamber", "Hirata Estate", 11000005) },

            // Ashina Castle
            { "Ashina Castle", new Idol("Ashina Castle", "Ashina Castle", 11110000) },
            { "Upper Tower - Antechamber", new Idol("Upper Tower - Antechamber", "Ashina Castle", 11110001) },
            { "Upper Tower - Ashina Dojo", new Idol("Upper Tower - Ashina Dojo", "Ashina Castle", 11110007) },
            { "Castle Tower Lookout", new Idol("Castle Tower Lookout", "Ashina Castle", 11110002) },
            { "Upper Tower - Kuro's Room", new Idol("Upper Tower - Kuro's Room", "Ashina Castle", 11110003) },
            { "Old Grave", new Idol("Old Grave", "Ashina Castle", 11110006) },
            { "Great Serpent Shrine", new Idol("Great Serpent Shrine", "Ashina Castle", 11110004) },
            { "Abandoned Dungeon Entrance", new Idol("Abandoned Dungeon Entrance", "Ashina Castle", 11110005) },
            { "Ashina Reservoir", new Idol("Ashina Reservoir", "Ashina Castle", 11120001) },
            { "Near Secret Passage", new Idol("Near Secret Passage", "Ashina Castle", 11120000) },

            // Abandoned Dungeon
            { "Underground Waterway", new Idol("Underground Waterway", "Abandoned Dungeon", 11300000) },
            { "Bottomless Hole", new Idol("Bottomless Hole", "Abandoned Dungeon", 11300001) },

            // Senpou Temple, Mt. Kongo
            { "Senpou Temple, Mt. Kongo", new Idol("Senpou Temple, Mt. Kongo", "Senpou Temple, Mt. Kongo", 12000000) },
            { "Shugendo", new Idol("Shugendo", "Senpou Temple, Mt. Kongo", 12000001) },
            { "Temple Grounds", new Idol("Temple Grounds", "Senpou Temple, Mt. Kongo", 12000002) },
            { "Main Hall", new Idol("Main Hall", "Senpou Temple, Mt. Kongo", 12000003) },
            { "Inner Sanctum", new Idol("Inner Sanctum", "Senpou Temple, Mt. Kongo", 12000004) },
            { "Sunken Valley Cavern", new Idol("Sunken Valley Cavern", "Senpou Temple, Mt. Kongo", 12000005) },
            { "Bell Demon's Temple", new Idol("Bell Demon's Temple", "Senpou Temple, Mt. Kongo", 12000006) },

            // Sunken Valley
            { "Under-Shrine Valley", new Idol("Under-Shrine Valley", "Sunken Valley", 11700007) },
            { "Sunken Valley", new Idol("Sunken Valley", "Sunken Valley", 11700000) },
            { "Gun Fort", new Idol("Gun Fort", "Sunken Valley", 11700001) },
            { "Riven Cave", new Idol("Riven Cave", "Sunken Valley", 11700002) },
            { "Bodhisattva Valley", new Idol("Bodhisattva Valley", "Sunken Valley", 11700008) },
            { "Guardian Ape's Watering Hole", new Idol("Guardian Ape's Watering Hole", "Sunken Valley", 11700003) },

            // Ashina Depths
            { "Ashina Depths", new Idol("Ashina Depths", "Ashina Depths", 11700005) },
            { "Poison Pool", new Idol("Poison Pool", "Ashina Depths", 11700004) },
            { "Guardian Ape's Burrow", new Idol("Guardian Ape's Burrow", "Ashina Depths", 11700006) },
            { "Hidden Forest", new Idol("Hidden Forest", "Ashina Depths", 11500000) },
            { "Mibu Village", new Idol("Mibu Village", "Ashina Depths", 11500001) },
            { "Water Mill", new Idol("Water Mill", "Ashina Depths", 11500002) },
            { "Wedding Cave Door", new Idol("Wedding Cave Door", "Ashina Depths", 11500003) },

            // Fountainhead Palace
            { "Fountainhead Palace", new Idol("Fountainhead Palace", "Fountainhead Palace", 12500000) },
            { "Vermilion Bridge", new Idol("Vermilion Bridge", "Fountainhead Palace", 12500001) },
            { "Mibu Manor", new Idol("Mibu Manor", "Fountainhead Palace", 12500006) },
            { "Flower Viewing Stage", new Idol("Flower Viewing Stage", "Fountainhead Palace", 12500002) },
            { "Great Sakura", new Idol("Great Sakura", "Fountainhead Palace", 12500003) },
            { "Palace Grounds", new Idol("Palace Grounds", "Fountainhead Palace", 12500004) },
            { "Feeding Grounds", new Idol("Feeding Grounds", "Fountainhead Palace", 12500007) },
            { "Near Pot Noble", new Idol("Near Pot Noble", "Fountainhead Palace", 12500008) },
            { "Sanctuary", new Idol("Sanctuary", "Fountainhead Palace", 12500005) }
        };

        public Idol IdolToEnum(string NIdol)
        {
            Idol selectedIdol;
            if (idolMap.ContainsKey(NIdol))
            {
                selectedIdol = idolMap[NIdol];
            }
            else
            {
                throw new ArgumentException("Invalid Idol.");
            }

            return selectedIdol;
        }

        public List<string> GetAllIdols() => idolMap.Keys.ToList();


        #endregion
        #region Position.Sekiro
        [Serializable]
        public class PositionS
        {
            public Vector3f vector = new Vector3f();
            public bool IsSplited = false;
            public string Mode;
            public string Title;
            public void SetVector(Vector3f vector) => this.vector = vector;
        }


        #endregion
        #region CustomFlag.Sekiro
        [Serializable]
        public class CfSk
        {
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
            public string Title;
        }

        #endregion
        #region MiniBoss.Sekiro
        [Serializable]
        public class MiniBossS
        {
            public string Title;
            public uint Id;
            public Vector3f vector;
            public bool IsSplited;
            public string Mode;
            public KindSplit kindSplit;
        }

        #endregion
        #region Level.Sekiro
        [Serializable]
        public class LevelS
        {
            public SoulMemory.Sekiro.Attribute Attribute;
            public uint Value;
            public bool IsSplited = false;
            public string Mode;
        }

        public SoulMemory.Sekiro.Attribute StringToEnumAttribute (string Attribute)
        {
            return (SoulMemory.Sekiro.Attribute)Enum.Parse(typeof(SoulMemory.Sekiro.Attribute), Attribute);
        }
        #endregion
        public enum KindSplit
        {
            ID,Position
        }
    }


    [Serializable]
    public class DTSekiro
    {
        //Settings Vars
        public bool enableSplitting = false;
        public bool autoTimer = false;
        public bool gameTimer = false;
        public int positionMargin = 5;
        public bool ResetIGTNG = false;
        //Flags to Split
        public bool mortalJourneyRun = false;
        public List<DefinitionsSekiro.BossS> bossToSplit = new List<DefinitionsSekiro.BossS>();
        public List<DefinitionsSekiro.MiniBossS> miniBossToSplit = new List<DefinitionsSekiro.MiniBossS>();
        public List<DefinitionsSekiro.Idol> idolsTosplit = new List<DefinitionsSekiro.Idol>();
        public List<DefinitionsSekiro.PositionS> positionsToSplit = new List<DefinitionsSekiro.PositionS>();
        public List<DefinitionsSekiro.LevelS> lvlToSplit = new List<DefinitionsSekiro.LevelS>();
        public List<DefinitionsSekiro.CfSk> flagToSplit = new List<DefinitionsSekiro.CfSk>();


        public List<DefinitionsSekiro.BossS> GetBossToSplit() => this.bossToSplit;

        public List<DefinitionsSekiro.Idol> GetidolsTosplit() => this.idolsTosplit;

        public List<DefinitionsSekiro.PositionS> GetPositionsToSplit() => this.positionsToSplit;

        public List<DefinitionsSekiro.CfSk> GetFlagToSplit() => this.flagToSplit;

        public List<DefinitionsSekiro.MiniBossS> GetMiniBossToSplit() => this.miniBossToSplit;

        public List<DefinitionsSekiro.LevelS> GetLvlToSplit() => this.lvlToSplit;

    }
}

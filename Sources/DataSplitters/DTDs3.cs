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
using System;
using System.Collections.Generic;

namespace AutoSplitterCore
{
    public class DefinitionsDs3
    {
        #region Boss.Ds3
        [Serializable]
        public class BossDs3
        {
            public string Title;
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
        }

        public BossDs3 StringToEnumBoss(string boss)
        {
            var bossMapping = new Dictionary<string, (string Title, uint Id)>
            {
                { "Iudex Gundyr", ("Iudex Gundyr", 14000800)},
                { "Vordt of the Boreal Valley", ("Vordt of the Boreal Valley", 13000800)},
                { "Curse-Rotted Greatwood", ("Curse-Rotted Greatwood", 13100800)},
                { "Crystal Sage", ("Crystal Sage", 13300850)},
                { "Abyss Watchers", ("Abyss Watchers", 13300800)},
                { "Deacons of the Deep", ("Deacons of the Deep", 13500800)},
                { "High Lord Wolnir", ("High Lord Wolnir", 13800800)},
                { "Old Demon King", ("Old Demon King", 13800830)},
                { "Pontiff Sulyvahn", ("Pontiff Sulyvahn", 13700850)},
                { "Yhorm the Giant", ("Yhorm the Giant", 13900800)},
                { "Aldrich, Devourer of Gods", ("Aldrich, Devourer of Gods", 13700800)},
                { "Dancer of the Boreal Valley", ("Dancer of the Boreal Valley", 13000890)},
                { "Dragonslayer Armour", ("Dragonslayer Armour", 13010800)},
                { "Oceiros, the Consumed King", ("Oceiros, the Consumed King", 13000830)},
                { "Champion Gundyr", ("Champion Gundyr", 14000830)},
                { "Lothric, Younger Prince", ("Lothric, Younger Prince", 13410830)},
                { "Ancient Wyvern", ("Ancient Wyvern", 13200800)},
                { "Nameless King", ("Nameless King", 13200850)},
                { "Soul of Cinder", ("Soul of Cinder", 14100800)},
                { "Sister Friede", ("Sister Friede", 14500800)},
                { "Champion's Gravetender & Gravetender Greatwolf", ("Champion's Gravetender & Gravetender Greatwolf", 14500860)},
                { "Demon in Pain & Demon From Below / Demon Prince", ("Demon in Pain & Demon From Below / Demon Prince", 15000800)},
                { "Halflight, Spear of the Church", ("Halflight, Spear of the Church", 15100800)},
                { "Darkeater Midir", ("Darkeater Midir", 15100850)},
                { "Slave Knight Gael", ("Slave Knight Gael", 15110800)}
            };

            if (bossMapping.TryGetValue(boss, out var bossInfo))
            {
                return new BossDs3 { Title = bossInfo.Title, Id = bossInfo.Id };
            }
            else
            {
                throw new ArgumentException($"Invalid boss string: {boss}");
            }
        }
        #endregion
        #region Bonfire.Ds3
        [Serializable]
        public class BonfireDs3
        {
            public string Title;
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
        }

        public BonfireDs3 StringToEnumBonfire(string Bonfire)
        {
            var bonfireMapping = new Dictionary<string, (string Title, uint Id)>
            {
                { "Firelink Shrine", ("Firelink Shrine", 14000000)},
                { "Cemetery of Ash", ("Cemetery of Ash", 14000001)},
                { "Iudex Gundyr", ("Iudex Gundyr", 14000002)},
                { "Untended Graves", ("Untended Graves", 14000003)},
                { "Champion Gundyr", ("Champion Gundyr", 14000004)},
                { "High Wall of Lothric", ("High Wall of Lothric", 13000009)},
                { "Tower on the Wall", ("Tower on the Wall", 13000005)},
                { "Vordt of the Boreal Valley", ("Vordt of the Boreal Valley", 13000002)},
                { "Dancer of the Boreal Valley", ("Dancer of the Boreal Valley", 13000004)},
                { "Oceiros, the Consumed King", ("Oceiros, the Consumed King", 13000001)},
                { "Foot of the High Wall", ("Foot of the High Wall", 13100004)},
                { "Undead Settlement", ("Undead Settlement", 13100000)},
                { "Cliff Underside", ("Cliff Underside", 13100002)},
                { "Dilapidated Bridge", ("Dilapidated Bridge", 13100003)},
                { "Pit of Hollows", ("Pit of Hollows", 13100001)},
                { "Road of Sacrifices", ("Road of Sacrifices", 13300006)},
                { "Halfway Fortress", ("Halfway Fortress", 13300000)},
                { "Crucifixion Woods", ("Crucifixion Woods", 13300007)},
                { "Crystal Sage", ("Crystal Sage", 13300002)},
                { "Farron Keep", ("Farron Keep", 13300003)},
                { "Keep Ruins", ("Keep Ruins", 13300004)},
                { "Farron Keep Perimeter", ("Farron Keep Perimeter", 13300008)},
                { "Old Wolf of Farron", ("Old Wolf of Farron", 13300005)},
                { "Abyss Watchers", ("Abyss Watchers", 13300001)},
                { "Cathedral of the Deep", ("Cathedral of the Deep", 13500003)},
                { "Cleansing Chapel", ("Cleansing Chapel", 13500000)},
                { "Rosaria's Bed Chamber", ("Rosaria's Bed Chamber", 13500002)},
                { "Deacons of the Deep", ("Deacons of the Deep", 13500001)},
                { "Catacombs of Carthus", ("Catacombs of Carthus", 13800006)},
                { "High Lord Wolnir", ("High Lord Wolnir", 13800000)},
                { "Abandoned Tomb", ("Abandoned Tomb", 13800001)},
                { "Old King's Antechamber", ("Old King's Antechamber", 13800002)},
                { "Demon Ruins", ("Demon Ruins", 13800003)},
                { "Old Demon King", ("Old Demon King", 13800004)},
                { "Irithyll of the Boreal Valley", ("Irithyll of the Boreal Valley", 13700007)},
                { "Central Irithyll", ("Central Irithyll", 13700004)},
                { "Church of Yorshka", ("Church of Yorshka", 13700000)},
                { "Distant Manor", ("Distant Manor", 13700005)},
                { "Pontiff Sulyvahn", ("Pontiff Sulyvahn", 13700001)},
                { "Water Reserve", ("Water Reserve", 13700006)},
                { "Anor Londo", ("Anor Londo", 13700003)},
                { "Prison Tower", ("Prison Tower", 13700008)},
                { "Aldrich, Devourer of Gods", ("Aldrich, Devourer of Gods", 13700002)},
                { "Irithyll Dungeon", ("Irithyll Dungeon", 13900000)},
                { "Profaned Capital", ("Profaned Capital", 13900002)},
                { "Yhorm the Giant", ("Yhorm the Giant", 13900001)},
                { "Lothric Castle", ("Lothric Castle", 13010000)},
                { "Dragon Barracks", ("Dragon Barracks", 13010002)},
                { "Dragonslayer Armour", ("Dragonslayer Armour", 13010001)},
                { "Grand Archives", ("Grand Archives", 13410001)},
                { "Twin Princes", ("Twin Princes", 13410000)},
                { "Archdragon Peak", ("Archdragon Peak", 13200000)},
                { "Dragon-Kin Mausoleum", ("Dragon-Kin Mausoleum", 13200003)},
                { "Great Belfry", ("Great Belfry", 13200002)},
                { "Nameless King", ("Nameless King", 13200001)},
                { "Flameless Shrine", ("Flameless Shrine", 14100000)},
                { "Kiln of the First Flame", ("Kiln of the First Flame", 14100001)},
                { "Snowfield", ("Snowfield", 14500001)},
                { "Rope Bridge Cave", ("Rope Bridge Cave", 14500002)},
                { "Corvian Settlement", ("Corvian Settlement", 14500003)},
                { "Snowy Mountain Pass", ("Snowy Mountain Pass", 14500004)},
                { "Ariandel Chapel", ("Ariandel Chapel", 14500005)},
                { "Sister Friede", ("Sister Friede", 14500000)},
                { "Depths of the Painting", ("Depths of the Painting", 14500007)},
                { "Champion's Gravetender", ("Champion's Gravetender", 14500006)},
                { "The Dreg Heap", ("The Dreg Heap", 15000001)},
                { "Earthen Peak Ruins", ("Earthen Peak Ruins", 15000002)},
                { "Within the Earthen Peak Ruins", ("Within the Earthen Peak Ruins", 15000003)},
                { "The Demon Prince", ("The Demon Prince", 15000000)},
                { "Mausoleum Lookout", ("Mausoleum Lookout", 15100002)},
                { "Ringed Inner Wall", ("Ringed Inner Wall", 15100003)},
                { "Ringed City Streets", ("Ringed City Streets", 15100004)},
                { "Shared Grave", ("Shared Grave", 15100005)},
                { "Church of Filianore", ("Church of Filianore", 15100000)},
                { "Darkeater Midir", ("Darkeater Midir", 15100001)},
                { "Filianore's Rest", ("Filianore's Rest", 15110001)},
                { "Slave Knight Gael", ("Slave Knight Gael", 15110000)}
            };

            if (bonfireMapping.TryGetValue(Bonfire, out var BonfireInfo))
            {
                return new BonfireDs3 { Title = BonfireInfo.Title, Id = BonfireInfo.Id };
            }
            else
            {
                throw new ArgumentException($"Invalid boss string: {Bonfire}");
            }
        }
        #endregion
        #region Lvl.Ds3
        [Serializable]
        public class LvlDs3
        {
            public SoulMemory.DarkSouls3.Attribute Attribute;
            public uint Value;
            public bool IsSplited = false;
            public string Mode;
        }

        public SoulMemory.DarkSouls3.Attribute StringToEnumAttribute(string attribute)
        {
            switch (attribute)
            {
                case "Vigor":
                    return SoulMemory.DarkSouls3.Attribute.Vigor;
                case "Attunement":
                    return SoulMemory.DarkSouls3.Attribute.Attunement;
                case "Endurance":
                    return SoulMemory.DarkSouls3.Attribute.Endurance;
                case "Vitality":
                    return SoulMemory.DarkSouls3.Attribute.Vitality;
                case "Strength":
                    return SoulMemory.DarkSouls3.Attribute.Strength;
                case "Dexterity":
                    return SoulMemory.DarkSouls3.Attribute.Dexterity;
                case "Intelligence":
                    return SoulMemory.DarkSouls3.Attribute.Intelligence;
                case "Faith":
                    return SoulMemory.DarkSouls3.Attribute.Faith;
                case "Luck":
                    return SoulMemory.DarkSouls3.Attribute.Luck;
                case "SoulLevel":
                    return SoulMemory.DarkSouls3.Attribute.SoulLevel;
                case "Humanity":
                    return SoulMemory.DarkSouls3.Attribute.Humanity;
                default: return SoulMemory.DarkSouls3.Attribute.Luck;
            }
        }
        #endregion
        #region CustomFlag.Ds3
        public class CfDs3
        {
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
            public string Title;
        }
        #endregion
        #region Position.Ds3
        public class PositionDs3
        {
            public Vector3f vector = new Vector3f();
            public bool IsSplited = false;
            public string Mode;
            public string Title;
        }
        #endregion
    }

    [Serializable]
    public class DTDs3
    {
        //var Settings
        public bool enableSplitting = false;
        public bool autoTimer = false;
        public bool gameTimer = false;
        public int positionMargin = 5;
        public bool ResetIGTNG = false;
        //Flags to Split
        public List<DefinitionsDs3.BossDs3> bossToSplit = new List<DefinitionsDs3.BossDs3>();
        public List<DefinitionsDs3.BonfireDs3> bonfireToSplit = new List<DefinitionsDs3.BonfireDs3>();
        public List<DefinitionsDs3.LvlDs3> lvlToSplit = new List<DefinitionsDs3.LvlDs3>();
        public List<DefinitionsDs3.CfDs3> flagToSplit = new List<DefinitionsDs3.CfDs3>();
        public List<DefinitionsDs3.PositionDs3> positionsToSplit = new List<DefinitionsDs3.PositionDs3>();

        public List<DefinitionsDs3.BossDs3> GetBossToSplit() => this.bossToSplit;

        public List<DefinitionsDs3.BonfireDs3> GetBonfireToSplit() => this.bonfireToSplit;

        public List<DefinitionsDs3.LvlDs3> GetLvlToSplit() => this.lvlToSplit;

        public List<DefinitionsDs3.CfDs3> GetFlagToSplit() => this.flagToSplit;

        public List<DefinitionsDs3.PositionDs3> GetPositionsToSplit() => this.positionsToSplit;
    }
}

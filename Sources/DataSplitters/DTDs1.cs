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
using SoulMemory;
using SoulMemory.DarkSouls1;

namespace AutoSplitterCore
{
    public class DefinitionsDs1
    {
        #region Boss.Ds1
        [Serializable]
        public class BossDs1
        {
            public string Title;
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
        }

        public BossDs1 StringToEnumBoss(string boss)
        {
            var bossMapping = new Dictionary<string, (string Title, uint Id)>
            {
                { "Asylum Demon", ("Asylum Demon", 16)},
                { "Bell Gargoyle", ("Bell Gargoyle", 3)},
                { "Capra Demon", ("Capra Demon", 11010902)},
                { "Ceaseless Discharge", ("Ceaseless Discharge", 11410900)},
                { "Centipede Demon", ("Centipede Demon", 11410901)},
                { "Chaos Witch Quelaag", ("Chaos Witch Quelaag", 9)},
                { "Crossbreed Priscilla", ("Crossbreed Priscilla", 4)},
                { "Dark Sun Gwyndolin", ("Dark Sun Gwyndolin", 11510900)},
                { "Demon Firesage", ("Demon Firesage", 11410410)},
                { "Four Kings", ("Four Kings", 13)},
                { "Gaping Dragon", ("Gaping Dragon", 2)},
                { "Great Grey Wolf Sif", ("Great Grey Wolf Sif", 5)},
                { "Gwyn Lord of Cinder", ("Gwyn Lord of Cinder", 15)},
                { "Iron Golem", ("Iron Golem", 11)},
                { "Moonlight Butterfly", ("Moonlight Butterfly", 11200900)},
                { "Nito", ("Nito", 7)},
                { "Ornstein And Smough", ("Ornstein And Smough", 12)},
                { "Pinwheel", ("Pinwheel", 6)},
                { "Seath the Scaleless", ("Seath the Scaleless", 14)},
                { "Stray Demon", ("Stray Demon", 11810900)},
                { "Taurus Demon", ("Taurus Demon", 11010901)},
                { "The Bed of Chaos", ("The Bed of Chaos", 10)},
                { "Artorias the Abysswalker", ("Artorias the Abysswalker", 11210001)},
                { "Black Dragon Kalameet", ("Black Dragon Kalameet", 11210004)},
                { "Manus, Father of the Abyss", ("Manus, Father of the Abyss", 11210002)},
                { "Sanctuary Guardian", ("Sanctuary Guardian", 11210000)}
            };

            if (bossMapping.TryGetValue(boss, out var bossInfo))
            {
                return new BossDs1 { Title = bossInfo.Title, Id = bossInfo.Id };
            }
            else
            {
                throw new ArgumentException($"Invalid boss string: {boss}");
            }
        }
        #endregion
        #region Bonfire.Ds1
        public class BonfireDs1
        {
            public string Title;
            public Bonfire Id;
            public BonfireState Value;
            public bool IsSplited = false;
            public string Mode;
        }

        public BonfireState StringtoEnumBonfireState(string state)
        {
            switch (state)
            {
                case "Discovered": return BonfireState.Discovered;
                case "Unlocked (R)": return BonfireState.Unlocked;
                case "Kindled 1": return BonfireState.Kindled1;
                case "Kindled 2": return BonfireState.Kindled2;
                case "Kindled 3": return BonfireState.Kindled3;
                default: return BonfireState.Unknown;
            }
        }
        public BonfireDs1 StringToEnumBonfire(string bonfire)
        {
            var bonfireMapping = new Dictionary<string, (string Title, Bonfire Id)>
            {
                { "Undead Asylum - Courtyard", ("Undead Asylum - Courtyard", Bonfire.UndeadAsylumCourtyard)},
                { "Undead Asylum - Interior", ("Undead Asylum - Interior", Bonfire.UndeadAsylumInterior)},
                { "Firelink Shrine", ("Firelink Shrine", Bonfire.FirelinkShrine)},
                { "Firelink Altar - Lordvessel", ("Firelink Altar - Lordvessel", Bonfire.FirelinkAltarLordvessel)},
                { "Undead Burg", ("Undead Burg", Bonfire.UndeadBurg)},
                { "Undead Burg - Sunlight Altar", ("Undead Burg - Sunlight Altar", Bonfire.UndeadBurgSunlightAltar)},
                { "Undead Parish", ("Undead Parish", Bonfire.UndeadParishAndre)},
                { "Darkroot Garden", ("Darkroot Garden", Bonfire.DarkrootGarden)},
                { "Darkroot Basin", ("Darkroot Basin", Bonfire.DarkrootBasin)},
                { "Depths", ("Depths", Bonfire.Depths)},
                { "Blighttown Catwalk", ("Blighttown Catwalk", Bonfire.BlighttownCatwalk)},
                { "Blighttown Swap", ("Blighttown Swap", Bonfire.BlighttownSwamp)},
                { "Quelaag's Domain - DaughterOfChaos", ("Quelaag's Domain - DaughterOfChaos", Bonfire.DaughterOfChaos)},
                { "The Great Hollow", ("The Great Hollow", Bonfire.TheGreatHollow)},
                { "Ash Lake", ("Ash Lake", Bonfire.AshLake)},
                { "Ash Lake - Stone Dragon", ("Ash Lake - Stone Dragon", Bonfire.AshLakeDragon)},
                { "Demon Ruins - Entrance", ("Demon Ruins - Entrance", Bonfire.DemonRuinsEntrance)},
                { "Demon Ruins - Staircase", ("Demon Ruins - Staircase", Bonfire.DemonRuinsStaircase)},
                { "Demon Ruins - Catacombs", ("Demon Ruins - Catacombs", Bonfire.DemonRuinsCatacombs)},
                { "Lost Izalith - Lava Pits", ("Lost Izalith - Lava Pits", Bonfire.LostIzalithLavaPits)},
                { "Lost Izalith - 2 (illusory wall)", ("Lost Izalith - 2 (illusory wall)", Bonfire.LostIzalith2)},
                { "Lost Izalith Heart of Chaos", ("Lost Izalith Heart of Chaos", Bonfire.LostIzalithHeartOfChaos)},
                { "Sen's Fortress", ("Sen's Fortress", Bonfire.SensFortress)},
                { "Anor Londo", ("Anor Londo", Bonfire.AnorLondo)},
                { "Anor Londo Darkmoon Tomb", ("Anor Londo Darkmoon Tomb", Bonfire.AnorLondoDarkmoonTomb)},
                { "Anor Londo Residence", ("Anor Londo Residence", Bonfire.AnorLondoResidence)},
                { "Anor Londo Chamber of the Princess", ("Anor Londo Chamber of the Princess", Bonfire.AnorLondoChamberOfThePrincess)},
                { "Painted World of Ariamis", ("Painted World of Ariamis", Bonfire.PaintedWorldOfAriamis)},
                { "The Duke's Archives 1 (entrance)", ("The Duke's Archives 1 (entrance)", Bonfire.DukesArchives1)},
                { "The Duke's Archives 2 (prison cell)", ("The Duke's Archives 2 (prison cell)", Bonfire.DukesArchives2)},
                { "The Duke's Archives 3 (balcony)", ("The Duke's Archives 3 (balcony)", Bonfire.DukesArchives3)},
                { "Crystal Cave", ("Crystal Cave", Bonfire.CrystalCave)},
                { "Catacombs 1 (necromancer)", ("Catacombs 1 (necromancer)", Bonfire.Catacombs1)},
                { "Catacombs 2 (illusory wall)", ("Catacombs 2 (illusory wall)", Bonfire.Catacombs2)},
                { "Tomb of the Giants - 1 (patches)", ("Tomb of the Giants - 1 (patches)", Bonfire.TombOfTheGiantsPatches)},
                { "Tomb of the Giants - 2", ("Tomb of the Giants - 2", Bonfire.TombOfTheGiants2)},
                { "Tomb of the Giants - Altar of the Gravelord", ("Tomb of the Giants - Altar of the Gravelord", Bonfire.TombOfTheGiantsAltarOfTheGravelord)},
                { "The Abyss", ("The Abyss", Bonfire.TheAbyss)},
                { "Oolacile - Sanctuary Garden", ("Oolacile - Sanctuary Garden", Bonfire.OolacileSanctuaryGarden)},
                { "Oolacile - Sanctuary", ("Oolacile - Sanctuary", Bonfire.OolacileSanctuary)},
                { "Oolacile - Township", ("Oolacile - Township", Bonfire.OolacileTownship)},
                { "Oolacile - Township Dungeon", ("Oolacile - Township Dungeon", Bonfire.OolacileTownshipDungeon)},
                { "Chasm of the Abyss", ("Chasm of the Abyss", Bonfire.ChasmOfTheAbyss)}
            };

            if (bonfireMapping.TryGetValue(bonfire, out var bonfireInfo))
            {
                return new BonfireDs1 { Title = bonfireInfo.Title, Id = bonfireInfo.Id };
            }
            else
            {
                throw new ArgumentException($"Invalid bonfire string: {bonfire}");
            }

        }
        #endregion
        #region Lvl.Ds1
        public class LvlDs1
        {
            public SoulMemory.DarkSouls1.Attribute Attribute;
            public uint Value;
            public bool IsSplited = false;
            public string Mode;
        }
        
        public SoulMemory.DarkSouls1.Attribute StringToEnumAttribute(string attribute)
        {
            LvlDs1 cLvl = new LvlDs1();
            switch (attribute)
            {

                case "Vitality":
                    return SoulMemory.DarkSouls1.Attribute.Vitality;
                case "Attunement":
                    return SoulMemory.DarkSouls1.Attribute.Attunement;
                case "Endurance":
                    return SoulMemory.DarkSouls1.Attribute.Endurance;
                case "Strength":
                    return SoulMemory.DarkSouls1.Attribute.Strength;
                case "Dexterity":
                    return SoulMemory.DarkSouls1.Attribute.Dexterity;
                case "Resistance":
                    return SoulMemory.DarkSouls1.Attribute.Resistance;
                case "Intelligence":
                    return SoulMemory.DarkSouls1.Attribute.Intelligence;
                case "Faith":
                    return SoulMemory.DarkSouls1.Attribute.Faith;
                case "SoulLevel":
                    return SoulMemory.DarkSouls1.Attribute.SoulLevel;
                case "Humanity":
                    return SoulMemory.DarkSouls1.Attribute.Humanity;
                default: return SoulMemory.DarkSouls1.Attribute.SoulLevel;
            }
        }
        #endregion
        #region Position.Ds1
        public class PositionDs1
        {
            public Vector3f vector = new Vector3f();
            public bool IsSplited = false;
            public string Mode;
            public string Title;
        }
        #endregion
        #region Items.Ds1
        public class ItemDs1
        {
            public string Title;
            public uint Id;
            public bool IsSplited = false;
            public string Mode;
        }

        public ItemDs1 StringToEnumItem(string item)
        {
           var itemMapping = new Dictionary<string, (string Title, uint Id)> 
           {
                { "Catarina Helm", ("Catarina Helm", 10000)},
                { "Catarina Armor", ("Catarina Armor", 11000)},
                { "Catarina Gauntlets", ("Catarina Gauntlets", 12000)},
                { "Catarina Leggings", ("Catarina Leggings", 13000)},
                { "Paladin Helm", ("Paladin Helm", 20000)},
                { "Paladin Armor", ("Paladin Armor", 21000)},
                { "Paladin Gauntlets", ("Paladin Gauntlets", 22000)},
                { "Paladin Leggings", ("Paladin Leggings", 23000)},
                { "Dark Mask", ("Dark Mask", 40000)},
                { "Dark Armor", ("Dark Armor", 41000)},
                { "Dark Gauntlets", ("Dark Gauntlets", 42000)},
                { "Dark Leggings", ("Dark Leggings", 43000)},
                { "Brigand Hood", ("Brigand Hood", 50000)},
                { "Brigand Armor", ("Brigand Armor", 51000)},
                { "Brigand Gauntlets", ("Brigand Gauntlets", 52000)},
                { "Brigand Trousers", ("Brigand Trousers", 53000)},
                { "Shadow Mask", ("Shadow Mask", 60000)},
                { "Shadow Garb", ("Shadow Garb", 61000)},
                { "Shadow Gauntlets", ("Shadow Gauntlets", 62000)},
                { "Shadow Leggings", ("Shadow Leggings", 63000)},
                { "Black Iron Helm", ("Black Iron Helm", 70000)},
                { "Black Iron Armor", ("Black Iron Armor", 71000)},
                { "Black Iron Gauntlets", ("Black Iron Gauntlets", 72000)},
                { "Black Iron Leggings", ("Black Iron Leggings", 73000)},
                { "Smough's Helm", ("Smough's Helm", 80000)},
                { "Smough's Armor", ("Smough's Armor", 81000)},
                { "Smough's Gauntlets", ("Smough's Gauntlets", 82000)},
                { "Smough's Leggings", ("Smough's Leggings", 83000)},
                { "Six-Eyed Helm of the Channelers", ("Six-Eyed Helm of the Channelers", 90000)},
                { "Robe of the Channelers", ("Robe of the Channelers", 91000)},
                { "Gauntlets of the Channelers", ("Gauntlets of the Channelers", 92000)},
                { "Waistcloth of the Channelers", ("Waistcloth of the Channelers", 93000)},
                { "Helm of Favor", ("Helm of Favor", 100000)},
                { "Embraced Armor of Favor", ("Embraced Armor of Favor", 101000)},
                { "Gauntlets of Favor", ("Gauntlets of Favor", 102000)},
                { "Leggings of Favor", ("Leggings of Favor", 103000)},
                { "Helm of the Wise", ("Helm of the Wise", 110000)},
                { "Armor of the Glorious", ("Armor of the Glorious", 111000)},
                { "Gauntlets of the Vanquisher", ("Gauntlets of the Vanquisher", 112000)},
                { "Boots of the Explorer", ("Boots of the Explorer", 113000)},
                { "Stone Helm", ("Stone Helm", 120000)},
                { "Stone Armor", ("Stone Armor", 121000)},
                { "Stone Gauntlets", ("Stone Gauntlets", 122000)},
                { "Stone Leggings", ("Stone Leggings", 123000)},
                { "Crystalline Helm", ("Crystalline Helm", 130000)},
                { "Crystalline Armor", ("Crystalline Armor", 131000)},
                { "Crystalline Gauntlets", ("Crystalline Gauntlets", 132000)},
                { "Crystalline Leggings", ("Crystalline Leggings", 133000)},
                { "Mask of the Sealer", ("Mask of the Sealer", 140000)},
                { "Crimson Robe", ("Crimson Robe", 141000)},
                { "Crimson Gloves", ("Crimson Gloves", 142000)},
                { "Crimson Waistcloth", ("Crimson Waistcloth", 143000)},
                { "Mask of Velka", ("Mask of Velka", 150000)},
                { "Black Cleric Robe", ("Black Cleric Robe", 151000)},
                { "Black Manchette", ("Black Manchette", 152000)},
                { "Black Tights", ("Black Tights", 153000)},
                { "Iron Helm", ("Iron Helm", 160000)},
                { "Armor of the Sun", ("Armor of the Sun", 161000)},
                { "Iron Bracelet", ("Iron Bracelet", 162000)},
                { "Iron Leggings", ("Iron Leggings", 163000)},
                { "Chain Helm", ("Chain Helm", 170000)},
                { "Chain Armor", ("Chain Armor", 171000)},
                { "Leather Gauntlets", ("Leather Gauntlets", 172000)},
                { "Chain Leggings", ("Chain Leggings", 173000)},
                { "Cleric Helm", ("Cleric Helm", 180000)},
                { "Cleric Armor", ("Cleric Armor", 181000)},
                { "Cleric Gauntlets", ("Cleric Gauntlets", 182000)},
                { "Cleric Leggings", ("Cleric Leggings", 183000)},
                { "Sunlight Maggot", ("Sunlight Maggot", 190000)},
                { "Helm of Thorns", ("Helm of Thorns", 200000)},
                { "Armor of Thorns", ("Armor of Thorns", 201000)},
                { "Gauntlets of Thorns", ("Gauntlets of Thorns", 202000)},
                { "Leggings of Thorns", ("Leggings of Thorns", 203000)},
                { "Standard Helm", ("Standard Helm", 210000)},
                { "Hard Leather Armor", ("Hard Leather Armor", 211000)},
                { "Hard Leather Gauntlets", ("Hard Leather Gauntlets", 212000)},
                { "Hard Leather Boots", ("Hard Leather Boots", 213000)},
                { "Sorcerer Hat", ("Sorcerer Hat", 220000)},
                { "Sorcerer Cloak", ("Sorcerer Cloak", 221000)},
                { "Sorcerer Gauntlets", ("Sorcerer Gauntlets", 222000)},
                { "Sorcerer Boots", ("Sorcerer Boots", 223000)},
                { "Tattered Cloth Hood", ("Tattered Cloth Hood", 230000)},
                { "Tattered Cloth Robe", ("Tattered Cloth Robe", 231000)},
                { "Tattered Cloth Manchette", ("Tattered Cloth Manchette", 232000)},
                { "Heavy Boots", ("Heavy Boots", 233000)},
                { "Pharis's Hat", ("Pharis's Hat", 240000)},
                { "Leather Armor", ("Leather Armor", 241000)},
                { "Leather Gloves", ("Leather Gloves", 242000)},
                { "Leather Boots", ("Leather Boots", 243000)},
                { "Painting Guardian Hood", ("Painting Guardian Hood", 250000)},
                { "Painting Guardian Robe", ("Painting Guardian Robe", 251000)},
                { "Painting Guardian Gloves", ("Painting Guardian Gloves", 252000)},
                { "Painting Guardian Waistcloth", ("Painting Guardian Waistcloth", 253000)},
                { "Ornstein's Helm", ("Ornstein's Helm", 270000)},
                { "Ornstein's Armor", ("Ornstein's Armor", 271000)},
                { "Ornstein's Gauntlets", ("Ornstein's Gauntlets", 272000)},
                { "Ornstein's Leggings", ("Ornstein's Leggings", 273000)},
                { "Eastern Helm", ("Eastern Helm", 280000)},
                { "Eastern Armor", ("Eastern Armor", 281000)},
                { "Eastern Gauntlets", ("Eastern Gauntlets", 282000)},
                { "Eastern Leggings", ("Eastern Leggings", 283000)},
                { "Xanthous Crown", ("Xanthous Crown", 290000)},
                { "Xanthous Overcoat", ("Xanthous Overcoat", 291000)},
                { "Xanthous Gloves", ("Xanthous Gloves", 292000)},
                { "Xanthous Waistcloth", ("Xanthous Waistcloth", 293000)},
                { "Thief Mask", ("Thief Mask", 300000)},
                { "Black Leather Armor", ("Black Leather Armor", 301000)},
                { "Black Leather Gloves", ("Black Leather Gloves", 302000)},
                { "Black Leather Boots", ("Black Leather Boots", 303000)},
                { "Priest's Hat", ("Priest's Hat", 310000)},
                { "Holy Robe", ("Holy Robe", 311000)},
                { "Traveling Gloves (Holy)", ("Traveling Gloves (Holy)", 312000)},
                { "Holy Trousers", ("Holy Trousers", 313000)},
                { "Black Knight Helm", ("Black Knight Helm", 320000)},
                { "Black Knight Armor", ("Black Knight Armor", 321000)},
                { "Black Knight Gauntlets", ("Black Knight Gauntlets", 322000)},
                { "Black Knight Leggings", ("Black Knight Leggings", 323000)},
                { "Crown of Dusk", ("Crown of Dusk", 330000)},
                { "Antiquated Dress", ("Antiquated Dress", 331000)},
                { "Antiquated Gloves", ("Antiquated Gloves", 332000)},
                { "Antiquated Skirt", ("Antiquated Skirt", 333000)},
                { "Witch Hat", ("Witch Hat", 340000)},
                { "Witch Cloak", ("Witch Cloak", 341000)},
                { "Witch Gloves", ("Witch Gloves", 342000)},
                { "Witch Skirt", ("Witch Skirt", 343000)},
                { "Elite Knight Helm", ("Elite Knight Helm", 350000)},
                { "Elite Knight Armor", ("Elite Knight Armor", 351000)},
                { "Elite Knight Gauntlets", ("Elite Knight Gauntlets", 352000)},
                { "Elite Knight Leggings", ("Elite Knight Leggings", 353000)},
                { "Wanderer Hood", ("Wanderer Hood", 360000)},
                { "Wanderer Coat", ("Wanderer Coat", 361000)},
                { "Wanderer Manchette", ("Wanderer Manchette", 362000)},
                { "Wanderer Boots", ("Wanderer Boots", 363000)},
                { "Big Hat", ("Big Hat", 380000)},
                { "Sage Robe", ("Sage Robe", 381000)},
                { "Traveling Gloves (Sage)", ("Traveling Gloves (Sage)", 382000)},
                { "Traveling Boots", ("Traveling Boots", 383000)},
                { "Knight Helm", ("Knight Helm", 390000)},
                { "Knight Armor", ("Knight Armor", 391000)},
                { "Knight Gauntlets", ("Knight Gauntlets", 392000)},
                { "Knight Leggings", ("Knight Leggings", 393000)},
                { "Dingy Hood", ("Dingy Hood", 400000)},
                { "Dingy Robe", ("Dingy Robe", 401000)},
                { "Dingy Gloves", ("Dingy Gloves", 402000)},
                { "Blood-Stained Skirt", ("Blood-Stained Skirt", 403000)},
                { "Maiden Hood", ("Maiden Hood", 410000)},
                { "Maiden Robe", ("Maiden Robe", 411000)},
                { "Maiden Gloves", ("Maiden Gloves", 412000)},
                { "Maiden Skirt", ("Maiden Skirt", 413000)},
                { "Silver Knight Helm", ("Silver Knight Helm", 420000)},
                { "Silver Knight Armor", ("Silver Knight Armor", 421000)},
                { "Silver Knight Gauntlets", ("Silver Knight Gauntlets", 422000)},
                { "Silver Knight Leggings", ("Silver Knight Leggings", 423000)},
                { "Havel's Helm", ("Havel's Helm", 440000)},
                { "Havel's Armor", ("Havel's Armor", 441000)},
                { "Havel's Gauntlets", ("Havel's Gauntlets", 442000)},
                { "Havel's Leggings", ("Havel's Leggings", 443000)},
                { "Brass Helm", ("Brass Helm", 450000)},
                { "Brass Armor", ("Brass Armor", 451000)},
                { "Brass Gauntlets", ("Brass Gauntlets", 452000)},
                { "Brass Leggings", ("Brass Leggings", 453000)},
                { "Gold-Hemmed Black Hood", ("Gold-Hemmed Black Hood", 460000)},
                { "Gold-Hemmed Black Cloak", ("Gold-Hemmed Black Cloak", 461000)},
                { "Gold-Hemmed Black Gloves", ("Gold-Hemmed Black Gloves", 462000)},
                { "Gold-Hemmed Black Skirt", ("Gold-Hemmed Black Skirt", 463000)},
                { "Golem Helm", ("Golem Helm", 470000)},
                { "Golem Armor", ("Golem Armor", 471000)},
                { "Golem Gauntlets", ("Golem Gauntlets", 472000)},
                { "Golem Leggings", ("Golem Leggings", 473000)},
                { "Hollow Soldier Helm", ("Hollow Soldier Helm", 480000)},
                { "Hollow Soldier Armor", ("Hollow Soldier Armor", 481000)},
                { "Hollow Soldier Waistcloth", ("Hollow Soldier Waistcloth", 483000)},
                { "Steel Helm", ("Steel Helm", 490000)},
                { "Steel Armor", ("Steel Armor", 491000)},
                { "Steel Gauntlets", ("Steel Gauntlets", 492000)},
                { "Steel Leggings", ("Steel Leggings", 493000)},
                { "Hollow Thief's Hood", ("Hollow Thief's Hood", 500000)},
                { "Hollow Thief's Leather Armor", ("Hollow Thief's Leather Armor", 501000)},
                { "Hollow Thief's Tights", ("Hollow Thief's Tights", 503000)},
                { "Balder Helm", ("Balder Helm", 510000)},
                { "Balder Armor", ("Balder Armor", 511000)},
                { "Balder Gauntlets", ("Balder Gauntlets", 512000)},
                { "Balder Leggings", ("Balder Leggings", 513000)},
                { "Hollow Warrior Helm", ("Hollow Warrior Helm", 520000)},
                { "Hollow Warrior Armor", ("Hollow Warrior Armor", 521000)},
                { "Hollow Warrior Waistcloth", ("Hollow Warrior Waistcloth", 523000)},
                { "Giant Helm", ("Giant Helm", 530000)},
                { "Giant Armor", ("Giant Armor", 531000)},
                { "Giant Gauntlets", ("Giant Gauntlets", 532000)},
                { "Giant Leggings", ("Giant Leggings", 533000)},
                { "Crown of the Dark Sun", ("Crown of the Dark Sun", 540000)},
                { "Moonlight Robe", ("Moonlight Robe", 541000)},
                { "Moonlight Gloves", ("Moonlight Gloves", 542000)},
                { "Moonlight Waistcloth", ("Moonlight Waistcloth", 543000)},
                { "Crown of the Great Lord", ("Crown of the Great Lord", 550000)},
                { "Robe of the Great Lord", ("Robe of the Great Lord", 551000)},
                { "Bracelet of the Great Lord", ("Bracelet of the Great Lord", 552000)},
                { "Anklet of the Great Lord", ("Anklet of the Great Lord", 553000)},
                { "Sack", ("Sack", 560000)},
                { "Symbol of Avarice", ("Symbol of Avarice", 570000)},
                { "Royal Helm", ("Royal Helm", 580000)},
                { "Mask of the Father", ("Mask of the Father", 590000)},
                { "Mask of the Mother", ("Mask of the Mother", 600000)},
                { "Mask of the Child", ("Mask of the Child", 610000)},
                { "Fang Boar Helm", ("Fang Boar Helm", 620000)},
                { "Gargoyle Helm", ("Gargoyle Helm", 630000)},
                { "Black Sorcerer Hat", ("Black Sorcerer Hat", 640000)},
                { "Black Sorcerer Cloak", ("Black Sorcerer Cloak", 641000)},
                { "Black Sorcerer Gauntlets", ("Black Sorcerer Gauntlets", 642000)},
                { "Black Sorcerer Boots", ("Black Sorcerer Boots", 643000)},
                { "Helm of Artorias", ("Helm of Artorias", 660000)},
                { "Armor of Artorias", ("Armor of Artorias", 661000)},
                { "Gauntlets of Artorias", ("Gauntlets of Artorias", 662000)},
                { "Leggings of Artorias", ("Leggings of Artorias", 663000)},
                { "Porcelain Mask", ("Porcelain Mask", 670000)},
                { "Lord's Blade Robe", ("Lord's Blade Robe", 671000)},
                { "Lord's Blade Gloves", ("Lord's Blade Gloves", 672000)},
                { "Lord's Blade Waistcloth", ("Lord's Blade Waistcloth", 673000)},
                { "Gough's Helm", ("Gough's Helm", 680000)},
                { "Gough's Armor", ("Gough's Armor", 681000)},
                { "Gough's Gauntlets", ("Gough's Gauntlets", 682000)},
                { "Gough's Leggings", ("Gough's Leggings", 683000)},
                { "Guardian Helm", ("Guardian Helm", 690000)},
                { "Guardian Armor", ("Guardian Armor", 691000)},
                { "Guardian Gauntlets", ("Guardian Gauntlets", 692000)},
                { "Guardian Leggings", ("Guardian Leggings", 693000)},
                { "Snickering Top Hat", ("Snickering Top Hat", 700000)},
                { "Chester's Long Coat", ("Chester's Long Coat", 701000)},
                { "Chester's Gloves", ("Chester's Gloves", 702000)},
                { "Chester's Trousers", ("Chester's Trousers", 703000)},
                { "Bloated Head", ("Bloated Head", 710000)},
                { "Bloated Sorcerer Head", ("Bloated Sorcerer Head", 720000)},
                { "Eye of Death", ("Eye of Death", 109)},
                { "Cracked Red Eye Orb", ("Cracked Red Eye Orb", 111)},
                { "Estus Flask", ("Estus Flask", 200)},
                { "Elizabeth's Mushroom", ("Elizabeth's Mushroom", 230)},
                { "Divine Blessing", ("Divine Blessing", 240)},
                { "Green Blossom", ("Green Blossom", 260)},
                { "Bloodred Moss Clump", ("Bloodred Moss Clump", 270)},
                { "Purple Moss Clump", ("Purple Moss Clump", 271)},
                { "Blooming Purple Moss Clump", ("Blooming Purple Moss Clump", 272)},
                { "Purging Stone", ("Purging Stone", 274)},
                { "Egg Vermifuge", ("Egg Vermifuge", 275)},
                { "Repair Powder", ("Repair Powder", 280)},
                { "Throwing Knife", ("Throwing Knife", 290)},
                { "Poison Throwing Knife", ("Poison Throwing Knife", 291)},
                { "Firebomb", ("Firebomb", 292)},
                { "Dung Pie", ("Dung Pie", 293)},
                { "Alluring Skull", ("Alluring Skull", 294)},
                { "Lloyd's Talisman", ("Lloyd's Talisman", 296)},
                { "Black Firebomb", ("Black Firebomb", 297)},
                { "Charcoal Pine Resin", ("Charcoal Pine Resin", 310)},
                { "Gold Pine Resin", ("Gold Pine Resin", 311)},
                { "Transient Curse", ("Transient Curse", 312)},
                { "Rotten Pine Resin", ("Rotten Pine Resin", 313)},
                { "Homeward Bone", ("Homeward Bone", 330)},
                { "Prism Stone", ("Prism Stone", 370)},
                { "Indictment", ("Indictment", 373)},
                { "Souvenir of Reprisal", ("Souvenir of Reprisal", 374)},
                { "Sunlight Medal", ("Sunlight Medal", 375)},
                { "Pendant", ("Pendant", 376)},
                { "Rubbish", ("Rubbish", 380)},
                { "Copper Coin", ("Copper Coin", 381)},
                { "Silver Coin", ("Silver Coin", 382)},
                { "Gold Coin", ("Gold Coin", 383)},
                { "Fire Keeper Soul (Anastacia of Astora)", ("Fire Keeper Soul (Anastacia of Astora)", 390)},
                { "Fire Keeper Soul (Darkmoon Knightess)", ("Fire Keeper Soul (Darkmoon Knightess)", 391)},
                { "Fire Keeper Soul (Daughter of Chaos)", ("Fire Keeper Soul (Daughter of Chaos)", 392)},
                { "Fire Keeper Soul (New Londo)", ("Fire Keeper Soul (New Londo)", 393)},
                { "Fire Keeper Soul (Blighttown)", ("Fire Keeper Soul (Blighttown)", 394)},
                { "Fire Keeper Soul (Duke's Archives)", ("Fire Keeper Soul (Duke's Archives)", 395)},
                { "Fire Keeper Soul (Undead Parish)", ("Fire Keeper Soul (Undead Parish)", 396)},
                { "Soul of a Lost Undead", ("Soul of a Lost Undead", 400)},
                { "Large Soul of a Lost Undead", ("Large Soul of a Lost Undead", 401)},
                { "Soul of a Nameless Soldier", ("Soul of a Nameless Soldier", 402)},
                { "Large Soul of a Nameless Soldier", ("Large Soul of a Nameless Soldier", 403)},
                { "Soul of a Proud Knight", ("Soul of a Proud Knight", 404)},
                { "Large Soul of a Proud Knight", ("Large Soul of a Proud Knight", 405)},
                { "Soul of a Brave Warrior", ("Soul of a Brave Warrior", 406)},
                { "Large Soul of a Brave Warrior", ("Large Soul of a Brave Warrior", 407)},
                { "Soul of a Hero", ("Soul of a Hero", 408)},
                { "Soul of a Great Hero", ("Soul of a Great Hero", 409)},
                { "Humanity", ("Humanity", 500)},
                { "Twin Humanities", ("Twin Humanities", 501)},
                { "Soul of Quelaag", ("Soul of Quelaag", 700)},
                { "Soul of Sif", ("Soul of Sif", 701)},
                { "Soul of Gwyn, Lord of Cinder", ("Soul of Gwyn, Lord of Cinder", 702)},
                { "Core of an Iron Golem", ("Core of an Iron Golem", 703)},
                { "Soul of Ornstein", ("Soul of Ornstein", 704)},
                { "Soul of the Moonlight Butterfly", ("Soul of the Moonlight Butterfly", 705)},
                { "Soul of Smough", ("Soul of Smough", 706)},
                { "Soul of Priscilla", ("Soul of Priscilla", 707)},
                { "Soul of Gwyndolin", ("Soul of Gwyndolin", 708)},
                { "Guardian Soul", ("Guardian Soul", 709)},
                { "Soul of Artorias", ("Soul of Artorias", 710)},
                { "Soul of Manus", ("Soul of Manus", 711)},
                { "Peculiar Doll", ("Peculiar Doll", 384)},
                { "Basement Key", ("Basement Key", 2001)},
                { "Crest of Artorias", ("Crest of Artorias", 2002)},
                { "Cage Key", ("Cage Key", 2003)},
                { "Archive Tower Cell Key", ("Archive Tower Cell Key", 2004)},
                { "Archive Tower Giant Door Key", ("Archive Tower Giant Door Key", 2005)},
                { "Archive Tower Giant Cell Key", ("Archive Tower Giant Cell Key", 2006)},
                { "Blighttown Key", ("Blighttown Key", 2007)},
                { "Key to New Londo Ruins", ("Key to New Londo Ruins", 2008)},
                { "Annex Key", ("Annex Key", 2009)},
                { "Dungeon Cell Key", ("Dungeon Cell Key", 2010)},
                { "Big Pilgrim's Key", ("Big Pilgrim's Key", 2011)},
                { "Undead Asylum F2 East Key", ("Undead Asylum F2 East Key", 2012)},
                { "Key to the Seal", ("Key to the Seal", 2013)},
                { "Key to Depths", ("Key to Depths", 2014)},
                { "Undead Asylum F2 West Key", ("Undead Asylum F2 West Key", 2016)},
                { "Mystery Key", ("Mystery Key", 2017)},
                { "Sewer Chamber Key", ("Sewer Chamber Key", 2018)},
                { "Watchtower Basement Key", ("Watchtower Basement Key", 2019)},
                { "Archive Prison Extra Key", ("Archive Prison Extra Key", 2020)},
                { "Residence Key", ("Residence Key", 2021)},
                { "Crest Key", ("Crest Key", 2022)},
                { "Master Key", ("Master Key", 2100)},
                { "Lord Soul (Nito)", ("Lord Soul (Nito)", 2500)},
                { "Lord Soul (Bed of Chaos)", ("Lord Soul (Bed of Chaos)", 2501)},
                { "Bequeathed Lord Soul Shard (Four Kings)", ("Bequeathed Lord Soul Shard (Four Kings)", 2502)},
                { "Bequeathed Lord Soul Shard (Seath)", ("Bequeathed Lord Soul Shard (Seath)", 2503)},
                { "Lordvessel", ("Lordvessel", 2510)},
                { "Broken Pendant", ("Broken Pendant", 2520)},
                { "Weapon Smithbox", ("Weapon Smithbox", 2600)},
                { "Armor Smithbox", ("Armor Smithbox", 2601)},
                { "Repairbox", ("Repairbox", 2602)},
                { "Rite of Kindling", ("Rite of Kindling", 2607)},
                { "Bottomless Box", ("Bottomless Box", 2608)},
                { "Dagger", ("Dagger", 100000)},
                { "Parrying Dagger", ("Parrying Dagger", 101000)},
                { "Ghost Blade", ("Ghost Blade", 102000)},
                { "Bandit's Knife", ("Bandit's Knife", 103000)},
                { "Priscilla's Dagger", ("Priscilla's Dagger", 104000)},
                { "Shortsword", ("Shortsword", 200000)},
                { "Longsword", ("Longsword", 201000)},
                { "Broadsword", ("Broadsword", 202000)},
                { "Broken Straight Sword", ("Broken Straight Sword", 203000)},
                { "Balder Side Sword", ("Balder Side Sword", 204000)},
                { "Crystal Straight Sword", ("Crystal Straight Sword", 205000)},
                { "Sunlight Straight Sword", ("Sunlight Straight Sword", 206000)},
                { "Barbed Straight Sword", ("Barbed Straight Sword", 207000)},
                { "Silver Knight Straight Sword", ("Silver Knight Straight Sword", 208000)},
                { "Astora's Straight Sword", ("Astora's Straight Sword", 209000)},
                { "Darksword", ("Darksword", 210000)},
                { "Drake Sword", ("Drake Sword", 211000)},
                { "Straight Sword Hilt", ("Straight Sword Hilt", 212000)},
                { "Bastard Sword", ("Bastard Sword", 300000)},
                { "Claymore", ("Claymore", 301000)},
                { "Man-serpent Greatsword", ("Man-serpent Greatsword", 302000)},
                { "Flamberge", ("Flamberge", 303000)},
                { "Crystal Greatsword", ("Crystal Greatsword", 304000)},
                { "Stone Greatsword", ("Stone Greatsword", 306000)},
                { "Greatsword of Artorias", ("Greatsword of Artorias", 307000)},
                { "Moonlight Greatsword", ("Moonlight Greatsword", 309000)},
                { "Black Knight Sword", ("Black Knight Sword", 310000)},
                { "Greatsword of Artorias (Cursed)", ("Greatsword of Artorias (Cursed)", 311000)},
                { "Great Lord Greatsword", ("Great Lord Greatsword", 314000)},
                { "Zweihander", ("Zweihander", 350000)},
                { "Greatsword", ("Greatsword", 351000)},
                { "Demon Great Machete", ("Demon Great Machete", 352000)},
                { "Dragon Greatsword", ("Dragon Greatsword", 354000)},
                { "Black Knight Greatsword", ("Black Knight Greatsword", 355000)},
                { "Scimitar", ("Scimitar", 400000)},
                { "Falchion", ("Falchion", 401000)},
                { "Shotel", ("Shotel", 402000)},
                { "Jagged Ghost Blade", ("Jagged Ghost Blade", 403000)},
                { "Painting Guardian Sword", ("Painting Guardian Sword", 405000)},
                { "Quelaag's Furysword", ("Quelaag's Furysword", 406000)},
                { "Server", ("Server", 450000)},
                { "Murakumo", ("Murakumo", 451000)},
                { "Gravelord Sword", ("Gravelord Sword", 453000)},
                { "Uchigatana", ("Uchigatana", 500000)},
                { "Washing Pole", ("Washing Pole", 501000)},
                { "Iaito", ("Iaito", 502000)},
                { "Chaos Blade", ("Chaos Blade", 503000)},
                { "Mail Breaker", ("Mail Breaker", 600000)},
                { "Rapier", ("Rapier", 601000)},
                { "Estoc", ("Estoc", 602000)},
                { "Velka's Rapier", ("Velka's Rapier", 603000)},
                { "Ricard's Rapier", ("Ricard's Rapier", 604000)},
                { "Hand Axe", ("Hand Axe", 700000)},
                { "Battle Axe", ("Battle Axe", 701000)},
                { "Crescent Axe", ("Crescent Axe", 702000)},
                { "Butcher Knife", ("Butcher Knife", 703000)},
                { "Golem Axe", ("Golem Axe", 704000)},
                { "Gargoyle Tail Axe", ("Gargoyle Tail Axe", 705000)},
                { "Greataxe", ("Greataxe", 750000)},
                { "Demon's Greataxe", ("Demon's Greataxe", 751000)},
                { "Dragon King Greataxe", ("Dragon King Greataxe", 752000)},
                { "Black Knight Greataxe", ("Black Knight Greataxe", 753000)},
                { "Club", ("Club", 800000)},
                { "Mace", ("Mace", 801000)},
                { "Morning Star", ("Morning Star", 802000)},
                { "Warpick", ("Warpick", 803000)},
                { "Pickaxe", ("Pickaxe", 804000)},
                { "Reinforced Club", ("Reinforced Club", 809000)},
                { "Blacksmith Hammer", ("Blacksmith Hammer", 810000)},
                { "Blacksmith Giant Hammer", ("Blacksmith Giant Hammer", 811000)},
                { "Hammer of Vamos", ("Hammer of Vamos", 812000)},
                { "Great Club", ("Great Club", 850000)},
                { "Grant", ("Grant", 851000)},
                { "Demon's Great Hammer", ("Demon's Great Hammer", 852000)},
                { "Dragon Tooth", ("Dragon Tooth", 854000)},
                { "Large Club", ("Large Club", 855000)},
                { "Smough's Hammer", ("Smough's Hammer", 856000)},
                { "Caestus", ("Caestus", 901000)},
                { "Claw", ("Claw", 902000)},
                { "Dragon Bone Fist", ("Dragon Bone Fist", 903000)},
                { "Dark Hand", ("Dark Hand", 904000)},
                { "Spear", ("Spear", 1000000)},
                { "Winged Spear", ("Winged Spear", 1001000)},
                { "Partizan", ("Partizan", 1002000)},
                { "Demon's Spear", ("Demon's Spear", 1003000)},
                { "Channeler's Trident", ("Channeler's Trident", 1004000)},
                { "Silver Knight Spear", ("Silver Knight Spear", 1006000)},
                { "Pike", ("Pike", 1050000)},
                { "Dragonslayer Spear", ("Dragonslayer Spear", 1051000)},
                { "Moonlight Butterfly Horn", ("Moonlight Butterfly Horn", 1052000)},
                { "Halberd", ("Halberd", 1100000)},
                { "Giant's Halberd", ("Giant's Halberd", 1101000)},
                { "Titanite Catch Pole", ("Titanite Catch Pole", 1102000)},
                { "Gargoyle's Halberd", ("Gargoyle's Halberd", 1103000)},
                { "Black Knight Halberd", ("Black Knight Halberd", 1105000)},
                { "Lucerne", ("Lucerne", 1106000)},
                { "Scythe", ("Scythe", 1107000)},
                { "Great Scythe", ("Great Scythe", 1150000)},
                { "Lifehunt Scythe", ("Lifehunt Scythe", 1151000)},
                { "Whip", ("Whip", 1600000)},
                { "Notched Whip", ("Notched Whip", 1601000)},
                { "Gold Tracer", ("Gold Tracer", 9010000)},
                { "Dark Silver Tracer", ("Dark Silver Tracer", 9011000)},
                { "Abyss Greatsword", ("Abyss Greatsword", 9012000)},
                { "Stone Greataxe", ("Stone Greataxe", 9015000)},
                { "Four-pronged Plow", ("Four-pronged Plow", 9016000)},
                { "Guardian Tail", ("Guardian Tail", 9019000)},
                { "Obsidian Greatsword", ("Obsidian Greatsword", 9020000)},
                { "Short Bow", ("Short Bow", 1200000)},
                { "Longbow", ("Longbow", 1201000)},
                { "Black Bow of Pharis", ("Black Bow of Pharis", 1202000)},
                { "Dragonslayer Greatbow", ("Dragonslayer Greatbow", 1203000)},
                { "Composite Bow", ("Composite Bow", 1204000)},
                { "Darkmoon Bow", ("Darkmoon Bow", 1205000)},
                { "Light Crossbow", ("Light Crossbow", 1250000)},
                { "Heavy Crossbow", ("Heavy Crossbow", 1251000)},
                { "Avelyn", ("Avelyn", 1252000)},
                { "Sniper Crossbow", ("Sniper Crossbow", 1253000)},
                { "Gough's Greatbow", ("Gough's Greatbow", 9021000)},
                { "Standard Arrow", ("Standard Arrow", 2000000)},
                { "Large Arrow", ("Large Arrow", 2001000)},
                { "Feather Arrow", ("Feather Arrow", 2002000)},
                { "Fire Arrow", ("Fire Arrow", 2003000)},
                { "Poison Arrow", ("Poison Arrow", 2004000)},
                { "Moonlight Arrow", ("Moonlight Arrow", 2005000)},
                { "Wooden Arrow", ("Wooden Arrow", 2006000)},
                { "Dragonslayer Arrow", ("Dragonslayer Arrow", 2007000)},
                { "Gough's Great Arrow", ("Gough's Great Arrow", 2008000)},
                { "Standard Bolt", ("Standard Bolt", 2100000)},
                { "Heavy Bolt", ("Heavy Bolt", 2101000)},
                { "Sniper Bolt", ("Sniper Bolt", 2102000)},
                { "Wood Bolt", ("Wood Bolt", 2103000)},
                { "Lightning Bolt", ("Lightning Bolt", 2104000)},
                { "Havel's Ring", ("Havel's Ring", 100)},
                { "Red Tearstone Ring", ("Red Tearstone Ring", 101)},
                { "Darkmoon Blade Covenant Ring", ("Darkmoon Blade Covenant Ring", 102)},
                { "Cat Covenant Ring", ("Cat Covenant Ring", 103)},
                { "Cloranthy Ring", ("Cloranthy Ring", 104)},
                { "Flame Stoneplate Ring", ("Flame Stoneplate Ring", 105)},
                { "Thunder Stoneplate Ring", ("Thunder Stoneplate Ring", 106)},
                { "Spell Stoneplate Ring", ("Spell Stoneplate Ring", 107)},
                { "Speckled Stoneplate Ring", ("Speckled Stoneplate Ring", 108)},
                { "Bloodbite Ring", ("Bloodbite Ring", 109)},
                { "Poisonbite Ring", ("Poisonbite Ring", 110)},
                { "Tiny Being's Ring", ("Tiny Being's Ring", 111)},
                { "Cursebite Ring", ("Cursebite Ring", 113)},
                { "White Seance Ring", ("White Seance Ring", 114)},
                { "Bellowing Dragoncrest Ring", ("Bellowing Dragoncrest Ring", 115)},
                { "Dusk Crown Ring", ("Dusk Crown Ring", 116)},
                { "Hornet Ring", ("Hornet Ring", 117)},
                { "Hawk Ring", ("Hawk Ring", 119)},
                { "Ring of Steel Protection", ("Ring of Steel Protection", 120)},
                { "Covetous Gold Serpent Ring", ("Covetous Gold Serpent Ring", 121)},
                { "Covetous Silver Serpent Ring", ("Covetous Silver Serpent Ring", 122)},
                { "Slumbering Dragoncrest Ring", ("Slumbering Dragoncrest Ring", 123)},
                { "Ring of Fog", ("Ring of Fog", 124)},
                { "Rusted Iron Ring", ("Rusted Iron Ring", 125)},
                { "Ring of Sacrifice", ("Ring of Sacrifice", 126)},
                { "Rare Ring of Sacrifice", ("Rare Ring of Sacrifice", 127)},
                { "Dark Wood Grain Ring", ("Dark Wood Grain Ring", 128)},
                { "Ring of the Sun Princess", ("Ring of the Sun Princess", 130)},
                { "Old Witch's Ring", ("Old Witch's Ring", 137)},
                { "Covenant of Artorias", ("Covenant of Artorias", 138)},
                { "Orange Charred Ring", ("Orange Charred Ring", 139)},
                { "Lingering Dragoncrest Ring", ("Lingering Dragoncrest Ring", 141)},
                { "Ring of the Evil Eye", ("Ring of the Evil Eye", 142)},
                { "Ring of Favor and Protection", ("Ring of Favor and Protection", 143)},
                { "Leo Ring", ("Leo Ring", 144)},
                { "East Wood Grain Ring", ("East Wood Grain Ring", 145)},
                { "Wolf Ring", ("Wolf Ring", 146)},
                { "Blue Tearstone Ring", ("Blue Tearstone Ring", 147)},
                { "Ring of the Sun's Firstborn", ("Ring of the Sun's Firstborn", 148)},
                { "Darkmoon Seance Ring", ("Darkmoon Seance Ring", 149)},
                { "Calamity Ring", ("Calamity Ring", 150)},
                { "Skull Lantern", ("Skull Lantern", 1396000)},
                { "East-West Shield", ("East-West Shield", 1400000)},
                { "Wooden Shield", ("Wooden Shield", 1401000)},
                { "Large Leather Shield", ("Large Leather Shield", 1402000)},
                { "Small Leather Shield", ("Small Leather Shield", 1403000)},
                { "Target Shield", ("Target Shield", 1404000)},
                { "Buckler", ("Buckler", 1405000)},
                { "Cracked Round Shield", ("Cracked Round Shield", 1406000)},
                { "Leather Shield", ("Leather Shield", 1408000)},
                { "Plank Shield", ("Plank Shield", 1409000)},
                { "Caduceus Round Shield", ("Caduceus Round Shield", 1410000)},
                { "Crystal Ring Shield", ("Crystal Ring Shield", 1411000)},
                { "Heater Shield", ("Heater Shield", 1450000)},
                { "Knight Shield", ("Knight Shield", 1451000)},
                { "Tower Kite Shield", ("Tower Kite Shield", 1452000)},
                { "Grass Crest Shield", ("Grass Crest Shield", 1453000)},
                { "Hollow Soldier Shield", ("Hollow Soldier Shield", 1454000)},
                { "Balder Shield", ("Balder Shield", 1455000)},
                { "Crest Shield", ("Crest Shield", 1456000)},
                { "Dragon Crest Shield", ("Dragon Crest Shield", 1457000)},
                { "Warrior's Round Shield", ("Warrior's Round Shield", 1460000)},
                { "Iron Round Shield", ("Iron Round Shield", 1461000)},
                { "Spider Shield", ("Spider Shield", 1462000)},
                { "Spiked Shield", ("Spiked Shield", 1470000)},
                { "Crystal Shield", ("Crystal Shield", 1471000)},
                { "Sunlight Shield", ("Sunlight Shield", 1472000)},
                { "Silver Knight Shield", ("Silver Knight Shield", 1473000)},
                { "Black Knight Shield", ("Black Knight Shield", 1474000)},
                { "Pierce Shield", ("Pierce Shield", 1475000)},
                { "Red and White Round Shield", ("Red and White Round Shield", 1476000)},
                { "Caduceus Kite Shield", ("Caduceus Kite Shield", 1477000)},
                { "Gargoyle's Shield", ("Gargoyle's Shield", 1478000)},
                { "Eagle Shield", ("Eagle Shield", 1500000)},
                { "Tower Shield", ("Tower Shield", 1501000)},
                { "Giant Shield", ("Giant Shield", 1502000)},
                { "Stone Greatshield", ("Stone Greatshield", 1503000)},
                { "Havel's Greatshield", ("Havel's Greatshield", 1505000)},
                { "Bonewheel Shield", ("Bonewheel Shield", 1506000)},
                { "Greatshield of Artorias", ("Greatshield of Artorias", 1507000)},
                { "Effigy Shield", ("Effigy Shield", 9000000)},
                { "Sanctus", ("Sanctus", 9001000)},
                { "Bloodshield", ("Bloodshield", 9002000)},
                { "Black Iron Greatshield", ("Black Iron Greatshield", 9003000)},
                { "Cleansing Greatshield", ("Cleansing Greatshield", 9014000)},
                { "Sorcery: Soul Arrow", ("Sorcery: Soul Arrow", 3000)},
                { "Sorcery: Great Soul Arrow", ("Sorcery: Great Soul Arrow", 3010)},
                { "Sorcery: Heavy Soul Arrow", ("Sorcery: Heavy Soul Arrow", 3020)},
                { "Sorcery: Great Heavy Soul Arrow", ("Sorcery: Great Heavy Soul Arrow", 3030)},
                { "Sorcery: Homing Soulmass", ("Sorcery: Homing Soulmass", 3040)},
                { "Sorcery: Homing Crystal Soulmass", ("Sorcery: Homing Crystal Soulmass", 3050)},
                { "Sorcery: Soul Spear", ("Sorcery: Soul Spear", 3060)},
                { "Sorcery: Crystal Soul Spear", ("Sorcery: Crystal Soul Spear", 3070)},
                { "Sorcery: Magic Weapon", ("Sorcery: Magic Weapon", 3100)},
                { "Sorcery: Great Magic Weapon", ("Sorcery: Great Magic Weapon", 3110)},
                { "Sorcery: Crystal Magic Weapon", ("Sorcery: Crystal Magic Weapon", 3120)},
                { "Sorcery: Magic Shield", ("Sorcery: Magic Shield", 3300)},
                { "Sorcery: Strong Magic Shield", ("Sorcery: Strong Magic Shield", 3310)},
                { "Sorcery: Hidden Weapon", ("Sorcery: Hidden Weapon", 3400)},
                { "Sorcery: Hidden Body", ("Sorcery: Hidden Body", 3410)},
                { "Sorcery: Cast Light", ("Sorcery: Cast Light", 3500)},
                { "Sorcery: Hush", ("Sorcery: Hush", 3510)},
                { "Sorcery: Aural Decoy", ("Sorcery: Aural Decoy", 3520)},
                { "Sorcery: Repair", ("Sorcery: Repair", 3530)},
                { "Sorcery: Fall Control", ("Sorcery: Fall Control", 3540)},
                { "Sorcery: Chameleon", ("Sorcery: Chameleon", 3550)},
                { "Sorcery: Resist Curse", ("Sorcery: Resist Curse", 3600)},
                { "Sorcery: Remedy", ("Sorcery: Remedy", 3610)},
                { "Sorcery: White Dragon Breath", ("Sorcery: White Dragon Breath", 3700)},
                { "Sorcery: Dark Orb", ("Sorcery: Dark Orb", 3710)},
                { "Sorcery: Dark Bead", ("Sorcery: Dark Bead", 3720)},
                { "Sorcery: Dark Fog", ("Sorcery: Dark Fog", 3730)},
                { "Sorcery: Pursuers", ("Sorcery: Pursuers", 3740)},
                { "Pyromancy: Fireball", ("Pyromancy: Fireball", 4000)},
                { "Pyromancy: Fire Orb", ("Pyromancy: Fire Orb", 4010)},
                { "Pyromancy: Great Fireball", ("Pyromancy: Great Fireball", 4020)},
                { "Pyromancy: Firestorm", ("Pyromancy: Firestorm", 4030)},
                { "Pyromancy: Fire Tempest", ("Pyromancy: Fire Tempest", 4040)},
                { "Pyromancy: Fire Surge", ("Pyromancy: Fire Surge", 4050)},
                { "Pyromancy: Fire Whip", ("Pyromancy: Fire Whip", 4060)},
                { "Pyromancy: Combustion", ("Pyromancy: Combustion", 4100)},
                { "Pyromancy: Great Combustion", ("Pyromancy: Great Combustion", 4110)},
                { "Pyromancy: Poison Mist", ("Pyromancy: Poison Mist", 4200)},
                { "Pyromancy: Toxic Mist", ("Pyromancy: Toxic Mist", 4210)},
                { "Pyromancy: Acid Surge", ("Pyromancy: Acid Surge", 4220)},
                { "Pyromancy: Iron Flesh", ("Pyromancy: Iron Flesh", 4300)},
                { "Pyromancy: Flash Sweat", ("Pyromancy: Flash Sweat", 4310)},
                { "Pyromancy: Undead Rapport", ("Pyromancy: Undead Rapport", 4360)},
                { "Pyromancy: Power Within", ("Pyromancy: Power Within", 4400)},
                { "Pyromancy: Great Chaos Fireball", ("Pyromancy: Great Chaos Fireball", 4500)},
                { "Pyromancy: Chaos Storm", ("Pyromancy: Chaos Storm", 4510)},
                { "Pyromancy: Chaos Fire Whip", ("Pyromancy: Chaos Fire Whip", 4520)},
                { "Pyromancy: Black Flame", ("Pyromancy: Black Flame", 4530)},
                { "Miracle: Heal", ("Miracle: Heal", 5000)},
                { "Miracle: Great Heal", ("Miracle: Great Heal", 5010)},
                { "Miracle: Great Heal Excerpt", ("Miracle: Great Heal Excerpt", 5020)},
                { "Miracle: Soothing Sunlight", ("Miracle: Soothing Sunlight", 5030)},
                { "Miracle: Replenishment", ("Miracle: Replenishment", 5040)},
                { "Miracle: Bountiful Sunlight", ("Miracle: Bountiful Sunlight", 5050)},
                { "Miracle: Gravelord Sword Dance", ("Miracle: Gravelord Sword Dance", 5100)},
                { "Miracle: Gravelord Greatsword Dance", ("Miracle: Gravelord Greatsword Dance", 5110)},
                { "Miracle: Homeward", ("Miracle: Homeward", 5210)},
                { "Miracle: Force", ("Miracle: Force", 5300)},
                { "Miracle: Wrath of the Gods", ("Miracle: Wrath of the Gods", 5310)},
                { "Miracle: Emit Force", ("Miracle: Emit Force", 5320)},
                { "Miracle: Seek Guidance", ("Miracle: Seek Guidance", 5400)},
                { "Miracle: Lightning Spear", ("Miracle: Lightning Spear", 5500)},
                { "Miracle: Great Lightning Spear", ("Miracle: Great Lightning Spear", 5510)},
                { "Miracle: Sunlight Spear", ("Miracle: Sunlight Spear", 5520)},
                { "Miracle: Magic Barrier", ("Miracle: Magic Barrier", 5600)},
                { "Miracle: Great Magic Barrier", ("Miracle: Great Magic Barrier", 5610)},
                { "Miracle: Karmic Justice", ("Miracle: Karmic Justice", 5700)},
                { "Miracle: Tranquil Walk of Peace", ("Miracle: Tranquil Walk of Peace", 5800)},
                { "Miracle: Vow of Silence", ("Miracle: Vow of Silence", 5810)},
                { "Miracle: Sunlight Blade", ("Miracle: Sunlight Blade", 5900)},
                { "Miracle: Darkmoon Blade", ("Miracle: Darkmoon Blade", 5910)},
                { "Sorcerer's Catalyst", ("Sorcerer's Catalyst", 1300000)},
                { "Beatrice's Catalyst", ("Beatrice's Catalyst", 1301000)},
                { "Tin Banishment Catalyst", ("Tin Banishment Catalyst", 1302000)},
                { "Logan's Catalyst", ("Logan's Catalyst", 1303000)},
                { "Tin Darkmoon Catalyst", ("Tin Darkmoon Catalyst", 1304000)},
                { "Oolacile Ivory Catalyst", ("Oolacile Ivory Catalyst", 1305000)},
                { "Tin Crystallization Catalyst", ("Tin Crystallization Catalyst", 1306000)},
                { "Demon's Catalyst", ("Demon's Catalyst", 1307000)},
                { "Izalith Catalyst", ("Izalith Catalyst", 1308000)},
                { "Pyromancy Flame", ("Pyromancy Flame", 1330000)},
                { "Pyromancy Flame (Ascended)", ("Pyromancy Flame (Ascended)", 1332000)},
                { "Talisman", ("Talisman", 1360000)},
                { "Canvas Talisman", ("Canvas Talisman", 1361000)},
                { "Thorolund Talisman", ("Thorolund Talisman", 1362000)},
                { "Ivory Talisman", ("Ivory Talisman", 1363000)},
                { "Sunlight Talisman", ("Sunlight Talisman", 1365000)},
                { "Darkmoon Talisman", ("Darkmoon Talisman", 1366000)},
                { "Velka's Talisman", ("Velka's Talisman", 1367000)},
                { "Manus Catalyst", ("Manus Catalyst", 9017000)},
                { "Oolacile Catalyst", ("Oolacile Catalyst", 9018000)},
                { "Large Ember", ("Large Ember", 800)},
                { "Very Large Ember", ("Very Large Ember", 801)},
                { "Crystal Ember", ("Crystal Ember", 802)},
                { "Large Magic Ember", ("Large Magic Ember", 806)},
                { "Enchanted Ember", ("Enchanted Ember", 807)},
                { "Divine Ember", ("Divine Ember", 808)},
                { "Large Divine Ember", ("Large Divine Ember", 809)},
                { "Dark Ember", ("Dark Ember", 810)},
                { "Large Flame Ember", ("Large Flame Ember", 812)},
                { "Chaos Flame Ember", ("Chaos Flame Ember", 813)},
                { "Titanite Shard", ("Titanite Shard", 1000)},
                { "Large Titanite Shard", ("Large Titanite Shard", 1010)},
                { "Green Titanite Shard", ("Green Titanite Shard", 1020)},
                { "Titanite Chunk", ("Titanite Chunk", 1030)},
                { "Blue Titanite Chunk", ("Blue Titanite Chunk", 1040)},
                { "White Titanite Chunk", ("White Titanite Chunk", 1050)},
                { "Red Titanite Chunk", ("Red Titanite Chunk", 1060)},
                { "Titanite Slab", ("Titanite Slab", 1070)},
                { "Blue Titanite Slab", ("Blue Titanite Slab", 1080)},
                { "White Titanite Slab", ("White Titanite Slab", 1090)},
                { "Red Titanite Slab", ("Red Titanite Slab", 1100)},
                { "Dragon Scale", ("Dragon Scale", 1110)},
                { "Demon Titanite", ("Demon Titanite", 1120)},
                { "Twinkling Titanite", ("Twinkling Titanite", 1130)},
                { "White Sign Soapstone", ("White Sign Soapstone", 100)},
                { "Red Sign Soapstone", ("Red Sign Soapstone", 101)},
                { "Red Eye Orb", ("Red Eye Orb", 102)},
                { "Black Separation Crystal", ("Black Separation Crystal", 103)},
                { "Orange Guidance Soapstone", ("Orange Guidance Soapstone", 106)},
                { "Book of the Guilty", ("Book of the Guilty", 108)},
                { "Servant Roster", ("Servant Roster", 112)},
                { "Blue Eye Orb", ("Blue Eye Orb", 113)},
                { "Dragon Eye", ("Dragon Eye", 114)},
                { "Black Eye Orb", ("Black Eye Orb", 115)},
                { "Darksign", ("Darksign", 117)},
                { "Purple Coward's Crystal", ("Purple Coward's Crystal", 118)},
                { "Silver Pendant", ("Silver Pendant", 220)},
                { "Binoculars", ("Binoculars", 371)},
                { "Dragon Head Stone", ("Dragon Head Stone", 377)},
                { "Dragon Torso Stone", ("Dragon Torso Stone", 378)},
                { "Dried Finger", ("Dried Finger", 385)},
                { "Hello Carving", ("Hello Carving", 510)},
                { "Thank you Carving", ("Thank you Carving", 511)},
                { "Very good! Carving", ("Very good! Carving", 512)},
                { "I'm sorry Carving", ("I'm sorry Carving", 513)},
                { "Help me! Carving", ("Help me! Carving", 514)}
           };

            if (itemMapping.TryGetValue(item, out var ItemInfo))
            {
                return new ItemDs1 { Title = ItemInfo.Title, Id = ItemInfo.Id };
            }
            else
            {
                throw new ArgumentException($"Invalid item string: {item}");
            }
        }
        #endregion
    }

    [Serializable]
    public class DTDs1
    { 
        //var Settings
        public bool enableSplitting = false;
        public bool autoTimer = false;
        public bool gameTimer = false;
        public int positionMargin = 5;
        //Flags to Split
        public List<DefinitionsDs1.BossDs1> bossToSplit = new List<DefinitionsDs1.BossDs1>();
        public List<DefinitionsDs1.BonfireDs1> bonfireToSplit = new List<DefinitionsDs1.BonfireDs1>();
        public List<DefinitionsDs1.LvlDs1> lvlToSplit = new List<DefinitionsDs1.LvlDs1>();
        public List<DefinitionsDs1.PositionDs1> positionsToSplit = new List<DefinitionsDs1.PositionDs1>();
        public List<DefinitionsDs1.ItemDs1> itemToSplit = new List<DefinitionsDs1.ItemDs1>();


        public List<DefinitionsDs1.BossDs1> GetBossToSplit() => this.bossToSplit;

        public List<DefinitionsDs1.BonfireDs1> GetBonfireToSplit() => this.bonfireToSplit;

        public List<DefinitionsDs1.LvlDs1> GetLvlToSplit() => this.lvlToSplit;

        public List<DefinitionsDs1.PositionDs1> GetPositionsToSplit() => this.positionsToSplit;

        public List<DefinitionsDs1.ItemDs1> GetItemsToSplit() => this.itemToSplit;
    }
}

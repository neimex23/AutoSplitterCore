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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulMemory.DarkSouls2;
using SoulMemory;

namespace AutoSplitterCore
{
    public class DefinitionsDs2
    {
        #region Boss.Ds2
        [Serializable]
        public class BossDs2
        {
            public string Title;
            public BossType Id;
            public bool IsSplited =false;
            public string Mode;
        }

        public BossDs2 StringToEnumBoss(string boss)
        {
            var bossMapping = new Dictionary<string, (string Title, BossType Id)> 
            {
                { "The Last Giant", ("The Last Giant", BossType.TheLastGiant)},
                { "The Pursuer", ("The Pursuer", BossType.ThePursuer)},
                { "Executioners Chariot", ("Executioners Chariot", BossType.ExecutionersChariot)},
                { "Looking Glass Knight", ("Looking Glass Knight", BossType.LookingGlassKnight)},
                { "The Skeleton Lords", ("The Skeleton Lords", BossType.TheSkeletonLords)},
                { "Flexile Sentry", ("Flexile Sentry", BossType.FlexileSentry)},
                { "Lost Sinner", ("Lost Sinner", BossType.LostSinner)},
                { "Belfry Gargoyles", ("Belfry Gargoyles", BossType.BelfryGargoyles)},
                { "Ruin Sentinels", ("Ruin Sentinels", BossType.RuinSentinels)},
                { "Royal Rat Vanguard", ("Royal Rat Vanguard", BossType.RoyalRatVanguard)},
                { "Royal Rat Authority", ("Royal Rat Authority", BossType.RoyalRatAuthority)},
                { "Scorpioness Najka", ("Scorpioness Najka", BossType.ScorpionessNajka)},
                { "The Duke's Dear Freja", ("The Duke's Dear Freja", BossType.TheDukesDearFreja)},
                { "Mytha, the Baneful Queen", ("Mytha, the Baneful Queen", BossType.MythaTheBanefulQueen)},
                { "The Rotten", ("The Rotten", BossType.TheRotten)},
                { "Old DragonSlayer", ("Old DragonSlayer", BossType.OldDragonSlayer)},
                { "Covetous Demon", ("Covetous Demon", BossType.CovetousDemon)},
                { "Smelter Demon", ("Smelter Demon", BossType.SmelterDemon)},
                { "Old Iron King", ("Old Iron King", BossType.OldIronKing)},
                { "Guardian Dragon", ("Guardian Dragon", BossType.GuardianDragon)},
                { "Demon of Song", ("Demon of Song", BossType.DemonOfSong)},
                { "Velstadt, The Royal Aegis", ("Velstadt, The Royal Aegis", BossType.VelstadtTheRoyalAegis)},
                { "Vendrick", ("Vendrick", BossType.Vendrick)},
                { "Darklurker", ("Darklurker", BossType.Darklurker)},
                { "Dragonrider", ("Dragonrider", BossType.Dragonrider)},
                { "Twin Dragonriders", ("Twin Dragonriders", BossType.TwinDragonriders)},
                { "Prowling Magnus and Congregation", ("Prowling Magnus and Congregation", BossType.ProwlingMagnusAndCongregation)},
                { "Giant Lord", ("Giant Lord", BossType.GiantLord)},
                { "Ancient Dragon", ("Ancient Dragon", BossType.AncientDragon)},
                { "Throne Watcher and Throne Defender", ("Throne Watcher and Throne Defender", BossType.ThroneWatcherAndThroneDefender)},
                { "Nashandra", ("Nashandra", BossType.Nashandra)},
                { "Aldia, Scholar of the First Sin", ("Aldia, Scholar of the First Sin", BossType.AldiaScholarOfTheFirstSin)},
                { "Elana, Squalid Queen", ("Elana, Squalid Queen", BossType.ElanaSqualidQueen)},
                { "Sinh, the Slumbering Dragon", ("Sinh, the Slumbering Dragon", BossType.SinhTheSlumberingDragon)},
                { "Afflicted Graverobber, Ancient Soldier Varg, and Cerah the Old Explorer", ("Afflicted Graverobber, Ancient Soldier Varg, and Cerah the Old Explorer", BossType.AfflictedGraverobberAncientSoldierVargCerahTheOldExplorer)},
                { "Blue Smelter Demon", ("Blue Smelter Demon", BossType.BlueSmelterDemon)},
                { "Fume knight", ("Fume knight", BossType.Fumeknight)},
                { "Sir Alonne", ("Sir Alonne", BossType.SirAlonne)},
                { "Burnt Ivory King", ("Burnt Ivory King", BossType.BurntIvoryKing)},
                { "Aava, the King's Pet", ("Aava, the King's Pet", BossType.AavaTheKingsPet)},
                { "Lud and Zallen, the King's Pets", ("Lud and Zallen, the King's Pets", BossType.LudAndZallenTheKingsPets) }
            };

            if (bossMapping.TryGetValue(boss, out var bossInfo))
            {
                return new BossDs2 { Title = bossInfo.Title, Id = bossInfo.Id };
            }
            else
            {
                throw new ArgumentException($"Invalid boss string: {boss}");
            }
        }
        #endregion
        #region Lvl.Ds2
        public class LvlDs2
        {
            public string Title;
            public SoulMemory.DarkSouls2.Attribute Attribute;
            public uint Value;
            public bool IsSplited = false;
            public string Mode;
        }

        public SoulMemory.DarkSouls2.Attribute StringToEnumAttribute(string attribute)
        {
            switch (attribute)
            {
                case "Vigor":
                    return SoulMemory.DarkSouls2.Attribute.Vigor;
                case "Attunement":
                    return SoulMemory.DarkSouls2.Attribute.Attunement;
                case "Endurance":
                    return SoulMemory.DarkSouls2.Attribute.Endurance;
                case "Vitality":
                    return SoulMemory.DarkSouls2.Attribute.Vitality;
                case "Strength":
                    return SoulMemory.DarkSouls2.Attribute.Strength;
                case "Dexterity":
                    return SoulMemory.DarkSouls2.Attribute.Dexterity;
                case "Intelligence":
                    return SoulMemory.DarkSouls2.Attribute.Intelligence;
                case "Faith":
                    return SoulMemory.DarkSouls2.Attribute.Faith;
                case "Adaptability":
                    return SoulMemory.DarkSouls2.Attribute.Adaptability;
                case "SoulLevel":
                    return SoulMemory.DarkSouls2.Attribute.SoulLevel;
                default: return SoulMemory.DarkSouls2.Attribute.SoulLevel;
            }
        }
        #endregion
        #region Position.Ds2
        [Serializable]
        public class PositionDs2
        {
            public Vector3f vector = new Vector3f();
            public bool IsSplited = false;
            public string Mode;
            public string Title;
        }
        #endregion
    }

    [Serializable]
    public class DTDs2
    {
        //var Settings
        public bool enableSplitting = false;
        public bool autoTimer = false;
        public bool gameTimer = false;
        public int positionMargin = 5;
        //Flags to Split
        public List<DefinitionsDs2.BossDs2> bossToSplit = new List<DefinitionsDs2.BossDs2>();
        public List<DefinitionsDs2.LvlDs2> lvlToSplit = new List<DefinitionsDs2.LvlDs2>();
        public List<DefinitionsDs2.PositionDs2> positionsToSplit = new List<DefinitionsDs2.PositionDs2>();

        public List<DefinitionsDs2.BossDs2> GetBossToSplit() => this.bossToSplit;
        public List<DefinitionsDs2.LvlDs2> GetLvlToSplit() => this.lvlToSplit;

        public List<DefinitionsDs2.PositionDs2> GetPositionsToSplit() => this.positionsToSplit;

    }
}

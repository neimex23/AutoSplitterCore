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
using LiveSplit.HollowKnight;

namespace AutoSplitterCore
{
    public class DefinitionHollow
    {
        #region ElementToSplitH
        [Serializable]
        public class ElementToSplitH
        {
            public string Title;
            public Offset Offset = new Offset();
            public bool IsSplited = false;
            public bool intMethod = false;
            public int intCompare = 0;
            public bool kingSoulsCase = false;

            public void SetData(Offset offset, string title)
            {
                this.Offset = offset;
                this.Title = title;
            }

            public void SetMethod(int intCompare)
            {
                this.intMethod = true;
                this.intCompare = intCompare;
            }

            public void KsC()
            {
                this.kingSoulsCase = true;
            }
        }

        public ElementToSplitH StringToEnum(string element)
        {
            ElementToSplitH elem = new ElementToSplitH();
            switch (element)
            {
                #region Boss
                case "Broken Vessel":
                    elem.SetData(Offset.killedInfectedKnight, "Broken Vessel");
                    break;
                case "Brooding Mawlek":
                    elem.SetData(Offset.killedMawlek, "Brooding Mawlek");
                    break;
                case "Collector":
                    elem.SetData(Offset.collectorDefeated, "Collector");
                    break;
                case "Crystal Guardian":
                    elem.SetData(Offset.defeatedMegaBeamMiner, "Crystal Guardian");
                    break;            
                case "Dung Defender":
                    elem.SetData(Offset.killedDungDefender, "Dung Defender");
                    break;
                case "Elder Hu":
                    elem.SetData(Offset.killedGhostHu, "Elder Hu");
                    break;
                case "False Knight":
                    elem.SetData(Offset.killedFalseKnight, "False Knight");
                    break;
                case "Failed Champion":
                    elem.SetData(Offset.falseKnightDreamDefeated, "Failed Champion");
                    break;
                case "Flukemarm":
                    elem.SetData(Offset.killedFlukeMother, "Flukemarm");
                    break;
                case "Galien":
                    elem.SetData(Offset.killedGhostGalien, "Galien");
                    break;
                case "God Tamer":
                    elem.SetData(Offset.killedLobsterLancer, "God Tamer");
                    break;
                case "Gorb":
                    elem.SetData(Offset.killedGhostAladar, "Gorb");
                    break;
                case "Grey Prince Zote":
                    elem.SetData(Offset.killedGreyPrince, "Grey Prince Zote");
                    break;
                case "Gruz Mother":
                    elem.SetData(Offset.killedBigFly, "Gruz Mother");
                    break;
                case "Hive Knight":
                    elem.SetData(Offset.killedHiveKnight, "Hive Knight");
                    break;
                case "Hornet (Greenpath)":
                    elem.SetData(Offset.killedHornet, "Hornet (Greenpath)");
                    break;
                case "Hornet (Kingdom's Edge)":
                    elem.SetData(Offset.hornetOutskirtsDefeated, "Hornet (Kingdom's Edge)");
                    break;
                case "Lost Kin":
                    elem.SetData(Offset.infectedKnightDreamDefeated, "Lost Kin");
                    break;
                case "Mantis Lords":
                    elem.SetData(Offset.defeatedMantisLords, "Mantis Lords");
                    break;
                case "Markoth":
                    elem.SetData(Offset.killedGhostMarkoth, "Markoth");
                    break;
                case "Marmu":
                    elem.SetData(Offset.killedGhostMarmu, "Marmu");
                    break;
                case "Massive Moss Charger":
                    elem.SetData(Offset.megaMossChargerDefeated, "Massive Moss Charger");
                    break;
                case "Nightmare King Grimm":
                    elem.SetData(Offset.killedNightmareGrimm, "Nightmare King Grimm");
                    break;
                case "No Eyes":
                    elem.SetData(Offset.killedGhostNoEyes, "No Eyes");
                    break;
                case "Nosk":
                    elem.SetData(Offset.killedMimicSpider, "Nosk");
                    break;
                case "Oro & Mato Nail Bros":
                    elem.SetData(Offset.killedNailBros, "Oro & Mato Nail Bros");
                    break;
                case "Pure Vessel":
                    elem.SetData(Offset.killedHollowKnightPrime, "Pure Vessel");
                    break;
                case "Radiance":
                    elem.SetData(Offset.killedFinalBoss, "Radiance");
                    break;
                case "The Hollow Knight":
                    elem.SetData(Offset.killedHollowKnight, "The Hollow Knight");
                    break;
                case "Paintmaster Sheo":
                    elem.SetData(Offset.killedPaintmaster, "Paintmaster Sheo");
                    break;
                case "Great Nailsage Sly":
                    elem.SetData(Offset.killedNailsage, "Great Nailsage Sly");
                    break;
                case "Soul Master":
                    elem.SetData(Offset.killedMageLord, "Soul Master");
                    break;
                case "Soul Tyrant":
                    elem.SetData(Offset.mageLordDreamDefeated, "Soul Tyrant");
                    break;
                case "Traitor Lord":
                    elem.SetData(Offset.killedTraitorLord, "Traitor Lord");
                    break;
                case "Troupe Master Grimm":
                    elem.SetData(Offset.killedGrimm, "Troupe Master Grimm");
                    break;
                case "Uumuu":
                    elem.SetData(Offset.killedMegaJellyfish, "Uumuu");
                    break;
                case "Watcher Knight":
                    elem.SetData(Offset.killedBlackKnight, "Watcher Knight");
                    break;
                case "White Defender":
                    elem.SetData(Offset.killedWhiteDefender, "White Defender");
                    break;
                case "Xero":
                    elem.SetData(Offset.killedGhostXero, "Xero");
                    break;
                #endregion
                #region MiniBoss- Dreamers - Others
                case "Enraged Guardian":
                    elem.SetData(Offset.killsMegaBeamMiner, "Enraged Guardian");
                    elem.SetMethod(0);
                    break;
                case "Oblobbles":
                    elem.SetData(Offset.killsOblobble, "Oblobbles");
                    elem.SetMethod(1);
                    break;
                case "Aspid Hunter":
                    elem.SetData(Offset.killsSpitter, "Aspid Hunter");
                    elem.SetMethod(17);
                    break;
                case "Moss Knight":
                    elem.SetData(Offset.killedMossKnight, "Moss Knight");
                    break;
                case "Shrumal Ogres":
                    elem.SetData(Offset.killsMushroomBrawler, "Shrumal Ogres");
                    elem.SetMethod(6);
                    break;
                case "Zote Rescued - Vengefly King":
                    elem.SetData(Offset.zoteRescuedBuzzer, "Zote Rescued - Vengefly King");
                    break;
                case "Zote Rescued - Deepnest":
                    elem.SetData(Offset.zoteRescuedDeepnest, "Zote Rescued - Deepnest");
                    break;
                case "Zote Defeated - Colosseum":
                    elem.SetData(Offset.killedZote, "Zote Defeated - Colosseum");
                    break;
                case "First Dreamer":
                    elem.SetData(Offset.guardiansDefeated, "First Dreamer");
                    elem.SetMethod(1);
                    break;
                case "Second Dreamer":
                    elem.SetData(Offset.guardiansDefeated, "Second Dreamer");
                    elem.SetMethod(2);
                    break;
                case "Third Dreamer":
                    elem.SetData(Offset.guardiansDefeated, "Third Dreamer");
                    elem.SetMethod(3);
                    break;
                case "Colosseum Warrior Completed":
                    elem.SetData(Offset.colosseumBronzeCompleted, "Colosseum Warrior Completed");
                    break;
                case "Colosseum Conqueror Completed":
                    elem.SetData(Offset.colosseumSilverCompleted, "Colosseum Conqueror Completed");
                    break;
                case "Colosseum Fool Completed":
                    elem.SetData(Offset.colosseumGoldCompleted, "Colosseum Fool Completed");
                     break;
                case "Aluba":
                    elem.SetData(Offset.killedLazyFlyer, "Aluba");
                    break;
                case "Great Hopper":
                    elem.SetData(Offset.killedGiantHopper, "Great Hopper");
                    break;
                case "Gorgeous Husk":
                    elem.SetData(Offset.killedGorgeousHusk, "Gorgeous Husk");
                    break;
                case "Menderbug":
                    elem.SetData(Offset.killedMenderBug, "Menderbug");
                    break;
                case "Soul Warrior":
                    elem.SetData(Offset.killedMageKnight, "Soul Warrior");
                    break;
                case "Soul Twister":
                    elem.SetData(Offset.killedMage, "Soul Twister");
                    break;
                case "Mimic 1":
                    elem.SetData(Offset.killsGrubMimic, "Mimic 1");
                    elem.SetMethod(4);
                    break;
                case "Mimic 2":
                    elem.SetData(Offset.killsGrubMimic, "Mimic 2");
                    elem.SetMethod(3);
                    break;
                case "Mimic 3":
                    elem.SetData(Offset.killsGrubMimic, "Mimic 3");
                    elem.SetMethod(2);
                    break;
                case "Mimic 4":
                    elem.SetData(Offset.killsGrubMimic, "Mimic 4");
                    elem.SetMethod(1);
                    break;
                case "Mimic 5":
                    elem.SetData(Offset.killsGrubMimic, "Mimic 5");
                    elem.SetMethod(0);
                    break;
                case "Path of Pain - Completed":
                    elem.SetData(Offset.newDataBindingSeal, "Path of Pain - Completed");
                    break;
                case "Flower Quest - Completed":
                    elem.SetData(Offset.xunRewardGiven, "Flower Quest - Completed");
                    break;
                #endregion
                #region Charms
                case "Baldur Shell":
                    elem.SetData(Offset.gotCharm_5, "Baldur Shell");
                    break;
                case "Dashmaster":
                    elem.SetData(Offset.gotCharm_31, "Dashmaster");
                    break;
                case "Deep Focus":
                    elem.SetData(Offset.gotCharm_34, "Deep Focus");
                    break;
                case "Defenders Crest":
                    elem.SetData(Offset.gotCharm_10, "Defenders Crest");
                    break;
                case "Dreamshield":
                    elem.SetData(Offset.gotCharm_38, "Dreamshield");
                    break;
                case "Dream Wielder":
                    elem.SetData(Offset.gotCharm_30, "Dream Wielder");
                    break;
                case "Flukenest":
                    elem.SetData(Offset.gotCharm_11, "Flukenest");
                    break;
                case "Fragile Greed":
                    elem.SetData(Offset.gotCharm_24, "Fragile Greed");
                    break;
                case "Fragile Heart":
                    elem.SetData(Offset.gotCharm_23, "Fragile Heart");
                    break;
                case "Fragile Strength":
                    elem.SetData(Offset.gotCharm_25, "Fragile Strength");
                    break;
                case "Fury of the Fallen":
                    elem.SetData(Offset.gotCharm_6, "Fury of the Fallen");
                    break;
                case "Gathering Swarm":
                    elem.SetData(Offset.gotCharm_1, "Gathering Swarm");
                    break;
                case "Glowing Womb":
                    elem.SetData(Offset.gotCharm_22, "Glowing Womb");
                    break;
                case "Grimmchild":
                    elem.SetData(Offset.gotCharm_40, "Grimmchild");
                    break;
                case "Grimmchild Lvl 2":
                    elem.SetData(Offset.grimmChildLevel, "Grimmchild Lvl 2");
                    elem.SetMethod(2);
                    break;
                case "Grimmchild Lvl 3":
                    elem.SetData(Offset.grimmChildLevel, "Grimmchild Lvl 3");
                    elem.SetMethod(3);
                    break;
                case "Grimmchild Lvl 4":
                    elem.SetData(Offset.grimmChildLevel, "Grimmchild Lvl 4");
                    elem.SetMethod(4);
                    break;
                case "Grubberfly's Elegy":
                    elem.SetData(Offset.gotCharm_35, "Grubberfly's Elegy");
                    break;
                case "Grubsong":
                    elem.SetData(Offset.gotCharm_3, "Grubsong");
                    break;
                case "Heavy Blow":
                    elem.SetData(Offset.gotCharm_15, "Heavy Blow");
                    break;
                case "Hiveblood":
                    elem.SetData(Offset.gotCharm_29, "Hiveblood");
                    break;
                case "Joni's Blessing":
                    elem.SetData(Offset.gotCharm_27, "Joni's Blessing");
                    break;
                case "White Fragment - Queen's":
                    elem.SetData(Offset.gotQueenFragment, "White Fragment - Queen's");
                    break;
                case "White Fragment - King's":
                    elem.SetData(Offset.gotKingFragment, "White Fragment - King's");
                    break;
                case "Kingsoul":
                    elem.SetData(Offset.ore, "Kingsoul");
                    elem.KsC();
                    break;
                case "Lifeblood Core":
                    elem.SetData(Offset.gotCharm_9, "Lifeblood Core");
                    break;
                case "Lifeblood Heart":
                    elem.SetData(Offset.gotCharm_8, "Lifeblood Heart");
                    break;
                case "Longnail":
                    elem.SetData(Offset.gotCharm_18, "Longnail");
                    break;
                case "Mark of Pride":
                    elem.SetData(Offset.gotCharm_13, "Mark of Pride");
                    break;
                case "Nailmaster's Glory":
                    elem.SetData(Offset.gotCharm_26, "Nailmaster's Glory");
                    break;
                case "Quick Focus":
                    elem.SetData(Offset.gotCharm_7, "Quick Focus");
                    break;
                case "Quick Slash":
                    elem.SetData(Offset.gotCharm_32, "Quick Slash");
                    break;
                case "Shaman Stone":
                    elem.SetData(Offset.gotCharm_19, "Shaman Stone");
                    break;
                case "Shape of Unn":
                    elem.SetData(Offset.gotCharm_28, "Shape of Unn");
                    break;
                case "Sharp Shadow":
                    elem.SetData(Offset.gotCharm_16, "Sharp Shadow");
                    break;
                case "Soul Catcher":
                    elem.SetData(Offset.gotCharm_20, "Soul Catcher");
                    break;
                case "Soul Eater":
                    elem.SetData(Offset.gotCharm_21, "Soul Eater");
                    break;
                case "Spell Twister":
                    elem.SetData(Offset.gotCharm_33, "Spell Twister");
                    break;
                case "Spore Shroom":
                    elem.SetData(Offset.gotCharm_17, "Spore Shroom");
                    break;
                case "Sprintmaster":
                    elem.SetData(Offset.gotCharm_37, "Sprintmaster");
                    break;
                case "Stalwart Shell":
                 elem.SetData(Offset.gotCharm_4, "Stalwart Shell");
                    break;
                case "Steady Body":
                    elem.SetData(Offset.gotCharm_14, "Steady Body");
                    break;
                case "Thorns of Agony":
                    elem.SetData(Offset.gotCharm_12, "Thorns of Agony");
                    break;
                case "Unbreakable Greed":
                    elem.SetData(Offset.fragileGreed_unbreakable, "Unbreakable Greed");
                    break;
                case "Unbreakable Heart":
                    elem.SetData(Offset.fragileHealth_unbreakable, "Unbreakable Heart");
                    break;
                case "Unbreakable Strength":
                    elem.SetData(Offset.fragileStrength_unbreakable, "Unbreakable Strength");
                    break;
                case "Void Heart":
                    elem.SetData(Offset.gotShadeCharm, "Void Heart");
                    break;
                case "Wayward Compass":
                    elem.SetData(Offset.gotCharm_2, "Wayward Compass");
                    break;
                case "Weaversong":
                    elem.SetData(Offset.gotCharm_39, "Weaversong");
                    break;
                case "Shrumal Ogres (Charm)":
                    elem.SetData(Offset.notchShroomOgres, "Shrumal Ogres (Charm)");
                    break;
                case "Fog Canyon":
                    elem.SetData(Offset.notchFogCanyon, "Fog Canyon");
                    break;
                case "Salubra 1":
                    elem.SetData(Offset.salubraNotch1, "Salubra 1");
                    break;
                case "Salubra 2":
                    elem.SetData(Offset.salubraNotch2, "Salubra 2");
                    break;
                case "Salubra 3":
                    elem.SetData(Offset.salubraNotch3, "Salubra 3");
                    break;
                case "Salubra 4":
                    elem.SetData(Offset.salubraNotch4, "Salubra 4");
                    break;

                #endregion
                #region Skills
                case "Abyss Shriek":
                    elem.SetData(Offset.screamLevel, "Abyss Shriek");
                    elem.SetMethod(2);
                    break;
                case "Crystal Heart":
                    elem.SetData(Offset.hasSuperDash, "Crystal Heart");
                    break;
                case "Cyclone Slash":
                    elem.SetData(Offset.hasCyclone, "Cyclone Slash");
                    break;
                case "Dash Slash":
                    elem.SetData(Offset.hasUpwardSlash, "Dash Slash");
                    break;
                case "Descending Dark":
                    elem.SetData(Offset.quakeLevel, "Descending Dark");
                    elem.SetMethod(2);
                    break;
                case "Desolate Dive":
                    elem.SetData(Offset.quakeLevel, "Desolate Dive");
                    elem.SetMethod(1);
                    break;
                case "Dream Nail":
                    elem.SetData(Offset.hasDreamNail, "Dream Nail");
                    break;
                case "Dream Nail - Awoken":
                    elem.SetData(Offset.dreamNailUpgraded, "Dream Nail - Awoken");
                    break;
                case "Dream Gate":
                    elem.SetData(Offset.hasDreamGate, "Dream Gate");
                    break;
                case "Great Slash":
                    elem.SetData(Offset.hasDashSlash, "Great Slash");
                    break;
                case "Howling Wraiths":
                    elem.SetData(Offset.screamLevel, "Howling Wraiths");
                    elem.SetMethod(1);
                    break;
                case "Isma's Tear":
                    elem.SetData(Offset.hasAcidArmour, "Isma's Tear");
                    break;
                case "Mantis Claw":
                    elem.SetData(Offset.hasWallJump, "Mantis Claw");
                    break;
                case "Monarch Wings":
                    elem.SetData(Offset.hasDoubleJump, "Monarch Wings");
                    break;
                case "Mothwing Cloak":
                    elem.SetData(Offset.hasDash, "Mothwing Cloak");
                    break;
                case "Shade Cloak":
                    elem.SetData(Offset.hasShadowDash, "Shade Cloak");
                    break;
                case "Shade Soul":
                    elem.SetData(Offset.fireballLevel, "Shade Soul");
                    elem.SetMethod(2);
                    break;
                case "Vengeful Spirit":
                    elem.SetData(Offset.fireballLevel, "Vengeful Spirit");
                    elem.SetMethod(1);
                    break;










                    #endregion
            }
            return elem;
        }
        #endregion
        #region Vector3F
        [Serializable]
        public class Vector3F
        {
            public PointF position = new PointF(0,0);
            public string sceneName = "None";
            public string previousScene = "None";
            public bool IsSplited = false;
            public string Title;
        }
        #endregion
        #region Pantheon
        [Serializable]
        public class Pantheon
        {
            public string Title;
            public bool IsSplited = false;
        }
        #endregion
    }

    [Serializable]
    public class DTHollow
    {
        //Settings Vars
        public bool enableSplitting = false;
        public bool autoTimer = false;
        public bool gameTimer = false;
        public int PantheonMode = 0;
        public int positionMargin = 5;
        //Flags to Split
        public List<DefinitionHollow.ElementToSplitH> bossToSplit = new List<DefinitionHollow.ElementToSplitH>();
        public List<DefinitionHollow.ElementToSplitH> miniBossToSplit = new List<DefinitionHollow.ElementToSplitH>();
        public List<DefinitionHollow.ElementToSplitH> charmToSplit = new List<DefinitionHollow.ElementToSplitH>();
        public List<DefinitionHollow.ElementToSplitH> skillsToSplit = new List<DefinitionHollow.ElementToSplitH>();
        public List<DefinitionHollow.Pantheon> phanteonToSplit = new List<DefinitionHollow.Pantheon>();
        public List<DefinitionHollow.Vector3F> positionToSplit = new List<DefinitionHollow.Vector3F>();

        public List<DefinitionHollow.ElementToSplitH> GetBosstoSplit() => this.bossToSplit;

        public List<DefinitionHollow.ElementToSplitH> GetMiniBossToSplit() => this.miniBossToSplit;

        public List<DefinitionHollow.Pantheon> GetPhanteonToSplit() => this.phanteonToSplit;

        public List<DefinitionHollow.ElementToSplitH> GetCharmToSplit() => this.charmToSplit;

        public List<DefinitionHollow.ElementToSplitH> GetSkillsToSplit() => this.skillsToSplit;

        public List<DefinitionHollow.Vector3F> GetPositionToSplit() => this.positionToSplit;
    }
}

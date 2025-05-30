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

using ReaLTaiizor.Controls;
using SoulMemory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSplitterCore
{
    public partial class AutoSplitter : ReaLTaiizor.Forms.PoisonForm
    {
        bool isInitializing = false;
        bool darkModeHCM = false;
        SekiroSplitter sekiroSplitter = SekiroSplitter.GetIntance();
        HollowSplitter hollowSplitter = HollowSplitter.GetIntance();
        EldenSplitter eldenSplitter = EldenSplitter.GetIntance();
        Ds3Splitter ds3Splitter = Ds3Splitter.GetIntance();
        Ds2Splitter ds2Splitter = Ds2Splitter.GetIntance();
        Ds1Splitter ds1Splitter = Ds1Splitter.GetIntance();
        CelesteSplitter celesteSplitter = CelesteSplitter.GetIntance();
        DishonoredSplitter dishonoredSplitter = DishonoredSplitter.GetIntance();
        CupheadSplitter cupSplitter = CupheadSplitter.GetIntance();
        UpdateModule updateModule = UpdateModule.GetIntance();
        SaveModule saveModule;

        HitterControl hitterControl = HitterControl.GetControl();

        public AutoSplitter(SaveModule saveModule, bool darkMode)
        {
            InitializeComponent();
            this.saveModule = saveModule;
            this.darkModeHCM = darkMode;

            HitterControl._saveModule = saveModule;
        }

        public void RefreshForm()
        {
            #region ControlTab
            try
            {
                this.TabControlGeneral.Controls.Remove(this.tabInfo);
                this.TabControlGeneral.Controls.Remove(this.tabLicense);
                this.TabControlGeneral.Controls.Remove(this.tabGeneral);
                this.TabControlGeneral.Controls.Remove(this.tabSekiro);
                this.TabControlGeneral.Controls.Remove(this.tabDs1);
                this.TabControlGeneral.Controls.Remove(this.tabDs2);
                this.TabControlGeneral.Controls.Remove(this.tabDs3);
                this.TabControlGeneral.Controls.Remove(this.tabElden);
                this.TabControlGeneral.Controls.Remove(this.tabHollow);
                this.TabControlGeneral.Controls.Remove(this.tabCeleste);
                this.TabControlGeneral.Controls.Remove(this.tabCuphead);
                this.TabControlGeneral.Controls.Remove(this.tabDishonored);
                TabControlGeneral.SelectTab(tabConfig);
            }
            catch (Exception)
            {
                //Catch exception for remove controls that not in tabControlGeneral*/
            }

            #endregion
            #region SekiroTab       
            panelPositionS.Hide();
            panelBossS.Hide();
            panelCfSekiro.Hide();
            panelIdolsS.Hide();
            panelMortalJourney.Hide();
            panelMinibossSekiro.Hide();
            panelLevelSekiro.Hide();
            groupBoxAshinaOutskirts.Hide();
            groupBoxHirataEstate.Hide();
            groupBoxAshinaCastle.Hide();
            groupBoxAbandonedDungeon.Hide();
            groupBoxSenpouTemple.Hide();
            groupBoxSunkenValley.Hide();
            groupBoxAshinaDepths.Hide();
            groupBoxFountainhead.Hide();
            comboBoxZoneSelectS.SelectedIndex = 0;
            #endregion
            #region HollowTab
            panelBossH.Hide();
            panelItemH.Hide();
            groupBossH.Hide();
            groupBoxMBH.Hide();
            groupBoxPantheon.Hide();
            checkedListBoxPantheon.Hide();
            checkedListBoxPp.Hide();
            lbl_warning.Hide();
            groupBoxCharms.Hide();
            groupBoxSkillsH.Hide();
            panelPositionH.Hide();
            #endregion
            #region EldenTab
            panelBossER.Hide();
            panelGraceER.Hide();
            panelPositionsER.Hide();
            panelCfER.Hide();
            #endregion
            #region Ds3Tab
            panelBossDs3.Hide();
            panelBonfireDs3.Hide();
            panelLvlDs3.Hide();
            panelCfDs3.Hide();
            panelPositionsDs3.Hide();
            #endregion
            #region CelesteTab
            panelChapterCeleste.Hide();
            panelCheckpointsCeleste.Hide();
            panelCassettesNHearts.Hide();
            #endregion
            #region Ds2Tab
            panelBossDS2.Hide();
            panelLvlDs2.Hide();
            panelPositionDs2.Hide();
            #endregion
            #region CupheadTab
            panelBossCuphead.Hide();
            panelLevelCuphead.Hide();
            #endregion
            #region Ds1Tab
            panelBossDs1.Hide();
            panelBonfireDs1.Hide();
            panelLvlDs1.Hide();
            panelPositionDs1.Hide();
            panelItemDs1.Hide();
            #endregion
            #region TimingTab
            groupBoxTSekiro.Hide();
            groupBoxTDs1.Hide();
            groupBoxTDs2.Hide();
            groupBoxTDs3.Hide();
            groupBoxTEr.Hide();
            groupBoxTHK.Hide();
            groupBoxTCeleste.Hide();
            groupBoxTCuphead.Hide();
            groupBoxTDishonored.Hide();
            #endregion
            #region Update
            cbCheckUpdatesOnStartup.Checked = updateModule.CheckUpdatesOnStartup;
            LabelVersion.Text = updateModule.currentVer;
            labelCloudVer.Text = updateModule.cloudVer;
            #endregion
            //Override StyleMode
            switch (saveModule.generalAS.StyleMode)
            {
                case StyleMode.Light: //Light by Default
                    break;
                case StyleMode.Dark:
                    DarkMode(true);
                    darkModeHCM = true;
                    break;
                case StyleMode.Default:
                default:
                    DarkMode(darkModeHCM);
                    break;
            }
            checkStatusGames();
        }

        public void DarkMode(bool status)
        {
            if (status)
            {
                Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;

                //Tabpages
                TabControlGeneral.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;

                this.tabConfig.BackColor = Color.Teal;
                this.tabInfo.BackColor = Color.Teal;
                this.tabLicense.BackColor = Color.Teal;
                this.tabGeneral.BackColor = Color.Teal;
                this.tabSekiro.BackColor = Color.Teal;
                this.tabDs1.BackColor = Color.Teal;
                this.tabDs2.BackColor = Color.Teal;
                this.tabDs3.BackColor = Color.Teal;
                this.tabCuphead.BackColor = Color.Teal;
                this.tabElden.BackColor = Color.Teal;
                this.tabHollow.BackColor = Color.Teal;
                this.tabCeleste.BackColor = Color.Teal;
                this.tabDishonored.BackColor = Color.Teal;

                labelWarning.ForeColor = Color.Gold;

                //Panels
                SetTealColorToLostBorderPanels();
            }
        }

        public void SetTealColorToLostBorderPanels()
        {
            panelMortalJourney.BackColor = Color.LightBlue;
            panelIdolsS.BackColor = Color.LightBlue;
            panelCfSekiro.BackColor = Color.LightBlue;
            panelPositionS.BackColor = Color.LightBlue;
            panelBossS.BackColor = Color.LightBlue;
            panelPositionDs1.BackColor = Color.LightBlue;
            panelBossDs1.BackColor = Color.LightBlue;
            panelItemDs1.BackColor = Color.LightBlue;
            panelLvlDs1.BackColor = Color.LightBlue;
            panelBonfireDs1.BackColor = Color.LightBlue;
            panelPositionDs2.BackColor = Color.LightBlue;
            panelBossDS2.BackColor = Color.LightBlue;
            panelLvlDs2.BackColor = Color.LightBlue;
            panelLvlDs3.BackColor = Color.LightBlue;
            panelBossDs3.BackColor = Color.LightBlue;
            panelBonfireDs3.BackColor = Color.LightBlue;
            panelCfDs3.BackColor = Color.LightBlue;
            panelPositionsDs3.BackColor = Color.LightBlue;
            panelPositionsER.BackColor = Color.LightBlue;
            panelLevelSekiro.BackColor = Color.LightBlue;
            panelBossER.BackColor = Color.LightBlue;
            panelCfER.BackColor = Color.LightBlue;
            panelGraceER.BackColor = Color.LightBlue;
            panelPositionH.BackColor = Color.LightBlue;
            panelItemH.BackColor = Color.LightBlue;
            panelBossH.BackColor = Color.LightBlue;
            panelCheckpointsCeleste.BackColor = Color.LightBlue;
            panelCassettesNHearts.BackColor = Color.LightBlue;
            panelChapterCeleste.BackColor = Color.LightBlue;
            panelLevelCuphead.BackColor = Color.LightBlue;
            panelBossCuphead.BackColor = Color.LightBlue;
        }


        private void AutoSplitter_Load(object sender, EventArgs e)
        {
            isInitializing = true;
            #region AdminCheck
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal main = new WindowsPrincipal(identity);
            if (main.IsInRole(WindowsBuiltInRole.Administrator))
            {
                labelWarning.Hide();
            }
            else
            {
                labelWarning.Show();
            }
            #endregion
            #region Loading Texts
            var ASCReadme = System.Text.Encoding.UTF8.GetString(Properties.Resources.AUTOSPLITTERREADME);
            var ThirdPartyLicence = System.Text.Encoding.UTF8.GetString(Properties.Resources.THIRDPARTYLICENSEREADME);
            var MortalJourney = Properties.Resources.MortalJourney;
            textBoxManual.Text = ASCReadme;
            textBoxLicenses.Text = ThirdPartyLicence;
            hopeTextBoxMortal.Text = MortalJourney;
            #endregion

            DTSekiro sekiroData = sekiroSplitter.GetDataSekiro();
            #region SekiroLoad.Bosses
            foreach (DefinitionsSekiro.BossS boss in sekiroData.GetBossToSplit())
            {
                listBoxBossesS.Items.Add(boss.Title + " - " + boss.Mode);
            }
            #endregion
            #region SekiroLoad.MiniBosses
            foreach (DefinitionsSekiro.MiniBossS boss in sekiroData.GetMiniBossToSplit())
            {
                listBoxMiniBossesS.Items.Add(boss.Title + " - " + boss.Mode);
            }
            #endregion
            #region SekiroLoad.Idols
            foreach (DefinitionsSekiro.Idol idols in sekiroData.GetidolsTosplit())
            {
                for (int i = 0; i < checkedListBoxAshina.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxAshina.Items[i].ToString())
                    {
                        checkedListBoxAshina.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxHirataEstate.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxHirataEstate.Items[i].ToString())
                    {
                        checkedListBoxHirataEstate.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxAshinaCastle.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxAshinaCastle.Items[i].ToString())
                    {
                        checkedListBoxAshinaCastle.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxAbandonedDungeon.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxAbandonedDungeon.Items[i].ToString())
                    {
                        checkedListBoxAbandonedDungeon.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxSenpouTemple.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxSenpouTemple.Items[i].ToString())
                    {
                        checkedListBoxSenpouTemple.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxSunkenValley.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxSunkenValley.Items[i].ToString())
                    {
                        checkedListBoxSunkenValley.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxAshinaDepths.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxAshinaDepths.Items[i].ToString())
                    {
                        checkedListBoxAshinaDepths.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxFountainhead.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxFountainhead.Items[i].ToString())
                    {
                        checkedListBoxFountainhead.SetItemChecked(i, true);
                    }
                }

            }
            #endregion
            #region SekiroLoad.Position
            foreach (DefinitionsSekiro.PositionS position in sekiroData.GetPositionsToSplit())
            {
                listBoxPositionsS.Items.Add(position.vector.X + "; " + position.vector.Y + "; " + position.vector.Z + " - " + position.Mode + position.Title);
            }
            comboBoxSizeS.SelectedIndex = sekiroData.positionMargin;
            #endregion
            #region SekiroLoad.MortalJourney
            if (sekiroData.mortalJourneyRun)
            {
                checkBoxMortalJourneyRun.Checked = true;
            }
            else
            {
                checkBoxMortalJourneyRun.Checked = false;
            }
            #endregion
            #region SekiroLoad.CustomFlag
            foreach (DefinitionsSekiro.CfSk cf in sekiroData.GetFlagToSplit())
            {
                listBoxCfS.Items.Add(cf.Id + " - " + cf.Mode + cf.Title);
            }
            #endregion
            #region SekiroLoad.Level
            foreach (DefinitionsSekiro.LevelS lvl in sekiroData.GetLvlToSplit())
            {
                listBoxAttributeS.Items.Add(lvl.Attribute + ": " + lvl.Value + " - " + lvl.Mode);
            }
            #endregion
            DTHollow hollowData = hollowSplitter.GetDataHollow();
            #region HollowLoad.Boss
            foreach (var b in hollowData.GetBosstoSplit())
            {
                for (int i = 0; i < checkedListBoxBossH.Items.Count; i++)
                {
                    if (b.Title == checkedListBoxBossH.Items[i].ToString())
                    {
                        checkedListBoxBossH.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region HollowLoad.MiniBoss
            foreach (var mb in hollowData.GetMiniBossToSplit())
            {
                for (int i = 0; i < checkedListBoxHMB.Items.Count; i++)
                {
                    if (mb.Title == checkedListBoxHMB.Items[i].ToString())
                    {
                        checkedListBoxHMB.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region HollowLoad.Pantheon
            foreach (var p in hollowData.GetPhanteonToSplit())
            {
                for (int i = 0; i < checkedListBoxPantheon.Items.Count; i++)
                {
                    if (p.Title == checkedListBoxPantheon.Items[i].ToString())
                    {
                        checkedListBoxPantheon.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxPp.Items.Count; i++)
                {
                    if (p.Title == checkedListBoxPp.Items[i].ToString())
                    {
                        checkedListBoxPp.SetItemChecked(i, true);
                    }
                }

            }
            comboBoxHowP.SelectedIndex = hollowData.PantheonMode;

            #endregion
            #region HollowLoad.Charm
            foreach (var c in hollowData.GetCharmToSplit())
            {
                for (int i = 0; i < checkedListBoxCharms.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxCharms.Items[i].ToString())
                    {
                        checkedListBoxCharms.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region HollowLoad.Skill
            foreach (var c in hollowData.GetSkillsToSplit())
            {
                for (int i = 0; i < checkedListBoxSkillsH.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxSkillsH.Items[i].ToString())
                    {
                        checkedListBoxSkillsH.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region HollowLoad.Position
            foreach (var p in hollowData.GetPositionToSplit())
            {
                listBoxPositionH.Items.Add(p.position + " - " + p.sceneName + p.Title);
            }
            comboBoxSizeH.SelectedIndex = hollowData.positionMargin;
            #endregion
            DTElden eldenData = eldenSplitter.GetDataElden();
            #region EldenLoad.Boss
            comboBoxBossER_DLC.Hide();
            foreach (DefinitionsElden.BossER boss in eldenData.GetBossToSplit())
            {
                listBoxBossER.Items.Add(boss.Title + " - " + boss.Mode);
            }
            #endregion
            #region EldenLoad.Grace
            comboBoxGraceDLC_ER.Hide();
            foreach (DefinitionsElden.GraceER grace in eldenData.GetGraceToSplit())
            {
                listBoxGrace.Items.Add(grace.Title + " - " + grace.Mode);
            }
            #endregion
            #region EldenLoad.Positions
            foreach (DefinitionsElden.PositionER position in eldenData.GetPositionToSplit())
            {
                listBoxPositionsER.Items.Add(position.vector + " - " + position.Mode + position.Title);
            }
            comboBoxSizeER.SelectedIndex = eldenData.positionMargin;
            #endregion
            #region EldenLoad.CustomFlags
            foreach (DefinitionsElden.CustomFlagER cf in eldenData.GetFlagsToSplit())
            {
                listBoxCfER.Items.Add(cf.Id + " - " + cf.Mode + cf.Title);
            }
            #endregion
            DTDs3 ds3Data = ds3Splitter.GetDataDs3();
            #region Ds3Load.Boss
            foreach (DefinitionsDs3.BossDs3 boss in ds3Data.GetBossToSplit())
            {
                listBoxBossDs3.Items.Add(boss.Title + " - " + boss.Mode);
            }
            #endregion
            #region Ds3Load.Bonfire
            foreach (DefinitionsDs3.BonfireDs3 bon in ds3Data.GetBonfireToSplit())
            {
                listBoxBonfireDs3.Items.Add(bon.Title + " - " + bon.Mode);
            }
            #endregion
            #region Ds3Load.Lvl
            foreach (DefinitionsDs3.LvlDs3 lvl in ds3Data.GetLvlToSplit())
            {
                listBoxAttributesDs3.Items.Add(lvl.Attribute + ": " + lvl.Value + " - " + lvl.Mode);
            }
            #endregion
            #region Ds3Load.CustomFlags
            foreach (DefinitionsDs3.CfDs3 cf in ds3Data.GetFlagToSplit())
            {
                listBoxCfDs3.Items.Add(cf.Id + " - " + cf.Mode + cf.Title);
            }
            #endregion
            #region Ds3Load.Position
            foreach (DefinitionsDs3.PositionDs3 position in ds3Data.GetPositionsToSplit())
            {
                listBoxPositionsDs3.Items.Add(position.vector + " - " + position.Mode + position.Title);
            }
            comboBoxMarginDs3.SelectedIndex = ds3Data.positionMargin;
            #endregion
            DTCeleste celesteData = celesteSplitter.GetDataCeleste();
            #region CelesteLoad.Chapters
            foreach (var c in celesteData.GetChapterToSplit())
            {
                for (int i = 0; i < checkedListBoxChapterCeleste.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxChapterCeleste.Items[i].ToString())
                    {
                        checkedListBoxChapterCeleste.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region CelesteLoad.Checkpoints
            foreach (var c in celesteData.GetChapterToSplit())
            {
                for (int i = 0; i < checkedListBoxCheckpointsCeleste.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxCheckpointsCeleste.Items[i].ToString())
                    {
                        checkedListBoxCheckpointsCeleste.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region CelesteLoad.CassettesNHearts
            foreach (var c in celesteData.GetChapterToSplit())
            {
                for (int i = 0; i < checkedListBoxCassettesNHearts.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxCassettesNHearts.Items[i].ToString())
                    {
                        checkedListBoxCassettesNHearts.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            labelCurrentPiped.Text = celesteSplitter.pipeConnected ? "Enabled" : "Disable";
            DTDs2 ds2Data = ds2Splitter.GetDataDs2();
            #region Ds2Load.Boss
            foreach (DefinitionsDs2.BossDs2 boss in ds2Data.GetBossToSplit())
            {
                listBoxBossDs2.Items.Add(boss.Title + " - " + boss.Mode);
            }
            #endregion
            #region Ds2Load.Lvl
            foreach (DefinitionsDs2.LvlDs2 lvl in ds2Data.GetLvlToSplit())
            {
                listBoxAttributeDs2.Items.Add(lvl.Attribute + ": " + lvl.Value + " - " + lvl.Mode);
            }
            #endregion
            #region Ds2Load.Position
            foreach (DefinitionsDs2.PositionDs2 position in ds2Data.GetPositionsToSplit())
            {
                listBoxPositionsDs2.Items.Add(position.vector + " - " + position.Mode + position.Title);
            }
            comboBoxSizeDs2.SelectedIndex = ds2Data.positionMargin;
            #endregion
            DTCuphead cupData = cupSplitter.GetDataCuphead();
            #region CupheadLoad.Boss&Level
            foreach (var c in cupData.GetElementToSplit())
            {
                for (int i = 0; i < checkedListBoxBossCuphead.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxBossCuphead.Items[i].ToString())
                    {
                        checkedListBoxBossCuphead.SetItemChecked(i, true);
                    }
                }
                for (int i = 0; i < checkedListLevelCuphead.Items.Count; i++)
                {
                    if (c.Title == checkedListLevelCuphead.Items[i].ToString())
                    {
                        checkedListLevelCuphead.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            DTDs1 ds1Data = ds1Splitter.GetDataDs1();
            #region Ds1Load.Boss
            foreach (DefinitionsDs1.BossDs1 boss in ds1Data.GetBossToSplit())
            {
                listBoxBossDs1.Items.Add(boss.Title + " - " + boss.Mode);
            }
            #endregion
            #region Ds1Load.Bonfire
            foreach (DefinitionsDs1.BonfireDs1 bon in ds1Data.GetBonfireToSplit())
            {
                listBoxBonfireDs1.Items.Add(bon.Title + " - " + bon.Value + " - " + bon.Mode);
            }
            #endregion
            #region Ds1Load.Lvl
            foreach (DefinitionsDs1.LvlDs1 lvl in ds1Data.GetLvlToSplit())
            {
                listBoxAttributesDs1.Items.Add(lvl.Attribute + ": " + lvl.Value + " - " + lvl.Mode);
            }
            #endregion
            #region Ds1Load.Position
            foreach (DefinitionsDs1.PositionDs1 position in ds1Data.GetPositionsToSplit())
            {
                listBoxPositionsDs1.Items.Add(position.vector + " - " + position.Mode + position.Title);
            }
            comboBoxSizeDs1.SelectedIndex = ds1Data.positionMargin;
            #endregion
            #region Ds1Load.Items
            foreach (DefinitionsDs1.ItemDs1 Item in ds1Data.GetItemsToSplit())
            {
                listBoxItemDs1.Items.Add(Item.Title + " - " + Item.Mode);
            }
            #endregion
            DTDishonored dishData = dishonoredSplitter.GetDataDishonored();
            #region DishonoredLoad.Options
            foreach (var o in dishData.GetOptionToSplit())
            {
                for (int i = 0; i < checkedListBoxDishonored.Items.Count; i++)
                {
                    if (o.Option == checkedListBoxDishonored.Items[i].ToString())
                    {
                        if (o.Enable)
                            checkedListBoxDishonored.SetItemChecked(i, true);
                        else
                            checkedListBoxDishonored.SetItemChecked(i, false);
                    }
                }
            }
            #endregion

            #region GeneralSettings
            checkBoxResetSplitNg.Checked = saveModule.generalAS.AutoResetSplit;
            checkBoxLogActive.Checked = saveModule.generalAS.LogFile;
            skyComboBoxOverrideStyleMode.SelectedItem = saveModule.generalAS.StyleMode.ToString();

            #region ProfileLink
            if (!SplitterControl.GetControl().GetDebug())
            {
                foreach (var profile in SplitterControl.GetControl().GetAllHcmProfile())
                {
                    skyComboBoxHcmProfile.Items.Add(profile);
                }
            }

            var savepath = saveModule.generalAS.saveProfilePath;
            if (!Directory.Exists(savepath))
            {
                Directory.CreateDirectory(savepath);
            }

            foreach (string file in Directory.GetFiles(savepath))
            {
                var auxfile = file.Remove(0, savepath.Length + 1);
                if (auxfile.Contains("xml"))
                {
                    skyComboBoxAscProfile.Items.Add(auxfile);
                }
            }

            foreach (var profileLink in saveModule.generalAS.ProfileLinks)
            {
                string getProfileLink = $"{profileLink.profileHCM} - {profileLink.profileASC}";
                listBoxLinkProfile.Items.Add(getProfileLink);
            }

            metroCheckBoxResetProfile.Checked = saveModule.generalAS.ResetProfile;

            #endregion

            if (saveModule.generalAS.HitMode == HitMode.Way) skyComboBoxHitMode.SelectedIndex = 0;
            else skyComboBoxHitMode.SelectedIndex = 1;

            //AutoHit
            checkBoxHitCeleste.Checked = saveModule.generalAS.AutoHitCeleste;
            checkBoxHitHollow.Checked = saveModule.generalAS.AutoHitHollow;

            //WebSockets
            textBoxUrl.Text = saveModule.generalAS.WebSocketSettings.Url;

            SetWebSocketConfig(skyTextBoxWSMHit, metroCheckBoxWSHit, saveModule.generalAS.WebSocketSettings.Hit);
            SetWebSocketConfig(skyTextBoxWSMSplit, metroCheckBoxWSSplit, saveModule.generalAS.WebSocketSettings.Split);
            SetWebSocketConfig(skyTextBoxWSMStart, metroCheckBoxWSStart, saveModule.generalAS.WebSocketSettings.Start);
            SetWebSocketConfig(skyTextBoxWSMReset, metroCheckBoxWSReset, saveModule.generalAS.WebSocketSettings.Reset);

            #endregion

            #region Timming

            if (sekiroData.autoTimer)
            {
                checkBoxATS.Checked = true;
                groupBoxTMS.Enabled = true;
            }
            else
            {
                checkBoxATS.Checked = false;
                groupBoxTMS.Enabled = false;
            }
            if (sekiroData.gameTimer)
            {
                radioIGTSTimer.Checked = true;
                radioRealTimerS.Checked = false;
            }
            else
            {
                radioIGTSTimer.Checked = false;
                radioRealTimerS.Checked = true;
            }

            if (ds1Data.autoTimer)
            {
                checkBoxATDs1.Checked = true;
                groupBoxTMDs1.Enabled = true;
            }
            else
            {
                checkBoxATDs1.Checked = false;
                groupBoxTMDs1.Enabled = false;
            }

            if (ds1Data.gameTimer)
            {
                radioIGTDs1.Checked = true;
                radioRealTimerDs1.Checked = false;
            }
            else
            {
                radioIGTDs1.Checked = false;
                radioRealTimerDs1.Checked = true;
            }

            if (ds2Data.autoTimer)
            {
                checkBoxATDs2.Checked = true;
                groupBoxTMDs2.Enabled = true;
            }
            else
            {
                checkBoxATDs2.Checked = false;
                groupBoxTMDs2.Enabled = false;
            }

            if (ds2Data.gameTimer)
            {
                radioIGTDs2.Checked = true;
                radioRealTimerDs2.Checked = false;
            }
            else
            {
                radioIGTDs2.Checked = false;
                radioRealTimerDs2.Checked = true;
            }

            if (ds3Data.autoTimer)
            {
                checkBoxATDs3.Checked = true;
                groupBoxTMDs3.Enabled = true;
            }
            else
            {
                checkBoxATDs3.Checked = false;
                groupBoxTMDs3.Enabled = false;
            }

            if (ds3Data.gameTimer)
            {
                radioIGTDs3.Checked = true;
                radioRealTimerDs3.Checked = false;
            }
            else
            {
                radioIGTDs3.Checked = false;
                radioRealTimerDs3.Checked = true;
            }


            if (eldenData.autoTimer)
            {
                checkBoxATEr.Checked = true;
                groupBoxTMEr.Enabled = true;
            }
            else
            {
                checkBoxATEr.Checked = false;
                groupBoxTMEr.Enabled = false;
            }

            if (eldenData.gameTimer)
            {
                radioIGTEr.Checked = true;
                radioRealTimerEr.Checked = false;
            }
            else
            {
                radioIGTEr.Checked = false;
                radioRealTimerEr.Checked = true;
            }

            if (hollowData.autoTimer)
            {
                checkBoxATHollow.Checked = true;
                groupBoxTMHollow.Enabled = true;
            }
            else
            {
                checkBoxATHollow.Checked = false;
                groupBoxTMHollow.Enabled = false;
            }

            if (hollowData.gameTimer)
            {
                radioIGTHollow.Checked = true;
                radioRealTimerHollow.Checked = false;
            }
            else
            {
                radioIGTHollow.Checked = false;
                radioRealTimerHollow.Checked = true;
            }

            if (celesteData.autoTimer)
            {
                checkBoxATCeleste.Checked = true;
                groupBoxTMCeleste.Enabled = true;
            }
            else
            {
                checkBoxATCeleste.Checked = false;
                groupBoxTMCeleste.Enabled = false;
            }

            if (celesteData.gameTimer)
            {
                radioIGTCeleste.Checked = true;
                radioRealTimerCeleste.Checked = false;
            }
            else
            {
                radioIGTCeleste.Checked = false;
                radioRealTimerCeleste.Checked = true;
            }

            if (cupData.autoTimer)
            {
                checkBoxATCuphead.Checked = true;
                groupBoxTMCuphead.Enabled = true;
            }
            else
            {
                checkBoxATCuphead.Checked = false;
                groupBoxTMCuphead.Enabled = false;
            }

            if (cupData.gameTimer)
            {
                radioIGTCuphead.Checked = true;
                radioRealTimerCuphead.Checked = false;
            }
            else
            {
                radioIGTCuphead.Checked = false;
                radioRealTimerCuphead.Checked = true;
            }

            if (dishData.autoTimer)
            {
                checkBoxATDishonored.Checked = true;
                groupBoxTMDishonored.Enabled = true;
            }
            else
            {
                checkBoxATDishonored.Checked = false;
                groupBoxTMDishonored.Enabled = false;
            }

            if (dishData.gameTimer)
            {
                radioIGTDishonored.Checked = true;
                radioRealTimerDishonored.Checked = false;
            }
            else
            {
                radioIGTDishonored.Checked = false;
                radioRealTimerDishonored.Checked = true;
            }
            #endregion
            #region Update
            cbCheckUpdatesOnStartup.Checked = updateModule.CheckUpdatesOnStartup;
            LabelVersion.Text = updateModule.currentVer;
            labelCloudVer.Text = updateModule.cloudVer;
            #endregion

            RefreshForm();
            isInitializing = false;
        }

        void SetWebSocketConfig(SkyTextBox messageBox, MetroCheckBox checkBox, WebSocketMessageConfig config)
        {
            messageBox.Text = config.Message;
            checkBox.Checked = config.Enabled;
        }

        private void Refresh_Btn(object sender, EventArgs e)
        {
            checkStatusGames();
        }

        #region checkStatusGames
        public void checkStatusGames()
        {
            if (sekiroSplitter.GetSekiroStatusProcess(0))
            {
                sekiroRunning.Show();
                SekiroNotRunning.Hide();
            }
            else
            {
                SekiroNotRunning.Show();
                sekiroRunning.Hide();
            }
            if (hollowSplitter.GetHollowStatusProcess(0))
            {
                HollowRunning.Show();
                HollowNotRunning.Hide();
            }
            else
            {
                HollowRunning.Hide();
                HollowNotRunning.Show();
            }
            if (eldenSplitter.GetEldenStatusProcess(0))
            {
                EldenRingRunning.Show();
                EldenRingNotRunning.Hide();
            }
            else
            {
                EldenRingRunning.Hide();
                EldenRingNotRunning.Show();
            }
            if (ds3Splitter.GetDs3StatusProcess(0))
            {
                Ds3Running.Show();
                Ds3NotRunning.Hide();
            }
            else
            {
                Ds3Running.Hide();
                Ds3NotRunning.Show();
            }
            if (celesteSplitter.GetCelesteStatusProcess(0))
            {
                CelesteRunning.Show();
                CelesteNotRunning.Hide();
            }
            else
            {
                CelesteNotRunning.Show();
                CelesteRunning.Hide();
            }
            if (ds2Splitter.GetDs2StatusProcess(0))
            {
                Ds2Running.Show();
                Ds2NotRunning.Hide();
            }
            else
            {
                Ds2NotRunning.Show();
                Ds2Running.Hide();
            }
            if (cupSplitter.GetCupheadStatusProcess(0))
            {
                CupheadRunning.Show();
                CupheadNotRunning.Hide();
            }
            else
            {
                CupheadNotRunning.Show();
                CupheadRunning.Hide();
            }
            if (ds1Splitter.GetDs1StatusProcess(0))
            {
                Ds1Running.Show();
                Ds1NotRunning.Hide();
            }
            else
            {
                Ds1NotRunning.Show();
                Ds1Running.Hide();
            }
            if (dishonoredSplitter.GetDishonoredStatusProcess())
            {
                DishonoredRunning.Show();
                DishonoredNotRunning.Hide();
            }
            else
            {
                DishonoredNotRunning.Show();
                DishonoredRunning.Hide();
            }
        }
        #endregion
        #region Config UI
        private void btnClose_Click(object sender, EventArgs e) => Close();

        #region General

        //General
        private void checkBoxResetSplitNg_CheckedChanged(object sender, EventArgs e) => saveModule.generalAS.AutoResetSplit = checkBoxResetSplitNg.Checked;

        private void checkBoxLogActive_CheckedChanged(object sender, EventArgs e) => saveModule.generalAS.LogFile = checkBoxLogActive.Checked;

        //Override StyleMode
        private void skyComboBoxOverrideStyleMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            if (Enum.TryParse(skyComboBoxOverrideStyleMode.SelectedItem.ToString(), out StyleMode selectedStyleMode))
            {
                saveModule.generalAS.StyleMode = selectedStyleMode;

                DialogResult result = MessageBox.Show("Do you want update Interface?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Controls.Clear();
                    this.InitializeComponent();
                    RefreshForm();
                    this.AutoSplitter_Load(null, null);//Load Others Games Settings
                    TabControlGeneral.TabPages.Add(tabGeneral);
                    TabControlGeneral.SelectTab(tabGeneral);
                }
            }
        }

        //ProfileLink
        private void metroCheckBoxResetProfile_CheckedChanged(object sender) => saveModule.generalAS.ResetProfile = metroCheckBoxResetProfile.Checked;

        private void ProfileLink_DropDown(object sender, EventArgs e)
        {
            SkyComboBox comboBox = sender as SkyComboBox;

            int maxWidth = comboBox.Width;
            using (Graphics g = comboBox.CreateGraphics())
            {
                foreach (var item in comboBox.Items)
                {
                    int itemWidth = (int)g.MeasureString(item.ToString(), comboBox.Font).Width;
                    if (itemWidth > maxWidth)
                    {
                        maxWidth = itemWidth;
                    }
                }
            }

            comboBox.DropDownWidth = maxWidth + 20;
        }

        private void listBoxLinkProfile_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxLinkProfile.SelectedItem != null)
            {
                int index = listBoxLinkProfile.SelectedIndex;
                if (index >= 0)
                {
                    saveModule.RemoveProfileLink(index);
                    listBoxLinkProfile.Items.RemoveAt(index);
                }
            }
        }

        private void buttonAddLinkProfile_Click(object sender, EventArgs e)
        {
            if (skyComboBoxAscProfile.SelectedIndex > -1 && skyComboBoxHcmProfile.SelectedIndex > -1)
            {
                string profileHcm = skyComboBoxHcmProfile.SelectedItem.ToString();
                string profileAsc = skyComboBoxAscProfile.SelectedItem.ToString();


                string profileLink = $"{profileHcm} - {profileAsc}";
                if (!listBoxLinkProfile.Items.Contains(profileLink))
                {
                    saveModule.AddProfileLink(profileHcm, profileAsc);
                    listBoxLinkProfile.Items.Add(profileLink);
                }
                else
                {
                    MessageBox.Show("You have already added this ProfileLink", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You must select HCM and ASC Profile to link", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region TimmingConfg
        private void comboBoxTGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBoxTSekiro.Hide();
            groupBoxTDs1.Hide();
            groupBoxTDs2.Hide();
            groupBoxTDs3.Hide();
            groupBoxTEr.Hide();
            groupBoxTHK.Hide();
            groupBoxTCeleste.Hide();
            groupBoxTCuphead.Hide();
            groupBoxTDishonored.Hide();

            switch (comboBoxTGame.SelectedIndex)
            {
                case 0: groupBoxTSekiro.Show(); break;
                case 1: groupBoxTDs1.Show(); break;
                case 2: groupBoxTDs2.Show(); break;
                case 3: groupBoxTDs3.Show(); break;
                case 4: groupBoxTEr.Show(); break;
                case 5: groupBoxTHK.Show(); break;
                case 6: groupBoxTCeleste.Show(); break;
                case 7: groupBoxTCuphead.Show(); break;
                case 8: groupBoxTDishonored.Show(); break;
            }
        }

        private void btnDesactiveAllTiming_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                sekiroSplitter.GetDataSekiro().autoTimer = false;
                sekiroSplitter.GetDataSekiro().gameTimer = false;
                ds1Splitter.GetDataDs1().autoTimer = false;
                ds1Splitter.GetDataDs1().gameTimer = false;
                ds2Splitter.GetDataDs2().autoTimer = false;
                ds2Splitter.GetDataDs2().gameTimer = false;
                ds3Splitter.GetDataDs3().autoTimer = false;
                ds3Splitter.GetDataDs3().gameTimer = false;
                eldenSplitter.GetDataElden().autoTimer = false;
                eldenSplitter.GetDataElden().gameTimer = false;
                hollowSplitter.GetDataHollow().autoTimer = false;
                hollowSplitter.GetDataHollow().gameTimer = false;
                celesteSplitter.GetDataCeleste().autoTimer = false;
                celesteSplitter.GetDataCeleste().gameTimer = false;
                cupSplitter.GetDataCuphead().autoTimer = false;
                cupSplitter.GetDataCuphead().gameTimer = false;
                dishonoredSplitter.GetDataDishonored().autoTimer = false;
                dishonoredSplitter.GetDataDishonored().gameTimer = false;
                this.Controls.Clear();
                this.InitializeComponent();
                RefreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControlGeneral.TabPages.Add(tabGeneral);
                TabControlGeneral.SelectTab(tabGeneral);
            }
        }

        private void radioIGTSTimer_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTSTimer.Checked ? sekiroSplitter.GetDataSekiro().gameTimer = true : sekiroSplitter.GetDataSekiro().gameTimer = false;
        }

        private void checkBoxATS_CheckedChanged(object sender, EventArgs e)
        {
            _ = checkBoxATS.Checked ? sekiroSplitter.GetDataSekiro().autoTimer = true : sekiroSplitter.GetDataSekiro().autoTimer = false;
            _ = checkBoxATS.Checked ? groupBoxTMS.Enabled = true : groupBoxTMS.Enabled = false;
            if (!checkBoxATS.Checked) { sekiroSplitter.GetDataSekiro().gameTimer = false; radioIGTSTimer.Checked = false; radioRealTimerS.Checked = true; }
        }

        private void checkBoxATDs1_CheckedChanged_1(object sender, EventArgs e)
        {
            _ = checkBoxATDs1.Checked ? ds1Splitter.GetDataDs1().autoTimer = true : ds1Splitter.GetDataDs1().autoTimer = false;
            _ = checkBoxATDs1.Checked ? groupBoxTMDs1.Enabled = true : groupBoxTMDs1.Enabled = false;
            if (!checkBoxATDs1.Checked) { ds1Splitter.GetDataDs1().gameTimer = false; radioIGTDs1.Checked = false; radioRealTimerDs1.Checked = true; }
        }

        private void radioIGTDs1_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTDs1.Checked ? ds1Splitter.GetDataDs1().gameTimer = true : ds1Splitter.GetDataDs1().gameTimer = false;
        }

        private void checkBoxATDs2_CheckedChanged_1(object sender, EventArgs e)
        {
            _ = checkBoxATDs2.Checked ? ds2Splitter.GetDataDs2().autoTimer = true : ds2Splitter.GetDataDs2().autoTimer = false;
            _ = checkBoxATDs2.Checked ? groupBoxTMDs2.Enabled = true : groupBoxTMDs2.Enabled = false;
            if (!checkBoxATDs2.Checked) { ds2Splitter.GetDataDs2().gameTimer = false; radioIGTDs2.Checked = false; radioRealTimerDs2.Checked = true; }
        }

        private void radioIGTDs2_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTDs2.Checked ? ds2Splitter.GetDataDs2().gameTimer = true : ds2Splitter.GetDataDs2().gameTimer = false;
        }

        private void checkBoxATDs3_CheckedChanged_1(object sender, EventArgs e)
        {
            _ = checkBoxATDs3.Checked ? ds3Splitter.GetDataDs3().autoTimer = true : ds3Splitter.GetDataDs3().autoTimer = false;
            _ = checkBoxATDs3.Checked ? groupBoxTMDs3.Enabled = true : groupBoxTMDs3.Enabled = false;
            if (!checkBoxATDs3.Checked) { ds3Splitter.GetDataDs3().gameTimer = false; radioIGTDs3.Checked = false; radioRealTimerDs3.Checked = true; }
        }

        private void radioIGTDs3_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTDs3.Checked ? ds3Splitter.GetDataDs3().gameTimer = true : ds3Splitter.GetDataDs3().gameTimer = false;
        }

        private void checkBoxATEr_CheckedChanged_1(object sender, EventArgs e)
        {
            _ = checkBoxATEr.Checked ? eldenSplitter.GetDataElden().autoTimer = true : eldenSplitter.GetDataElden().autoTimer = false;
            _ = checkBoxATEr.Checked ? groupBoxTMEr.Enabled = true : groupBoxTMEr.Enabled = false;
            if (!checkBoxATEr.Checked) { eldenSplitter.GetDataElden().gameTimer = false; radioIGTEr.Checked = false; radioRealTimerEr.Checked = true; }
        }

        private void radioIGTEr_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTEr.Checked ? eldenSplitter.GetDataElden().gameTimer = true : eldenSplitter.GetDataElden().gameTimer = false;
        }

        private void checkBoxATCeleste_CheckedChanged_1(object sender, EventArgs e)
        {
            _ = checkBoxATCeleste.Checked ? celesteSplitter.GetDataCeleste().autoTimer = true : celesteSplitter.GetDataCeleste().autoTimer = false;
            _ = checkBoxATCeleste.Checked ? groupBoxTMCeleste.Enabled = true : groupBoxTMCeleste.Enabled = false;
            if (!checkBoxATCeleste.Checked) { celesteSplitter.GetDataCeleste().gameTimer = false; radioIGTCeleste.Checked = false; radioRealTimerCeleste.Checked = true; }
        }

        private void radioIGTCeleste_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTCeleste.Checked == true ? celesteSplitter.GetDataCeleste().gameTimer = true : celesteSplitter.GetDataCeleste().gameTimer = false;
        }

        private void checkBoxATCuphead_CheckedChanged_1(object sender, EventArgs e)
        {
            _ = checkBoxATCuphead.Checked ? cupSplitter.GetDataCuphead().autoTimer = true : cupSplitter.GetDataCuphead().autoTimer = false;
            _ = checkBoxATCuphead.Checked ? groupBoxTMCuphead.Enabled = true : groupBoxTMCuphead.Enabled = false;
            if (!checkBoxATCuphead.Checked) { cupSplitter.GetDataCuphead().gameTimer = false; radioIGTCuphead.Checked = false; radioRealTimerCuphead.Checked = true; }
        }

        private void radioIGTCuphead_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTCuphead.Checked ? cupSplitter.GetDataCuphead().gameTimer = true : cupSplitter.GetDataCuphead().gameTimer = false;
        }

        private void checkBoxATHollow_CheckedChanged(object sender, EventArgs e)
        {
            _ = checkBoxATHollow.Checked ? hollowSplitter.GetDataHollow().autoTimer = true : hollowSplitter.GetDataHollow().autoTimer = false;
            _ = checkBoxATHollow.Checked ? groupBoxTMHollow.Enabled = true : groupBoxTMHollow.Enabled = false;
            if (!checkBoxATHollow.Checked) { hollowSplitter.GetDataHollow().gameTimer = false; radioIGTHollow.Checked = false; radioRealTimerHollow.Checked = true; }
        }

        private void radioIGTHollow_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTHollow.Checked == true ? hollowSplitter.GetDataHollow().gameTimer = true : hollowSplitter.GetDataHollow().gameTimer = false;
        }

        private void checkBoxATDishonored_CheckedChanged(object sender, EventArgs e)
        {
            _ = checkBoxATDishonored.Checked ? dishonoredSplitter.GetDataDishonored().autoTimer = true : dishonoredSplitter.GetDataDishonored().autoTimer = false;
            _ = checkBoxATDishonored.Checked ? groupBoxTMDishonored.Enabled = true : groupBoxTMDishonored.Enabled = false;
            if (!checkBoxATDishonored.Checked) { dishonoredSplitter.GetDataDishonored().gameTimer = false; radioIGTDishonored.Checked = false; radioRealTimerDishonored.Checked = true; }
        }

        private void radioIGTDishonored_CheckedChanged(object sender, EventArgs e)
        {
            _ = radioIGTDishonored.Checked == true ? dishonoredSplitter.GetDataDishonored().gameTimer = true : dishonoredSplitter.GetDataDishonored().gameTimer = false;
        }

        private void checkBoxResetIgtDs3_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioIGTDs3.Checked && checkBoxResetIgtDs3.Checked) { MessageBox.Show("You should activate IGT in timing options", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); checkBoxResetIgtDs3.Checked = false; }
            else
            {
                _ = checkBoxResetIgtDs3.Checked ? ds3Splitter.GetDataDs3().ResetIGTNG = true : ds3Splitter.GetDataDs3().ResetIGTNG = false;
            }
        }

        private void checkBoxResetIGTNGEr_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioIGTEr.Checked && checkBoxResetIGTNGEr.Checked) { MessageBox.Show("You should activate IGT in timing options", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); checkBoxResetIGTNGEr.Checked = false; }
            else
            {
                _ = checkBoxResetIGTNGEr.Checked ? eldenSplitter.GetDataElden().ResetIGTNG = true : eldenSplitter.GetDataElden().ResetIGTNG = false;
            }
        }

        private void checkBoxResetIGTSekiro_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioIGTSTimer.Checked && checkBoxResetIGTSekiro.Checked) { MessageBox.Show("You should activate IGT in timing options", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); checkBoxResetIGTSekiro.Checked = false; }
            else
            {
                _ = checkBoxResetIGTSekiro.Checked ? sekiroSplitter.GetDataSekiro().ResetIGTNG = true : sekiroSplitter.GetDataSekiro().ResetIGTNG = false;
            }
        }
        #endregion
        #region AutoHitter

        private void skyComboBoxHitMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveModule.generalAS.HitMode = skyComboBoxHitMode.SelectedIndex == 0
                ? HitMode.Way
                : HitMode.Boss;
        }

        private void checkBoxHitCeleste_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHitCeleste.Checked)
            {
                saveModule.generalAS.AutoHitCeleste = true;
                hitterControl.StartCeleste();
            }
            else
            {
                saveModule.generalAS.AutoHitCeleste = false;
                hitterControl.StopCeleste();
            }
        }

        private void checkBoxHitHollow_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHitHollow.Checked)
            {
                saveModule.generalAS.AutoHitHollow = true;
                hitterControl.StartHollow();
            }
            else
            {
                saveModule.generalAS.AutoHitHollow = false;
                hitterControl.StopHollow();
            }
        }

        private void linkLabelEverest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => AutoSplitterMainModule.OpenWithBrowser(new Uri("https://everestapi.github.io/"));

        private void linkLabelDeathCounter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => AutoSplitterMainModule.OpenWithBrowser(new Uri("https://gamebanana.com/mods/577187"));
        #endregion
        #region WebSockets
        private void CheckBoxWS_CheckedChanged(object sender)
        {
            if (sender is MetroCheckBox checkBox)
            {
                var ws = saveModule.generalAS.WebSocketSettings;

                switch (checkBox.Name)
                {
                    case "metroCheckBoxWSHit":
                        ws.Hit.Enabled = checkBox.Checked;
                        break;
                    case "metroCheckBoxWSSplit":
                        ws.Split.Enabled = checkBox.Checked;
                        break;
                    case "metroCheckBoxWSStart":
                        ws.Start.Enabled = checkBox.Checked;
                        break;
                    case "metroCheckBoxWSReset":
                        ws.Reset.Enabled = checkBox.Checked;
                        break;
                }
            }
        }

        private void textBoxWS_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is SkyTextBox textBox)
            {
                var ws = saveModule.generalAS.WebSocketSettings;

                switch (textBox.Name)
                {
                    case "textBoxUrl":
                        ws.Url = textBox.Text;
                        break;
                    case "skyTextBoxWSMHit":
                        ws.Hit.Message = textBox.Text;
                        break;
                    case "skyTextBoxWSMSplit":
                        ws.Split.Message = textBox.Text;
                        break;
                    case "skyTextBoxWSMStart":
                        ws.Start.Message = textBox.Text;
                        break;
                    case "skyTextBoxWSMReset":
                        ws.Reset.Message = textBox.Text;
                        break;
                }
            }
        }

        private void textBoxUrl_TextChanged(object sender, EventArgs e) =>
             btnSetUrl.Visible = textBoxUrl.Text != saveModule.generalAS.WebSocketSettings.Url;

        private void btnSetUrl_Click(object sender, EventArgs e)
        {
            string currentUrl = saveModule.generalAS.WebSocketSettings.Url;
            try
            {
                saveModule.generalAS.WebSocketSettings.Url = textBoxUrl.Text;
                WebSockets.Instance.ReConnect();
            }
            catch (Exception ex)
            {
                try
                {
                    textBoxUrl.Text = currentUrl;
                    saveModule.generalAS.WebSocketSettings.Url = textBoxUrl.Text;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    WebSockets.Instance.ReConnect();
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally { btnSetUrl.Visible = false; }
        }
        #endregion
        #region MainDashboard
        private void btnHowSetup_Click(object sender, EventArgs e)
        {
            AutoSplitterMainModule.OpenWithBrowser(new Uri("https://github.com/neimex23/AutoSplitterCore/wiki"));
        }

        private void btnGoToDownloadPage_Click(object sender, EventArgs e)
        {
            AutoSplitterMainModule.OpenWithBrowser(new Uri("https://github.com/neimex23/AutoSplitterCore/releases/latest"));
        }
        private void cbCheckUpdatesOnStartup_CheckedChanged(object sender) => updateModule.CheckUpdatesOnStartup = cbCheckUpdatesOnStartup.Checked;


        //Buttons Dashboard
        private void btnCheckVersion_Click(object sender, EventArgs e)
        {
            updateModule.CheckUpdates(true);
            LabelVersion.Text = updateModule.currentVer;
            labelCloudVer.Text = updateModule.cloudVer;
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            if (!TabControlGeneral.TabPages.Contains(tabInfo))
            {
                TabControlGeneral.TabPages.Add(tabInfo);
            }
            if (!TabControlGeneral.TabPages.Contains(tabLicense))
            {
                TabControlGeneral.TabPages.Add(tabLicense);
            }
            TabControlGeneral.SelectTab(tabInfo);
        }

        private void btnSekiro_Click(object sender, EventArgs e)
        {
            if (!TabControlGeneral.TabPages.Contains(tabSekiro))
            {
                TabControlGeneral.TabPages.Add(tabSekiro);
            }
            TabControlGeneral.SelectTab(tabSekiro);

        }

        private void btnDs1_Click(object sender, EventArgs e)
        {
            if (!TabControlGeneral.TabPages.Contains(tabDs1))
            {
                TabControlGeneral.TabPages.Add(tabDs1);
            }
            TabControlGeneral.SelectTab(tabDs1);
        }

        private void btnDs2_Click(object sender, EventArgs e)
        {
            if (!TabControlGeneral.TabPages.Contains(tabDs2))
            {
                TabControlGeneral.TabPages.Add(tabDs2);
            }
            TabControlGeneral.SelectTab(tabDs2);
        }

        private void btnDs3_Click(object sender, EventArgs e)
        {
            if (!TabControlGeneral.TabPages.Contains(tabDs3))
            {
                TabControlGeneral.TabPages.Add(tabDs3);
            }
            TabControlGeneral.SelectTab(tabDs3);
        }

        private void btnHollow_Click(object sender, EventArgs e)
        {
            if (!TabControlGeneral.TabPages.Contains(tabHollow))
            {
                TabControlGeneral.TabPages.Add(tabHollow);
            }
            TabControlGeneral.SelectTab(tabHollow);

        }

        private void btnCeleste_Click(object sender, EventArgs e)
        {
            if (!TabControlGeneral.TabPages.Contains(tabCeleste))
            {
                TabControlGeneral.TabPages.Add(tabCeleste);
            }
            TabControlGeneral.SelectTab(tabCeleste);
        }

        private void btnCuphead_Click(object sender, EventArgs e)
        {
            if (!TabControlGeneral.TabPages.Contains(tabCuphead))
            {
                TabControlGeneral.TabPages.Add(tabCuphead);
            }
            TabControlGeneral.SelectTab(tabCuphead);
        }

        private void btnElden_Click(object sender, EventArgs e)
        {
            if (!TabControlGeneral.TabPages.Contains(tabElden))
            {
                TabControlGeneral.TabPages.Add(tabElden);
            }
            TabControlGeneral.SelectTab(tabElden);
        }

        private void btnDishonored_Click(object sender, EventArgs e)
        {
            if (!TabControlGeneral.TabPages.Contains(tabDishonored))
            {
                TabControlGeneral.TabPages.Add(tabDishonored);
            }
            TabControlGeneral.SelectTab(tabDishonored);
        }

        private void btnTiming_Click(object sender, EventArgs e)
        {
            if (!TabControlGeneral.TabPages.Contains(tabGeneral))
            {
                TabControlGeneral.TabPages.Add(tabGeneral);
            }
            TabControlGeneral.SelectTab(tabGeneral);
        }

        private void btnASL_Click(object sender, EventArgs e)
        {
            if (ASLSplitter.GetInstance().HCMv2)
            {
                ASLSplitter.GetInstance().OpenForm();
                return;
            }

            var form = new ASLForm(saveModule);
            Point parentLocation = this.Location;

            int newX = parentLocation.X + this.Width - 20;
            int newY = parentLocation.Y;

            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(newX, newY);
            form.ShowDialog();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            var form = new ProfileManager(saveModule, darkModeHCM);

            Point parentLocation = this.Location;

            int newX = parentLocation.X + this.Width + 10;
            int newY = parentLocation.Y;

            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(newX, newY);
            form.ShowDialog();
            this.Controls.Clear();
            this.InitializeComponent();
            RefreshForm();
            this.AutoSplitter_Load(null, null);
        }


        #endregion
        #endregion

        #region SekiroUI
        private void toSplitSelectSekiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panelPositionS.Hide();
            this.panelBossS.Hide();
            this.panelIdolsS.Hide();
            this.panelCfSekiro.Hide();
            this.panelMortalJourney.Hide();
            this.panelMinibossSekiro.Hide();
            this.panelLevelSekiro.Hide();

            switch (toSplitSelectSekiro.SelectedIndex)
            {
                case 0: //Kill a Boss
                    this.panelBossS.Show();
                    break;
                case 1: //Kill a miniboss
                    this.panelMinibossSekiro.Show();
                    break;
                case 2: // Is Activated a Idol
                    this.panelIdolsS.Show();
                    break;
                case 3: //Target Position
                    this.panelPositionS.Show();
                    break;
                case 4://Mortal Journey
                    this.panelMortalJourney.Show(); break;
                case 5:
                    this.panelLevelSekiro.Show(); break;
                case 6: //CustomFlags
                    this.panelCfSekiro.Show(); break;
            }
        }

        private void checkBoxMortalJourneyRun_CheckedChanged(object sender)
        {
            _ = checkBoxMortalJourneyRun.Checked ? sekiroSplitter.GetDataSekiro().mortalJourneyRun = true : sekiroSplitter.GetDataSekiro().mortalJourneyRun = false;
        }

        //Positions Triggers
        private void btnGetPositionS_Click(object sender, EventArgs e)
        {
            var Vector = sekiroSplitter.GetCurrentPosition();
            this.textBoxXS.Text = string.Empty;
            this.textBoxYS.Text = string.Empty;
            this.textBoxZS.Text = string.Empty;
            this.textBoxXS.Text = (Vector.X.ToString("0.00"));
            this.textBoxYS.Text = (Vector.Y.ToString("0.00"));
            this.textBoxZS.Text = (Vector.Z.ToString("0.00"));

        }

        private void btnAddPosition_Click(object sender, EventArgs e)
        {
            if (textBoxXS.Text != null || textBoxYS.Text != null || textBoxZS.Text != null)
            {
                if (comboBoxHowPositionS.SelectedIndex == -1)
                {
                    MessageBox.Show("Select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        var X = float.Parse(textBoxXS.Text, new CultureInfo("en-US"));
                        var Y = float.Parse(textBoxYS.Text, new CultureInfo("en-US"));
                        var Z = float.Parse(textBoxZS.Text, new CultureInfo("en-US"));
                        var contains1 = !listBoxPositionsS.Items.Contains(X + "; " + Y + "; " + Z + " - " + "Inmediatly");
                        var contains2 = !listBoxPositionsS.Items.Contains(X + "; " + Y + "; " + Z + " - " + "Loading game after");
                        if (contains1 && contains2)
                        {
                            if (X == 0.00 && Y == 0.00 && Z == 0.00)
                            {
                                MessageBox.Show("Dont use cords 0,0,0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string title = string.Empty;
                                if (textBoxTitlePositionS.Text != string.Empty)
                                {
                                    title = " - " + textBoxTitlePositionS.Text;
                                    textBoxTitlePositionS.Text = string.Empty;
                                }

                                listBoxPositionsS.Items.Add(X + "; " + Y + "; " + Z + " - " + comboBoxHowPositionS.Text.ToString() + title);
                                sekiroSplitter.AddPosition(X, Y, Z, comboBoxHowPositionS.Text.ToString(), title);
                            }
                        }
                        else
                        {
                            MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Check Coordinates and try again", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Plase get/set a position ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxPositions_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (this.listBoxPositionsS.SelectedItem != null)
            {
                int i = listBoxPositionsS.Items.IndexOf(listBoxPositionsS.SelectedItem);
                sekiroSplitter.RemovePosition(i);
                listBoxPositionsS.Items.Remove(listBoxPositionsS.SelectedItem);
            }
        }

        private void comboBoxMargin_SelectedIndexChanged(object sender, EventArgs e)
        {
            int select = comboBoxSizeS.SelectedIndex;
            sekiroSplitter.SetPositionMargin(select);
        }

        //Level Triggers
        private void btnAddAttributeS_Click(object sender, EventArgs e)
        {
            if (comboBoxAttributeS.SelectedIndex == -1 || comboBoxAttributeS.SelectedIndex == -1 || textBoxValueS.Text == null)
            {
                MessageBox.Show("Plase select Attribute, Value and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var value = uint.Parse(textBoxValueS.Text);
                    var contains1 = !listBoxAttributeS.Items.Contains(comboBoxAttributeS.Text.ToString() + ": " + value + " - " + "Inmediatly");
                    var contains2 = !listBoxAttributeS.Items.Contains(comboBoxAttributeS.Text.ToString() + ": " + value + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        sekiroSplitter.AddAttribute(comboBoxAttributeS.Text.ToString(), comboBoxHowAttributeS.Text.ToString(), value);
                        listBoxAttributeS.Items.Add(comboBoxAttributeS.Text.ToString() + ": " + value + " - " + comboBoxHowAttributeS.Text.ToString());
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Check Value and try again", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxAttributeS_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxAttributeS.SelectedItem != null)
            {
                int i = listBoxAttributeS.Items.IndexOf(listBoxAttributeS.SelectedItem);
                sekiroSplitter.RemoveAttribute(i);
                listBoxAttributeS.Items.Remove(listBoxAttributeS.SelectedItem);
            }
        }

        //Bosses Triggers

        private void btn_AddBossS_Click(object sender, EventArgs e)
        {
            if (comboBoxBossS.SelectedIndex == -1 || comboBoxHowBossS.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select boss and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBossesS.Items.Contains(comboBoxBossS.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxBossesS.Items.Contains(comboBoxBossS.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    sekiroSplitter.AddBoss(comboBoxBossS.Text.ToString(), comboBoxHowBossS.Text.ToString());
                    listBoxBossesS.Items.Add(comboBoxBossS.Text.ToString() + " - " + comboBoxHowBossS.Text.ToString());
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBosses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listBoxBossesS.SelectedItem != null)
            {
                int i = listBoxBossesS.Items.IndexOf(listBoxBossesS.SelectedItem);
                sekiroSplitter.RemoveBoss(i);
                listBoxBossesS.Items.Remove(listBoxBossesS.SelectedItem);
            }
        }

        private void btnAddAllBossesS_Click(object sender, EventArgs e)
        {
            if (comboBoxHowBossS.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int i = 0; i < comboBoxBossS.Items.Count; i++)
                {
                    comboBoxBossS.SelectedIndex = i;
                    btn_AddBossS_Click(null, null);
                }
            }
        }

        //Idols Triggers
        private void comboBoxZoneSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBoxAshinaOutskirts.Hide();
            groupBoxHirataEstate.Hide();
            groupBoxAshinaCastle.Hide();
            groupBoxAbandonedDungeon.Hide();
            groupBoxSenpouTemple.Hide();
            groupBoxSunkenValley.Hide();
            groupBoxAshinaDepths.Hide();
            groupBoxFountainhead.Hide();

            switch (comboBoxZoneSelectS.SelectedIndex)
            {
                case 0: //Ashina Outskirts
                    groupBoxAshinaOutskirts.Show();
                    break;
                case 1: //Hirata Estate
                    groupBoxHirataEstate.Show();
                    break;
                case 2: //Ashina Castle
                    groupBoxAshinaCastle.Show();
                    break;
                case 3: //Abandoned Dungeon
                    groupBoxAbandonedDungeon.Show();
                    break;
                case 4: //Senpou Temple
                    groupBoxSenpouTemple.Show();
                    break;
                case 5: //Sunken Valley
                    groupBoxSunkenValley.Show();
                    break;
                case 6: //Ashina Depths
                    groupBoxAshinaDepths.Show();
                    break;
                case 7: //Fountainhead Palace
                    groupBoxFountainhead.Show();
                    break;
            }
        }

        private void listBoxAshinaOutskirts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAshinaOutskirts.SelectedIndex != -1)
            {
                labelIdolSelectedAO.Text = listBoxAshinaOutskirts.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxAshinaOutskirts.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmAO.Checked = false;
                    radioLagAO.Checked = true;

                }
                else
                {
                    radioImmAO.Checked = true;
                    radioLagAO.Checked = false;
                }
            }
        }

        private void listBoxHirataEstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxHirataEstate.SelectedIndex != -1)
            {
                labelIdolSelectedHE.Text = listBoxHirataEstate.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxHirataEstate.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmHE.Checked = false;
                    radioLagHE.Checked = true;

                }
                else
                {
                    radioImmHE.Checked = true;
                    radioLagHE.Checked = false;
                }
            }

        }

        private void listBoxAshinaCastle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAshinaCastle.SelectedIndex != -1)
            {
                labelIdolSelectedAC.Text = listBoxAshinaCastle.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxAshinaCastle.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmAC.Checked = false;
                    radioLagAC.Checked = true;

                }
                else
                {
                    radioImmAC.Checked = true;
                    radioLagAC.Checked = false;
                }
            }
        }

        private void listBoxAbandonedDungeon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAbandonedDungeon.SelectedIndex != -1)
            {
                labelIdolSelectedAD.Text = listBoxAbandonedDungeon.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxAbandonedDungeon.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmAD.Checked = false;
                    radioLagAD.Checked = true;

                }
                else
                {
                    radioImmAD.Checked = true;
                    radioLagAD.Checked = false;
                }
            }
        }

        private void listBoxSunkenValley_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSunkenValley.SelectedIndex != -1)
            {
                labelIdolSelectedSV.Text = listBoxSunkenValley.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxSunkenValley.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmSV.Checked = false;
                    radioLagSV.Checked = true;

                }
                else
                {
                    radioImmSV.Checked = true;
                    radioLagSV.Checked = false;
                }
            }
        }

        private void listBoxAshinaDepths_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAshinaDepths.SelectedIndex != -1)
            {
                labelIdolSelectedADe.Text = listBoxAshinaDepths.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxAshinaDepths.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmADe.Checked = false;
                    radioLagADe.Checked = true;

                }
                else
                {
                    radioImmADe.Checked = true;
                    radioLagADe.Checked = false;
                }
            }
        }

        private void listBoxSenpouTemple_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSenpouTemple.SelectedIndex != -1)
            {
                labelIdolSelectedTS.Text = listBoxSenpouTemple.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxSenpouTemple.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmTS.Checked = false;
                    radioLagTS.Checked = true;

                }
                else
                {
                    radioImmTS.Checked = true;
                    radioLagTS.Checked = false;
                }
            }
        }

        private void listBoxFountainhead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFountainhead.SelectedIndex != -1)
            {
                labelIdolSelectedF.Text = listBoxFountainhead.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxFountainhead.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmF.Checked = false;
                    radioLagF.Checked = true;

                }
                else
                {
                    radioImmF.Checked = true;
                    radioLagF.Checked = false;
                }
            }
        }

        private void btnAddAshinaOutskirts_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxAshinaOutskirts.SelectedIndex != -1)
            {
                if (!checkedListBoxAshina.GetItemChecked(listBoxAshinaOutskirts.SelectedIndex))
                {
                    checkedListBoxAshina.SetItemChecked(listBoxAshinaOutskirts.SelectedIndex, true);
                    if (radioImmAO.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxAshinaOutskirts.SelectedItem.ToString(), mode);
                }
                else
                {
                    checkedListBoxAshina.SetItemChecked(listBoxAshinaOutskirts.SelectedIndex, false);
                    sekiroSplitter.RemoveIdol(listBoxAshinaOutskirts.SelectedItem.ToString());
                    radioImmAO.Checked = true;
                    radioLagAO.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddHirata_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxHirataEstate.SelectedIndex != -1)
            {
                if (!checkedListBoxHirataEstate.GetItemChecked(listBoxHirataEstate.SelectedIndex))
                {
                    checkedListBoxHirataEstate.SetItemChecked(listBoxHirataEstate.SelectedIndex, true);
                    if (radioImmHE.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxHirataEstate.SelectedItem.ToString(), mode);
                }
                else
                {
                    checkedListBoxHirataEstate.SetItemChecked(listBoxHirataEstate.SelectedIndex, false);
                    sekiroSplitter.RemoveIdol(listBoxHirataEstate.SelectedItem.ToString());
                    radioImmHE.Checked = true;
                    radioLagHE.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddAC_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxAshinaCastle.SelectedIndex != -1)
            {
                if (!checkedListBoxAshinaCastle.GetItemChecked(listBoxAshinaCastle.SelectedIndex))
                {
                    checkedListBoxAshinaCastle.SetItemChecked(listBoxAshinaCastle.SelectedIndex, true);
                    if (radioImmAC.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxAshinaCastle.SelectedItem.ToString(), mode);
                }
                else
                {
                    checkedListBoxAshinaCastle.SetItemChecked(listBoxAshinaCastle.SelectedIndex, false);
                    sekiroSplitter.RemoveIdol(listBoxAshinaCastle.SelectedItem.ToString());
                    radioImmAC.Checked = true;
                    radioLagAC.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddAD_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxAbandonedDungeon.SelectedIndex != -1)
            {
                if (!checkedListBoxAbandonedDungeon.GetItemChecked(listBoxAbandonedDungeon.SelectedIndex))
                {
                    checkedListBoxAbandonedDungeon.SetItemChecked(listBoxAbandonedDungeon.SelectedIndex, true);
                    if (radioImmAC.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxAbandonedDungeon.SelectedItem.ToString(), mode);
                }
                else
                {
                    checkedListBoxAbandonedDungeon.SetItemChecked(listBoxAbandonedDungeon.SelectedIndex, false);
                    sekiroSplitter.RemoveIdol(listBoxAbandonedDungeon.SelectedItem.ToString());
                    radioImmAC.Checked = true;
                    radioLagAC.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddTS_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxSenpouTemple.SelectedIndex != -1)
            {
                if (!checkedListBoxSenpouTemple.GetItemChecked(listBoxSenpouTemple.SelectedIndex))
                {
                    checkedListBoxSenpouTemple.SetItemChecked(listBoxSenpouTemple.SelectedIndex, true);
                    if (radioImmTS.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxSenpouTemple.SelectedItem.ToString(), mode);
                }
                else
                {
                    checkedListBoxSenpouTemple.SetItemChecked(listBoxSenpouTemple.SelectedIndex, false);
                    sekiroSplitter.RemoveIdol(listBoxSenpouTemple.SelectedItem.ToString());
                    radioImmTS.Checked = true;
                    radioLagTS.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddSV_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxSunkenValley.SelectedIndex != -1)
            {
                if (!checkedListBoxSunkenValley.GetItemChecked(listBoxSunkenValley.SelectedIndex))
                {
                    checkedListBoxSunkenValley.SetItemChecked(listBoxSunkenValley.SelectedIndex, true);
                    if (radioImmTS.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxSunkenValley.SelectedItem.ToString(), mode);
                }
                else
                {
                    checkedListBoxSunkenValley.SetItemChecked(listBoxSunkenValley.SelectedIndex, false);
                    sekiroSplitter.RemoveIdol(listBoxSunkenValley.SelectedItem.ToString());
                    radioImmTS.Checked = true;
                    radioLagTS.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddADe_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxAshinaDepths.SelectedIndex != -1)
            {
                if (!checkedListBoxAshinaDepths.GetItemChecked(listBoxAshinaDepths.SelectedIndex))
                {
                    checkedListBoxAshinaDepths.SetItemChecked(listBoxAshinaDepths.SelectedIndex, true);
                    if (radioImmTS.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxAshinaDepths.SelectedItem.ToString(), mode);
                }
                else
                {
                    checkedListBoxAshinaDepths.SetItemChecked(listBoxAshinaDepths.SelectedIndex, false);
                    sekiroSplitter.RemoveIdol(listBoxAshinaDepths.SelectedItem.ToString());
                    radioImmTS.Checked = true;
                    radioLagTS.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddF_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxFountainhead.SelectedIndex != -1)
            {
                if (!checkedListBoxFountainhead.GetItemChecked(listBoxFountainhead.SelectedIndex))
                {
                    checkedListBoxFountainhead.SetItemChecked(listBoxFountainhead.SelectedIndex, true);
                    if (radioImmF.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxFountainhead.SelectedItem.ToString(), mode);
                }
                else
                {
                    checkedListBoxFountainhead.SetItemChecked(listBoxFountainhead.SelectedIndex, false);
                    sekiroSplitter.RemoveIdol(listBoxFountainhead.SelectedItem.ToString());
                    radioImmF.Checked = true;
                    radioLagF.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddAllIdolsS_Click(object sender, EventArgs e)
        {
            groupBoxAddAllIdol.Location = new Point(400, groupBoxAddAllIdol.Location.Y);
        }


        private void XAddAllIdol_Click(object sender, EventArgs e)
        {
            groupBoxAddAllIdol.Location = new Point(472, groupBoxAddAllIdol.Location.Y);
        }

        private void HowAddAllIdol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HowAddAllIdol.SelectedIndex != -1)
            {

                btnRemoveAllIdolsS_Click(null, null); //Remove all for avoid duplicates

                string mode = HowAddAllIdol.Text.ToString();
                foreach (var AllIdols in sekiroSplitter.GetAllIdols())
                {
                    sekiroSplitter.AddIdol(AllIdols, mode);
                }

                this.Controls.Clear();
                this.InitializeComponent();
                RefreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControlGeneral.TabPages.Add(tabSekiro);
                TabControlGeneral.SelectTab(tabSekiro);
                toSplitSelectSekiro.SelectedIndex = 2;
                comboBoxZoneSelectS.SelectedIndex = 0;
                groupBoxAshinaOutskirts.Show();
                HowAddAllIdol.SelectedIndex = -1;
                XAddAllIdol_Click(null, null);
            }
        }


        private void btnRemoveAllIdolsS_Click(object sender, EventArgs e)
        {
            foreach (var AllIdols in sekiroSplitter.GetAllIdols())
            {
                sekiroSplitter.RemoveIdol(AllIdols);
            }

            if (sender != null)
            {
                this.Controls.Clear();
                this.InitializeComponent();
                RefreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControlGeneral.TabPages.Add(tabSekiro);
                TabControlGeneral.SelectTab(tabSekiro);
                toSplitSelectSekiro.SelectedIndex = 2;
                comboBoxZoneSelectS.SelectedIndex = 0;
                groupBoxAshinaOutskirts.Show();
            }
        }

        private void btnGetListFlagsSekiro_Click(object sender, EventArgs e)
        {
            AutoSplitterMainModule.OpenWithBrowser(new Uri("https://docs.google.com/spreadsheets/d/1Nwp6XwURGksUu-_jCVhcyXh4KH7hTCXYsJTCHbw87JQ/edit?usp=sharing"));
        }

        private void btnAddCfS_Click(object sender, EventArgs e)
        {

            if (textBoxCfIdS.Text == null || comboBoxHowCfS.SelectedIndex == -1)
            {
                MessageBox.Show("Plase set a ID and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var id = uint.Parse(textBoxCfIdS.Text);
                    var contains1 = !listBoxCfS.Items.Contains(id + " - " + "Inmediatly");
                    var contains2 = !listBoxCfS.Items.Contains(id + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        string title = string.Empty;
                        if (textBoxTitleCFS.Text != string.Empty)
                        {
                            title = " - " + textBoxTitleCFS.Text;
                            textBoxTitleCFS.Text = string.Empty;
                        }

                        sekiroSplitter.AddCustomFlag(id, comboBoxHowCfS.Text.ToString(), title);
                        listBoxCfS.Items.Add(id + " - " + comboBoxHowCfS.Text.ToString() + title);
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Wrong ID", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void listBoxCfS_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxCfS.SelectedItem != null)
            {
                int i = listBoxCfS.Items.IndexOf(listBoxCfS.SelectedItem);
                sekiroSplitter.RemoveCustomFlag(i);
                listBoxCfS.Items.Remove(listBoxCfS.SelectedItem);
            }
        }

        private void btnAddMiniBossSekiro_Click(object sender, EventArgs e)
        {
            if (comboBoxMiniBossSekiro.SelectedIndex == -1 || comboBoxHowMiniBossS.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select boss and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxMiniBossesS.Items.Contains(comboBoxMiniBossSekiro.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxMiniBossesS.Items.Contains(comboBoxMiniBossSekiro.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    sekiroSplitter.AddMiniBoss(comboBoxMiniBossSekiro.Text.ToString(), comboBoxHowMiniBossS.Text.ToString());
                    listBoxMiniBossesS.Items.Add(comboBoxMiniBossSekiro.Text.ToString() + " - " + comboBoxHowMiniBossS.Text.ToString());
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxMiniBossSekiro_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxMiniBossesS.SelectedItem != null)
            {
                int i = listBoxMiniBossesS.Items.IndexOf(listBoxMiniBossesS.SelectedItem);
                sekiroSplitter.RemoveMiniBoss(i);
                listBoxMiniBossesS.Items.Remove(listBoxMiniBossesS.SelectedItem);
            }
        }

        private void comboBoxMiniBossSekiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMiniBossSekiro.SelectedIndex >= 0) textBoxDescriptionMiniBoss.Text = sekiroSplitter.GetMiniBossDescription(comboBoxMiniBossSekiro.SelectedItem.ToString());
        }

        private void btnAddAllMiniBossesS_Click(object sender, EventArgs e)
        {
            if (comboBoxHowMiniBossS.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Some triggers are incompatible with each other, do you want to continue?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    for (int i = 0; i < comboBoxMiniBossSekiro.Items.Count; i++)
                    {
                        comboBoxMiniBossSekiro.SelectedIndex = i;
                        btnAddMiniBossSekiro_Click(null, null);
                    }
            }
        }

        private void btnDesactiveSekiro_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                sekiroSplitter.ClearData();
                this.Controls.Clear();
                this.InitializeComponent();
                RefreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControlGeneral.TabPages.Add(tabSekiro);
                TabControlGeneral.SelectTab(tabSekiro);
            }
        }
        #endregion
        #region Ds1 UI
        private void comboBoxToSplitDs1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossDs1.Hide();
            panelBonfireDs1.Hide();
            panelLvlDs1.Hide();
            panelPositionDs1.Hide();
            panelItemDs1.Hide();

            switch (comboBoxToSplitDs1.SelectedIndex)
            {
                case 0: panelBossDs1.Show(); break;
                case 1: panelBonfireDs1.Show(); break;
                case 2: panelLvlDs1.Show(); break;
                case 3: panelPositionDs1.Show(); break;
                case 4: panelItemDs1.Show(); break;
            }
        }

        private void btnAddBossDs1_Click(object sender, EventArgs e)
        {
            if (comboBoxBossDs1.SelectedIndex == -1 || comboBoxHowBossDs1.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select boss and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBossDs1.Items.Contains(comboBoxBossDs1.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxBossDs1.Items.Contains(comboBoxBossDs1.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    ds1Splitter.AddBoss(comboBoxBossDs1.Text.ToString(), comboBoxHowBossDs1.Text.ToString());
                    listBoxBossDs1.Items.Add(comboBoxBossDs1.Text.ToString() + " - " + comboBoxHowBossDs1.Text.ToString());
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBossDs1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxBossDs1.SelectedItem != null)
            {
                int i = listBoxBossDs1.Items.IndexOf(listBoxBossDs1.SelectedItem);
                ds1Splitter.RemoveBoss(i);
                listBoxBossDs1.Items.Remove(listBoxBossDs1.SelectedItem);
            }
        }

        private void btnAddAllBossesDs1_Click(object sender, EventArgs e)
        {
            if (comboBoxHowBossDs1.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int i = 0; i < comboBoxBossDs1.Items.Count; i++)
                {
                    comboBoxBossDs1.SelectedIndex = i;
                    btnAddBossDs1_Click(null, null);
                }
            }
        }

        private void btnAddBonfireDs1_Click(object sender, EventArgs e)
        {
            if (comboBoxBonfireDs1.SelectedIndex == -1 || comboBoxHowBonfireDs1.SelectedIndex == -1 || comboBoxStateDs1.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select state, bonefire and 'How' do you want split", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBonfireDs1.Items.Contains(comboBoxBonfireDs1.Text.ToString() + " - " + ds1Splitter.ConvertStringToState(comboBoxStateDs1.Text.ToString()) + " - " + "Inmediatly");
                var contains2 = !listBoxBonfireDs1.Items.Contains(comboBoxBonfireDs1.Text.ToString() + " - " + ds1Splitter.ConvertStringToState(comboBoxStateDs1.Text.ToString()) + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    ds1Splitter.AddBonfire(comboBoxBonfireDs1.Text.ToString(), comboBoxHowBonfireDs1.Text.ToString(), comboBoxStateDs1.Text.ToString());
                    listBoxBonfireDs1.Items.Add(comboBoxBonfireDs1.Text.ToString() + " - " + ds1Splitter.ConvertStringToState(comboBoxStateDs1.Text.ToString()) + " - " + comboBoxHowBonfireDs1.Text.ToString());
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBonfireDs1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxBonfireDs1.SelectedItem != null)
            {
                int i = listBoxBonfireDs1.Items.IndexOf(listBoxBonfireDs1.SelectedItem);
                ds1Splitter.RemoveBonfire(i);
                listBoxBonfireDs1.Items.Remove(listBoxBonfireDs1.SelectedItem);
            }
        }

        private void btnAddAttributeDs1_Click(object sender, EventArgs e)
        {
            if (comboBoxAttributesDs1.SelectedIndex == -1 || comboBoxHowAttributesDs1.SelectedIndex == -1 || textBoxValueDs1.Text == null)
            {
                MessageBox.Show("Plase select Attribute, Value and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var value = uint.Parse(textBoxValueDs1.Text);
                    var contains1 = !listBoxAttributesDs1.Items.Contains(comboBoxAttributesDs1.Text.ToString() + ": " + value + " - " + "Inmediatly");
                    var contains2 = !listBoxAttributesDs1.Items.Contains(comboBoxAttributesDs1.Text.ToString() + ": " + value + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        ds1Splitter.AddAttribute(comboBoxAttributesDs1.Text.ToString(), comboBoxHowAttributesDs1.Text.ToString(), value);
                        listBoxAttributesDs1.Items.Add(comboBoxAttributesDs1.Text.ToString() + ": " + value + " - " + comboBoxHowAttributesDs1.Text.ToString());
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Check Value and try again", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnAddAllBonfireDs1_Click(object sender, EventArgs e)
        {
            if (comboBoxHowBonfireDs1.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxStateDs1.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select 'State' for do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < comboBoxBonfireDs1.Items.Count; i++)
            {
                comboBoxBonfireDs1.SelectedIndex = i;
                btnAddBonfireDs1_Click(null, null);
            }
        }

        private void listBoxAttributeDs1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxAttributesDs1.SelectedItem != null)
            {
                int i = listBoxAttributesDs1.Items.IndexOf(listBoxAttributesDs1.SelectedItem);
                ds1Splitter.RemoveAttribute(i);
                listBoxAttributesDs1.Items.Remove(listBoxAttributesDs1.SelectedItem);
            }
        }

        Vector3f VectorDs1;
        private void btnGetPositionDs1_Click(object sender, EventArgs e)
        {
            var Vector = ds1Splitter.GetCurrentPosition();
            this.VectorDs1 = Vector;
            this.textBoxXDs1.Text = string.Empty;
            this.textBoxYDs1.Text = string.Empty;
            this.textBoxZDs1.Text = string.Empty;
            this.textBoxXDs1.Text = (Vector.X.ToString("0.00"));
            this.textBoxYDs1.Text = (Vector.Y.ToString("0.00"));
            this.textBoxZDs1.Text = (Vector.Z.ToString("0.00"));
        }

        private void listBoxPositionDs1_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxPositionsDs1.SelectedItem != null)
            {
                int i = listBoxPositionsDs1.Items.IndexOf(listBoxPositionsDs1.SelectedItem);
                ds1Splitter.RemovePosition(i);
                listBoxPositionsDs1.Items.Remove(listBoxPositionsDs1.SelectedItem);
            }
        }

        private void comboBoxMarginDs1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds1Splitter.GetDataDs1().positionMargin = comboBoxSizeDs1.SelectedIndex;
        }

        private void btnAddPositionDs1_Click(object sender, EventArgs e)
        {
            var X = float.Parse(textBoxXDs1.Text, new CultureInfo("en-US"));
            var Y = float.Parse(textBoxYDs1.Text, new CultureInfo("en-US"));
            var Z = float.Parse(textBoxZDs1.Text, new CultureInfo("en-US"));
            VectorDs1 = new Vector3f(X, Y, Z);
            var contains1 = !listBoxPositionsDs1.Items.Contains(this.VectorDs1 + " - " + "Inmediatly");
            var contains2 = !listBoxPositionsDs1.Items.Contains(this.VectorDs1 + " - " + "Loading game after");
            if (contains1 && contains2)
            {
                if (comboBoxHowPositionsDs1.SelectedIndex == -1)
                {
                    MessageBox.Show("Select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (this.VectorDs1.X == 0 && this.VectorDs1.Y == 0 && this.VectorDs1.Z == 0)
                    {
                        MessageBox.Show("Dont use cords 0,0,0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string title = string.Empty;
                        if (textBoxTitlePositionDs1.Text != string.Empty)
                        {
                            title = " - " + textBoxTitlePositionDs1.Text;
                            textBoxTitlePositionDs1.Text = string.Empty;
                        }

                        listBoxPositionsDs1.Items.Add(this.VectorDs1 + " - " + comboBoxHowPositionsDs1.Text.ToString() + title);
                        ds1Splitter.AddPosition(this.VectorDs1, comboBoxHowPositionsDs1.Text.ToString(), title);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddItemDs1_Click(object sender, EventArgs e)
        {

            if (comboBoxItemDs1.SelectedIndex == -1 || comboBoxHowItemDs1.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select Item and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxItemDs1.Items.Contains(comboBoxItemDs1.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxItemDs1.Items.Contains(comboBoxItemDs1.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    ds1Splitter.AddItem(comboBoxItemDs1.Text.ToString(), comboBoxHowItemDs1.Text.ToString());
                    listBoxItemDs1.Items.Add(comboBoxItemDs1.Text.ToString() + " - " + comboBoxHowItemDs1.Text.ToString());
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxItemDs1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxItemDs1.SelectedItem != null)
            {
                int i = listBoxItemDs1.Items.IndexOf(listBoxItemDs1.SelectedItem);
                ds1Splitter.RemoveItem(i);
                listBoxItemDs1.Items.Remove(listBoxItemDs1.SelectedItem);
            }
        }
        private void btnAddAllItemsDs1_Click(object sender, EventArgs e)
        {
            if (comboBoxHowItemDs1.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int i = 0; i < comboBoxItemDs1.Items.Count; i++)
                {
                    comboBoxItemDs1.SelectedIndex = i;
                    btnAddItemDs1_Click(null, null);
                }
            }
        }

        private void btnDesactiveAllDs1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ds1Splitter.ClearData();
                this.Controls.Clear();
                this.InitializeComponent();
                RefreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControlGeneral.TabPages.Add(tabDs1);
                TabControlGeneral.SelectTab(tabDs1);
            }
        }
        #endregion
        #region Ds2 UI
        private void comboBoxToSplitDs2_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossDS2.Hide();
            panelLvlDs2.Hide();
            panelPositionDs2.Hide();


            switch (comboBoxToSplitDs2.SelectedIndex)
            {
                case 0: panelBossDS2.Show(); break;
                case 1: panelLvlDs2.Show(); break;
                case 2: panelPositionDs2.Show(); break;
            }
        }

        private void btnAddBossDS2_Click(object sender, EventArgs e)
        {

            if (comboBoxBossDs2.SelectedIndex == -1 || comboBoxHowBossDs2.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select boss and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBossDs2.Items.Contains(comboBoxBossDs2.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxBossDs2.Items.Contains(comboBoxBossDs2.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    ds2Splitter.AddBoss(comboBoxBossDs2.Text.ToString(), comboBoxHowBossDs2.Text.ToString());
                    listBoxBossDs2.Items.Add(comboBoxBossDs2.Text.ToString() + " - " + comboBoxHowBossDs2.Text.ToString());
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBossDs2_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxBossDs2.SelectedItem != null)
            {
                int i = listBoxBossDs2.Items.IndexOf(listBoxBossDs2.SelectedItem);
                ds2Splitter.RemoveBoss(i);
                listBoxBossDs2.Items.Remove(listBoxBossDs2.SelectedItem);
            }
        }

        private void btnAddAllBossesDs2_Click(object sender, EventArgs e)
        {
            if (comboBoxHowBossDs2.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int i = 0; i < comboBoxBossDs2.Items.Count; i++)
                {
                    comboBoxBossDs2.SelectedIndex = i;
                    btnAddBossDS2_Click(null, null);
                }
            }
        }
        private void btnAddAttributeDs2_Click(object sender, EventArgs e)
        {
            if (comboBoxAttributeDs2.SelectedIndex == -1 || comboBoxHowAttributeDs2.SelectedIndex == -1 || textBoxValueDs2.Text == null)
            {
                MessageBox.Show("Plase select Attribute, Value and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var value = uint.Parse(textBoxValueDs2.Text);
                    var contains1 = !listBoxAttributeDs2.Items.Contains(comboBoxAttributeDs2.Text.ToString() + ": " + value + " - " + "Inmediatly");
                    var contains2 = !listBoxAttributeDs2.Items.Contains(comboBoxAttributeDs2.Text.ToString() + ": " + value + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        ds2Splitter.AddAttribute(comboBoxAttributeDs2.Text.ToString(), comboBoxHowAttributeDs2.Text.ToString(), value);
                        listBoxAttributeDs2.Items.Add(comboBoxAttributeDs2.Text.ToString() + ": " + value + " - " + comboBoxHowAttributeDs2.Text.ToString());
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Check Value and try again", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxAttributeDs2_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxAttributeDs2.SelectedItem != null)
            {
                int i = listBoxAttributeDs2.Items.IndexOf(listBoxAttributeDs2.SelectedItem);
                ds2Splitter.RemoveAttribute(i);
                listBoxAttributeDs2.Items.Remove(listBoxAttributeDs2.SelectedItem);
            }
        }

        Vector3f VectorDs2;
        private void btnGetPositionDs2_Click(object sender, EventArgs e)
        {
            var Vector = ds2Splitter.GetCurrentPosition();
            this.VectorDs2 = Vector;
            this.textBoxXDs2.Text = string.Empty;
            this.textBoxYDs2.Text = string.Empty; ;
            this.textBoxZDs2.Text = string.Empty; ;
            this.textBoxXDs2.Text = (Vector.X.ToString("0.00"));
            this.textBoxYDs2.Text = (Vector.Y.ToString("0.00"));
            this.textBoxZDs2.Text = (Vector.Z.ToString("0.00"));
        }

        private void comboBoxMarginDs2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds2Splitter.GetDataDs2().positionMargin = comboBoxSizeDs2.SelectedIndex;
        }

        private void btnAddPositionDs2_Click(object sender, EventArgs e)
        {
            var X = float.Parse(textBoxXDs2.Text, new CultureInfo("en-US"));
            var Y = float.Parse(textBoxYDs2.Text, new CultureInfo("en-US"));
            var Z = float.Parse(textBoxZDs2.Text, new CultureInfo("en-US"));
            VectorDs1 = new Vector3f(X, Y, Z);
            if (this.VectorDs2 != null)
            {
                var contains1 = !listBoxPositionsDs2.Items.Contains(this.VectorDs2 + " - " + "Inmediatly");
                var contains2 = !listBoxPositionsDs2.Items.Contains(this.VectorDs2 + " - " + "Loading game after");
                if (contains1 && contains2)
                {

                    if (comboBoxHowPositionsDs2.SelectedIndex == -1)
                    {
                        MessageBox.Show("Select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (this.VectorDs2.X == 0 && this.VectorDs2.Y == 0 && this.VectorDs2.Z == 0)
                        {
                            MessageBox.Show("Dont use cords 0,0,0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string title = string.Empty;
                            if (textBoxTitlePositionDs2.Text != string.Empty)
                            {
                                title = " - " + textBoxTitlePositionDs2.Text;
                                textBoxTitlePositionDs2.Text = string.Empty;
                            }
                            listBoxPositionsDs2.Items.Add(this.VectorDs2 + " - " + comboBoxHowPositionsDs2.Text.ToString() + title);
                            ds2Splitter.AddPosition(this.VectorDs2, comboBoxHowPositionsDs2.Text.ToString(), title);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Plase get a position ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxPositionsDs2_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxPositionsDs2.SelectedItem != null)
            {
                int i = listBoxPositionsDs2.Items.IndexOf(listBoxPositionsDs2.SelectedItem);
                ds2Splitter.RemovePosition(i);
                listBoxPositionsDs2.Items.Remove(listBoxPositionsDs2.SelectedItem);
            }
        }

        private void btnDesactiveAllDs2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ds2Splitter.ClearData();
                this.Controls.Clear();
                this.InitializeComponent();
                RefreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControlGeneral.TabPages.Add(tabDs2);
                TabControlGeneral.SelectTab(tabDs2);
            }
        }


        #endregion
        #region Ds3 UI
        private void comboBoxToSplitSelectDs3_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossDs3.Hide();
            panelBonfireDs3.Hide();
            panelLvlDs3.Hide();
            panelCfDs3.Hide();
            panelPositionsDs3.Hide();


            switch (comboBoxToSplitSelectDs3.SelectedIndex)
            {
                case 0:
                    panelBossDs3.Show();
                    break;
                case 1:
                    panelBonfireDs3.Show();
                    break;
                case 2:
                    panelLvlDs3.Show();
                    break;
                case 3:
                    panelCfDs3.Show();
                    break;
                case 4:
                    panelPositionsDs3.Show();
                    break;
            }
        }

        private void btnAddBossDs3_Click(object sender, EventArgs e)
        {
            if (comboBoxBossDs3.SelectedIndex == -1 || comboBoxHowBossDs3.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select boss and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBossDs3.Items.Contains(comboBoxBossDs3.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxBossDs3.Items.Contains(comboBoxBossDs3.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    ds3Splitter.AddBoss(comboBoxBossDs3.Text.ToString(), comboBoxHowBossDs3.Text.ToString());
                    listBoxBossDs3.Items.Add(comboBoxBossDs3.Text.ToString() + " - " + comboBoxHowBossDs3.Text.ToString());
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBossDs3_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxBossDs3.SelectedItem != null)
            {
                int i = listBoxBossDs3.Items.IndexOf(listBoxBossDs3.SelectedItem);
                ds3Splitter.RemoveBoss(i);
                listBoxBossDs3.Items.Remove(listBoxBossDs3.SelectedItem);
            }
        }

        private void btnAddAllBossesDS3_Click(object sender, EventArgs e)
        {
            if (comboBoxHowBossDs3.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int i = 0; i < comboBoxBossDs3.Items.Count; i++)
                {
                    comboBoxBossDs3.SelectedIndex = i;
                    btnAddBossDs3_Click(null, null);
                }
            }
        }

        private void btnAddBonfireDs3_Click(object sender, EventArgs e)
        {
            if (comboBoxBonfireDs3.SelectedIndex == -1 || comboBoxHowBonfireDs3.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select bonefire and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBonfireDs3.Items.Contains(comboBoxBonfireDs3.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxBonfireDs3.Items.Contains(comboBoxBonfireDs3.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    ds3Splitter.AddBonfire(comboBoxBonfireDs3.Text.ToString(), comboBoxHowBonfireDs3.Text.ToString());
                    listBoxBonfireDs3.Items.Add(comboBoxBonfireDs3.Text.ToString() + " - " + comboBoxHowBonfireDs3.Text.ToString());
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBonfireDs3_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxBonfireDs3.SelectedItem != null)
            {
                int i = listBoxBonfireDs3.Items.IndexOf(listBoxBonfireDs3.SelectedItem);
                ds3Splitter.RemoveBonfire(i);
                listBoxBonfireDs3.Items.Remove(listBoxBonfireDs3.SelectedItem);
            }
        }

        private void btnAddAllBonefireDs3_Click(object sender, EventArgs e)
        {
            if (comboBoxHowBonfireDs3.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int i = 0; i < comboBoxBonfireDs3.Items.Count; i++)
                {
                    comboBoxBonfireDs3.SelectedIndex = i;
                    btnAddBonfireDs3_Click(null, null);
                }
            }
        }

        private void btnAddAttributeDs3_Click(object sender, EventArgs e)
        {
            if (comboBoxAttributeDs3.SelectedIndex == -1 || comboBoxHowAttributeDs3.SelectedIndex == -1 || textBoxValueDs3.Text == null)
            {
                MessageBox.Show("Plase select Attribute, Value and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var value = uint.Parse(textBoxValueDs3.Text);
                    var contains1 = !listBoxAttributesDs3.Items.Contains(comboBoxAttributeDs3.Text.ToString() + ": " + value + " - " + "Inmediatly");
                    var contains2 = !listBoxAttributesDs3.Items.Contains(comboBoxAttributeDs3.Text.ToString() + ": " + value + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        ds3Splitter.AddAttribute(comboBoxAttributeDs3.Text.ToString(), comboBoxHowAttributeDs3.Text.ToString(), value);
                        listBoxAttributesDs3.Items.Add(comboBoxAttributeDs3.Text.ToString() + ": " + value + " - " + comboBoxHowAttributeDs3.Text.ToString());
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Check Value and try again", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxAttributesDs3_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxAttributesDs3.SelectedItem != null)
            {
                int i = listBoxAttributesDs3.Items.IndexOf(listBoxAttributesDs3.SelectedItem);
                ds3Splitter.RemoveAttribute(i);
                listBoxAttributesDs3.Items.Remove(listBoxAttributesDs3.SelectedItem);
            }
        }

        private void btnGetListFlagDs3_Click(object sender, EventArgs e)
        {
            AutoSplitterMainModule.OpenWithBrowser(new Uri("https://pastebin.com/3DyjrgUN"));
        }

        private void btnAddCfeDs3_Click(object sender, EventArgs e)
        {
            if (textBoxIdDs3.Text == null || comboBoxHowCfDs3.SelectedIndex == -1)
            {
                MessageBox.Show("Plase set a ID and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var id = uint.Parse(textBoxIdDs3.Text);
                    var contains1 = !listBoxCfDs3.Items.Contains(id + " - " + "Inmediatly");
                    var contains2 = !listBoxCfDs3.Items.Contains(id + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        string title = string.Empty;
                        if (textBoxTitleCFDs3.Text != string.Empty)
                        {
                            title = " - " + textBoxTitleCFDs3.Text;
                            textBoxTitleCFDs3.Text = string.Empty;
                        }

                        ds3Splitter.AddCustomFlag(id, comboBoxHowCfDs3.Text.ToString(), title);
                        listBoxCfDs3.Items.Add(id + " - " + comboBoxHowCfDs3.Text.ToString() + title);
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Wrong ID", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void listBoxCfDs3_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxCfDs3.SelectedItem != null)
            {
                int i = listBoxCfDs3.Items.IndexOf(listBoxCfDs3.SelectedItem);
                ds3Splitter.RemoveCustomFlag(i);
                listBoxCfDs3.Items.Remove(listBoxCfDs3.SelectedItem);
            }
        }

        Vector3f VectorDs3;
        private void btnGetPositionDs3_Click(object sender, EventArgs e)
        {
            var Vector = ds3Splitter.GetCurrentPosition();
            this.VectorDs3 = Vector;
            this.textBoxXDs3.Text = string.Empty;
            this.textBoxYDs3.Text = string.Empty;
            this.textBoxZDs3.Text = string.Empty;
            this.textBoxXDs3.Text = (Vector.X.ToString("0.00"));
            this.textBoxYDs3.Text = (Vector.Y.ToString("0.00"));
            this.textBoxZDs3.Text = (Vector.Z.ToString("0.00"));
        }

        private void btnAddPositionDs3_Click(object sender, EventArgs e)
        {
            var X = float.Parse(textBoxXDs3.Text, new CultureInfo("en-US"));
            var Y = float.Parse(textBoxYDs3.Text, new CultureInfo("en-US"));
            var Z = float.Parse(textBoxZDs3.Text, new CultureInfo("en-US"));
            VectorDs1 = new Vector3f(X, Y, Z);
            if (this.VectorDs3 != null)
            {
                var contains1 = !listBoxPositionsDs3.Items.Contains(this.VectorDs3 + " - " + "Inmediatly");
                var contains2 = !listBoxPositionsDs3.Items.Contains(this.VectorDs3 + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    if (comboBoxHowPositionsDs3.SelectedIndex == -1)
                    {
                        MessageBox.Show("Select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (this.VectorDs3.X == 0 && this.VectorDs3.Y == 0 && this.VectorDs3.Z == 0)
                        {
                            MessageBox.Show("Dont use cords 0,0,0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string title = string.Empty;
                            if (textBoxTitlePositionDs3.Text != string.Empty)
                            {
                                title = " - " + textBoxTitlePositionDs3.Text;
                                textBoxTitlePositionDs3.Text = string.Empty;
                            }
                            listBoxPositionsDs3.Items.Add(this.VectorDs3 + " - " + comboBoxHowPositionsDs3.Text.ToString() + title);
                            ds3Splitter.AddPosition(this.VectorDs3, comboBoxHowPositionsDs3.Text.ToString(), title);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Plase get a position ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxPositionDs3_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxPositionsDs3.SelectedItem != null)
            {
                int i = listBoxPositionsDs3.Items.IndexOf(listBoxPositionsDs3.SelectedItem);
                ds3Splitter.RemovePosition(i);
                listBoxPositionsDs3.Items.Remove(listBoxPositionsDs3.SelectedItem);
            }
        }

        private void comboBoxMarginDs3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds3Splitter.GetDataDs3().positionMargin = comboBoxMarginDs3.SelectedIndex;
        }

        private void btnDesactiveAllDs3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ds3Splitter.ClearData();
                this.Controls.Clear();
                this.InitializeComponent();
                RefreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControlGeneral.TabPages.Add(tabDs3);
                TabControlGeneral.SelectTab(tabDs3);
            }
        }
        #endregion
        #region Elden Ring UI
        private void comboBoxToSplitEldenRing_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossER.Hide();
            panelGraceER.Hide();
            panelPositionsER.Hide();
            panelCfER.Hide();

            switch (comboBoxToSplitEldenRing.SelectedIndex)
            {
                case 0:
                    panelBossER.Show(); break;
                case 1:
                    panelGraceER.Show(); break;
                case 2:
                    panelPositionsER.Show(); break;
                case 3:
                    panelCfER.Show(); break;
            }
        }

        private void checkBoxDLC_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxDLCBossER.Checked)
            {
                comboBoxBossER_DLC.Show();
                comboBoxBossER.Hide();
            }
            else
            {
                comboBoxBossER.Show();
                comboBoxBossER_DLC.Hide();
            }
        }

        private void btnAddBossER_Click(object sender, EventArgs e)
        {
            string comboBoxTextSelected = string.Empty;
            _ = checkBoxDLCBossER.Checked ? comboBoxTextSelected = comboBoxTextSelected = comboBoxBossER_DLC.Text : comboBoxTextSelected = comboBoxBossER.Text;

            if (comboBoxTextSelected == string.Empty || comboBoxHowBossER.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select boss and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxBossER.Items.Contains(comboBoxTextSelected + " - " + "Inmediatly");
                var contains2 = !listBoxBossER.Items.Contains(comboBoxTextSelected + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    eldenSplitter.AddBoss(comboBoxTextSelected, comboBoxHowBossER.Text.ToString());
                    listBoxBossER.Items.Add(comboBoxTextSelected + " - " + comboBoxHowBossER.Text.ToString());
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBossER_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxBossER.SelectedItem != null)
            {
                int i = listBoxBossER.Items.IndexOf(listBoxBossER.SelectedItem);
                eldenSplitter.RemoveBoss(i);
                listBoxBossER.Items.Remove(listBoxBossER.SelectedItem);
            }
        }

        private void checkBoxViewDlcGrace_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxViewDlcGrace.Checked)
            {
                comboBoxGraceDLC_ER.Show();
                comboBoxGraceER.Hide();
            }
            else
            {
                comboBoxGraceER.Show();
                comboBoxGraceDLC_ER.Hide();
            }
        }

        private void btnAddAllBossesER_Click(object sender, EventArgs e)
        {
            if (comboBoxHowBossER.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int i = 0; i < comboBoxBossER.Items.Count; i++)
                {
                    comboBoxBossER.SelectedIndex = i;
                    btnAddBossER_Click(null, null);
                }
                checkBoxDLCBossER.Checked = true;
                for (int i = 0; i < comboBoxBossER_DLC.Items.Count; i++)
                {
                    comboBoxBossER_DLC.SelectedIndex = i;
                    btnAddBossER_Click(null, null);
                }
            }
        }

        private void btnAddGraceER_Click(object sender, EventArgs e)
        {
            string comboBoxTextSelected = string.Empty;
            _ = checkBoxViewDlcGrace.Checked ? comboBoxTextSelected = comboBoxTextSelected = comboBoxGraceDLC_ER.Text : comboBoxTextSelected = comboBoxGraceER.Text;

            if (comboBoxTextSelected == string.Empty || comboBoxHowGraceER.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select grace and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxGrace.Items.Contains(comboBoxTextSelected + " - " + "Inmediatly");
                var contains2 = !listBoxGrace.Items.Contains(comboBoxTextSelected + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    eldenSplitter.AddGrace(comboBoxTextSelected, comboBoxHowGraceER.Text.ToString());
                    listBoxGrace.Items.Add(comboBoxTextSelected + " - " + comboBoxHowGraceER.Text.ToString());
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxGrace_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBoxGrace.SelectedItem != null)
            {
                int i = listBoxGrace.Items.IndexOf(listBoxGrace.SelectedItem);
                eldenSplitter.RemoveGrace(i);
                listBoxGrace.Items.Remove(listBoxGrace.SelectedItem);
            }
        }

        private void btnAddAllGraceER_Click(object sender, EventArgs e)
        {
            if (comboBoxHowGraceER.SelectedIndex == -1)
            {
                MessageBox.Show("Plase select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int i = 0; i < comboBoxGraceER.Items.Count; i++)
                {
                    comboBoxGraceER.SelectedIndex = i;
                    btnAddGraceER_Click(null, null);
                }
                checkBoxViewDlcGrace.Checked = true;
                for (int i = 0; i < comboBoxGraceDLC_ER.Items.Count; i++)
                {
                    comboBoxGraceDLC_ER.SelectedIndex = i;
                    btnAddGraceER_Click(null, null);
                }
            }
        }

        SoulMemory.EldenRing.Position VectorER;
        private void comboBoxMarginER_SelectedIndexChanged(object sender, EventArgs e)
        {
            eldenSplitter.GetDataElden().positionMargin = comboBoxSizeER.SelectedIndex; ;
        }

        private void btnGetPositionER_Click(object sender, EventArgs e)
        {
            var Vector = eldenSplitter.GetCurrentPosition();
            this.VectorER = Vector;
            this.textBoxXEr.Text = string.Empty;
            this.textBoxYEr.Text = string.Empty;
            this.textBoxZEr.Text = string.Empty;
            this.textBoxXEr.Text = (Vector.X.ToString("0.00"));
            this.textBoxYEr.Text = (Vector.Y.ToString("0.00"));
            this.textBoxZEr.Text = (Vector.Z.ToString("0.00"));
        }

        private void btnAddPositionER_Click(object sender, EventArgs e)
        {
            if (this.VectorER != null)
            {
                var contains1 = !listBoxPositionsER.Items.Contains(this.VectorER + " - " + "Inmediatly");
                var contains2 = !listBoxPositionsER.Items.Contains(this.VectorER + " - " + "Loading game after");
                if (contains1 && contains2)
                {

                    if (comboBoxHowPositionsER.SelectedIndex == -1)
                    {
                        MessageBox.Show("Select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (this.VectorER.X == 0 && this.VectorER.Y == 0 && this.VectorER.Z == 0)
                        {
                            MessageBox.Show("Dont use cords 0,0,0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string title = string.Empty;
                            if (textBoxTitlePositionER.Text != string.Empty)
                            {
                                title = " - " + textBoxTitlePositionER.Text;
                                textBoxTitlePositionER.Text = string.Empty;
                            }
                            listBoxPositionsER.Items.Add(this.VectorER + " - " + comboBoxHowPositionsER.Text.ToString() + title);
                            eldenSplitter.AddPosition(this.VectorER, comboBoxHowPositionsER.Text.ToString(), title);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Plase get a position", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxPositionsER_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxPositionsER.SelectedItem != null)
            {
                int i = listBoxPositionsER.Items.IndexOf(listBoxPositionsER.SelectedItem);
                eldenSplitter.RemovePosition(i);
                listBoxPositionsER.Items.Remove(listBoxPositionsER.SelectedItem);
            }
        }

        private void btnGetListER_Click(object sender, EventArgs e)
        {
            AutoSplitterMainModule.OpenWithBrowser(new Uri("https://pastebin.com/p8gByAgU"));
        }

        private void btnAddCfER_Click(object sender, EventArgs e)
        {
            if (textBoxIdER.Text == null || comboBoxHowCfER.SelectedIndex == -1)
            {
                MessageBox.Show("Plase set a ID and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var id = uint.Parse(textBoxIdER.Text);
                    var contains1 = !listBoxCfER.Items.Contains(id + " - " + "Inmediatly");
                    var contains2 = !listBoxCfER.Items.Contains(id + " - " + "Loading game after");
                    if (contains1 && contains2)
                    {
                        string title = string.Empty;
                        if (textBoxTitleCFER.Text != string.Empty)
                        {
                            title = " - " + textBoxTitleCFER.Text;
                            textBoxTitleCFER.Text = string.Empty;
                        }

                        eldenSplitter.AddCustomFlag(id, comboBoxHowCfER.Text.ToString(), title);
                        listBoxCfER.Items.Add(id + " - " + comboBoxHowCfER.Text.ToString() + title);
                    }
                    else
                    {
                        MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Wrong ID", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void listBoxCfER_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxCfER.SelectedItem != null)
            {
                int i = listBoxCfER.Items.IndexOf(listBoxCfER.SelectedItem);
                eldenSplitter.RemoveCustomFlag(i);
                listBoxCfER.Items.Remove(listBoxCfER.SelectedItem);
            }
        }

        private void btn_DesactiveAllElden_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                eldenSplitter.ClearData();
                this.Controls.Clear();
                this.InitializeComponent();
                RefreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControlGeneral.TabPages.Add(tabElden);
                TabControlGeneral.SelectTab(tabElden);
            }
        }
        #endregion
        #region Hollow UI
        private void toSplitSelectHollow_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossH.Hide();
            panelItemH.Hide();
            panelPositionH.Hide();

            switch (toSplitSelectHollow.SelectedIndex)
            {
                case 0: //Kill a enemy
                    panelBossH.Show();
                    break;
                case 1: //Obtain a item
                    panelItemH.Show();
                    break;
                case 2: //Get Position
                    panelPositionH.Show();
                    break;
            }
        }

        private void comboBoxSelectKindBoss_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBossH.Hide();
            groupBoxMBH.Hide();
            groupBoxPantheon.Hide();

            switch (comboBoxSelectKindBoss.SelectedIndex)
            {
                case 0: //Boss
                    groupBossH.Show();
                    break;
                case 1: //Phanteom
                    groupBoxPantheon.Show();
                    break;
                case 2: //MiniBoss - Dreamers- Coliseum and Others
                    groupBoxMBH.Show();
                    break;
            }
        }

        private void checkedListBoxBossH_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxBossH.SelectedIndex != -1)
            {
                if (checkedListBoxBossH.GetItemChecked(checkedListBoxBossH.SelectedIndex) == false)
                {
                    hollowSplitter.AddBoss(checkedListBoxBossH.SelectedItem.ToString());
                }
                else
                {
                    hollowSplitter.RemoveBoss(checkedListBoxBossH.SelectedItem.ToString());
                }
            }
        }

        private void checkedListBoxMBH_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxHMB.SelectedIndex != -1)
            {
                if (checkedListBoxHMB.GetItemChecked(checkedListBoxHMB.SelectedIndex) == false)
                {
                    hollowSplitter.AddMiniBoss(checkedListBoxHMB.SelectedItem.ToString());
                }
                else
                {
                    hollowSplitter.RemoveMiniBoss(checkedListBoxHMB.SelectedItem.ToString());
                }
            }
        }

        private void checkedListBoxPantheon_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxPantheon.SelectedIndex != -1)
            {
                if (checkedListBoxPantheon.GetItemChecked(checkedListBoxPantheon.SelectedIndex) == false)
                {
                    hollowSplitter.AddPantheon(checkedListBoxPantheon.SelectedItem.ToString());
                }
                else
                {
                    hollowSplitter.RemovePantheon(checkedListBoxPantheon.SelectedItem.ToString());
                }
            }
        }

        private void checkedListBoxPp_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxPp.SelectedIndex != -1)
            {
                if (checkedListBoxPp.GetItemChecked(checkedListBoxPp.SelectedIndex) == false)
                {
                    hollowSplitter.AddPantheon(checkedListBoxPp.SelectedItem.ToString());
                }
                else
                {
                    hollowSplitter.RemovePantheon(checkedListBoxPp.SelectedItem.ToString());
                }
            }
        }


        private void checkedListBoxCharms_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxCharms.SelectedIndex != -1)
            {
                if (checkedListBoxCharms.GetItemChecked(checkedListBoxCharms.SelectedIndex) == false)
                {
                    hollowSplitter.AddCharm(checkedListBoxCharms.SelectedItem.ToString());
                }
                else
                {
                    hollowSplitter.RemoveCharm(checkedListBoxCharms.SelectedItem.ToString());
                }
            }
        }

        private void checkedListBoxSkillsH_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxSkillsH.SelectedIndex != -1)
            {
                if (checkedListBoxSkillsH.GetItemChecked(checkedListBoxSkillsH.SelectedIndex) == false)
                {
                    hollowSplitter.AddSkill(checkedListBoxSkillsH.SelectedItem.ToString());
                }
                else
                {
                    hollowSplitter.RemoveSkill(checkedListBoxSkillsH.SelectedItem.ToString());
                }
            }
        }

        private void comboBoxHowP_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBoxPantheon.Hide();
            checkedListBoxPp.Hide();
            lbl_warning.Hide();

            if (comboBoxHowP.SelectedIndex != -1)
            {
                switch (comboBoxHowP.SelectedIndex)
                {
                    case 0: //P1+P2+P3+P4 or P5                       
                        checkedListBoxPantheon.Show();
                        lbl_warning.Show();
                        hollowSplitter.GetDataHollow().PantheonMode = 0;
                        break;
                    case 1: //Split one per Pantheon
                        checkedListBoxPp.Show();
                        hollowSplitter.GetDataHollow().PantheonMode = 1;
                        break;
                }
            }
        }

        private void comboBoxItemSelectH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxItemSelectH.SelectedIndex != -1)
            {
                groupBoxCharms.Hide();
                groupBoxSkillsH.Hide();
                switch (comboBoxItemSelectH.SelectedIndex)
                {
                    case 0: //Skills                     
                        groupBoxSkillsH.Show();
                        break;
                    case 1: //Charms
                        groupBoxCharms.Show();
                        break;
                }
            }
        }

        PointF VectorH;
        private void btn_getPositionH_Click(object sender, EventArgs e)
        {

            var Vector = hollowSplitter.GetCurrentPosition();
            this.VectorH = Vector;
            this.textBoxXh.Text = string.Empty;
            this.textBoxYh.Text = string.Empty;
            this.textBoxSh.Text = string.Empty;
            this.textBoxXh.Text = (Vector.X.ToString("0.00"));
            this.textBoxYh.Text = (Vector.Y.ToString("0.00"));
            this.textBoxSh.Text = (hollowSplitter.currentPosition.sceneName == String.Empty ? "NULL" : hollowSplitter.currentPosition.sceneName);
        }

        private void comboBoxMarginH_SelectedIndexChanged(object sender, EventArgs e)
        {
            int select = comboBoxSizeH.SelectedIndex;
            hollowSplitter.GetDataHollow().positionMargin = select;
        }

        private void btn_AddPositionH_Click(object sender, EventArgs e)
        {
            if (this.VectorH != null)
            {
                var contains1 = !listBoxPositionH.Items.Contains(this.VectorH + " - " + textBoxSh.Text);
                if (contains1)
                {
                    if (this.VectorH.X == 0 && this.VectorH.Y == 0 && textBoxSh.Text == "NULL")
                    {
                        MessageBox.Show("Dont use cords 0,0 or Scene Null", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string title = string.Empty;
                        if (textBoxTitlePositionHK.Text != string.Empty)
                        {
                            title = " - " + textBoxTitlePositionHK.Text;
                            textBoxTitlePositionHK.Text = string.Empty; ;
                        }
                        listBoxPositionH.Items.Add(this.VectorH + " - " + textBoxSh.Text + title);
                        hollowSplitter.AddPosition(this.VectorH, textBoxSh.Text, title);
                    }
                }
                else
                {
                    MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Plase get a position ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void listBoxPositionH_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxPositionH.SelectedItem != null)
            {
                int i = listBoxPositionH.Items.IndexOf(listBoxPositionH.SelectedItem);
                hollowSplitter.RemovePosition(i);
                listBoxPositionH.Items.Remove(listBoxPositionH.SelectedItem);
            }
        }

        private void btn_DesactiveAllH_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                hollowSplitter.ClearData();
                this.Controls.Clear();
                this.InitializeComponent();
                RefreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControlGeneral.TabPages.Add(tabHollow);
                TabControlGeneral.SelectTab(tabHollow);
            }
        }
        #endregion
        #region Celeste UI
        private void comboBoxToSplitCeleste_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelChapterCeleste.Hide();
            panelCheckpointsCeleste.Hide();
            panelCassettesNHearts.Hide();

            switch (comboBoxToSplitCeleste.SelectedIndex)
            {
                case 0:
                    panelChapterCeleste.Show(); break;
                case 1:
                    panelCheckpointsCeleste.Show(); break;
                case 2:
                    panelCassettesNHearts.Show(); break;
            }

        }
        private void checkedListBoxCeleste_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxChapterCeleste.SelectedIndex != -1)
            {
                if (checkedListBoxChapterCeleste.GetItemChecked(checkedListBoxChapterCeleste.SelectedIndex) == false)
                {
                    celesteSplitter.AddChapter(checkedListBoxChapterCeleste.SelectedItem.ToString());
                }
                else
                {
                    celesteSplitter.RemoveChapter(checkedListBoxChapterCeleste.SelectedItem.ToString());
                }
            }
        }

        private void checkedListBoxCheckpointsCeleste_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxCheckpointsCeleste.SelectedIndex != -1)
            {
                if (checkedListBoxCheckpointsCeleste.GetItemChecked(checkedListBoxCheckpointsCeleste.SelectedIndex) == false)
                {
                    celesteSplitter.AddChapter(checkedListBoxCheckpointsCeleste.SelectedItem.ToString());
                }
                else
                {
                    celesteSplitter.RemoveChapter(checkedListBoxCheckpointsCeleste.SelectedItem.ToString());
                }
            }
        }

        private void checkedListBoxCassettesNHearts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxCassettesNHearts.SelectedIndex != -1)
            {
                if (checkedListBoxCassettesNHearts.GetItemChecked(checkedListBoxCassettesNHearts.SelectedIndex) == false)
                {
                    celesteSplitter.AddChapter(checkedListBoxCassettesNHearts.SelectedItem.ToString());
                }
                else
                {
                    celesteSplitter.RemoveChapter(checkedListBoxCassettesNHearts.SelectedItem.ToString());
                }
            }
        }


        private void btnRemoveAllCeleste_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                celesteSplitter.ClearData();
                this.Controls.Clear();
                this.InitializeComponent();
                RefreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControlGeneral.TabPages.Add(tabCeleste);
                TabControlGeneral.SelectTab(tabCeleste);
            }
        }
        #endregion
        #region Cuphead UI
        private void comboBoxToSplitCuphead_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossCuphead.Hide();
            panelLevelCuphead.Hide();
            switch (comboBoxToSplitCuphead.SelectedIndex)
            {
                case 0:
                    panelBossCuphead.Show(); break;
                case 1:
                    panelLevelCuphead.Show(); break;
            }
        }

        private void checkedListBoxBossCuphead_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxBossCuphead.SelectedIndex != -1)
            {
                if (checkedListBoxBossCuphead.GetItemChecked(checkedListBoxBossCuphead.SelectedIndex) == false)
                {
                    cupSplitter.AddElement(checkedListBoxBossCuphead.SelectedItem.ToString());
                }
                else
                {
                    cupSplitter.RemoveElement(checkedListBoxBossCuphead.SelectedItem.ToString());
                }
            }
        }

        private void checkedListLevelCuphead_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListLevelCuphead.SelectedIndex != -1)
            {
                if (checkedListLevelCuphead.GetItemChecked(checkedListLevelCuphead.SelectedIndex) == false)
                {
                    cupSplitter.AddElement(checkedListLevelCuphead.SelectedItem.ToString());
                }
                else
                {
                    cupSplitter.RemoveElement(checkedListLevelCuphead.SelectedItem.ToString());
                }
            }
        }

        private void btnRemoveAllCuphead_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                cupSplitter.ClearData();
                this.Controls.Clear();
                this.InitializeComponent();
                RefreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControlGeneral.TabPages.Add(tabCuphead);
                TabControlGeneral.SelectTab(tabCuphead);
            }
        }

        #endregion
        #region DishonoredUI
        private void checkedListBoxDishonored_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxDishonored.SelectedIndex != -1)
            {
                if (checkedListBoxDishonored.GetItemChecked(checkedListBoxDishonored.SelectedIndex) == false)
                {
                    dishonoredSplitter.AddElement(checkedListBoxDishonored.SelectedItem.ToString());
                }
                else
                {
                    dishonoredSplitter.RemoveElement(checkedListBoxDishonored.SelectedItem.ToString());
                }
            }
        }
        private void btnDesactiveAllDishonored_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dishonoredSplitter.ClearData();
                this.Controls.Clear();
                this.InitializeComponent();
                RefreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
                TabControlGeneral.TabPages.Add(tabDishonored);
                TabControlGeneral.SelectTab(tabDishonored);
            }
        }
        #endregion

        #region MultiSelectionMode
        private Dictionary<string, Func<List<string>>> GetSelectionListMapping()
        {
            return new Dictionary<string, Func<List<string>>>
            {
                { "panelBossS", () => comboBoxBossS.Items.OfType<string>().ToList() },
                { "panelMinibossSekiro", () => comboBoxMiniBossSekiro.Items.OfType<string>().ToList() },
                { "panelIdolsS", () => sekiroSplitter.GetAllIdols() },
                { "panelBossER", () => comboBoxBossER.Items.OfType<string>().Concat(comboBoxBossER_DLC.Items.OfType<string>()).ToList() },
                { "panelGraceER", () => comboBoxGraceER.Items.OfType<string>().Concat(comboBoxGraceDLC_ER.Items.OfType<string>()).ToList() },
                { "panelBossDs1", () => comboBoxBossDs1.Items.OfType<string>().ToList() },
                { "panelBonfireDs1", () => comboBoxBonfireDs1.Items.OfType<string>().ToList() },
                { "panelItemDs1", () => comboBoxItemDs1.Items.OfType<string>().ToList() },
                { "panelBossDS2", () => comboBoxBossDs2.Items.OfType<string>().ToList() },
                { "panelBossDs3", () => comboBoxBossDs3.Items.OfType<string>().ToList() },
                { "panelBonfireDs3", () => comboBoxBonfireDs3.Items.OfType<string>().ToList() }
            };
        }
        private List<string> GetSelectionList(string panelName)
        {
            var selectionListMapping = GetSelectionListMapping();
            return selectionListMapping.TryGetValue(panelName, out var getList) ? getList() : new List<string>();
        }

        private void linkLabelMultiSelection_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is LinkLabel linkLabel && linkLabel.Parent is LostBorderPanel panel)
            {
                Cursor = Cursors.WaitCursor;
                var list = GetSelectionList(panel.Name);
                var form = new MultiSelectionMode(list);
                form.Show();
                Cursor = Cursors.Default;

                Task.Run(() =>
                {
                    while (!form.ReadyToRead)
                    {
                        Thread.Sleep(700);
                    }

                    this.Invoke((MethodInvoker)delegate
                    {
                        foreach (var item in form.ReadyElements)
                        {
                            ProccessSelection(item, panel.Name);
                        }
                        form.Close();
                    });
                });
            }
            else
            {
                MessageBox.Show("The LinkLabel is not inside an expected panel");
            }
        }

        private void ProccessSelection(KeyValuePair<string, string> item, string Panel)
        {
            switch (Panel)
            {
                #region Sekiro
                case "panelBossS":
                    comboBoxBossS.SelectedIndex = comboBoxBossS.FindString(item.Key.ToString());
                    comboBoxHowBossS.SelectedIndex = comboBoxHowBossS.FindString(item.Value.ToString());

                    btn_AddBossS_Click(null, null);
                    break;
                case "panelMinibossSekiro":
                    comboBoxMiniBossSekiro.SelectedIndex = comboBoxMiniBossSekiro.FindString(item.Key.ToString());
                    comboBoxHowMiniBossS.SelectedIndex = comboBoxHowBossS.FindString(item.Value.ToString());

                    btnAddMiniBossSekiro_Click(null, null);
                    break;
                case "panelIdolsS":
                    sekiroSplitter.AddIdol(item.Key, item.Value);
                    this.Controls.Clear();
                    this.InitializeComponent();
                    RefreshForm();
                    this.AutoSplitter_Load(null, null);//Load Others Games Settings
                    TabControlGeneral.TabPages.Add(tabSekiro);
                    TabControlGeneral.SelectTab(tabSekiro);
                    toSplitSelectSekiro.SelectedIndex = 2;
                    comboBoxZoneSelectS.SelectedIndex = 0;
                    groupBoxAshinaOutskirts.Show();
                    break;
                #endregion
                #region ER
                case "panelBossER":
                    comboBoxBossER.SelectedIndex = comboBoxBossER.FindString(item.Key.ToString());
                    comboBoxBossER_DLC.SelectedIndex = comboBoxBossER_DLC.FindString(item.Key.ToString());
                    comboBoxHowBossER.SelectedIndex = comboBoxHowBossER.FindString(item.Value.ToString());

                    checkBoxDLCBossER.Checked = comboBoxBossER_DLC.SelectedIndex > -1;
                    btnAddBossER_Click(null, null);
                    break;
                case "panelGraceER":
                    comboBoxGraceER.SelectedIndex = comboBoxGraceER.FindString(item.Key.ToString());
                    comboBoxGraceDLC_ER.SelectedIndex = comboBoxGraceDLC_ER.FindString(item.Key.ToString());
                    comboBoxHowGraceER.SelectedIndex = comboBoxHowGraceER.FindString(item.Value.ToString());

                    checkBoxViewDlcGrace.Checked = comboBoxGraceDLC_ER.SelectedIndex > -1;
                    btnAddGraceER_Click(null, null);
                    break;
                #endregion
                #region DS1
                case "panelBossDs1":
                    comboBoxBossDs1.SelectedIndex = comboBoxBossDs1.FindString(item.Key.ToString());
                    comboBoxHowBossDs1.SelectedIndex = comboBoxHowBossDs1.FindString(item.Value.ToString());

                    btnAddBossDs1_Click(null, null);
                    break;
                case "panelBonfireDs1":
                    comboBoxStateDs1.SelectedIndex = 1; // Unlocked Default add
                    comboBoxBonfireDs1.SelectedIndex = comboBoxBonfireDs1.FindString(item.Key.ToString());
                    comboBoxHowBonfireDs1.SelectedIndex = comboBoxHowBonfireDs1.FindString(item.Value.ToString());

                    btnAddBonfireDs1_Click(null, null);
                    break;
                case "panelItemDs1":
                    comboBoxItemDs1.SelectedIndex = comboBoxItemDs1.FindString(item.Key.ToString());
                    comboBoxHowItemDs1.SelectedIndex = comboBoxHowItemDs1.FindString(item.Value.ToString());

                    btnAddItemDs1_Click(null, null);
                    break;
                #endregion
                #region DS2
                case "panelBossDS2":
                    comboBoxBossDs2.SelectedIndex = comboBoxBossDs2.FindString(item.Key.ToString());
                    comboBoxHowBossDs2.SelectedIndex = comboBoxHowBossDs2.FindString(item.Value.ToString());

                    btnAddBossDS2_Click(null, null);
                    break;
                #endregion
                #region DS3
                case "panelBossDs3":
                    comboBoxBossDs3.SelectedIndex = comboBoxBossDs3.FindString(item.Key.ToString());
                    comboBoxHowBossDs3.SelectedIndex = comboBoxHowBossDs3.FindString(item.Value.ToString());

                    btnAddBossDs3_Click(null, null);
                    break;
                case "panelBonfireDs3":
                    comboBoxBonfireDs3.SelectedIndex = comboBoxBonfireDs3.FindString(item.Key.ToString());
                    comboBoxHowBonfireDs3.SelectedIndex = comboBoxHowBonfireDs3.FindString(item.Value.ToString());

                    btnAddBonfireDs3_Click(null, null);
                    break;
                #endregion
                default:
                    MessageBox.Show("The LinkLabel is not inside an expected panel");
                    break;
            }
        }
        #endregion
    }
}

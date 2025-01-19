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

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using HitCounterManager;

namespace AutoSplitterCore
{
    public partial class Debug : Form
    {
        private AutoSplitterMainModule mainModule;
        private int GameActive = 0;
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 100 };

        public Debug(AutoSplitterMainModule mainModule)
        {
            InitializeComponent();
            this.mainModule = mainModule;
            mainModule.InitDebug();
            mainModule.RegisterHitCounterManagerInterface(new AutoSplitterCoreInterface(new HitCounterManager.Form1()));
            _update_timer.Tick += (sender, args) => CheckInfo();
            _update_timer.Enabled = true;
        }

        private void Debug_Load(object sender, EventArgs e)
        {
            comboBoxIGTConversion.SelectedIndex = 1;
            checkBoxPracticeMode.Checked = mainModule.GetPracticeMode();
            List<string> GameList = mainModule.GetGames();
            foreach (string i in GameList) comboBoxGame.Items.Add(i);
            switch (mainModule.GetSplitterEnable())
            {
                case GameConstruction.SekiroSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.SekiroSplitterIndex; break;
                case GameConstruction.Ds1SplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.Ds1SplitterIndex; break;
                case GameConstruction.Ds2SplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.Ds2SplitterIndex; break;
                case GameConstruction.Ds3SplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.Ds3SplitterIndex; break;
                case GameConstruction.EldenSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.EldenSplitterIndex; break;
                case GameConstruction.HollowSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.HollowSplitterIndex; break;
                case GameConstruction.CelesteSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.CelesteSplitterIndex; break;
                case GameConstruction.CupheadSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.CupheadSplitterIndex; break;
                case GameConstruction.DishonoredSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.DishonoredSplitterIndex; break;
                case GameConstruction.NoneSplitterIndex:
                default: comboBoxGame.SelectedIndex = GameConstruction.NoneSplitterIndex; break;
            }
            LabelVersion.Text = mainModule.updateModule.currentVer;
            labelCloudVer.Text = mainModule.updateModule.cloudVer;
#if !HCMv2
            mainModule.debugForm = this;
#endif
            listBoxLog.Items.Clear(); 
        }
        private void comboBoxGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainModule.EnableSplitting(comboBoxGame.SelectedIndex);
            GameActive = comboBoxGame.SelectedIndex;
            mainModule.igtModule.gameSelect = GameActive;
            listBoxLog.Items.Add("CheckFlags is changed by: " + comboBoxGame.Text);
        }

        public void UpdateBoxes()
        {
            comboBoxGame.SelectedIndex = mainModule.GetSplitterEnable();
            checkBoxPracticeMode.Checked = mainModule.GetPracticeMode();
        }

        bool debugSplit = false;
        private void CheckInfo()
        {         
            this.textBoxX.Clear();
            this.textBoxY.Clear();
            this.textBoxZ.Clear();
            this.textBoxSceneName.Clear();
            this.textBoxIGT.Clear();
            bool status = false;
            int conv = 1;
            switch (comboBoxIGTConversion.SelectedIndex)
            {
                case 0: conv = 1; break;
                case 1: conv = 1000; break;
                case 2: conv = 60000; break;
            }

            if(GameActive > 0) CheckingSplit();
            switch (GameActive)
            {
                case GameConstruction.SekiroSplitterIndex:
                    var Vector1 = mainModule.sekiroSplitter.GetCurrentPosition();
                    this.textBoxX.Paste(Vector1.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector1.Y.ToString("0.00"));
                    this.textBoxZ.Paste(Vector1.Z.ToString("0.00"));
                    status = mainModule.sekiroSplitter._StatusSekiro;
                    break;
                case GameConstruction.Ds1SplitterIndex:
                    var Vector2 = mainModule.ds1Splitter.GetCurrentPosition();
                    this.textBoxX.Paste(Vector2.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector2.Y.ToString("0.00"));
                    this.textBoxZ.Paste(Vector2.Z.ToString("0.00"));
                    status = mainModule.ds1Splitter._StatusDs1;
                    break;
                case GameConstruction.Ds2SplitterIndex:
                    var Vector3 = mainModule.ds2Splitter.GetCurrentPosition();
                    this.textBoxX.Paste(Vector3.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector3.Y.ToString("0.00"));
                    this.textBoxZ.Paste(Vector3.Z.ToString("0.00"));
                    status = mainModule.ds2Splitter._StatusDs2;
                    break;
                case GameConstruction.Ds3SplitterIndex: 
                    status = mainModule.ds3Splitter._StatusDs3;
                    var VectorF = mainModule.ds3Splitter.GetCurrentPosition();
                    this.textBoxX.Paste(VectorF.X.ToString("0.00"));
                    this.textBoxY.Paste(VectorF.Y.ToString("0.00"));
                    this.textBoxZ.Paste(VectorF.Z.ToString("0.00"));
                    break;
                case GameConstruction.EldenSplitterIndex:
                   var Vector5 = mainModule.eldenSplitter.GetCurrentPosition(); 
                    this.textBoxX.Paste(Vector5.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector5.Y.ToString("0.00"));
                    this.textBoxZ.Paste(Vector5.Z.ToString("0.00"));
                    status = mainModule.eldenSplitter._StatusElden;
                    break;
                case GameConstruction.HollowSplitterIndex: 
                    var Vector6 = mainModule.hollowSplitter.GetCurrentPosition();
                    this.textBoxX.Paste(Vector6.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector6.Y.ToString("0.00"));
                    this.textBoxSceneName.Paste(mainModule.hollowSplitter.currentPosition.sceneName.ToString());
                    status = mainModule.hollowSplitter._StatusHollow;
                    break;
                case GameConstruction.CelesteSplitterIndex:
                    this.textBoxSceneName.Paste(mainModule.celesteSplitter.GetLevelName());
                    status = mainModule.celesteSplitter._StatusCeleste;
                    break;
                case GameConstruction.CupheadSplitterIndex:
                    this.textBoxSceneName.Paste(mainModule.cupSplitter.GetSceneName());
                    status = mainModule.cupSplitter._StatusCuphead;
                    break;
                case GameConstruction.DishonoredSplitterIndex:
                    status = mainModule.dishonoredSplitter._StatusDish;
                    break;
                    
                case GameConstruction.NoneSplitterIndex:
                default: break;
            }
            this.textBoxIGT.Paste((mainModule.ReturnCurrentIGT() / conv).ToString());
            if (status) { Running.Show(); NotRunning.Hide(); } else { NotRunning.Show(); Running.Hide(); }           
        }

        private void CheckingSplit()
        {
            /*
            switch (GameActive)
            {
                case GameConstruction.SekiroSplitterIndex:
                    debugSplit = mainModule.sekiroSplitter._SplitGo;
                    break;
                case GameConstruction.Ds1SplitterIndex:
                    debugSplit = mainModule.ds1Splitter._SplitGo;
                    break;
                case GameConstruction.Ds2SplitterIndex:
                    debugSplit = mainModule.ds2Splitter._SplitGo;
                    break;
                case GameConstruction.Ds3SplitterIndex:
                    debugSplit = mainModule.ds3Splitter._SplitGo;
                    break;
                case GameConstruction.EldenSplitterIndex:
                    debugSplit = mainModule.ds3Splitter._SplitGo;
                    break;
                case GameConstruction.HollowSplitterIndex:
                    debugSplit = mainModule.hollowSplitter._SplitGo;
                    break;
                case GameConstruction.CelesteSplitterIndex:
                    debugSplit = mainModule.celesteSplitter._SplitGo;
                    break;
                case GameConstruction.CupheadSplitterIndex:
                    debugSplit = mainModule.cupSplitter._SplitGo;
                    break;
                case GameConstruction.DishonoredSplitterIndex:
                    debugSplit = mainModule.dishonoredSplitter._SplitGo; break;
            
                case GameConstruction.NoneSplitterIndex:
                default: break;
            }*/
            Thread.Sleep(1200);
            if (debugSplit) { listBoxLog.Items.Add("The Game: " + comboBoxGame.Text + "Generate a Flag"); debugSplit = false; }
            if (listBoxLog.Items.Count > 1) listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
        }

        private void btnSplitter_Click(object sender, EventArgs e)
        {
            mainModule.AutoSplitterForm(false);
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            mainModule.SaveAutoSplitterSettings();
            MessageBox.Show("Save Successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefreshGame_Click(object sender, EventArgs e)
        {
            switch (GameActive)
            {
                case GameConstruction.SekiroSplitterIndex:
                    mainModule.sekiroSplitter.GetSekiroStatusProcess(0);
                    break;
                case GameConstruction.Ds1SplitterIndex:
                    mainModule.ds1Splitter.GetDs1StatusProcess(0);
                    break;
                case GameConstruction.Ds2SplitterIndex:
                    mainModule.ds2Splitter.GetDs2StatusProcess(0);
                    break;
                case GameConstruction.Ds3SplitterIndex:
                    mainModule.ds3Splitter.GetDs3StatusProcess(0);
                    break;
                case GameConstruction.EldenSplitterIndex:
                    mainModule.eldenSplitter.GetEldenStatusProcess(0);
                    break;
                case GameConstruction.HollowSplitterIndex:
                    mainModule.hollowSplitter.GetHollowStatusProcess(0);
                    break;
                case GameConstruction.CelesteSplitterIndex:
                    mainModule.celesteSplitter.GetCelesteStatusProcess(0);
                    break;
                case GameConstruction.CupheadSplitterIndex:
                    mainModule.cupSplitter.GetCupheadStatusProcess(0);
                    break;
                case GameConstruction.DishonoredSplitterIndex:
                    mainModule.dishonoredSplitter.GetDishonoredStatusProcess();
                    break;
                case GameConstruction.NoneSplitterIndex:
                default: break;
            }
            listBoxLog.Items.Add("Refreshed Process");
        }

        private void textBoxCfID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCfID.Text != null && textBoxCfID.Text != String.Empty)
            {
                try
                {
                    var flag = uint.Parse(textBoxCfID.Text);
                    bool status = false;
                    if (mainModule.GameOn())
                    {
                        switch (GameActive)
                        {
                            case GameConstruction.SekiroSplitterIndex:
                                status = mainModule.sekiroSplitter.CheckFlag(flag);
                                break;
                            case GameConstruction.Ds1SplitterIndex:
                                status = mainModule.ds1Splitter.CheckFlag(flag);
                                break;
                            case GameConstruction.Ds2SplitterIndex:
                                status = mainModule.ds2Splitter.CheckFlag(flag);
                                break;
                            case GameConstruction.Ds3SplitterIndex:
                                status = mainModule.ds3Splitter.CheckFlag(flag);
                                break;
                            case GameConstruction.EldenSplitterIndex:
                                status = mainModule.eldenSplitter.CheckFlag(flag);
                                break;
                            case GameConstruction.HollowSplitterIndex:
                                break;
                            case GameConstruction.CelesteSplitterIndex:
                                break;
                            case GameConstruction.CupheadSplitterIndex:
                                break;
                            case GameConstruction.DishonoredSplitterIndex:
                                break;
                            case GameConstruction.NoneSplitterIndex:
                            default: break;
                        }
                        if (status) { btnSplitCf.BackColor = System.Drawing.Color.Green; } else { btnSplitCf.BackColor = System.Drawing.Color.Red; }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Check Flag", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBoxPracticeMode_CheckedChanged(object sender, EventArgs e)
        {
            mainModule.SetPracticeMode(checkBoxPracticeMode.Checked);
        }

        private void btnSplitCf_Click(object sender, EventArgs e)
        {
            textBoxCfID_TextChanged(null, null);
        }

        private void btnResetFlags_Click(object sender, EventArgs e)
        {
            mainModule.ResetSplitterFlags();
            listBoxLog.Items.Add("Reseted Flags of: " + comboBoxGame.Text);
        }
    }
}

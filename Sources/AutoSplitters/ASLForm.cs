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

using LiveSplit.Model;
using LiveSplit.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSplitterCore
{
    public partial class ASLForm : ReaLTaiizor.Forms.MaterialForm
    {
        ASLSplitter aslSplitter = ASLSplitter.GetInstance();
        SaveModule SaveModule = null;

        public ASLForm(SaveModule savemodule)
        {
            InitializeComponent();
            this.SaveModule = savemodule;
        }

        private void ASLForm_Load(object sender, EventArgs e)
        {
            labelInfoASL.Text = Properties.Resources.ASLInfo;
            using (var stream = new System.IO.MemoryStream(Properties.Resources.AutoSplitterSetup))
            {
                this.Icon = new System.Drawing.Icon(stream);
            }
            Control controlObteined = aslSplitter.AslControl;
            controlObteined.Margin = new Padding(5);
            controlObteined.Padding = new Padding(5);
            lostBorderPanelASLConfig.Controls.Add(controlObteined);

            metroCheckBoxIGT.Checked = SaveModule.generalAS.ASLIgt;
            tabPageASLConfig.Focus();

            btnWebsite.Show();
            btnActivate.Enabled = false;
            btnWebsite.Enabled = false;
            cbxGameName.Enabled = false;

            Task.Run(async () =>
            {
                try
                {
                    // Cambiar el cursor de toda la aplicación a "Wait"
                    labelLoading.Invoke((Action)(() => Application.UseWaitCursor = true));

                    while (!loadedGames)
                    {
                        await Task.Delay(1000);
                    }
                }
                finally
                {
                    labelLoading.Invoke((Action)(() => Application.UseWaitCursor = false));
                }
            });

            FillCbxGameName();
        }

        private void metroCheckBoxIGT_CheckedChanged(object sender)
        {
            aslSplitter.IGTEnable = metroCheckBoxIGT.Checked;
            SaveModule.generalAS.ASLIgt = metroCheckBoxIGT.Checked;

            if (SplitterControl.GetControl().GetDebug()) DebugLog.LogMessage($"Trace ASL: IGT ASL Changed to: {SaveModule.generalAS.ASLIgt}");
        }

        private string[] gameNames;
        private bool loadedGames = false;
        private void FillCbxGameName()
        {
            Task.Run(() =>
            {
                try
                {
                    IEnumerable<string> cachedGameNames = CompositeGameList.Instance.GetGameNames(true);

                    if (cachedGameNames != null)
                    {
                        gameNames = cachedGameNames.ToArray();

                        this.InvokeIfRequired(() =>
                        {
                            Application.DoEvents();
                            cbxGameName.BeginUpdate();
                            cbxGameName.Items.Clear();
                            cbxGameName.Items.AddRange(gameNames);
                            labelLoading.Hide();
                            cbxGameName.Enabled = true;
                            loadedGames = true;
                            cbxGameName.EndUpdate();
                        });
                    }
                    else
                    {
                        cachedGameNames = new string[0];
                    }
                }
                catch (Exception ex)
                {
                    DebugLog.LogMessage($"Error Produced on FillCbxGameName: {ex.Message}",ex);
                }
            });
        }

        LiveSplit.Model.AutoSplitter splitter;

        private void cbxGameName_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitter = AutoSplitterFactory.Instance.Create(cbxGameName.Text);
            RefreshAutoSplittingUI();
        }


        protected void RefreshAutoSplittingUI()
        {
            if (splitter != null)
            {
                if (splitter.Type == AutoSplitterType.Script)
                {
                    lblDescription.Text = splitter.Description + $"\nFile: {splitter.FileName}";
                    btnActivate.Enabled = true;
                }
                else
                {
                    btnActivate.Enabled = false;
                    lblDescription.Text += "\n(NOT a ASL Script - Contact ASC Developer to develop this dll if community demand)";
                }

            }
            else
            {
                lblDescription.Text = "There is no Auto Splitter available for this game.";
                btnActivate.Enabled = false;
            }
            btnWebsite.Enabled = splitter != null && splitter.Website != null;
        }


        private void btnWebsite_Click(object sender, EventArgs e)
        {
            var url = new Uri(splitter.Website);
            AutoSplitterMainModule.OpenWithBrowser(url);
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            string savePath = Path.GetFullPath("./ASLFiles");

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            WebClient webClient = new WebClient();
            foreach (string uRL in splitter.URLs)
            {
                string text = uRL;
                int num = uRL.LastIndexOf('/') + 1;
                string text2 = text.Substring(num, text.Length - num);
                string path = text2 + "-temp";
                string fullPath = Path.GetFullPath(Path.Combine($"{savePath}\\", text2));
                string fullPath2 = Path.GetFullPath(Path.Combine($"{savePath}\\", path));
                try
                {
                    webClient.DownloadFile(new Uri(uRL), fullPath2);
                    File.Copy(fullPath2, fullPath, overwrite: true);
                }
                catch (Exception ex2)
                {
                    DebugLog.LogMessage("Error Download ASLFile: " + ex2.Message);
                }
                finally
                {
                    try
                    {
                        File.Delete(fullPath2);
                    }
                    catch (Exception)
                    {
                        DebugLog.LogMessage("Failed to delete temp file: " + fullPath2);
                    }
                    MessageBox.Show($"ASL File Download Successfully\nSaved on: {fullPath}", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnGetASL_Click(object sender, EventArgs e) => AutoSplitterMainModule.OpenWithBrowser(new Uri("https://github.com/neimex23/AutoSplitterCore/wiki/English#asl-scripts"));

    }
    internal static class FormControl
    {
        /// <summary>
        /// Executes an <see cref="Action"/>, invoking it if necessary.
        /// </summary>
        /// <param name="control">The control to act upon.</param>
        /// <param name="action">The action to execute.</param>
        public static void InvokeIfRequired(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}

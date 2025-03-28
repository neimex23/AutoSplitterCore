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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveSplit.Model;
using LiveSplit.Web;
using System.Threading;
using System.IO;
using LiveSplit.UI.Components;
using System.Net;

namespace ASLBridge
{
    public partial class ASLFormServer : ReaLTaiizor.Forms.MaterialForm
    {
        ASLSplitterServer aslSplitter = ASLSplitterServer.GetInstance();
        public static bool IGTActive = false;

        public ASLFormServer()
        {
            InitializeComponent();
        }

        private void ASLForm_Load(object sender, EventArgs e)
        {
            labelInfoASL.Text = "Start: Triggered when the Start Trigger produces an HCM Timer activation.\r\nSplit: Triggered when the SplitFlag Trigger generates an HCM Split.\r\nReset: Triggered when the Reset Trigger initiates an HCM Restart Run.";
            Control controlObteined = aslSplitter.AslControl;
            controlObteined.Margin = new Padding(5);
            controlObteined.Padding = new Padding(5);
            lostBorderPanelASLConfig.Controls.Add(controlObteined);

            metroCheckBoxIGT.Checked = IGTActive;
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
            IGTActive = metroCheckBoxIGT.Checked;

            if (aslSplitter.IGTEnable)
                MainModuleServer.BroadcastEvent("event:enableigt");
            else
                MainModuleServer.BroadcastEvent("event:disableigt");
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
                    Console.WriteLine($"Error Produced on FillCbxGameName: {ex.Message}");
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
                    lblDescription.Text += "\n(NOT a ASL Script - Contact ASC Developer to develop this dll if community demand)";
                }

            } else
            {
                lblDescription.Text = "There is no Auto Splitter available for this game.";
            }
            btnWebsite.Enabled = splitter != null && splitter.Website != null;
        }


        private void btnWebsite_Click(object sender, EventArgs e)
        {
            var url = new Uri(splitter.Website);
            MainModuleServer.OpenWithBrowser(url);
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
                   Console.WriteLine("Error Download ASLFile: " + ex2.Message);
                }
                finally
                {
                    try
                    {
                        File.Delete(fullPath2);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to delete temp file: " + fullPath2);
                    }
                    MessageBox.Show($"ASL File Download Successfully\nSaved on: {fullPath}", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnGetASL_Click(object sender, EventArgs e) => MainModuleServer.OpenWithBrowser(new Uri("https://github.com/neimex23/AutoSplitterCore/wiki/English#asl-scripts"));

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

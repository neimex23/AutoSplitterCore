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

namespace ASLBridge
{
    public partial class ASLFormServer : ReaLTaiizor.Forms.MaterialForm
    {
        ASLSplitterServer aslSplitter = ASLSplitterServer.GetInstance();

        private static readonly ASLFormServer instance = new ASLFormServer();
        public static ASLFormServer GetIntance() => instance;

        private ASLFormServer()
        {
            DebugLog.Initialize();
            InitializeComponent();

            var contextMenu = new ContextMenuStrip();
            var exitItem = new ToolStripMenuItem("Close ASLBridge");
            var openASLBridge = new ToolStripMenuItem("Open ASLBridge");
            exitItem.Click += (s, e) =>
            {
                notifyIconASLService.Visible = false;
                notifyIconASLService.Dispose();
                MainModuleServer.InternalExitCommand();
            };
            openASLBridge.Click += (s, e) =>
            {
                ShowForm();
            };

            contextMenu.Items.Add(openASLBridge);
            contextMenu.Items.Add(exitItem);
            notifyIconASLService.ContextMenuStrip = contextMenu;
        }

        public void ShowForm() => this.InvokeIfRequired(() =>
        {
            RefreshAslSettings();
            notifyIconASLService.Visible = false;
            this.Show();
        });

        public void HideForm() => this.InvokeIfRequired(() =>
        {
            this.Hide();
            notifyIconASLService.Visible = true;
        });

        public void CloseForm() => this.InvokeIfRequired(() =>
        {
            this.Close();
        });

        private bool firstStart = true;
        private void ASLFormServer_Shown(object sender, EventArgs e)
        {
            if (firstStart)
            {
                HideForm();
                firstStart = false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                HideForm();
            }
            base.OnFormClosing(e);
        }

        private void ASLForm_Load(object sender, EventArgs e)
        {
            SaveModuleServer.GetIntance().LoadASLSettings();
            MainModuleServer.LoadProcess();

            labelInfoASL.Text = "Start: Triggered when the Start Trigger produces an HCM Timer activation.\r\nSplit: Triggered when the SplitFlag Trigger generates an HCM Split.\r\nReset: Triggered when the Reset Trigger initiates an HCM Restart Run.";
            RefreshAslSettings();

            tabPageASLConfig.Focus();

            btnWebsite.Show();
            btnActivate.Enabled = false;
            btnWebsite.Enabled = false;
            cbxGameName.Enabled = false;

            FillCbxGameName();
        }

        private void RefreshAslSettings()
        {
            Control controlObteined = aslSplitter.AslControl;
            controlObteined.Margin = new Padding(5);
            controlObteined.Padding = new Padding(5);
            if (lostBorderPanelASLConfig.Controls.Count > 0) lostBorderPanelASLConfig.Controls.Clear();
            lostBorderPanelASLConfig.Controls.Add(controlObteined);
        }

        private bool suppressIGTEvent = false;
        public void SetIgt(bool status)
        {
            this.InvokeIfRequired(() =>
            {
                suppressIGTEvent = true;
                metroCheckBoxIGT.Checked = status;
                metroCheckBoxIGT.Refresh();
                suppressIGTEvent = false;
            });
        }

        private void metroCheckBoxIGT_CheckedChanged(object sender)
        {
            if (suppressIGTEvent) return;

            if (metroCheckBoxIGT.Checked)
                Task.Run(() => MainModuleServer.BroadcastEvent("event:enableigt"));
            else
                Task.Run(() => MainModuleServer.BroadcastEvent("event:disableigt"));
        }

        private string[] gameNames;
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
                    DebugLog.LogMessage($"Error Produced on FillCbxGameName: {ex.Message}");
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
            lblDescription.Text = string.Empty;
            btnActivate.Enabled = false;
            btnWebsite.Enabled = false;

            if (splitter == null)
            {
                lblDescription.Text = "There is no Auto Splitter available for this game.";
                return;
            }

            string description = splitter.Description ?? "(No description available)";

            if (splitter.Type == AutoSplitterType.Script)
            {
                bool isASLHelper = splitter.URLs != null &&
                                   splitter.URLs.Any(url => url?.IndexOf("ASLHelper", StringComparison.OrdinalIgnoreCase) >= 0);

                if (isASLHelper)
                {
                    lblDescription.Text = $"{description}\n(This is an ASL Helper Script. ASC does not support these scripts. Please use another compatible one.)";
                }
                else
                {
                    lblDescription.Text = $"{description}\nFile: {splitter.FileName ?? "Unknown"}";
                    btnActivate.Enabled = true;
                }
            }
            else
            {
                lblDescription.Text = $"{description}\n(Not an ASL Script - Contact the ASC developer if community support is requested for this DLL.)";
            }

            btnWebsite.Enabled = !string.IsNullOrWhiteSpace(splitter.Website);
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

        private void btnGetASL_Click(object sender, EventArgs e) => MainModuleServer.OpenWithBrowser(new Uri("https://github.com/neimex23/AutoSplitterCore/wiki/English#asl-scripts"));

        private void notifyIconASLService_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowForm();
        }
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

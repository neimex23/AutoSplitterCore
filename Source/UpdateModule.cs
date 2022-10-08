//MIT License

//Copyright (c) 2022 Ezequiel Medina
//Based on Update.cs of HitCounterManager by Peter Kirmeier - License: MIT

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
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using TinyJson;
using System.IO;

namespace AutoSplitterCore
{
    public class UpdateModule
    {
        WebClient client = new WebClient();
        public string currentVer;
        public string cloudVer;
        public bool CheckUpdatesOnStartup;
        private static List<Version> Releases = new List<Version>();
        private Assembly dll;
        public bool DebugMode = false;

        public void CheckUpdates(bool ForceUpdate)
        {
            try
            {
                client.Encoding = System.Text.Encoding.UTF8;

                // https://developer.github.com/v3/#user-agent-required
                client.Headers.Add("User-Agent", "HitCounterManager/" + Application.ProductVersion.ToString());
                // https://developer.github.com/v3/media/#request-specific-version
                client.Headers.Add("Accept", "application/vnd.github.v3.text+json");
                // https://developer.github.com/v3/repos/releases/#get-a-single-release
                string response = client.DownloadString("http://api.github.com/repos/neimex23/HitCounterManager/releases");

                var auxReleases = response.FromJson<List<Dictionary<string, object>>>();

                /* For Test New Version
                Version debugVer = Version.Parse("1.7.0");
                Releases.Add(debugVer);
                */

                Releases.Clear();
                foreach (var aux in auxReleases)
                {
                    var ver = aux["tag_name"].ToString();
                    if (ver.StartsWith("ASC_"))
                    {
                        ver = ver.Remove(0, 5); //Remove "ASC_v"
                        ver = ver + ".0"; //Add .0
                        Version version = Version.Parse(ver);
                        Releases.Add(version);
                    }
                }
                cloudVer = Releases[0].ToString();
                if (!DebugMode) dll = Assembly.LoadFrom("AutoSplitterCore.dll");
                _ = DebugMode ? currentVer = Application.ProductVersion.ToString() + ".0" : currentVer = dll.GetName().Version.ToString();
            }
            catch (Exception) { };
            if (CheckUpdatesOnStartup && Releases.Count > 0 && dll != null && (Releases[0] > dll.GetName().Version))
            {
                Form aux = new Form();
                if (NewVersionDialog(aux) == DialogResult.Yes) System.Diagnostics.Process.Start("https://github.com/neimex23/HitCounterManager/releases/latest");
            } else if (ForceUpdate) { MessageBox.Show("You have the latest Version", "Last Version", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        public static DialogResult NewVersionDialog(Form ParentWindow)
        {
            const int ClientPad = 15;
            Form frm = new Form();

            frm.StartPosition = FormStartPosition.CenterParent;
            frm.FormBorderStyle = FormBorderStyle.FixedDialog;
            frm.Icon = ParentWindow.Icon;
            frm.ShowInTaskbar = false;
            frm.FormBorderStyle = FormBorderStyle.Sizable;
            frm.MaximizeBox = true;
            frm.MinimizeBox = false;
            frm.ClientSize = new Size(345, 190);
            frm.MinimumSize = frm.ClientSize;
            frm.Text = "New version available";

            Label label = new Label();
            label.Size = new Size(frm.ClientSize.Width - ClientPad, 20);
            label.Location = new Point(ClientPad, ClientPad);
            label.Text = "Latest available version:      " + Releases[0].ToString();
            frm.Controls.Add(label);

            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Location = new Point(frm.ClientSize.Width - okButton.Size.Width - ClientPad, frm.ClientSize.Height - okButton.Size.Height - ClientPad);
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Text = "&OK";
            frm.Controls.Add(okButton);

            Button wwwButton = new Button();
            wwwButton.DialogResult = DialogResult.Yes;
            wwwButton.Name = "wwwButton";
            wwwButton.Location = new Point(frm.ClientSize.Width - wwwButton.Size.Width - ClientPad - okButton.Size.Width - ClientPad, frm.ClientSize.Height - wwwButton.Size.Height - ClientPad);
            wwwButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            wwwButton.Text = "&Go to download page";
            wwwButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            wwwButton.AutoSize = true;
            frm.Controls.Add(wwwButton);

            frm.AcceptButton = okButton;
            return frm.ShowDialog(ParentWindow);
        }
    }
}

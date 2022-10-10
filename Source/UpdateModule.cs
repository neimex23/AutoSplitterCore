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
            if ((CheckUpdatesOnStartup && Releases.Count > 0 && dll != null && (Releases[0] > dll.GetName().Version))) //|| (DebugMode))
            {
                Form aux = new UpdateShowDialog(this);
                aux.ShowDialog();
            } else if (ForceUpdate) { MessageBox.Show("You have the latest Version", "Last Version", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
    }
}

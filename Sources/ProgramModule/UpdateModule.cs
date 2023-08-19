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
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using TinyJson;

namespace AutoSplitterCore
{
    public class UpdateModule
    {
        WebClient client = new WebClient();
        public string currentVer;
        public string currentVersionNotDot;
        public string cloudVer;
        public string cloudVerNotDot;
        public string cloudSoulsVer;
        public string cloudSoulsVerNotDot;
        public string currentSoulsVer;
        public bool CheckUpdatesOnStartup;
        private static List<Version> Releases = new List<Version>();
        private static List<Version> SoulsMemoryRelease = new List<Version>();
        private Assembly dll;
        private Assembly SoulDll;
        public bool DebugMode = false;

        public void CheckUpdates(bool ForceUpdate)
        {
            bool update = false;
            try
            {
                //AutoSplitterCore GetVersions
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
                        Releases.Add(Version.Parse(ver));
                    }
                }
                cloudVer = Releases[0].ToString() + ".0";
                cloudVerNotDot = Releases[0].ToString();
                if (!DebugMode)
                {
                    dll = Assembly.LoadFrom("AutoSplitterCore.dll");
                    currentVer = dll.GetName().Version.ToString();
                    currentVersionNotDot = currentVer;
                    currentVersionNotDot = currentVersionNotDot.Remove(currentVersionNotDot.LastIndexOf(".0"));
                }
                else
                {
                    currentVer = Application.ProductVersion.ToString() + ".0";
                    currentVersionNotDot = Application.ProductVersion.ToString();
                }

                //SoulsMemory GetVersions
                client.Headers.Clear();
                SoulsMemoryRelease.Clear();
                SoulDll = Assembly.LoadFrom("SoulMemory.dll");
                currentSoulsVer = SoulDll.GetName().Version.ToString();
                client.Headers.Add("User-Agent", "SoulSplitter/" + SoulDll.ToString());
                client.Headers.Add("Accept", "application/vnd.github.v3.text+json");
                response = client.DownloadString("http://api.github.com/repos/FrankvdStam/SoulSplitter/releases");
                auxReleases = response.FromJson<List<Dictionary<string, object>>>();
                foreach (var aux in auxReleases)
                {
                    if (aux.TryGetValue("tag_name", out var tagValue) && tagValue is string ver)
                    {
                        Version outVer = null;
                        Version.TryParse(ver, out outVer);
                        if (outVer != null) 
                            SoulsMemoryRelease.Add(outVer);
                    }                        
                }

                if (SoulsMemoryRelease.Count > 0)
                {
                    cloudSoulsVerNotDot = SoulsMemoryRelease[0].ToString();
                    cloudSoulsVer = SoulsMemoryRelease[0].ToString() + ".0";
                }
            }
            catch (Exception) { };
            if ((CheckUpdatesOnStartup)) //|| (DebugMode))
            {
                if (Releases.Count > 0 && dll != null && Releases[0] > dll.GetName().Version)
                {
                    update = true;
                    Form aux = new UpdateShowDialog(this);
                    aux.ShowDialog();
                }

                if (SoulsMemoryRelease.Count > 0 && SoulDll != null && cloudSoulsVer != SoulDll.GetName().Version.ToString())
                {
                    update = true;
                    Form aux2 = new UpdateShowDialogSouls(this);
                    aux2.ShowDialog();
                }

                if (!update && ForceUpdate) MessageBox.Show("You have the latest Version", "Last Version", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}

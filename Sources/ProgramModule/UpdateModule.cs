//MIT License

//Copyright (c) 2022-2025 Ezequiel Medina
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

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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

        #region SingletonFactory
        private static UpdateModule _intance = new UpdateModule();

        private UpdateModule() { }

        public static UpdateModule GetIntance() { return _intance; }
        #endregion

        private void AddAuthorization()
        {
            try
            {
                string jsonresult = string.Empty;
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("AutoSplitterCore.appsettings.json"))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonContent = reader.ReadToEnd();
                    var jsonObject = JObject.Parse(jsonContent);
                    jsonresult = jsonObject["GitHubApi"]["PublicRepoToken"].ToString();
                }
                // Authorization Token with PublicRepo Permissions
                client.Headers.Add("Authorization", jsonresult);
            }
            catch (Exception ex)
            {
                DebugLog.LogMessage($"Exception produced on Authorization JSON on UpdateModule: {ex.Message}");
            }
        }

        public void CheckUpdates(bool ForceUpdate)
        {
            bool update = false;
            try
            {
                //AutoSplitterCore GetVersions

                // https://developer.github.com/v3/#user-agent-required
                client.Headers.Add("User-Agent", "AutoSplitterCore/" + Application.ProductVersion.ToString());
                // https://developer.github.com/v3/media/#request-specific-version
                client.Headers.Add("Accept", "application/vnd.github.v3.text+json");
                AddAuthorization();
                // https://developer.github.com/v3/repos/releases/#get-a-single-release
                string response = client.DownloadString("https://api.github.com/repos/neimex23/AutoSplitterCore/releases");

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
                if (!SplitterControl.GetControl().GetDebug())
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
                AddAuthorization();
                response = client.DownloadString("https://api.github.com/repos/FrankvdStam/SoulSplitter/releases");
                auxReleases = response.FromJson<List<Dictionary<string, object>>>();
                foreach (var aux in auxReleases)
                {
                    if (aux.TryGetValue("tag_name", out var tagValue) && tagValue is string ver)
                    {
                        Version.TryParse(ver, out Version outVer);
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
            catch (Exception ex) { DebugLog.LogMessage("Error on UpdateModule: " + ex.Message); }
            ;

            //Debug Propouses
            //new UpdateShowDialog().ShowDialog();
            //new UpdateShowDialogSouls().ShowDialog();

            if (CheckUpdatesOnStartup)
            {
                if (Releases.Count > 0 && dll != null && Releases[0] > dll.GetName().Version)
                {
                    update = true;
                    Form aux = new UpdateShowDialog();
                    aux.ShowDialog();
                }

                if (SoulsMemoryRelease.Count > 0 && SoulDll != null && cloudSoulsVer != SoulDll.GetName().Version.ToString())
                {
                    update = true;
                    Form aux2 = new UpdateShowDialogSouls();
                    aux2.ShowDialog();
                }

                if (!update && ForceUpdate) MessageBox.Show("You have the latest Version", "Last Version", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}

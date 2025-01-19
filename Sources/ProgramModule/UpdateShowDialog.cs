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

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace AutoSplitterCore
{
    public partial class UpdateShowDialog : Form
    {
        private UpdateModule updateModule;
        public UpdateShowDialog(UpdateModule updateModule)
        {
            InitializeComponent();
            this.updateModule = updateModule;
        }

        private void UpdateShowDialog_Load(object sender, EventArgs e)
        {
            LabelVersion.Text = updateModule.currentVer;
            labelCloudVer.Text = updateModule.cloudVer;
            groupBoxInstallerSelect.Hide();
            groupBoxUpdating.Hide();
        }

        private void btnGoToDownloadPage_Click(object sender, EventArgs e)
        {
            AutoSplitterMainModule.OpenWithBrowser(new Uri("https://github.com/neimex23/HitCounterManager/releases/latest"));
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            groupBoxInstallerSelect.Show();
            groupBoxUpdate.Hide();
        }

        private void btnInstaller_Click(object sender, EventArgs e)
        {
            groupBoxUpdating.Show();
            groupBoxInstallerSelect.Hide();
            Refresh();
            downloadInstall(1);
        }

        private void btnPortable_Click(object sender, EventArgs e)
        {
            groupBoxUpdating.Show();
            groupBoxInstallerSelect.Hide();
            Refresh();
            downloadInstall(2);
        }

        private void downloadInstall (int method)
        {
            WebClient webClient = new WebClient();
            string ver = updateModule.cloudVerNotDot;
            string url = "https://github.com/neimex23/HitCounterManager/releases/download/ASC_v" + ver + "/AutoSplitterCore_";
            string to = string.Empty;
            string extractPath = Directory.GetCurrentDirectory() + "/Update";

            if (!Directory.Exists(extractPath)) Directory.CreateDirectory(extractPath);
            DirectoryInfo directory = new DirectoryInfo(extractPath);
            foreach (FileInfo file in directory.GetFiles()) file.Delete();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.GetFullPath("UpdateScriptASC.bat");
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            progressBarUpdating.Increment(30);

            switch (method)
            {
                case 1:
                    url += "Installer_v" + ver + ".msi";
                    to = "UpdateASCInstaller.msi";
                    progressBarUpdating.Increment(20);
                    webClient.DownloadFile(url, to);
                    progressBarUpdating.Increment(50);
                    MessageBox.Show("Download Successfully \n The Program will close to Install Update", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    startInfo.Arguments = "1";
                    Process.Start(startInfo);
                    break;
                case 2:
                    url += "Portable_v" + ver + ".zip";
                    to = "UpdateASCPortable.zip";
                    progressBarUpdating.Increment(20);
                    webClient.DownloadFile(url, to);
                    ZipFile.ExtractToDirectory(to, extractPath);
                    File.Delete(to);
                    progressBarUpdating.Increment(50);
                    MessageBox.Show("Download Successfully \n The Program will close to Install Update", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
 
                    startInfo.Arguments = "2";
                    Process.Start(startInfo);
                    break;
                default: break;
            }
        }

    }
}

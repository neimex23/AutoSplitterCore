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
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.IO.Compression;
using System.Diagnostics;

namespace AutoSplitterCore
{
    public partial class UpdateShowDialogSouls : Form
    {
        private UpdateModule updateModule;
        public UpdateShowDialogSouls(UpdateModule updateModule)
        {
            InitializeComponent();
            this.updateModule = updateModule;
        }

        private void btnGoToDownloadPage_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/FrankvdStam/SoulSplitter/releases/latest");
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateShowDialogSouls_Load(object sender, EventArgs e)
        {
            LabelVersion.Text = updateModule.currentSoulsVer;
            labelCloudVer.Text = updateModule.cloudSoulsVer;
            groupBoxUpdating.Hide();
        }


        private void btnDownload_Click(object sender, EventArgs e)
        {
            groupBoxUpdate.Hide();
            groupBoxUpdating.Show();
            Refresh();
            WebClient webClient = new WebClient();
            string ver = updateModule.cloudSoulsVerNotDot;
            string url = "https://github.com/FrankvdStam/SoulSplitter/releases/download/" + ver + "/"+ ver + ".zip";
            string to = Path.GetFullPath("SoulMemory.dll").ToString() + ".zip";
            string extractPath = Directory.GetCurrentDirectory() + "/Update";
            progressBarUpdating.Increment(20);
            webClient.DownloadFile(url, to);
            
            if (!Directory.Exists(extractPath)) Directory.CreateDirectory(extractPath);
            DirectoryInfo directory = new DirectoryInfo(extractPath);
            foreach (FileInfo file in directory.GetFiles()) file.Delete();

            progressBarUpdating.Increment(30);
            ZipFile.ExtractToDirectory(to, extractPath);
            File.Delete(to);
            progressBarUpdating.Increment(50);
            MessageBox.Show("Download Successfully \n The Program will close to Install Update", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.GetFullPath("UpdateScriptASC.bat");
            startInfo.Arguments = "3";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;

            Process.Start(startInfo);

            Close();
        }
    }
}

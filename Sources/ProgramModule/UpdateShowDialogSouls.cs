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
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.IO.Compression;
using System.Diagnostics;
using ReaLTaiizor.Forms;
using System.Threading.Tasks;

namespace AutoSplitterCore
{
    public partial class UpdateShowDialogSouls : MaterialForm
    {
        private UpdateModule updateModule;

        public UpdateShowDialogSouls(UpdateModule updateModule)
        {
            InitializeComponent();
            this.updateModule = updateModule;
        }

        private void UpdateShowDialogSouls_Load(object sender, EventArgs e)
        {
            LabelVersion.Text = updateModule.currentSoulsVer;
            labelCloudVer.Text = updateModule.cloudSoulsVer;
            groupBoxUpdating.Hide();
        }

        private void btnGoToDownloadPage_Click(object sender, EventArgs e)
        {
            AutoSplitterMainModule.OpenWithBrowser(new Uri("https://github.com/FrankvdStam/SoulSplitter/releases/latest"));
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            groupBoxUpdate.Hide();
            groupBoxUpdating.Show();
            Refresh();

            await DownloadAndUpdateAsync();
        }

        private async Task DownloadAndUpdateAsync()
        {
            string ver = updateModule.cloudSoulsVerNotDot;
            string url = $"https://github.com/FrankvdStam/SoulSplitter/releases/download/{ver}/{ver}.zip";
            string zipFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SoulMemory.zip");
            string extractPath = Path.Combine(Directory.GetCurrentDirectory(), "Update");

            progressBarUpdating.Value = 0;

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    progressBarUpdating.PerformStep(); // 20%
                    await webClient.DownloadFileTaskAsync(new Uri(url), zipFilePath);
                }

                if (!Directory.Exists(extractPath))
                    Directory.CreateDirectory(extractPath);

                foreach (var file in Directory.GetFiles(extractPath))
                    File.Delete(file);

                progressBarUpdating.PerformStep(); // 50%

                ZipFile.ExtractToDirectory(zipFilePath, extractPath);
                File.Delete(zipFilePath);

                progressBarUpdating.PerformStep(); // 100%

                MessageBox.Show("Download Successful. The program will close to install the update.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = Path.GetFullPath("UpdateScriptASC.bat"),
                    Arguments = "3",
                    WindowStyle = ProcessWindowStyle.Normal
                };

                Process.Start(startInfo);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


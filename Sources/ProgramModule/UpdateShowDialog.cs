using ReaLTaiizor.Forms;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSplitterCore
{
    public partial class UpdateShowDialog : MaterialForm
    {
        private UpdateModule updateModule = UpdateModule.GetIntance();

        public UpdateShowDialog()
        {
            InitializeComponent();
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
            AutoSplitterMainModule.OpenWithBrowser(new Uri("https://github.com/neimex23/AutoSplitterCore/releases/latest"));
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            groupBoxInstallerSelect.Show();
            groupBoxUpdate.Hide();
        }

        private async void btnInstaller_Click(object sender, EventArgs e)
        {
            groupBoxUpdating.Show();
            groupBoxInstallerSelect.Hide();
            await DownloadAndInstallAsync(1);
        }

        private async void btnPortable_Click(object sender, EventArgs e)
        {
            groupBoxUpdating.Show();
            groupBoxInstallerSelect.Hide();
            await DownloadAndInstallAsync(2);
        }

        private async Task DownloadAndInstallAsync(int method)
        {
            string ver = updateModule.cloudVerNotDot;
            string url = $"https://github.com/neimex23/AutoSplitterCore/releases/download/ASC_v{ver}/AutoSplitterCore_";
            string extractPath = Path.Combine(Directory.GetCurrentDirectory(), "Update");

            if (!Directory.Exists(extractPath)) Directory.CreateDirectory(extractPath);

            foreach (var file in Directory.GetFiles(extractPath))
            {
                File.Delete(file);
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "UpdateScriptASC.bat",
                WindowStyle = ProcessWindowStyle.Normal
            };

            progressBarUpdating.Value = 0;
            progressBarUpdating.Step = 50;
            progressBarUpdating.PerformStep();

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    switch (method)
                    {
                        case 1:
                            url += $"Installer_v{ver}.msi";
                            await webClient.DownloadFileTaskAsync(new Uri(url), "UpdateASCInstaller.msi");
                            break;
                        case 2:
                            url += $"Portable_v{ver}.zip";
                            string zipPath = "UpdateASCPortable.zip";
                            await webClient.DownloadFileTaskAsync(new Uri(url), zipPath);
                            ZipFile.ExtractToDirectory(zipPath, extractPath);
                            File.Delete(zipPath);
                            break;
                        default:
                            throw new ArgumentException("Invalid update method.");
                    }
                }

                progressBarUpdating.PerformStep();

                MessageBox.Show("Download successful. The program will close to install the update.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                startInfo.Arguments = method.ToString();
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

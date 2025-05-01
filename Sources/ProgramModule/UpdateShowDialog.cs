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
            using (var stream = new System.IO.MemoryStream(Properties.Resources.AutoSplitterSetup))
            {
                this.Icon = new System.Drawing.Icon(stream);
            }
            LabelVersion.Text = updateModule.currentVer;
            labelCloudVer.Text = updateModule.cloudVer;
            groupBoxInstallerSelect.Hide();
            groupBoxUpdating.Hide();
            groupBoxHCMversion.Hide();
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

        private void btnPortable_Click(object sender, EventArgs e)
        {
            groupBoxInstallerSelect.Hide();
            groupBoxHCMversion.Show();
        }

        private async void buttonHCM_Click(object sender, EventArgs e)
        {
            groupBoxUpdating.Show();
            groupBoxInstallerSelect.Hide();

            ReaLTaiizor.Controls.Button senderButton = (ReaLTaiizor.Controls.Button)sender;

            int method = ((ReaLTaiizor.Controls.Button)sender).Text == "v1.x" ? 2 : 3;
            await DownloadAndInstallAsync(method);
        }

        private async Task DownloadAndInstallAsync(int method)
        {
            string ver = updateModule.cloudVerNotDot;
            string url = $"https://github.com/neimex23/AutoSplitterCore/releases/download/ASC_v{ver}/AutoSplitterCore_";
            string extractPath = Path.Combine(Directory.GetCurrentDirectory(), "Update");

            if (Directory.Exists(extractPath)) Directory.Delete(extractPath, true);
            Directory.CreateDirectory(extractPath);

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
                            url += $"Installer_v{ver}.exe";
                            await webClient.DownloadFileTaskAsync(new Uri(url), "UpdateASCInstaller.exe");
                            break;
                        case 2:
                            url += $"Portable_v{ver}.zip";
                            break;
                        case 3:
                            url += $"Portable_HCMv2_v{ver}.zip";
                            method = 2; // UpdateASC Script not change
                            break;
                        default:
                            throw new ArgumentException("Invalid update method.");
                    }

                    if (method == 2)
                    {
                        string zipPath = "UpdateASCPortable.zip";
                        await webClient.DownloadFileTaskAsync(new Uri(url), zipPath);
                        ZipFile.ExtractToDirectory(zipPath, extractPath);
                        File.Delete(zipPath);
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

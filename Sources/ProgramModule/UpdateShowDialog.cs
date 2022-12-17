using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void btnGoToDownloadPage_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/neimex23/HitCounterManager/releases/latest");
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

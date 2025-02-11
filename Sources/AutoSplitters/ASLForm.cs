using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSplitterCore.Sources.AutoSplitters
{
    public partial class ASLForm : ReaLTaiizor.Forms.MaterialForm
    {
        ASLSplitter splitter = new ASLSplitter();
        public ASLForm()
        {
            InitializeComponent();
        }

        private void ASLForm_Load(object sender, EventArgs e)
        {
            tabPageASLConfig.Controls.Add(splitter.Controls);
        }
    }
}

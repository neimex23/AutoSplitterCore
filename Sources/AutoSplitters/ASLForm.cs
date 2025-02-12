//MIT License

//Copyright (c) 2022-2025 Ezequiel Medina

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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSplitterCore
{
    public partial class ASLForm : ReaLTaiizor.Forms.MaterialForm
    {
        ASLSplitter aslSplitter = ASLSplitter.GetInstance();
        SaveModule SaveModule =null;

        public ASLForm(SaveModule savemodule)
        {
            InitializeComponent();
            this.SaveModule = savemodule;
        }

        private void ASLForm_Load(object sender, EventArgs e)
        {
            Control controlObteined = aslSplitter.AslControl;
            lostBorderPanelASLConfig.Margin = new Padding(5);
            lostBorderPanelASLConfig.Padding = new Padding(5);

            lostBorderPanelASLConfig.Controls.Add(controlObteined);

            metroCheckBoxIGT.Checked = SaveModule.generalAS.ASLIgt;
        }

        private void metroCheckBoxIGT_CheckedChanged(object sender)
        {
            aslSplitter.IGTEnable = metroCheckBoxIGT.Checked;
            SaveModule.generalAS.ASLIgt = metroCheckBoxIGT.Checked;

            if (SplitterControl.GetControl().GetDebug()) DebugLog.LogMessage($"Trace ASL: IGT ASL Changed to: {SaveModule.generalAS.ASLIgt}");
        }
    }
}

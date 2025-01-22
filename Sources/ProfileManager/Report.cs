using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
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
    public partial class Report : ReaLTaiizor.Forms.MaterialForm
    {
        private string EmailLoged;
        private SheetsService sheetService;

        string idFile = string.Empty;
        string nameFile = string.Empty;

        static string spreadsheetId = "1syCG3vchr4rHu-_9vQAS6UCJaoonZ06imyPzWNCRtRU";

        public Report(string EmailLoged, SheetsService sheetService, string idFile, string nameFile)
        {
            InitializeComponent();
            this.EmailLoged = EmailLoged; this.sheetService = sheetService;
            this.idFile = idFile; this.nameFile = nameFile;
            labelEmail.Text = EmailLoged;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show($"Do you want Report the profile: {nameFile} - ID: {idFile}\nSubmitting incorrect or false reports may result in sanctions. Do you Continue?","Continue?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string rason = aloneComboBoxRason.Text;
                string detail = textBoxDetail.Text;

                if (string.IsNullOrEmpty(rason) || string.IsNullOrEmpty(detail))
                {
                    MessageBox.Show("Please fill in all the fields before submitting the report.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    IList<IList<object>> values = new List<IList<object>>
                    {
                        new List<object> { EmailLoged, idFile, nameFile, rason, detail, DateTime.Now.ToString("dd/MM/yyyy") }
                    };

                    string range = "Sheet1!G2:L2"; 

                    var valueRange = new ValueRange
                    {
                        Values = values
                    };

                    var appendRequest = sheetService.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
                    appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

                    var appendResponse = appendRequest.Execute();

                    MessageBox.Show("The report has been submitted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while submitting the report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

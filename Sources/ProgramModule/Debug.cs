using HitCounterManager;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSplitterCore
{
    public interface IDebugLogger
    {
        /// <summary>
        /// Set a Log Message on ListBOXLog on debug mode
        /// </summary>
        /// <param name="message"></param>
        void LogMessage(string message);
    }

    public partial class Debug : Form, IDebugLogger
    {
        #region SingletonFactory
        private static IDebugLogger _logger;
        private static readonly object _lock = new object ();

        public static void RegisterDebugInterfaces(Debug debugForm)
        {
            _logger = debugForm;
        }

        public static IDebugLogger GetDebugInterface() => _logger;

        #endregion


        private readonly AutoSplitterMainModule mainModule;
        private int gameActive;
        private readonly SynchronizationContext syncContext;

        public Debug(AutoSplitterMainModule mainModule)
        {
            InitializeComponent();
            this.mainModule = mainModule;
            this.syncContext = SynchronizationContext.Current;

            mainModule.InitDebug();
            mainModule.RegisterHitCounterManagerInterface(new AutoSplitterCoreInterface(new HitCounterManager.Form1()));

            RegisterDebugInterfaces(this);

            var updateTimer = new System.Windows.Forms.Timer { Interval = 100 };
            updateTimer.Tick += (sender, args) => CheckInfo();
            updateTimer.Start();
        }

        private void Debug_Load(object sender, EventArgs e)
        {
            comboBoxIGTConversion.SelectedIndex = 1;
            checkBoxPracticeMode.Checked = mainModule.GetPracticeMode();

            var gameList = mainModule.GetGames();
            comboBoxGame.Items.AddRange(gameList.ToArray());
            comboBoxGame.SelectedIndex = mainModule.GetSplitterEnable();

            LabelVersion.Text = mainModule.updateModule.currentVer;
            labelCloudVer.Text = mainModule.updateModule.cloudVer;

            listBoxLog.Items.Clear();
        }

        private void comboBoxGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            gameActive = comboBoxGame.SelectedIndex;
            mainModule.EnableSplitting(gameActive);
            mainModule.igtModule.gameSelect = gameActive;
            LogMessage($"CheckFlags is changed by: {comboBoxGame.Text}");
        }

        public void UpdateBoxes()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateBoxes));
                return;
            }

            comboBoxGame.SelectedIndex = mainModule.GetSplitterEnable();
            checkBoxPracticeMode.Checked = mainModule.GetPracticeMode();
        }

        private void CheckInfo()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(CheckInfo));
                return;
            }

            ClearTextBoxes();

            bool status = false;
            int conversionFactor = 1;

            switch (comboBoxIGTConversion.SelectedIndex)
            {
                case 0: conversionFactor = 1; break;
                case 1: conversionFactor = 1000; break;
                case 2: conversionFactor = 60000; break;
            }

            status = UpdateGameSpecificInfo();
            textBoxIGT.Text = (mainModule.ReturnCurrentIGT() / conversionFactor).ToString();

            if (status)
            {
                Running.Show();
                NotRunning.Hide();
            }
            else
            {
                NotRunning.Show();
                Running.Hide();
            }
        }

        private bool UpdateGameSpecificInfo()
        {
            var status = false;
            switch (gameActive)
            {
                case GameConstruction.SekiroSplitterIndex:
                    var sekiroPosition = mainModule.sekiroSplitter.GetCurrentPosition();
                    Vector3 sekiroFormat = new Vector3(sekiroPosition.X, sekiroPosition.Y, sekiroPosition.Z);
                    UpdatePositionTextBoxes(sekiroFormat);
                    status = mainModule.sekiroSplitter._StatusSekiro;
                    break;

                case GameConstruction.Ds1SplitterIndex:
                    var ds1Position = mainModule.ds1Splitter.GetCurrentPosition();
                    Vector3 ds1Format = new Vector3(ds1Position.X, ds1Position.Y, ds1Position.Z);
                    UpdatePositionTextBoxes(ds1Format);
                    status = mainModule.ds1Splitter._StatusDs1;
                    break;

                case GameConstruction.Ds2SplitterIndex:
                    var ds2Position = mainModule.ds2Splitter.GetCurrentPosition();
                    Vector3 ds2Format = new Vector3(ds2Position.X, ds2Position.Y, ds2Position.Z);
                    UpdatePositionTextBoxes(ds2Format);
                    status = mainModule.ds2Splitter._StatusDs2;
                    break;

                case GameConstruction.Ds3SplitterIndex:
                    var ds3Position = mainModule.ds3Splitter.GetCurrentPosition();
                    Vector3 ds3Format = new Vector3(ds3Position.X, ds3Position.Y, ds3Position.Z);
                    UpdatePositionTextBoxes(ds3Format);
                    status = mainModule.ds3Splitter._StatusDs3;
                    break;

                case GameConstruction.EldenSplitterIndex:
                    var eldenPosition = mainModule.eldenSplitter.GetCurrentPosition();
                    Vector3 eldenFormat = new Vector3(eldenPosition.X, eldenPosition.Y, eldenPosition.Z);
                    UpdatePositionTextBoxes(eldenFormat);
                    status = mainModule.eldenSplitter._StatusElden;
                    break;

                case GameConstruction.HollowSplitterIndex:
                    var hollowPosition = mainModule.hollowSplitter.GetCurrentPosition();
                    Vector3 hollowFormat = new Vector3(hollowPosition.X, hollowPosition.Y, 0);
                    UpdatePositionTextBoxes(hollowFormat);
                    this.textBoxSceneName.Paste(mainModule.hollowSplitter.currentPosition.sceneName.ToString());
                    status = mainModule.hollowSplitter._StatusHollow;
                    break;

                case GameConstruction.CelesteSplitterIndex:
                    this.textBoxSceneName.Paste(mainModule.celesteSplitter.GetLevelName());
                    status = mainModule.celesteSplitter._StatusCeleste;
                    break;

                case GameConstruction.CupheadSplitterIndex:
                    this.textBoxSceneName.Paste(mainModule.cupSplitter.GetSceneName());
                    status = mainModule.cupSplitter._StatusCuphead;
                    break;

                case GameConstruction.DishonoredSplitterIndex:
                    status = mainModule.dishonoredSplitter._StatusDish;
                    break;

            }
            return status;
        }

        private void ClearTextBoxes()
        {
            textBoxX.Clear();
            textBoxY.Clear();
            textBoxZ.Clear();
            textBoxSceneName.Clear();
            textBoxIGT.Clear();
        }

        private void UpdatePositionTextBoxes(Vector3 position)
        {
            textBoxX.Text = position.X.ToString("0.00");
            textBoxY.Text = position.Y.ToString("0.00");
            textBoxZ.Text = position.Z.ToString("0.00");
        }

        public void LogMessage(string message)
        {
            if (listBoxLog.InvokeRequired)
            {
                listBoxLog.Invoke(new Action<string>(LogMessage), message);
                return;
            }

            listBoxLog.Items.Add(message);
            if (listBoxLog.Items.Count > 1)
            {
                listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
            }
        }

        private void btnRefreshGame_Click(object sender, EventArgs e)
        {
            switch (gameActive)
            {
                case GameConstruction.SekiroSplitterIndex:
                    mainModule.sekiroSplitter.GetSekiroStatusProcess(0);
                    break;
                case GameConstruction.Ds1SplitterIndex:
                    mainModule.ds1Splitter.GetDs1StatusProcess(0);
                    break;
                case GameConstruction.Ds2SplitterIndex:
                    mainModule.ds2Splitter.GetDs2StatusProcess(0);
                    break;
                case GameConstruction.Ds3SplitterIndex:
                    mainModule.ds3Splitter.GetDs3StatusProcess(0);
                    break;
                case GameConstruction.EldenSplitterIndex:
                    mainModule.eldenSplitter.GetEldenStatusProcess(0);
                    break;
                case GameConstruction.HollowSplitterIndex:
                    mainModule.hollowSplitter.GetHollowStatusProcess(0);
                    break;
                case GameConstruction.CelesteSplitterIndex:
                    mainModule.celesteSplitter.GetCelesteStatusProcess(0);
                    break;
                case GameConstruction.CupheadSplitterIndex:
                    mainModule.cupSplitter.GetCupheadStatusProcess(0);
                    break;
                case GameConstruction.DishonoredSplitterIndex:
                    mainModule.dishonoredSplitter.GetDishonoredStatusProcess();
                    break;
                case GameConstruction.NoneSplitterIndex:
                default: break;
            }
            LogMessage("Refreshed Process");
        }

        private void btnResetFlags_Click(object sender, EventArgs e)
        {
            mainModule.ResetSplitterFlags();
            LogMessage($"Reset Flags of: {comboBoxGame.Text}");
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            mainModule.SaveAutoSplitterSettings();
            MessageBox.Show("Save Successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBoxPracticeMode_CheckedChanged(object sender, EventArgs e)
        {
            mainModule.SetPracticeMode(checkBoxPracticeMode.Checked);
        }

        private void btnSplitter_Click(object sender, EventArgs e)
        {
            mainModule.AutoSplitterForm(false);
            UpdateBoxes();
        }

    }
}

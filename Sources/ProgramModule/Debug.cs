using HitCounterManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;

namespace AutoSplitterCore
{
    public partial class Debug : Form
    {
        private readonly AutoSplitterMainModule mainModule;
        private int gameActive;

        public Debug(AutoSplitterMainModule mainModule)
        {
            InitializeComponent();
            this.mainModule = mainModule;

            DebugLog.SetLogger(this);
            DebugLog.LogMessage("Log System Started successfully");

            ListenerASL.Initialize();

            mainModule.InitDebug();
#if !HCMv2
            mainModule.RegisterHitCounterManagerInterface(new AutoSplitterCoreInterface(new HitCounterManager.Form1()));
#endif

            var updateTimer = new System.Windows.Forms.Timer { Interval = 100 };
            updateTimer.Tick += (sender, args) => CheckInfo();
            updateTimer.Start();

            InitializeLogListView();
        }

        private void InitializeLogListView()
        {
            listViewLog.View = View.Details;
            listViewLog.FullRowSelect = true;
            listViewLog.GridLines = true;
            listViewLog.Columns.Add("Timestamp", 150);
            listViewLog.Columns.Add("Message", 750);

            listViewLog.VirtualMode = true;
            listViewLog.RetrieveVirtualItem += ListViewLog_RetrieveVirtualItem;

            listViewLog.MouseDoubleClick += ListViewLog_MouseDoubleClick;
        }

        private void Debug_Load(object sender, EventArgs e)
        {
            using (var stream = new System.IO.MemoryStream(Properties.Resources.AutoSplitterSetup))
            {
                this.Icon = new System.Drawing.Icon(stream);
            }
            checkBoxPracticeMode.Checked = AutoSplitterMainModule.GetPracticeMode();

            var gameList = mainModule.GetGames();
            comboBoxGame.Items.AddRange(gameList.ToArray());
            comboBoxGame.SelectedIndex = AutoSplitterMainModule.GetSplitterEnable();

            LabelVersion.Text = AutoSplitterMainModule.updateModule.currentVer;
            labelCloudVer.Text = AutoSplitterMainModule.updateModule.cloudVer;

            listViewLog.Items.Clear();

            var assembly = Assembly.GetExecutingAssembly();

            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                DebugLog.LogMessage($"Resources embedded in the assembly: {resourceName}");
            }
        }

        private void comboBoxGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            gameActive = comboBoxGame.SelectedIndex;
            mainModule.EnableSplitting(gameActive);
            AutoSplitterMainModule.igtModule.gameSelect = gameActive;
            LogMessage($"CheckFlags is changed by: {comboBoxGame.Text}");
        }

        public void UpdateBoxes()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateBoxes));
                return;
            }

            comboBoxGame.SelectedIndex = AutoSplitterMainModule.GetSplitterEnable();
            checkBoxPracticeMode.Checked = AutoSplitterMainModule.GetPracticeMode();
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
            status = UpdateGameSpecificInfo();

            TimeSpan tiempo = TimeSpan.FromMilliseconds(mainModule.ReturnCurrentIGT());
            textBoxIGT.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", tiempo.Hours, tiempo.Minutes, tiempo.Seconds);

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
            try
            {
                switch (gameActive)
                {
                    case (int)GameConstruction.Game.Sekiro:
                        var sekiroPosition = SekiroSplitter.GetIntance().GetCurrentPosition();
                        Vector3 sekiroFormat = new Vector3(sekiroPosition.X, sekiroPosition.Y, sekiroPosition.Z);
                        UpdatePositionTextBoxes(sekiroFormat);
                        status = SekiroSplitter.GetIntance()._StatusSekiro;
                        break;

                    case (int)GameConstruction.Game.DarkSouls1:
                        var ds1Position = Ds1Splitter.GetIntance().GetCurrentPosition();
                        Vector3 ds1Format = new Vector3(ds1Position.X, ds1Position.Y, ds1Position.Z);
                        UpdatePositionTextBoxes(ds1Format);
                        status = Ds1Splitter.GetIntance()._StatusDs1;
                        break;

                    case (int)GameConstruction.Game.DarkSouls2:
                        var ds2Position = Ds2Splitter.GetIntance().GetCurrentPosition();
                        Vector3 ds2Format = new Vector3(ds2Position.X, ds2Position.Y, ds2Position.Z);
                        UpdatePositionTextBoxes(ds2Format);
                        status = Ds2Splitter.GetIntance()._StatusDs2;
                        break;

                    case (int)GameConstruction.Game.DarkSouls3:
                        var ds3Position = Ds3Splitter.GetIntance().GetCurrentPosition();
                        Vector3 ds3Format = new Vector3(ds3Position.X, ds3Position.Y, ds3Position.Z);
                        UpdatePositionTextBoxes(ds3Format);
                        status = Ds3Splitter.GetIntance()._StatusDs3;
                        break;

                    case (int)GameConstruction.Game.EldenRing:
                        var eldenPosition = EldenSplitter.GetIntance().GetCurrentPosition();
                        Vector3 eldenFormat = new Vector3(eldenPosition.X, eldenPosition.Y, eldenPosition.Z);
                        UpdatePositionTextBoxes(eldenFormat);
                        status = EldenSplitter.GetIntance()._StatusElden;
                        break;

                    case (int)GameConstruction.Game.HollowKnight:
                        var hollowPosition = HollowSplitter.GetIntance().GetCurrentPosition();
                        Vector3 hollowFormat = new Vector3(hollowPosition.X, hollowPosition.Y, 0);
                        UpdatePositionTextBoxes(hollowFormat);
                        this.textBoxSceneName.Paste(HollowSplitter.GetIntance().currentPosition.sceneName.ToString());
                        status = HollowSplitter.GetIntance()._StatusHollow;
                        break;

                    case (int)GameConstruction.Game.Celeste:
                        this.textBoxSceneName.Paste(CelesteSplitter.GetIntance().GetLevelName());
                        status = CelesteSplitter.GetIntance()._StatusCeleste;
                        break;

                    case (int)GameConstruction.Game.Cuphead:
                        this.textBoxSceneName.Paste(CupheadSplitter.GetIntance().GetSceneName());
                        status = CupheadSplitter.GetIntance()._StatusCuphead;
                        break;

                    case (int)GameConstruction.Game.Dishonored:
                        status = DishonoredSplitter.GetIntance()._StatusDish;
                        break;

                    case (int)GameConstruction.Game.ASLMethod:
                        status = ASLSplitter.GetInstance().GetStatusGame();
                        break;

                }
            }
            catch (Exception ex) { LogMessage($"Exception Produce on CheckBoxes: {ex.Message}"); }
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

        private List<LogEntry> logEntries = new List<LogEntry>();


        public void LogMessage(string message)
        {
            if (listViewLog.InvokeRequired)
            {
                listViewLog.Invoke(new Action<string>(LogMessage), message);
                return;
            }

            logEntries.Add(new LogEntry
            {
                Timestamp = DateTime.Now,
                Message = message
            });

            listViewLog.VirtualListSize = logEntries.Count;
            listViewLog.Invalidate();
        }

        private void ListViewLog_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex >= 0 && e.ItemIndex < logEntries.Count)
            {
                var log = logEntries[e.ItemIndex];
                var item = new ListViewItem(log.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"))
                {
                    SubItems = { log.Message }
                };

                if (e.ItemIndex % 2 == 0)
                {
                    item.BackColor = Color.LightGray;
                }

                e.Item = item;
            }
        }

        private void ListViewLog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hit = listViewLog.HitTest(e.Location);
            if (hit.Item != null)
            {
                int index = hit.Item.Index;
                if (index >= 0 && index < logEntries.Count)
                {
                    MessageBox.Show(logEntries[index].Message, "FullLog", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e) => listViewLog.Clear();


        private readonly Dictionary<int, Func<bool>> splitterRefreshProcess = new Dictionary<int, Func<bool>>
        {
            { (int)GameConstruction.Game.Sekiro, () => SekiroSplitter.GetIntance().GetSekiroStatusProcess(0) },
            { (int)GameConstruction.Game.DarkSouls1, () => Ds1Splitter.GetIntance().GetDs1StatusProcess(0) },
            { (int)GameConstruction.Game.DarkSouls2, () => Ds2Splitter.GetIntance().GetDs2StatusProcess(0) },
            { (int)GameConstruction.Game.DarkSouls3, () => Ds3Splitter.GetIntance().GetDs3StatusProcess(0) },
            { (int)GameConstruction.Game.EldenRing, () => EldenSplitter.GetIntance().GetEldenStatusProcess(0) },
            { (int)GameConstruction.Game.HollowKnight, () => HollowSplitter.GetIntance().GetHollowStatusProcess(0) },
            { (int)GameConstruction.Game.Celeste, () => CelesteSplitter.GetIntance().GetCelesteStatusProcess(0) },
            { (int)GameConstruction.Game.Dishonored, () => DishonoredSplitter.GetIntance().GetDishonoredStatusProcess() },
            { (int)GameConstruction.Game.Cuphead, () => CupheadSplitter.GetIntance().GetCupheadStatusProcess(0) }
        };

        private void btnRefreshGame_Click(object sender, EventArgs e)
        {
            if (splitterRefreshProcess.TryGetValue(gameActive, out var action))
            {
                action.Invoke();
            }
            DebugLog.LogMessage("Refreshed Process");
        }

        private void btnResetFlags_Click(object sender, EventArgs e)
        {
            AutoSplitterMainModule.ResetSplitterFlags();
            DebugLog.LogMessage($"Reset Flags of: {comboBoxGame.Text}");
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            AutoSplitterMainModule.SaveAutoSplitterSettings();
            MessageBox.Show("Save Successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBoxPracticeMode_CheckedChanged(object sender, EventArgs e)
        {
            mainModule.SetPracticeMode(checkBoxPracticeMode.Checked);
            DebugLog.LogMessage($"Practice Mode change: {checkBoxPracticeMode.Checked}");
        }

        private void btnSplitter_Click(object sender, EventArgs e)
        {
            mainModule.AutoSplitterForm(false);
            UpdateBoxes();
        }


    }

    public class LogEntry
    {
        public DateTime Timestamp { get; set; }

        public Exception Exception { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            if (Exception != null)
            {
                return $"{Timestamp:yyyy-MM-dd HH:mm:ss} - [ERROR] {Exception.GetType().Name}: {Message} // {Exception.StackTrace}";
            }
            return $"{Timestamp:yyyy-MM-dd HH:mm:ss} - {Message}";
        }
    }

    public static class DebugLog
    {
        private static Debug _logger;
        public static List<LogEntry> logEntries = new List<LogEntry>();
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly string LogFilePath = Path.Combine(LogDirectory, "asc_log.txt");
        private const long MaxLogFileSizeBytes = 5 * 1024 * 1024; // 5 MB
        private static StreamWriter _streamWriter;

        public static void Initialize()
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }

                if (File.Exists(LogFilePath))
                {
                    var fileInfo = new FileInfo(LogFilePath);
                    if (fileInfo.Length > MaxLogFileSizeBytes)
                    {
                        File.Delete(LogFilePath);
                    }
                }

                _streamWriter = new StreamWriter(LogFilePath, append: true) { AutoFlush = true };
                string hcmVersion = "HCMv1";
#if HCMv2
                hcmVersion = "HCMv2";
#endif
                var separator = $"\n==================== NEW ASC SESSION {hcmVersion} [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ====================\n";
                _streamWriter.WriteLine(separator);
            }
            catch
            {
            }
        }

        public static void SetLogger(Debug logger)
        {
            _logger = logger;
        }


        public static void LogMessage(string message, Exception exception = null)
        {
            _logger?.LogMessage(message);
            Console.WriteLine(message);

            if (AutoSplitterMainModule.saveModule.generalAS.LogFile)
            {
                var entry = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Message = message,
                    Exception = exception
                };

                logEntries.Add(entry);
                WriteToFile(entry.ToString());
            }
        }

        private static void WriteToFile(string message)
        {
            try
            {
                _streamWriter?.WriteLine(message);
            }
            catch
            {
                // Ignora errores de escritura para no interrumpir el programa
            }
        }

        public static void Close()
        {
            try
            {
                _streamWriter?.Close();
                _streamWriter = null;
            }
            catch
            {
            }
        }
    }

}

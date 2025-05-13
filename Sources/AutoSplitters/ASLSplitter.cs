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

using LiveSplit.Model;
using LiveSplit.Model.Comparisons;
using LiveSplit.Options;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using LiveSplit.Web;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AutoSplitterCore
{
    public class ListenerASL : TraceListener
    {
        private static bool Initialized = false;

        public override void Write(string message) => RegisterLog(message);

        public override void WriteLine(string message) => RegisterLog(message);

        private void RegisterLog(string message) => DebugLog.LogMessage($"Trace ASL: {message}");

        public static void Initialize()
        {
            if (ListenerASL.Initialized) return;
            var listener = new ListenerASL();
            listener.Filter = new EventTypeFilter(SourceLevels.All);
            Trace.Listeners.Add(listener);

            Log.Info("Started Successfully");
            ListenerASL.Initialized = true;
        }
    }

    public class ASLSplitter
    {
        public bool PracticeMode { get; set; } = false;

        //Force true to Debug on ASLBridge (Use Start Seccuence to build and Start ASC and ASLBridge)
        public bool HCMv2 { get; set; } = false;

        public LiveSplitState state = null;
        public ASLComponent asl;

        ISplitterControl splitterControl = SplitterControl.GetControl();
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer() { Interval = 100 };

        #region ASLConstructor
        private static ASLSplitter _instance = new ASLSplitter();
        public static ASLSplitter GetInstance() => _instance;

        private ASLSplitter()
        {
#if HCMv2
            HCMv2 = true;
#endif
            if (HCMv2)
                InitializePipeClient();
            else
            {
                Task.Run(() => _ = CompositeGameList.Instance.GetGameNames(false)); //Initialice ASL Games Titles
                state = GeneratorState();
                asl = new ASLComponent(state);
                _timer.Tick += ASCHandlerSetters;

#if !DEBUG
                ListenerASL.Initialize();
#endif
            }
        }

        ~ASLSplitter() => Dispose(); 

        public void Dispose() 
        {
            if (HCMv2)
            {
                _client?.Disconnect();
                _clientIgt?.Disconnect();
                Thread.Sleep(2000); //Wait for Subprocess Closings
                var processes = Process.GetProcessesByName("ASLBridge");
                if (processes.Any())
                {
                    foreach (var proc in processes)
                    {
                        proc.Kill();
                    }
                }
            }
        }


        private NamedPipeClient _client;
        private NamedPipeClientIGT _clientIgt;

        public async Task<bool> InitializePipeClient()
        {
            var exeName = "ASLBridge";
            if (!Process.GetProcessesByName(exeName).Any())
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ASLBridge.exe");

                Process.Start(new ProcessStartInfo
                {
                    FileName = path,
                    Arguments = "--from-client",
                    UseShellExecute = false,
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                });
            }

            _client = new NamedPipeClient();
            _clientIgt = new NamedPipeClientIGT();

            int maxRetrys = 5;
            int actualRetry = 0;
            int intervaleMS = 1000;

            while (actualRetry < maxRetrys)
            {
                try
                {
                    await _client.ConnectAsync();
                    await _clientIgt.ConnectAsync();

                    await _client.SendCommand("ping");
                    DebugLog.LogMessage("Connection to ASLBridge successful.");

                    _client.OnSplit += (s, e) => ASCOnSplit(s, e);
                    _client.OnStart += (s, e) => ASCOnStart(s, e);
                    _client.OnReset += (s, e) => ASCOnReset(s, e);
                    return true;
                }
                catch (Exception ex)
                {
                    DebugLog.LogMessage($"Try {actualRetry + 1}: Could not connect to Pipe. Waiting for retry... {ex.Message}");
                    actualRetry++;
                    await Task.Delay(intervaleMS);
                }
            }

            DebugLog.LogMessage("Could not connect to Pipe after several attempts");
            return false;
        }

        private LiveSplitState GeneratorState()
        {
            Form liveSplitForm = new Form
            {
                Text = "LiveSplit Form",
                Width = 300,
                Height = 200
            };

            var layoutSettings = new LiveSplit.Options.LayoutSettings();
            var layout = new Layout();

            IComparisonGeneratorsFactory comparisonFactory = new StandardComparisonGeneratorsFactory();
            IRun run = new LiveSplit.Model.Run(comparisonFactory)
            {
                GameName = "Cuphead", //For Definitions of Irun but its not used
                CategoryName = "Any%"
            };
            run.AddSegment("Start");
            run.AddSegment("Middle");
            run.AddSegment("End");

            ISettings settings = new Settings();

            var state = new LiveSplitState(run, liveSplitForm, layout, layoutSettings, settings);
            ITimerModel timerModel = new TimerModel() { CurrentState = state };
            timerModel.InitializeGameTime();
            state.RegisterTimerModel(timerModel);

            return state;
        }

        private bool ASCHandlerSetted = false;
        private void ASCHandlerSetters(object sender, EventArgs e)
        {
            if (ASCHandlerSetted)
            {
                _timer.Stop(); return;
            }

            if (asl.Script != null)
            {
                asl.Script.ShouldSplit += ASCOnSplit;
                asl.Script.StartRun += ASCOnStart;
                asl.Script.ResetRun += ASCOnReset;
                ASCHandlerSetted = true;
                Log.Info("Handlers ASL Setted");
            }
        }

        public Control AslControl
        {
            //HCMv2 Controlled on ASLBridge
            get
            {
                return asl != null ? asl.GetSettingsControl(LayoutMode.Vertical) : null;
            }
        }

#endregion
        #region Control Management
        public void setData(XmlNode node)
        {
            if (node != null && asl != null)
            {
                asl.SetSettings(node);
            }
        }

        public XmlNode getData(XmlDocument doc)
        {
            return asl != null ? asl.GetSettings(doc) : null;
        }

        public bool _AslActive { get; private set; } = false;
        public void SetStatusSplitting(bool status)
        {
            _AslActive = status;
            if (!HCMv2 && !ASCHandlerSetted) _timer.Start();
        }

        public async Task OpenForm()
        {
            if (HCMv2 && _client != null)
                if (_client.PipeOnline)
                {
                    await _client.SendCommand("openform");
                }
                else
                {
                    MessageBox.Show("Internal Communication with Piped ASL Bridge is disconnected");
                }
        }

        public async Task SaveASLBridgeSettings() //Load Process on ASLBridge
        {
            if (HCMv2 && _client != null)
                if (_client.PipeOnline)
                    await _client.SendCommand("save");
        }

        #endregion
        #region Checking
        private bool _igtEnable = false;
        public bool setedBridge = false;

        public bool IGTEnable
        {
            get => _igtEnable;
            set
            {
                if (value == _igtEnable) return;

                _igtEnable = value;


                if (HCMv2 && !setedBridge && value)
                {
                    _ = EnableAslBridgeIGT();
                }
            }
        }


        private async Task EnableAslBridgeIGT()
        {
            while (_client == null || !_client.PipeOnline)
                await Task.Delay(1000);
            await _client?.SendCommand("enableigt");
        }


        public bool GetStatusGame()
        {
            if (!HCMv2)
            {
                return asl?.Script?.ProccessAtached() ?? false;
            }
            else
            {
                // Not critical to implement. Multiple concurrent queries were causing issues, 
                // so a similar approach to GetIngameTime should be used with a separate named pipe.
                // Execution should be considered active only if the pipe is connected.
                if (_client != null)
                    return _client.PipeOnline;
                else return false;
            }
        }

        public long GetIngameTime()
        {
            if (!HCMv2)
            {
                var time = (long?)state?.CurrentTime.GameTime?.TotalMilliseconds;
                return time != null && time >= 0 ? (long)time : -1;
            }
            else
            {
                return _clientIgt != null ? _clientIgt.LastIGT : -1;
            }
        }
        #endregion
        #region CheckFlag Init()

        private void ASCOnSplit(object sender, EventArgs e)
        {
            if (!PracticeMode && _AslActive)
                splitterControl.SplitCheck("Trace ASL: SplitFlag Produced on ASL");
        }

        private void ASCOnStart(object sender, EventArgs e)
        {
            if (splitterControl.GetDebug())
            {
                DebugLog.LogMessage("Trace ASL: Start Run Trigger Produced on ASL");
                return; //StartStopTimer and timmer not implemented on debugmode
            }

            if (!PracticeMode && _AslActive && !IGTEnable && !splitterControl.GetTimerRunning())
            {
                splitterControl.StartStopTimer(true);
            }
        }

        private void ASCOnReset(object sender, EventArgs e)
        {
            if (splitterControl.GetDebug())
            {
                DebugLog.LogMessage("Trace ASL: Reset Run Trigger Produced on ASL");
                return; //ProfileReset not implemented on debugmode
            }

            if (!PracticeMode && _AslActive)
                splitterControl.ProfileReset();
        }
        #endregion
    }
}

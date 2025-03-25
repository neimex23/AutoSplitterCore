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

using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSplit;
using LiveSplit.Model;
using System.Windows.Forms;
using LiveSplit.UI;
using LiveSplit.Model.Comparisons;
using LiveSplit.Options;
using System.Diagnostics;
using System.Threading;
using System.Xml;
using LiveSplit.Web;
using System.IO;

namespace AutoSplitterCore 
{ 
    public class ListenerASL : TraceListener
    {
        public override void Write(string message) => RegisterLog(message);

        public override void WriteLine(string message) => RegisterLog(message);

        private void RegisterLog(string message) => DebugLog.LogMessage($"Trace ASL: {message}");

        public static void Initialize()
        {
            var listener = new ListenerASL();
            listener.Filter = new EventTypeFilter(SourceLevels.All);
            Trace.Listeners.Add(listener);

            Log.Info("Started Successfully");
        }
    }

    public class ASLSplitter
    {
        public bool PracticeMode { get; set; } = false;

        public LiveSplitState state = null;
        public ASLComponent asl;

        ISplitterControl splitterControl = SplitterControl.GetControl();
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer() { Interval = 100 };

        #region ASLConstructor
        private static ASLSplitter _instance = new ASLSplitter();
        public static ASLSplitter GetInstance() => _instance;

        private ASLSplitter()
        {
            #if !HCMv2
            Task.Run(() => _ = CompositeGameList.Instance.GetGameNames(false)); //Initialice ASL Games Titles
            state = GeneratorState();
            asl = new ASLComponent(state);
            _timer.Tick += ASCHandlerSetters;
            #endif
        }

        private WebSocketClient _client;
        public async Task InitializeWebSocket()
        {
            var exeName = "ASLBridge"; // sin .exe
            if (!Process.GetProcessesByName(exeName).Any())
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ASLBridge.exe");
                Process.Start(path);
                Console.WriteLine("ASLBridge iniciado.");
                Thread.Sleep(2000); // Espera opcional para que el WebSocket se prepare
            }

            _client = new WebSocketClient();

            _client.OnSplit += (s, e) => ASCOnSplit(s,e);
            _client.OnStart += (s, e) => ASCOnStart(s,e);
            _client.OnReset += (s, e) => ASCOnReset(s,e);

            await _client.ConnectAsync();
        }

        private LiveSplitState GeneratorState() {
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
            ITimerModel timerModel = new TimerModel() { CurrentState = state};
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
            if (node != null) 
            {
#if !HCMv2
                asl.SetSettings(node);
#else
                _client.SendCommand($"set xml:{node}");
#endif
            } 
        }

        public async Task<XmlNode> getData(XmlDocument doc)
        {
#if !HCMv2
                    await Task.Delay(10);
                    return asl.GetSettings(doc);
        #else
                   string xmlString = await _client.SendCommandWithResponse("get xml");
                    XmlDocument responseDoc = new XmlDocument();
                    responseDoc.LoadXml(xmlString);
                    return responseDoc.DocumentElement;
        #endif
        }

        public bool _AslActive { get; private set; } = false;
        public void SetStatusSplitting(bool status)
        {
            _AslActive = status;
            if (!ASCHandlerSetted) _timer.Start();
        }

        public async Task OpenForm()
        {
            await _client.SendCommand("openform");
        }

#endregion
        #region Checking
        public bool IGTEnable { get; set; } = false;

        public async Task<bool> GetStatusGame() {
#if HCMv2
            string status = _client.SendCommandWithResponse("status").GetAwaiter().GetResult();
            return status == "Attached";
#endif
            return asl.Script != null ? asl.Script.ProccessAtached() : false;         
         }

        public async Task<long> GetIngameTime()
        {
#if HCMv2
        
string response = _client.SendCommandWithResponse("igt").GetAwaiter().GetResult();
long time = -1;
long.TryParse(response, out time);
return time;
#endif

            return state != null ? (long)state.CurrentTime.GameTime.Value.TotalMilliseconds : -1;
            #endregion
        }
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

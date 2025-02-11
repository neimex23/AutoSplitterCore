using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSplit;
using LiveSplit.ASL;
using LiveSplit.Model;
using System.Windows.Forms;
using LiveSplit.UI;
using LiveSplit.Model.Comparisons;
using LiveSplit.Options;
using System.Diagnostics;
using System.Threading;
using Google.Type;

namespace AutoSplitterCore 
{ 
    public class ListenerASL : TraceListener
    {
        private IDebugLogger _logger = Debug.GetDebugInterface();
        public override void Write(string message) => RegisterLog(message);

        public override void WriteLine(string message) => RegisterLog(message);

        private void RegisterLog(string message) => _logger.LogMessage($"Trace ASL: {message}");
    }

    public class ASLSplitter
    {
        ASLComponent asl;
        private static ISplitterControl splitterControl = SplitterControl.GetControl();
        public static bool _PracticeMode = false;
        public static bool _ShowSettings = false;

        private System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer() { Interval = 100 };

        public Control Controls { get; private set; }

        public ASLSplitter()
        {
            LiveSplitState state = GeneratorState();
            asl = new ASLComponent(state);
            Controls = asl.GetSettingsControl(LayoutMode.Vertical);

            _timer.Tick += ASCHandlerSetters;
            _timer.Start();



            var listener = new ListenerASL();
            listener.Filter = new EventTypeFilter(SourceLevels.All); 
            Trace.Listeners.Add(listener);
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
            var run = new Run(comparisonFactory)
            {
                GameName = "Example Game",
                CategoryName = "Any%"
            };
            run.AddSegment("Start");
            run.AddSegment("Middle");
            run.AddSegment("End");

            ISettings settings = new Settings();

            var state = new LiveSplitState(run, liveSplitForm, layout, layoutSettings, settings);
            ITimerModel timerModel = new TimerModel();
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
                ASCHandlerSetted = true;
                Log.Info("Handler Setted");
            }                
        }

        private void ASCOnSplit(object sender, EventArgs e)
        {
            splitterControl.SplitCheck("Trace ASL: SplitFlag Produced on ASL");
        }

        private void ASCOnStart( object sender, EventArgs e)
        {
            Log.Info("GameStart");
        }


    }
}

using HitCounterManager;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSplitterCore
{
    public interface ISplitterControl
    {
        /// <summary>
        /// Sets the AutoSplitter interface to call functions in the main program.
        /// </summary>
        /// <param name="interfaceHcm">An instance of IAutoSplitterCoreInterface to interact with HCM main program.</param>
        void SetInterface(IAutoSplitterCoreInterface interfaceHcm);

        /// <summary>
        /// Enables or disables the internal timer to check the SplitGo value when splitters call SpliterCheck() and a "Splitter State" is reached.
        /// Disables the timer in PracticeMode, when ShowDialog(MainForm), or when no splitter is selected.
        /// </summary>
        /// <param name="checking">True to enable the timer, false to disable it.</param>
        void SetChecking(bool checking);

        /// <summary>
        /// Setting DebugSpecial Method
        /// </summary>
        /// <param name="status">True to enable the timer, false to disable it.</param>
        void SetDebug(bool status);

        /// <summary>
        /// Processes a split in the main HCM program.
        /// </summary>
        void SplitCheck();

        /// <summary>
        /// Returns the current SplitStatus value.
        /// SplitStatus is set to false if the user activates Practice mode after a flag check has been executed.
        /// </summary>
        /// <returns>Returns true if the split status is valid, or false if Practice mode was enabled.</returns>
        bool GetSplitStatus();
    }

    public class SplitterControl : ISplitterControl
    {
        #region SingletonFactory
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };
        private static SplitterControl intanceSplitterControl = null;
        private SplitterControl() 
        {
            _update_timer.Tick += (sender, args) => SplitGo();
        }

        public static ISplitterControl GetControl() 
        {
            if (intanceSplitterControl == null) intanceSplitterControl = new SplitterControl();
            return intanceSplitterControl;
        }
        #endregion

        private bool enableChecking = true;
        private bool DebugMode = false;

        private bool _SplitGo = false;
        private bool SplitStatus = false;
        private IAutoSplitterCoreInterface interfaceHCM;

        public void SetInterface(IAutoSplitterCoreInterface interfaceHcm) => interfaceHCM = interfaceHcm;

        public void SetChecking(bool checking)
        {
            enableChecking = checking;
            if (enableChecking) {
                _update_timer.Enabled = true;
                _update_timer.Start();
            }
            else
            {
                _update_timer.Stop();
                _update_timer.Enabled = false;        
            }
        }

        public void SetDebug(bool status) => DebugMode = status;

        public bool GetSplitStatus() => SplitStatus;


        private static readonly object _object = new object(); //To look multiple threadings
        public void SplitCheck() //SplitStatus is seted false if user set Practice mode after a flagcheck is produced 
        {
            lock (_object)
            {
                if (!enableChecking)
                    SplitStatus = false;
                else
                {
                    if (_SplitGo) { Thread.Sleep(2000); }
                    _SplitGo = true;
                    SplitStatus = true;
                }
            }
        }

        private void SplitGo() // Checking by _update_timer on Interval 1000ms
        {
            if (_SplitGo && !DebugMode)
            {
                try { interfaceHCM.ProfileSplitGo(+1); } catch (Exception) { }
                _SplitGo = false;
            }
        }

    }
}

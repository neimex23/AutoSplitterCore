//MIT License

//Copyright (c) 2022-2024 Ezequiel Medina

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

        // <summary>
        /// Start or Stop Timer of HCM
        /// </summary>
        /// <param name="stop">True to enable the timer, false to disable it.</param>
        void StartStopTimer(bool stop);


        // <summary>
        /// Set Index of ComboBox Game on HCM
        /// </summary>
        /// <param name="index">index >-1: Set Index Combobox </param>
        void SetActiveGameIndex(int index);


        // <summary>
        /// Set ComboBox Practice on HCM
        /// </summary>
        /// <param name="status"></param>
        void SetPracticeMode(bool status);

        /// <summary>
        /// Processes a split in the main HCM program.
        /// </summary>
        void SplitCheck();

        /// <summary>
        /// Force to Internal Proces of HCM update timer values to current value
        /// </summary>
        void UpdateDuration();

        /// <summary>
        /// Reset CurrentSplit to first Split and add a attemps run
        /// Stop and Reset Timer
        /// </summary>
        void ProfileReset();

        /// <summary>
        /// Returns if HCM Current Split is the last
        /// </summary>
        /// <returns>Returns true/false if CurrentSplit = LastSplit</returns>
        bool CurrentFinalSplit();


        /// <summary>
        /// Returns the current SplitStatus value.
        /// SplitStatus is set to false if the user activates Practice mode after a flag check has been executed.
        /// </summary>
        /// <returns>Returns true if the split status is valid, or false if Practice mode was enabled.</returns>
        bool GetSplitStatus();

        /// <summary>
        /// Returns the current Timer Running value of Timer on HCM.
        /// </summary>
        /// <returns>Returns true if the Timer is running, or false if Timer is Stop.</returns>
        bool GetTimerRunning();
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

        //Checking Splits Procedures Functions
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

        // Chequing to HCM Interface functions
        public void SetDebug(bool status) => DebugMode = status;

        public bool GetTimerRunning() => interfaceHCM.TimerRunning;

        public void StartStopTimer(bool stop) => interfaceHCM.StartStopTimer(stop);

        public bool CurrentFinalSplit() => interfaceHCM.ActiveSplit == interfaceHCM.SplitCount;

        public void UpdateDuration() => interfaceHCM.UpdateDuration();

        public void ProfileReset() => interfaceHCM.ProfileReset();

        public void SetActiveGameIndex(int index) => interfaceHCM.ActiveGameIndex = index;

        public void SetPracticeMode(bool status) => interfaceHCM.PracticeMode = status;

    }
}

//MIT License

//Copyright (c) 2022-2023 Ezequiel Medina

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
using LiveSplit.Dishonored;
using System.Diagnostics;

namespace AutoSplitterCore
{
    public class DishonoredSplitter
    {
        public static GameMemory Dish = new GameMemory();
        public bool _StatusDish = false;
        public bool _SplitGo = false;
        public bool _PracticeMode = false;
        public bool _ShowSettings = false;
        public bool _runStarted = false;
        public bool isLoading = false;
        public DTDishonored dataDish;
        public DefinitionDishonored defDish = new DefinitionDishonored();
        public ProfilesControl _profile;
        private static readonly object _object = new object();
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };
        public bool DebugMode = false;

        #region Control Management
        public DTDishonored getDataDishonored()
        {
            return this.dataDish;
        }

        public void setDataDishonored(DTDishonored data, ProfilesControl profile)
        {
            this.dataDish = data;
            this._profile = profile;
            _update_timer.Tick += (sender, args) => SplitGo();
            _update_timer.Tick += (sender, args) => getDishonoredStatusProcess();
            Dish.OnFirstLevelLoading += Dish_OnFirstLevelLoading;
            Dish.OnPlayerGainedControl += Dish_OnPlayerGainedControl;
            Dish.OnLoadStarted += Dish_OnLoadStarted;
            Dish.OnAreaCompleted += Dish_OnAreaCompleted;
        }

        public bool getDishonoredStatusProcess()
        {
            try
            {
                Dish.Update();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return _StatusDish = false;
            }
            return _StatusDish = Dish._process != null;
        }

        public void SplitGo()
        {
            if (_SplitGo && !DebugMode)
            {
                try { _profile.ProfileSplitGo(+1); } catch (Exception) { }
                _SplitGo = false;
            }
        }

        private void SplitCheck()
        {
            lock (_object)
            {
                if (_SplitGo) { Thread.Sleep(2000); }
                _SplitGo = true;
            }
        }

        public void setStatusSplitting(bool status)
        {
            dataDish.enableSplitting = status;
            if (status) { _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }
        #endregion
        #region Object Management
        public void AddElement(string Element)
        {
            int index = dataDish.getOptionToSplit().FindIndex(i => Element == i.Option);
            dataDish.getOptionToSplit()[index].Enable = true;
        }

        public void RemoveElement(string Element)
        {
            int index = dataDish.getOptionToSplit().FindIndex(i => Element == i.Option);
            dataDish.getOptionToSplit()[index].Enable = false;
        }

        public void clearData()
        {
            foreach (var i in dataDish.getOptionToSplit())
            {
                i.Enable = false;
            }
        }
        #endregion
        #region CheckFlag Init()
        void Dish_OnPlayerGainedControl(object sender, EventArgs e)
        {
            if (_StatusDish && !_PracticeMode && !_ShowSettings)
                _runStarted = true;
        }

        void Dish_OnFirstLevelLoading(object sender, EventArgs e)
        {
            if (_StatusDish && !_PracticeMode && !_ShowSettings)
                _runStarted = false;
        }


        void Dish_OnLoadStarted(object sender, EventArgs e)
        {
            if (_StatusDish && !_PracticeMode && !_ShowSettings)
                isLoading = true;
        }

        void Dish_OnAreaCompleted(object sender, GameMemory.AreaCompletionType type)
        {
            if (_StatusDish && !_PracticeMode && !_ShowSettings)
            {
                if ((type == GameMemory.AreaCompletionType.IntroEnd && dataDish.DishonoredOptions[0].Enable)
                    || (type == GameMemory.AreaCompletionType.MissionEnd && dataDish.DishonoredOptions[1].Enable)
                    || (type == GameMemory.AreaCompletionType.PrisonEscape && dataDish.DishonoredOptions[2].Enable)
                    || (type == GameMemory.AreaCompletionType.OutsidersDream && dataDish.DishonoredOptions[3].Enable)
                    || (type == GameMemory.AreaCompletionType.Weepers && dataDish.DishonoredOptions[4].Enable)
                    || (type == GameMemory.AreaCompletionType.DLC06IntroEnd && dataDish.DishonoredOptions[5].Enable))
                {
                    SplitCheck();
                }
            }
        }
        #endregion
    }
}
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
using LiveSplit.Dishonored;
using System.Diagnostics;
using System.Xml.Linq;

namespace AutoSplitterCore
{
    public class DishonoredSplitter
    {
        private static GameMemory Dish = new GameMemory();
        private DTDishonored dataDish;
        private DefinitionDishonored defDish = new DefinitionDishonored();
        private ISplitterControl splitterControl = SplitterControl.GetControl();

        public bool _StatusDish = false;
        public bool _PracticeMode = false;
        public bool _ShowSettings = false;
        public bool _runStarted = false;
        public bool isLoading = false;
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1000 };


        #region Control Management
        public DTDishonored GetDataDishonored() => dataDish;

        public void SetDataDishonored(DTDishonored data)
        {
            dataDish = data;
            _update_timer.Tick += (sender, args) => GetDishonoredStatusProcess();
            Dish.OnFirstLevelLoading += Dish_OnFirstLevelLoading;
            Dish.OnPlayerGainedControl += Dish_OnPlayerGainedControl;
            Dish.OnLoadStarted += Dish_OnLoadStarted;
            Dish.OnAreaCompleted += Dish_OnAreaCompleted;
        }

        public bool GetDishonoredStatusProcess()
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


        public void SetStatusSplitting(bool status)
        {
            dataDish.enableSplitting = status;
            if (status) { _update_timer.Enabled = true; } else { _update_timer.Enabled = false; }
        }
        #endregion
        #region Object Management
        public void AddElement(string Element)
        {
            int index = dataDish.GetOptionToSplit().FindIndex(i => Element == i.Option);
            dataDish.GetOptionToSplit()[index].Enable = true;
        }

        public void RemoveElement(string Element)
        {
            int index = dataDish.GetOptionToSplit().FindIndex(i => Element == i.Option);
            dataDish.GetOptionToSplit()[index].Enable = false;
        }

        public void ClearData()
        {
            foreach (var i in dataDish.GetOptionToSplit())
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
                    splitterControl.SplitCheck($"SplitFlags is produced by: Dishonored -> {type.ToString()}");
                }
            }
        }
        #endregion
    }
}
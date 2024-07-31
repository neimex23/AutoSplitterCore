//MIT License

//Copyright (c) 2022 Ezequiel Medina

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
using System.Collections.Generic;

namespace AutoSplitterCore
{
    public class DefinitionDishonored
    {
        [Serializable]
        public class DishonoredOptions
        {
            public string Option = string.Empty;
            public bool Enable = false;
        }
    }
    public class DTDishonored
    {
        //Settings Vars
        public bool enableSplitting = false;
        public bool autoTimer = false;
        public bool gameTimer = false;
        //Flags to Split
        public List<DefinitionDishonored.DishonoredOptions> DishonoredOptions = new List<DefinitionDishonored.DishonoredOptions>()
        {
            new DefinitionDishonored.DishonoredOptions() { Option = "Intro End"},
            new DefinitionDishonored.DishonoredOptions() { Option = "Mission End"},
            new DefinitionDishonored.DishonoredOptions() { Option = "Prision Escape (Sewer Entrace)"},
            new DefinitionDishonored.DishonoredOptions() { Option = "Outsider's Dream"},
            new DefinitionDishonored.DishonoredOptions() { Option = "Weepers"}
        };

        public List<DefinitionDishonored.DishonoredOptions> GetOptionToSplit() => this.DishonoredOptions;

    }
}

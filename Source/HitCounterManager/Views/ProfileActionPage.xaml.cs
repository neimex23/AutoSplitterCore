﻿//MIT License

//Copyright (c) 2021-2021 Peter Kirmeier

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
using Xamarin.Forms;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Views
{
    public partial class ProfileActionPage : ContentPage
    {
        ProfileActionPageViewModel vm;
        public ProfileActionPage(ProfileActionPageViewModel.ProfileAction Action, ProfileViewModel Origin)
        {
            InitializeComponent();
            vm = (ProfileActionPageViewModel)BindingContext;
            vm.Action = Action;
            vm.Origin = Origin;
        }

        protected override void OnAppearing() => vm.OnAppearing();

        private void PopBack_Dismiss_Clicked(object sender, EventArgs e) => Navigation.PopModalAsync();
        private void PopBack_OK_Clicked(object sender, EventArgs e)
        {
            // TODO: Move this to a Command that the button can be enabled/disabled when action is possible/implossibles (e.g. overwriting an existing entry)
            if (vm.Submit()) Navigation.PopModalAsync();
        }
    }
}

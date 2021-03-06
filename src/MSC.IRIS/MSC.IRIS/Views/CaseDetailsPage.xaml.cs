﻿using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Ioc;
using MSC.IRIS.ViewModels;
using Xamarin.Forms;

namespace MSC.IRIS
{
    public partial class CaseDetailsPage : ContentPage
    {
        public CaseDetailsPage ()
        {
            InitializeComponent ();

            // setup the databinding
            this.BindingContext = SimpleIoc.Default.GetInstance<CaseDetailsPageViewModel> ();

            // wire up teh tapped event
            socialContentList.ItemTapped += (sender, args) =>
            {
                (this.BindingContext as CaseDetailsPageViewModel).SocialContentItemTappedCommand.Execute (socialContentList.SelectedItem);
                socialContentList.SelectedItem = null;
            };
        }
    }
}

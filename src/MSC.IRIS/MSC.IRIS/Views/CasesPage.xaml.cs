using System;
using System.Collections.Generic;
using MSC.IRIS.ViewModels;
using Xamarin.Forms;

namespace MSC.IRIS
{
    public partial class CasesPage : ContentPage
    {
        public CasesPage ()
        {
            // hide the backbutton
            NavigationPage.SetHasBackButton (this, false);
            NavigationPage.SetHasNavigationBar (this, false);

            InitializeComponent ();

            // set the binding context
            this.BindingContext = ViewModelLocator.Default.GetViewModel<CasesPageViewModel> ();
        }
    }
}

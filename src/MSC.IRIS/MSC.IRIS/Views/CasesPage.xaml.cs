using System;
using System.Collections.Generic;
using MSC.IRIS.ViewModels;
using Xamarin.Forms;

namespace MSC.IRIS
{
    public partial class CasesPage : TabbedPage
    {
        public CasesPage ()
        {
            NavigationPage.SetHasBackButton (this, false);

            InitializeComponent ();

            // set the binding context
            this.BindingContext = ViewModelLocator.Default.GetViewModel<CasesPageViewModel> ();
        }

    }

   
}

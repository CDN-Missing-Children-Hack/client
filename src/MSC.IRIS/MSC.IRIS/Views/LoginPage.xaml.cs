using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Ioc;
using MSC.IRIS.ViewModels;
using Xamarin.Forms;

namespace MSC.IRIS
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage ()
        {
            // hide the navigation bar
            NavigationPage.SetHasNavigationBar (this, false);

            // register the navigation service
            SimpleIoc.Default.Register<INavigation> (() => this.Navigation);

            InitializeComponent ();

            // set the binding context for datababinding
            this.BindingContext = ViewModelLocator.Default.GetViewModel<LoginPageViewModel> ();
        }
    }
}

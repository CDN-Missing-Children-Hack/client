using System;
using System.Collections.Generic;
using MSC.IRIS.ViewModels;
using Xamarin.Forms;

namespace MSC.IRIS
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage ()
        {
            InitializeComponent ();

            // set the binding context for datababinding
            this.BindingContext = new LoginPageViewModel (this.Navigation);
        }
    }
}

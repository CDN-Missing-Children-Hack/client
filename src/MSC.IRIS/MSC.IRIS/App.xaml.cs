using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MSC.IRIS.Models;
using Xamarin.Forms;

namespace MSC.IRIS
{
	public partial class App : Application
	{
		public App ()
		{
            // set the defaul locator provider
            ServiceLocator.SetLocatorProvider (() => SimpleIoc.Default);
            SimpleIoc.Default.Register<ApiServices> ();

			InitializeComponent();

            MainPage = new NavigationPage (new MSC.IRIS.LoginPage ());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Gcm.Client;



namespace MSC.IRIS.Droid
{
	[Activity (Label = "MSC.IRIS", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		public static MainActivity instance;

		protected override void OnCreate (Bundle bundle)
		{


			instance = this;

	

			// Set your view from the "main" layout resource
			//SetContentView(Resource.Layout.Main);

			// Get your button from the layout resource,
			// and attach an event to it
			//Button button = FindViewById<Button>(Resource.Id.myButton);

			RegisterWithGCM();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate (bundle);


			global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new MSC.IRIS.App ());
		}
		private void RegisterWithGCM()
		{
			// Check to ensure everything's set up right
			GcmClient.CheckDevice(this);
			GcmClient.CheckManifest(this);

			// Register for push notifications
			Log.Info("MainActivity", "Registering...");
			GcmClient.Register(this, Constants.SenderID);
		}
	}
}


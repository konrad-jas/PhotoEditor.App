using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Ninject;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace PhotoEditor.Droid
{
    [Activity(Label = "PhotoEditor", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
			var container = new StandardKernel();
            var device = AndroidDevice.CurrentDevice;
            container.Bind<IMediaPicker>().ToMethod(context => device.MediaPicker).InTransientScope();
            LoadApplication(new App(container));
        }
    }
}


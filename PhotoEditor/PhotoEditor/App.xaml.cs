using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using PhotoEditor.Utility;
using PhotoEditor.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PhotoEditor
{
    public partial class App : Application
    {
		public static StandardKernel Container { get; private set; }
        public App(StandardKernel container)
        {
            InitializeComponent();
	        Container = container;
			var rootPage = new RootPage();
            var navigationPage = new NavigationPage(rootPage);
	        Bootstrapper._initialize(rootPage);
	        MainPage = navigationPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

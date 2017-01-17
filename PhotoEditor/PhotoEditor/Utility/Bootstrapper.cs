using System.Collections.Generic;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.ViewModels;
using PhotoEditor.Views;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace PhotoEditor.Utility
{
	public class Bootstrapper
	{
		public static void _initialize(RootPage mainPage)
		{
			LoadDependencies();
		    App.Container.Bind<IPopupInflater>().ToConstant(mainPage);
			mainPage.Init(App.Container.Get<PreviewViewModel>(), App.Container.Get<FlowBuilderViewModel>());
		}

		private static void LoadDependencies()
		{
			App.Container.Load(new List<NinjectModule>
			{
				new ServicesModule(),
				new ViewModelsModule()
			});
		}
	}
}

using System.Collections.Generic;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using PhotoEditor.ViewModels;
using PhotoEditor.Views;

namespace PhotoEditor.Utility
{
	public class Bootstrapper
	{
		public static void _initialize(RootPage mainPage)
		{
			LoadDependencies();
			mainPage.Init(App.Container.Get<PreviewViewModel>(), App.Container.Get<FlowBuilderViewModel>());
		}

		private static void LoadDependencies()
		{
			App.Container.Load(new List<NinjectModule>
			{
				new StubServicesModule(),
				new ViewModelsModule()
			});
		}
	}
}

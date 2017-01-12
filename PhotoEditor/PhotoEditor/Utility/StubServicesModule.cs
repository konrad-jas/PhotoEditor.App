using Ninject.Modules;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Services.Stubs;

namespace PhotoEditor.Utility
{
	public class StubServicesModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IImageProvider>().To<ImageProviderStub>().InSingletonScope();
		}
	}
}

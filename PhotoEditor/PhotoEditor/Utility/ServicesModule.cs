using Ninject.Modules;
using PhotoEditor.Services;
using PhotoEditor.Services.Interfaces;

namespace PhotoEditor.Utility
{
	public class ServicesModule : NinjectModule
	{
		public override void Load()
		{
		    Bind<IFiltersProvider>().To<FiltersProvider>().InSingletonScope();
		}
	}
}

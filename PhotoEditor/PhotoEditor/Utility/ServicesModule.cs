﻿using Ninject.Modules;
using PhotoEditor.Services;
using PhotoEditor.Services.Interfaces;

namespace PhotoEditor.Utility
{
	public class ServicesModule : NinjectModule
	{
		public override void Load()
		{
		    Bind<IFiltersProvider>().To<FiltersProvider>().InSingletonScope();
		    Bind<ISoapServiceClient>().To<SoapServiceClient>().InSingletonScope();
		    Bind<IFilterExecutorFactory>().To<FilterExecutorFactory>().InSingletonScope();
		}
	}
}

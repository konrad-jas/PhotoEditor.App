using Ninject.Modules;
using PhotoEditor.ViewModels;

namespace PhotoEditor.Utility
{
	public class ViewModelsModule : NinjectModule
	{
		public override void Load()
		{
			Bind<PreviewViewModel>().ToSelf();
			Bind<FlowBuilderViewModel>().ToSelf();
		    Bind<ParamsPickerViewModel>().ToSelf();
		    Bind<FilterPickerViewModel>().ToSelf();
		}
	}
}

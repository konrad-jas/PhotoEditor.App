using System.Threading.Tasks;
using Ninject;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;
using PhotoEditor.ViewModels;

namespace PhotoEditor.Views
{
    public partial class RootPage : IPopupInflater
    {
        public RootPage()
        {
            InitializeComponent();
        }

	    public void Init(PreviewViewModel previewViewModel, FlowBuilderViewModel flowBuilderViewModel)
	    {
		    PreviewPage.BindingContext = previewViewModel;
		    FlowBuilderPage.BindingContext = flowBuilderViewModel;
	    }

        public async Task InflatePopup(string title, string message, string confirmation)
        {
            await DisplayAlert(title, message, confirmation);
        }

        public async Task ShowParamsPicker(FilterType filter)
        {
            var page = new ParameterPickerPage();
            var vm = App.Container.Get<ParamsPickerViewModel>();
            vm.Init(filter);
            page.BindingContext = vm;
            await Navigation.PushModalAsync(page, true);
        }

        public async Task ClosePopup()
        {
            await Navigation.PopModalAsync(true);
        }
    }
}

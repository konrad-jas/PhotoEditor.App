using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ninject;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;
using PhotoEditor.ViewModels;
using Xamarin.Forms;

namespace PhotoEditor.Views
{
    public partial class RootPage : INavigator
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

        protected override bool OnBackButtonPressed()
        {
            var backable = CurrentPage.BindingContext as IBackable;
            backable?.OnBack();
            return base.OnBackButtonPressed();
        }

        public async Task InflatePopup(string title, string message, string confirmation)
        {
            await DisplayAlert(title, message, confirmation);
        }

        public async Task<bool> ShowViewModel<TViewModel>(object args = null) where TViewModel : BaseViewModel
        {
            if (Navigation.NavigationStack.Last().BindingContext.GetType() == typeof (TViewModel))
                return false;

            var page = (ContentPage)App.Container.Get(_viewsDictionary[typeof (TViewModel)]);
            var vm = App.Container.Get<TViewModel>();
            if(args != null)
                vm.Init(args);

            page.BindingContext = vm;
            await Navigation.PushAsync(page, true);
            return true;
        }

        private readonly Dictionary<Type, Type> _viewsDictionary = new Dictionary<Type, Type>
        {
            {typeof(ParamsPickerViewModel), typeof(ParameterPickerPage)},
            {typeof(FilterPickerViewModel), typeof(FilterPickerPage) }
        }; 

        public async Task GoBack()
        {
            await Navigation.PopAsync(true);
            var backable = Navigation.NavigationStack.Last().BindingContext as IBackable;
            backable?.OnBack();
        }
    }
}

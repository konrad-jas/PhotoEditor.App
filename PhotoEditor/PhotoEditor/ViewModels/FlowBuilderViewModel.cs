using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;
using Xamarin.Forms;

namespace PhotoEditor.ViewModels
{
	public class FlowBuilderViewModel : BaseViewModel, IBackable
	{
	    private readonly IFilterExecutorFactory _executorFactory;
	    private readonly IImageProvider _imageProvider;
	    private readonly IFiltersProvider _filtersProvider;
        private MemoryStream _selectedImage;

        public FlowBuilderViewModel(INavigator navigator, IFilterExecutorFactory executorFactory,
	        IImageProvider imageProvider, IFiltersProvider filtersProvider) : base(navigator)
	    {
	        _executorFactory = executorFactory;
	        _imageProvider = imageProvider;
	        _filtersProvider = filtersProvider;

            AddFilterCommand = new Command(AddFilterAction);
            RemoveFilterCommand = new Command<ParametrizedFilter>(RemoveFilterAction);
            SelectedFilters = new ObservableCollection<ParametrizedFilter>();
            SelectParametersCommand = new Command<ParametrizedFilter>(SelectParamsAction);
            FilterCommand = new Command(FilterAction);
            ChooseImageCommand = new Command(ChooseImageAction);
            ClearCommand = new Command(ClearAction);
	    }

	    public Command ClearCommand { get; set; }
	    private void ClearAction()
	    {
	        SelectedFilters.Clear();
	    }

	    public Command ChooseImageCommand { get; set; }
	    private async void ChooseImageAction()
	    {
            if (Busy)
                return;

            var imageStream = await _imageProvider.GetImage();
            if (imageStream == null)
                return;

            await SetImageSource(imageStream);
        }

	    public Command FilterCommand { get; set; }

	    private void FilterAction()
	    {
            RunInBackground(async () =>
            {
                var executor = _executorFactory.GetExecutor().Configure(_selectedImage, FilterType.Combined);
                return await executor.ExecuteFilter();
            }, async image => await SetImageSource(image));
        }

	    public Command<ParametrizedFilter> SelectParametersCommand { get; set; }
	    private async void SelectParamsAction(ParametrizedFilter obj)
	    {
	        if (obj.Options.Any())
	        {
	            var navigated = await Navigator.ShowViewModel<ParamsPickerViewModel>(obj.FilterType);
                if(navigated == false)
                    return;

	            MessagingCenter.Subscribe<ParamsPickerViewModel, IEnumerable<FilterOption>>(this, "Process",
                (model, options) =>
                {
                    UpdateOptions(obj, options);
                });
	        }
	    }

	    private void UpdateOptions(ParametrizedFilter parametrizedFilter, IEnumerable<FilterOption> options)
	    {
            CleanupSubscription();
	        parametrizedFilter.Options = options;
	    }

	    public Command<ParametrizedFilter> RemoveFilterCommand { get; set; }
	    private void RemoveFilterAction(ParametrizedFilter obj)
	    {
	        SelectedFilters.Remove(obj);
	    }

	    public ObservableCollection<ParametrizedFilter> SelectedFilters { get; set; }

	    public Command AddFilterCommand { get; set; }
	    private async void AddFilterAction()
	    {
	        var navigated = await Navigator.ShowViewModel<FilterPickerViewModel>();
            if(navigated)
	            MessagingCenter.Subscribe<FilterPickerViewModel, FilterNO>(this, "FilterPicked", FilterPicked);
	    }

	    private void FilterPicked(FilterPickerViewModel filterPickerViewModel, FilterNO filterNo)
	    {
	        CleanupSubscription();
            SelectedFilters.Add(new ParametrizedFilter
            {
                Command = SelectParametersCommand,
                FilterType = filterNo.Type,
                Name = filterNo.Name,
                RemoveCommand = RemoveFilterCommand,
                Options = _filtersProvider.GetFilterOptions(filterNo.Type)
            });
	    }

	    private async Task SetImageSource(Stream imageStream)
        {
            _selectedImage?.Dispose();
            _selectedImage = new MemoryStream();
            await imageStream.CopyToAsync(_selectedImage);
            _selectedImage.Position = 0;
            imageStream.Position = 0;
            ImageSource = ImageSource.FromStream(() => imageStream);
            RaisePropertyChanged(() => ImageSource);
            RaisePropertyChanged(() => ImageChosen);
            FilterCommand.ChangeCanExecute();
        }

        public bool ImageChosen => ImageSource != null;
        public ImageSource ImageSource { get; set; }
	    public void OnBack()
	    {
	        CleanupSubscription();
	    }

	    private void CleanupSubscription()
	    {
            MessagingCenter.Unsubscribe<FilterPickerViewModel, FilterNO>(this, "FilterPicked");
            MessagingCenter.Unsubscribe<ParamsPickerViewModel, IEnumerable<FilterOption>>(this, "Process");
        }
    }
}

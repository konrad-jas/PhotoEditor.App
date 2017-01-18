using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;
using Xamarin.Forms;

namespace PhotoEditor.ViewModels
{
	public class PreviewViewModel : BaseViewModel, IBackable
	{
	    private readonly IImageProvider _imageProvider;
	    private readonly IFiltersProvider _filtersProvider;
	    private readonly IFilterExecutorFactory _executorFactory;
	    private MemoryStream _selectedImage;

	    public PreviewViewModel(IImageProvider imageProvider, IFiltersProvider filtersProvider, IFilterExecutorFactory executorFactory, INavigator inflater) : base(inflater)
		{
	        _imageProvider = imageProvider;
	        _filtersProvider = filtersProvider;
	        _executorFactory = executorFactory;
	        ChooseImageCommand = new Command(ChooseImageAction);
            FilterCommand = new Command<FilterType>(FilterAction);
	        var filters = _filtersProvider.GetFilters().ToList();
	        foreach (var filter in filters)
	        {
	            filter.FilterCommand = FilterCommand;
	        }
	        Filters = filters;
		}

	    private async void FilterAction(FilterType obj)
	    {
            if (ImageChosen == false)
            {
                await Navigator.InflatePopup("Error", "Please choose image first", "Ok");
                return;
            }
            if(Busy)
                return;

            if (_filtersProvider.GetFilterOptions(obj).Any())
	        {
	            var navigated = await Navigator.ShowViewModel<ParamsPickerViewModel>(obj);
                if(navigated == false)
                    return;

	            MessagingCenter.Subscribe<ParamsPickerViewModel, IEnumerable<FilterOption>>(this, "Process",
	                (model, options) =>
	                {
	                    ProcessFilter(obj, options);
	                });
	        }
	        else
	            ProcessFilter(obj, Enumerable.Empty<FilterOption>());
	    }

	    private void ProcessFilter(FilterType filterType, IEnumerable<FilterOption> options)
	    {
	        CleanupSubscription();
            RunInBackground(async () =>
            {
                var executor = _executorFactory.GetExecutor().Configure(_selectedImage).ForFilter(filterType);
                foreach (var option in options)
                {
                    executor.WithParameter(option.Value);
                }
                return await executor.ExecuteFilter();
            }, async image => await SetImageSource(image));
	    }

	    public Command<FilterType> FilterCommand { get; set; }

	    public List<FilterNO> Filters { get; set; }

	    public bool ImageChosen => ImageSource != null;

	    public ImageSource ImageSource { get; set; }

		public Command ChooseImageCommand { get; private set; }

	    private async void ChooseImageAction()
	    {
            if(Busy)
                return;

	        var imageStream = await _imageProvider.GetImage();
            if(imageStream == null)
                return;

	        await SetImageSource(imageStream);
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

	    private void CleanupSubscription()
	    {
	        MessagingCenter.Unsubscribe<ParamsPickerViewModel, IEnumerable<FilterOption>>(this, "Process");
        }

        public void OnBack()
	    {
	        CleanupSubscription();
	    }
	}
}

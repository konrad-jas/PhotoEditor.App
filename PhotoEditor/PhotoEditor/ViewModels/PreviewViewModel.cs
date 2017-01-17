using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExifLib;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;

namespace PhotoEditor.ViewModels
{
	public class PreviewViewModel : BaseViewModel
	{
	    private readonly IMediaPicker _mediaPicker;
	    private readonly IFiltersProvider _filtersProvider;
	    private readonly IPopupInflater _popupInflater;
	    private string _imagePath;

	    public PreviewViewModel(IMediaPicker mediaPicker, IFiltersProvider filtersProvider, IPopupInflater popupInflater)
		{
	        _mediaPicker = mediaPicker;
	        _filtersProvider = filtersProvider;
	        _popupInflater = popupInflater;
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
	        //if (ImageChosen == false)
	        //{
	        //    await _popupInflater.InflatePopup("Error", "Please choose image first", "Ok");
	        //    return;
	        //}
            MessagingCenter.Subscribe<ParamsPickerViewModel, IEnumerable<FilterOption>>(this, "Process",
                async (model, options) =>
                {
                    await ProcessFilter(obj, options);
                });
	        if(_filtersProvider.GetFilterOptions(obj).Any())
	            await _popupInflater.ShowParamsPicker(obj);
	    }

	    private async Task ProcessFilter(FilterType filterType, IEnumerable<FilterOption> options)
	    {
	        MessagingCenter.Unsubscribe<ParamsPickerViewModel>(this, "Process");
	    }

	    public Command<FilterType> FilterCommand { get; set; }

	    public List<FilterNO> Filters { get; set; }

	    public bool ImageChosen => string.IsNullOrWhiteSpace(_imagePath) == false;

	    public ImageSource ImageSource { get; set; }

		public Command ChooseImageCommand { get; private set; }
		private async void ChooseImageAction()
		{
		    try
		    {
		        var media = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions {PercentQuality = 50});
		        _imagePath = media.Path;
		        ImageSource = ImageSource.FromStream(() =>
		        {
		            var stream = media.Source;
		            media.Dispose();
		            return stream;
		        });
		        RaisePropertyChanged(() => ImageSource);
		        RaisePropertyChanged(() => ImageChosen);
                FilterCommand.ChangeCanExecute();
		    }
		    catch (TaskCanceledException)
		    {
		    }
		}
	}
}

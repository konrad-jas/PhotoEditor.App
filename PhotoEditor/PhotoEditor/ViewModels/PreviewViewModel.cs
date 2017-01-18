using System;
using System.Collections.Generic;
using System.IO;
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
	    private readonly IImageProvider _imageProvider;
	    private readonly IFiltersProvider _filtersProvider;
	    private readonly IPopupInflater _popupInflater;
	    private Stream _selectedImage;

	    public PreviewViewModel(IImageProvider imageProvider, IFiltersProvider filtersProvider, IPopupInflater popupInflater)
		{
	        _imageProvider = imageProvider;
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

	    public bool ImageChosen => ImageSource != null;

	    public ImageSource ImageSource { get; set; }

		public Command ChooseImageCommand { get; private set; }

	    private async void ChooseImageAction()
	    {
	        var imageStream = await _imageProvider.GetImage();
            if(imageStream == null)
                return;

	        _selectedImage?.Dispose();
            _selectedImage = new MemoryStream();
	        await imageStream.CopyToAsync(_selectedImage);
	        _selectedImage.Position = 0;
	        ImageSource = ImageSource.FromStream(() => imageStream);
	        RaisePropertyChanged(() => ImageSource);
	        RaisePropertyChanged(() => ImageChosen);
	        FilterCommand.ChangeCanExecute();
	    }
	}
}

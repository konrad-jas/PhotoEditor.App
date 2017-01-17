using System.Collections.Generic;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;
using Xamarin.Forms;

namespace PhotoEditor.ViewModels
{
    public class ParamsPickerViewModel : BaseViewModel
    {
        private readonly IFiltersProvider _filtersProvider;
        private readonly IPopupInflater _popupInflater;
        private FilterType _filterType;

        public ParamsPickerViewModel(IFiltersProvider filtersProvider, IPopupInflater popupInflater)
        {
            _filtersProvider = filtersProvider;
            _popupInflater = popupInflater;
            ConfirmCommand = new Command(ConfirmAction);
        }

        private async void ConfirmAction()
        {
            await _popupInflater.ClosePopup();
            MessagingCenter.Send(this, "Process", FilterOptions);
        }

        public override void Init(object args)
        {
            _filterType = (FilterType) args;
            FilterOptions = _filtersProvider.GetFilterOptions(_filterType);
        }

        public Command ConfirmCommand { get; set; }

        public IEnumerable<FilterOption> FilterOptions { get; set; }
    }
}

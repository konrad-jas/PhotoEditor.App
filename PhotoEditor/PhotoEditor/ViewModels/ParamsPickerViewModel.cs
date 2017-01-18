using System.Collections.Generic;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;
using Xamarin.Forms;

namespace PhotoEditor.ViewModels
{
    public class ParamsPickerViewModel : BaseViewModel
    {
        private readonly IFiltersProvider _filtersProvider;
        private FilterType _filterType;

        public ParamsPickerViewModel(IFiltersProvider filtersProvider, IPopupInflater popupInflater) : base(popupInflater)
        {
            _filtersProvider = filtersProvider;
            ConfirmCommand = new Command(ConfirmAction);
        }

        private async void ConfirmAction()
        {
            await PopupInflater.ClosePopup();
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

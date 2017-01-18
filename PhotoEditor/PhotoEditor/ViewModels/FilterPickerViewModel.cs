using System.Collections.Generic;
using System.Linq;
using PhotoEditor.Services.Interfaces;
using Xamarin.Forms;

namespace PhotoEditor.ViewModels
{
    public class FilterPickerViewModel : BaseViewModel
    {
        public FilterPickerViewModel(INavigator navigator, IFiltersProvider filtersProvider) : base(navigator)
        {
            Filters = filtersProvider.GetFilters().ToList();
            foreach (var filter in Filters)
            {
                filter.FilterCommand = FilterSelectedCommand;
            }
            FilterSelectedCommand = new Command<FilterNO>(FilterSelectedAction);
        }

        public Command<FilterNO> FilterSelectedCommand { get; set; }

        private async void FilterSelectedAction(FilterNO obj)
        {
            MessagingCenter.Send(this, "FilterSelected", obj);
            await Navigator.GoBack();
        }

        public IEnumerable<FilterNO> Filters { get; set; }
    }
}

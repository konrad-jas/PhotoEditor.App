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
            FilterSelectedCommand = new Command<FilterNO>(FilterSelectedAction);
            foreach (var filter in Filters)
            {
                filter.FilterCommand = FilterSelectedCommand;
            }
        }

        public Command<FilterNO> FilterSelectedCommand { get; set; }

        private async void FilterSelectedAction(FilterNO obj)
        {
            MessagingCenter.Send(this, "FilterPicked", obj);
            await Navigator.GoBack();
        }

        public IEnumerable<FilterNO> Filters { get; set; }
    }
}

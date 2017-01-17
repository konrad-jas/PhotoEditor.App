using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PhotoEditor.Views
{
    public partial class ParameterPickerPage : ContentPage
    {
        public ParameterPickerPage()
        {
            InitializeComponent();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView.SelectedItem = null;
        }
    }
}

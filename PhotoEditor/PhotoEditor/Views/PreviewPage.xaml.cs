using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PhotoEditor.Views
{
    public partial class PreviewPage : ContentPage
    {
        public PreviewPage()
        {
            InitializeComponent();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView.SelectedItem = null;
        }
    }
}

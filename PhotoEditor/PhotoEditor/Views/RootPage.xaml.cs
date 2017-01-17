﻿using System.Threading.Tasks;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.ViewModels;

namespace PhotoEditor.Views
{
    public partial class RootPage : IPopupInflater
    {
        public RootPage()
        {
            InitializeComponent();
        }

	    public void Init(PreviewViewModel previewViewModel, FlowBuilderViewModel flowBuilderViewModel)
	    {
		    PreviewPage.BindingContext = previewViewModel;
		    FlowBuilderPage.BindingContext = flowBuilderViewModel;
	    }

        public async Task InflatePopup(string title, string message, string confirmation)
        {
            await DisplayAlert(title, message, confirmation);
        }
    }
}
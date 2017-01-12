using PhotoEditor.ViewModels;

namespace PhotoEditor.Views
{
    public partial class RootPage
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
    }
}

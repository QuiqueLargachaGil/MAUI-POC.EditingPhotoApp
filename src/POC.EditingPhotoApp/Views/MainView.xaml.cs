using POC.EditingPhotoApp.ViewModels;

namespace POC.EditingPhotoApp.Views;

public partial class MainView : ContentPage
{
	public MainView(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
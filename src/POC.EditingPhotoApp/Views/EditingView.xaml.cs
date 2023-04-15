using POC.EditingPhotoApp.ViewModels;

namespace POC.EditingPhotoApp.Views;

public partial class EditingView : ContentPage
{
	public EditingView(EditingViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;

        viewModel.GetEditedPhotoEvent += GetEditedPhoto;
	}

	private async Task<Stream> GetEditedPhoto()
	{
		var screenshot = await Editor.CaptureAsync();
		var stream = await screenshot.OpenReadAsync();

		return stream;
	}
}

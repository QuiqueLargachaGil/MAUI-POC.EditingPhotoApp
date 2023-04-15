using POC.EditingPhotoApp.Views;

namespace POC.EditingPhotoApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(MainView), typeof(MainView));
		Routing.RegisterRoute(nameof(EditingView), typeof(EditingView));
	}
}

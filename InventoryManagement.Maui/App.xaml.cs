using InventoryManagement.Maui.Views;

namespace InventoryManagement.Maui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
        Routing.RegisterRoute(nameof(ProductListView), typeof(ProductListView));
    }
}

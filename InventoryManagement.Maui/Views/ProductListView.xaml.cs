using InventoryManagement.Device;

namespace InventoryManagement.Maui.Views;

public partial class ProductListView : ContentPage
{
    public ProductListView()
    {
        InitializeComponent();
        BindingContext = vm;
    }
    readonly ProductViewModel vm = new();
}
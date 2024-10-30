using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryManagement.Shared;

namespace InventoryManagement.Device;
public partial class ProductViewModel : ObservableObject
{
    #region Constructor
    public ProductViewModel()
    {
    } 
    static readonly Lazy<IHttpClientService> clientService = new(() => new HttpClientService());
    #endregion

    [ObservableProperty]
    ObservableCollection<ProductItemView> products = [];
    [RelayCommand]
    private async Task LoadProducts()
    {

        try
        {
            var client = clientService.Value.Client;
            var result = await client.GetFromJsonAsync<List<ProductDetail>>("api/Product");
            
            Products.Clear();
            if (result != null)
            {
                foreach (var product in result)
                {
                    Products.Add(new ProductItemView(product));
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading products: {ex.Message}");
        }
        finally
        {
        }
    }
}

#region Product Item View
public class ProductItemView
{
    public ProductItemView(ProductDetail source)
    {
        data = source;
    }
    readonly ProductDetail data;
    public string Name => data.Name;
    public string Code => data.Code;
    public string Price => data.Price.ToString();
}
#endregion
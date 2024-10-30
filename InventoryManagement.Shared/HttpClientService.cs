using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Shared;
public interface IHttpClientService
{
    HttpClient Client { get; }
}

public class HttpClientService : IHttpClientService
{
    public HttpClient Client { get; }
    public HttpClientService()
    {
        Client = new HttpClient
        {
            BaseAddress = new Uri("https://127.0.0.1:44302/")
        };
    }
}
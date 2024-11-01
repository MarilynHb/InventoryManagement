using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
#if ANDROID
        Client = new HttpClient
        {
            BaseAddress = new Uri("https://10.0.2.2:44302/")
        };
#else
        Client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44302/")
        };
#endif

#if DEBUG
        var handler = new HttpsClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };
        Client = new HttpClient(handler);
#endif
    }
}

public class HttpsClientHandler : HttpClientHandler
{
    public HttpsClientHandler()
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
    }
}

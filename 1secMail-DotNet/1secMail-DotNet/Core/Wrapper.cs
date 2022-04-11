using OneSecEmailDotNet.Models;
using OneSecEmailDotNet.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OneSecEmailDotNet.Core
{
    public sealed class Wrapper : IDisposable
    {
        public Wrapper(IWebProxy proxy = null)
        {
            var httpClientHandler = new HttpClientHandler();
            if (proxy is not null)
            {
                httpClientHandler.Proxy = proxy;
                httpClientHandler.UseProxy = true;
            }
            client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
            client.BaseAddress = new Uri("https://www.1secmail.com");
        }

        private readonly HttpClient client;

        private readonly string _apiActionPath = $"api/v1/?action=";

        public async Task<Email> CreateAsync()
        {
            using (var res = await client.GetAsync($"{_apiActionPath}genRandomMailbox&count={1}"))
            {
                res.EnsureSuccessStatusCode();
                return JsonHelper.ParseEmail(await res.Content.ReadAsStringAsync())[0];
            }
        }

        public async Task<List<Email>> CreateAsync(uint count)
        {
            using (var res = await client.GetAsync($"{_apiActionPath}genRandomMailbox&count={count}"))
            {
                res.EnsureSuccessStatusCode();
                return JsonHelper.ParseEmail(await res.Content.ReadAsStringAsync());
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}

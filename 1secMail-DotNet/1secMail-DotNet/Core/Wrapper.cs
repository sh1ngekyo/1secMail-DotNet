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
            => client = new HttpClient(
                handler: new HttpClientHandler()
                {
                    Proxy = proxy ?? null,
                    UseProxy = proxy is null ? false : true
                },
                disposeHandler: true)
            { BaseAddress = new Uri("https://www.1secmail.com") };

        private readonly HttpClient client;

        private readonly string _apiActionPath = $"api/v1/?action=";

        public async Task<Email> CreateAsync()
            => JsonHelper.ParseEmail(
                await (await client.GetAsync($"{_apiActionPath}genRandomMailbox&count={1}"))
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsStringAsync())[0];

        public async Task<List<Email>> CreateAsync(uint count)
            => JsonHelper.ParseEmail(
                await (await client.GetAsync($"{_apiActionPath}genRandomMailbox&count={count}"))
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsStringAsync());

        public async Task UpdateEmailAsync(Email email) 
            => await JsonHelper.GetAllId(
                await (await client.GetAsync($"{_apiActionPath}getMessages&login={email.Name}&domain={email.Domain}"))
                .Content
                .ReadAsStringAsync())
            .Where(id 
                => !email.Messages.Any(message => id == message.Id))
            .ToList()
            .ForEachAsync(async (id) 
                => email.Messages.Add(item: await GetMessageByIdAsync(email, id)));

        public async Task<EmailMessage> GetMessageByIdAsync(Email email, int id)
            => JsonHelper.Parse<EmailMessage>(
                await (await client.GetAsync($"{_apiActionPath}readMessage&login={email.Name}&domain={email.Domain}&id={id}"))
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsStringAsync());

        public void Dispose()
            => client.Dispose();
    }
}

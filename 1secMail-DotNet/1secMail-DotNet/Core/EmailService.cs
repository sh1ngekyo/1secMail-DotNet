using OneSecEmailDotNet.Models;
using OneSecEmailDotNet.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneSecEmailDotNet.Core
{ 
    /// <summary>
    /// Service for managing temporary mailboxes
    /// </summary>
    public sealed class EmailService : IDisposable
    {
        public EmailService(IWebProxy proxy = null)
            => _client = new HttpClient(
                handler: new HttpClientHandler()
                {
                    Proxy = proxy ?? null,
                    UseProxy = proxy is null ? false : true
                },
                disposeHandler: true)
            { BaseAddress = new Uri("https://www.1secmail.com") };

        /// <summary>
        /// HttpClient instance
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Global API URI part
        /// </summary>
        private readonly string _apiActionPath = $"api/v1/?action=";

        /// <summary>
        /// Create single mailbox
        /// </summary>
        /// <returns>New email(mailbox)</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<Email> CreateAsync()
            => JsonHelper.ParseEmail(
                await (await _client.GetAsync($"{_apiActionPath}genRandomMailbox&count={1}"))
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsStringAsync())[0];

        /// <summary>
        /// Create list of mailboxes
        /// </summary>
        /// <param name="count">Number of emails to generate</param>
        /// <returns>New list of emails(mailboxes)</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<List<Email>> CreateAsync(uint count)
            => JsonHelper.ParseEmail(
                await (await _client.GetAsync($"{_apiActionPath}genRandomMailbox&count={count}"))
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsStringAsync());

        /// <summary>
        /// Check mailbox for update. If there a new messages - add them to the list of messages
        /// </summary>
        /// <param name="email">Mailbox to check</param>
        /// <exception cref="HttpRequestException"></exception>
        public async Task UpdateEmailAsync(Email email)
            => await JsonHelper.GetAllId(
                await (await _client.GetAsync($"{_apiActionPath}getMessages&login={email.Name}&domain={email.Domain}"))
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsStringAsync())
            .Where(id
                => !email.Messages.Any(message => id == message.Id))
            .ToList()
            .ForEachAsync(async (id)
                => email.Messages.Add(item: await GetMessageByIdAsync(email, id)));

        /// <summary>
        /// Get message by certain id
        /// </summary>
        /// <param name="email">Mailbox with message</param>
        /// <param name="id">Message id</param>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<EmailMessage> GetMessageByIdAsync(Email email, int id)
            => JsonHelper.Parse<EmailMessage>(
                await (await _client.GetAsync($"{_apiActionPath}readMessage&login={email.Name}&domain={email.Domain}&id={id}"))
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsStringAsync());

        /// <summary>
        /// Download attachement into currect directory
        /// </summary>
        /// <param name="email">Mailbox with message</param>
        /// <param name="messageId">Message id</param>
        /// <param name="attachmentFullName">FullName of attachment</param>
        /// <exception cref="HttpRequestException"></exception>
        public async Task DownloadAttachmentAsync(Email email, int messageId, string attachmentFullName)
            => await _client.DownloadFileTaskAsync(
                uri: $"{_client.BaseAddress.OriginalString}/{_apiActionPath}download&login={email.Name}&domain={email.Domain}&id={messageId}&file={attachmentFullName}",
                fileName: attachmentFullName);

        public void Dispose()
            => _client.Dispose();
    }
}

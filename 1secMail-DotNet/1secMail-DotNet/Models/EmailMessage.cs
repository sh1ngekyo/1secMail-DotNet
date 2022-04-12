using System;
using System.Collections.Generic;

namespace OneSecEmailDotNet.Models
{
    public class EmailMessage
    {
        /// <summary>
        /// Message id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sender email address
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Receive date
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Receive date as DateTime object
        /// </summary>
        public DateTime DateTime => DateTime.Parse(Date);

        /// <summary>
        /// Message body (html if exists, text otherwise)
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Message body (html)
        /// </summary>
        public string HtmlBody { get; set; }

        /// <summary>
        /// Message body (text)
        /// </summary>
        public string TextBody { get; set; }

        /// <summary>
        /// Attachments list
        /// </summary>
        public List<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
    }
}

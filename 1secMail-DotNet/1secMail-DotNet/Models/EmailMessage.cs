using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSecEmailDotNet.Models
{
    public class EmailMessage
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public DateTime Date { get; set; }

        public string Body { get; set; }

        public string BodyAsHtml { get; set; }

        public string Text { get; set; }

        public List<EmailAttachment> Attachments { get; set; }
    }
}

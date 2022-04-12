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

        public string Date { get; set; }

        public DateTime DateTime => DateTime.Parse(Date);

        public string Body { get; set; }

        public string HtmlBody { get; set; }

        public string TextBody { get; set; }

        public List<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
    }
}

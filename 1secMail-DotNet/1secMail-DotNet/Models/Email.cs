using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSecEmailDotNet.Models
{
    public class Email
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string FullName => Name + "@" + Domain;

        public IEnumerable<EmailMessage> Messages { get; set; }
    }
}

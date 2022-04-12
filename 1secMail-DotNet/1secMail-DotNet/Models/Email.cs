using System.Collections.Generic;

namespace OneSecEmailDotNet.Models
{
    public class Email
    {
        /// <summary>
        /// Name of mailbox
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of domain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Get full email representation
        /// </summary>
        public string FullName => Name + "@" + Domain;

        /// <summary>
        /// List of messages sended to this mailbox
        /// </summary>
        public List<EmailMessage> Messages { get; set; }
    }
}

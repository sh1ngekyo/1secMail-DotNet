namespace OneSecEmailDotNet.Models
{
    public class EmailAttachment
    {
        /// <summary>
        /// File name of attachment
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Type of content
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Size of attachment
        /// </summary>
        public int Size { get; set; }
    }
}

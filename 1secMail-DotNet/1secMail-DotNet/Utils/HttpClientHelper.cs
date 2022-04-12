using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneSecEmailDotNet.Utils
{
    internal static class HttpClientHelper
    {
        public static async Task DownloadFileTaskAsync(this HttpClient client, string uri, string fileName)
        {
            using (var s = await client.GetStreamAsync(uri))
            {
                using (var fs = new FileStream(fileName, FileMode.CreateNew))
                {
                    await s.CopyToAsync(fs);
                }
            }
        }
    }
}

using OneSecEmailDotNet.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneSecEmailDotNet.Utils
{
    public static class JsonHelper
    {
        public static List<Email> ParseEmail(this string jsonContent)
        {
            var list = new List<Email>();
            JsonSerializer.Deserialize<List<string>>(jsonContent).ForEach(email =>
            {
                var parsed = email.Split('@');
                list.Add(new Email
                {
                    Name = parsed[0],
                    Domain = parsed[1]
                });
            });
            return list;
        }
    }
}

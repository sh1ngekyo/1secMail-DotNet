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
                    Domain = parsed[1],
                    Messages = new List<EmailMessage>()
                });
            });
            return list;
        }

        public static List<int> GetAllId(this string jsonContent)
        {
            var idList = new List<int>();
            JsonDocument.Parse(jsonContent).RootElement
                        .EnumerateArray()
                        .ToList()
                        .ForEach(x => idList.Add(x.GetProperty("id").GetInt32()));
            return idList;
        }

        public static T Parse<T>(this string jsonContent)
        {
            return JsonSerializer.Deserialize<T>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}

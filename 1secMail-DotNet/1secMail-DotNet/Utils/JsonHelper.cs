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
        public static Email ParseEmail(this string content)
        {
            var parsed = JsonSerializer.Deserialize<List<string>>(content)[0]
                                       .Split('@');
            return new Email
            {
                Name = parsed[0],
                Domain = parsed[1]
            };
        }
    }
}

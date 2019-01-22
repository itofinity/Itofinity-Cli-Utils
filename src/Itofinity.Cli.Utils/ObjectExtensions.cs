using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Itofinity.Refit.Cli.Utils
{
    public static class ObjectExtensions
    {
        public static void Output(this object value, bool indented, string path, string filter)
        {
            var format = indented ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
            string contents;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                var jobject = JObject.FromObject(value);
                var matches = jobject.SelectTokens(filter);
                var contentsBuffer = new StringBuilder();
                matches.ToList().ForEach(m => contentsBuffer.AppendLine(m.ToString(format)));
                contents = contentsBuffer.ToString();
            }
            else
            {
                contents = Newtonsoft.Json.JsonConvert.SerializeObject(value, format);
            }

            if (!string.IsNullOrWhiteSpace(path))
            {
                File.WriteAllText(path, contents);
            }
            else
            {
                Console.Write($"{contents}");
                Console.WriteLine();
            }
        }
    }
}
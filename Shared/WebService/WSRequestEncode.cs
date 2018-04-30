using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Shared.WebService
{
    public static class WSRequestEncode
    {
        public static void AddParametr(StringBuilder builder, string name, IEnumerable<string> values)
        {
            if (values == null) return;

            foreach (var value in values)
            {
                AddParametr(builder, name, value);
            }
        }

        public static void AddParametr(StringBuilder builder, string name, string value)
        {
            if (builder.Length != 0)
                builder.Append("&");
            builder.Append(name);
            builder.Append("=");
            builder.Append(HttpUtility.UrlEncode(value));
        }
    }
}

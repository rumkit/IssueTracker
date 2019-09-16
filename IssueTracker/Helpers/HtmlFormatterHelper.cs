using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Helpers
{
    public static class HtmlFormatterHelper
    {
        public static string MakeChangeList(string[] changes)
        {
            if(changes.Length == 0)
                return string.Empty;
            var sb = new StringBuilder();
            var changelist = changes.Select(c => $"<li>{c}</li>");
            sb.Append("<ul>");
            foreach (var change in changelist)
            {
                sb.Append(change);
            }
            sb.Append("</ul>");
            return sb.ToString();
        }
    }
}

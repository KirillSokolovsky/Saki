namespace Saki.Framework.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class LoggingExtensions
    {
        public static readonly string ListNewLine = $"{Environment.NewLine}\t-\t";

        public static void INFO(this ILogger log, string listHeader, IEnumerable<string> list)
        {
            var msg = $"{listHeader}{ListNewLine}{string.Join(ListNewLine, list)}";

            log?.INFO(msg);
        }
    }
}

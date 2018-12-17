namespace Saki.Framework.Logging.Simple
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LogItem : ILogItem
    {
        public LogLevel Level { get; set; }

        public DateTime Timestamp { get; set; }

        public string Message { get; set; }
    }
}

namespace Saki.Framework.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ILogItem
    {
        LogLevel Level { get; }

        DateTime Timestamp { get; }

        string Message { get; }
    }
}

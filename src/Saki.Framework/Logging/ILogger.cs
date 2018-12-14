namespace Saki.Framework.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ILogger
    {
        ILogger CreateChildLogger(string name);

        void INFO(string message);

        void ERROR(string message, Exception exception = null);

        void FILE(string message, byte[] fileBytes, string extension, bool isError);

        void ITEM(ILogItem logItem);


        event EventHandler<ILogItem> OnLogItemAdded;

        event EventHandler<ILogItem> OnAnyLogItemAdded;
    }
}

namespace Saki.Framework.Logging.Simple
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SimpleLogger : ILogger
    {
        public event EventHandler<ILogItem> OnLogItemAdded;
        public event EventHandler<ILogItem> OnAnyLogItemAdded;

        private int _tabsCount = 1;

        public SimpleLogger()
        {
        }

        private SimpleLogger(string name, int tabsCount)
        {
            _tabsCount = tabsCount - 1;
            INFO(name);
            _tabsCount++;
        }

        public ILogger CreateChildLogger(string name)
        {
            return new SimpleLogger(name, _tabsCount + 1);
        }

        public void ERROR(string message, Exception exception = null)
        {
            Write(LogLevel.ERROR, $"{message}", exception);
        }

        public void FILE(string message, byte[] fileBytes, string extension, bool isError)
        {
            Write(LogLevel.INFO, $"{message}");
        }

        public void INFO(string message)
        {
            Write(LogLevel.INFO, $"{message}");
        }

        public void ITEM(ILogItem logItem)
        {
            Write(logItem.Level, $"{logItem.Message}");
        }

        private void Write(LogLevel level, string text, Exception exception = null)
        {
            var sb = new StringBuilder();

            foreach (var line in text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                sb.Append('\t', _tabsCount);
                sb.AppendLine(line);
            }

            if (exception != null)
            {
                sb.Append('\t', _tabsCount);
                sb.AppendLine("Exception:");
                foreach (var line in exception.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sb.Append('\t', _tabsCount + 1);
                    sb.AppendLine(line);
                }
            }

            var item = new LogItem { Level = level, Message = sb.ToString(), Timestamp = DateTime.UtcNow };

            Console.Write($"{item.Level} | {sb}");

            OnLogItemAdded?.Invoke(this, item);
            OnAnyLogItemAdded?.Invoke(this, item);
        }
    }
}

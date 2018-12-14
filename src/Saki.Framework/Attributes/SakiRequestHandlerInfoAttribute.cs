namespace Saki.Framework.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SakiRequestHandlerInfoAttribute : Attribute
    {
        public string HandledCommandName { get; set; }
        public Type ProcessingDataType { get; set; }

        public SakiRequestHandlerInfoAttribute(string handledCommandName, Type processingDataType)
        {
            HandledCommandName = handledCommandName;
            ProcessingDataType = processingDataType;
        }
    }
}

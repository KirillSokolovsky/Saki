using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class SakiRequestInfoAttribute : Attribute
    {
        public string CommandName { get; set; }
        public Type DataType { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class SakiRequestHandlerInfoAttribute : Attribute
    {
        public string HandledCommandName { get; set; }
        public Type ProcessingDataType { get; set; }
    }
}

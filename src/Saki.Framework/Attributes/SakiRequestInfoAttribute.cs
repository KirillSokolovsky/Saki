namespace Saki.Framework.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SakiRequestInfoAttribute : Attribute
    {
        public string CommandName { get; set; }
        public Type DataType { get; set; }

        public SakiRequestInfoAttribute(string commandName, Type dataType)
        {
            CommandName = commandName;
            DataType = dataType;
        }
    }
}

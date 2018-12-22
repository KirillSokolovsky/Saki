namespace Saki.Framework.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SakiRequestInfoAttribute : Attribute
    {
        public string CommandName { get; set; }

        public SakiRequestInfoAttribute(string commandName)
        {
            CommandName = commandName;
        }
    }
}

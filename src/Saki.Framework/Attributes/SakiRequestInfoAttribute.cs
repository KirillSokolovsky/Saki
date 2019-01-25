namespace Saki.Framework.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SakiRequestInfoAttribute : Attribute
    {
        public string RequestName { get; set; }

        public SakiRequestInfoAttribute(string requestName)
        {
            RequestName = requestName;
        }
    }
}

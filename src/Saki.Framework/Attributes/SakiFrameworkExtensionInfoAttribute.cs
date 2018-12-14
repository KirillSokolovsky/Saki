namespace Saki.Framework.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class SakiFrameworkExtensionInfoAttribute : Attribute
    {
        public Type InfoProviderType { get; set; }

        public SakiFrameworkExtensionInfoAttribute(Type descriptorType)
        {
            InfoProviderType = descriptorType;
        }
    }
}

namespace Saki.Framework.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class SakiFrameworkDesktopExtensionInfoAttribute : Attribute
    {
        public Type InfoProviderType { get; private set; }

        public SakiFrameworkDesktopExtensionInfoAttribute(Type infoProviderType)
        {
            InfoProviderType = infoProviderType;
        }
    }
}

namespace Saki.Framework.ExtensionsService
{
    using Saki.Framework.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    internal class SakiFrameworkExtensionDescriptor
    {
        private readonly Assembly _assembly;
        private readonly Type _descriptorType;

        public SakiFrameworkExtensionDescriptor(Assembly assembly, SakiFrameworkExtensionInfoAttribute infoAttribute)
        {
            _assembly = assembly;
            _descriptorType = infoAttribute.InfoProviderType;
        }
    }
}

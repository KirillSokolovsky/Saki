namespace Saki.Framework.Services.ExtensionsService.Registered
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Saki.Framework.Logging;

    public class RegisteredCommandsProvider
    {
        public Type ProviderType { get; private set; }
        public RegisteredExtension Extension { get; private set; }
        public RegisteredItemDataType ItemDataType { get; private set; }

        public RegisteredCommandsProvider(Type providerType, RegisteredExtension extension, RegisteredItemDataType itemDataType)
        {
            ProviderType = providerType;
            Extension = extension;
            ItemDataType = itemDataType;
        }

        internal void LogFullTree(ILogger log)
        {
            throw new NotImplementedException();
        }
    }
}

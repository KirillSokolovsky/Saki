namespace Saki.Framework.Services.ExtensionsService.Registered
{
    using Saki.Framework.Internal;
    using Saki.Framework.Logging;
    using Saki.Framework.Result;
    using Saki.Framework.Services.ExtensionsService.Descriptors;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RegisteredCommandsProviders
    {
        private readonly RegisteredItemDataTypes _itemDataTypes;

        public RegisteredCommandsProviders(RegisteredItemDataTypes registeredItemDataTypes)
        {
            _itemDataTypes = registeredItemDataTypes;
        }

        public SakiResult RegisterProviders(RegisteredExtension extension, List<SakiCommandsProviderDescriptor> providerDescriptors, ILogger log)
        {
            var result = new SakiResult();
            if (providerDescriptors == null || providerDescriptors.Count == 0) return result;

            log = log?.CreateChildLogger($"Register {providerDescriptors.Count} commands providers for extension: {extension.ExtensionName}");

            foreach (var providerDescriptor in providerDescriptors)
            {
                var regResult = RegisterProvider(extension, providerDescriptor, log);
                result.AddResult(regResult);
            }

            return result;
        }

        public SakiResult RegisterProvider(RegisteredExtension extension, SakiCommandsProviderDescriptor providerDescriptor, ILogger log)
        {
            log = log?.CreateChildLogger($"Register command providers for ItemDataType: {providerDescriptor.ItemDataType} for extension: {extension.ExtensionName}");
            var result = new SakiResult();

            var regedType = _itemDataTypes.GetRegisteredItemDataTypeForType(providerDescriptor.ItemDataType);

            if (regedType == null)
            {
                var sakiEx = new SakiExtensionsServiceException(nameof(RegisterProvider),
                    $"ItemDataType: {providerDescriptor.ItemDataType} is not registered");
                log?.ERROR($"Erro occurred during registering provider", sakiEx);
                result.AddError(new SakiError(sakiEx));
                return result;
            }

            var provider = new RegisteredCommandsProvider(providerDescriptor.ProviderType, extension, regedType);

            var extName = extension.ExtensionName;

            if (regedType.CommandsProviders.ContainsKey(extName))
            {
                var sakiEx = new SakiExtensionsServiceException(nameof(RegisterProvider),
                    $"There is another provider for ItemDataType: {regedType.ItemDataType} for extension: {extName}");
                log?.ERROR($"Erro occurred during registering provider", sakiEx);
                result.AddError(new SakiError(sakiEx));
                return result;
            }

            regedType.CommandsProviders.Add(extName, provider);

            return result;
        }

        public RegisteredCommandsProvider GetCommandsProviderOrDefault(RegisteredItemDataType itemDataType, string extensionName)
        {
            if (!itemDataType.CommandsProviders.ContainsKey(extensionName)) return null;

            return itemDataType.CommandsProviders[extensionName];
        }
    }
}

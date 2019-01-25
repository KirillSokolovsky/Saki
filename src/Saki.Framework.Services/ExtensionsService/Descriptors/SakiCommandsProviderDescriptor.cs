namespace Saki.Framework.Services.ExtensionsService.Descriptors
{
    using Saki.Framework.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiCommandsProviderDescriptor
    {
        public Type ProviderType { get; private set; }
        public Type ItemDataType { get; private set; }

        public SakiCommandsProviderDescriptor(Type providerType, SakiCommandsProviderAttribute infoAttribute)
        {
            ProviderType = providerType;
            ItemDataType = infoAttribute.ItemDataType;
        }

        public override string ToString()
        {
            return $"Commands provider type: {ProviderType}" +
                $"{Environment.NewLine}ProcessingRequestItemDataType: {ItemDataType}";
        }
    }
}

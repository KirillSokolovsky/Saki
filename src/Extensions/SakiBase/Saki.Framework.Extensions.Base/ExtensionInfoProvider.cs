using Saki.Framework.Attributes;

[assembly:SakiFrameworkExtensionInfo(typeof(Saki.Framework.Extensions.Base.ExtensionInfoProvider))]

namespace Saki.Framework.Extensions.Base
{
    using Saki.Framework.Internal.Interfaces;

    public class ExtensionInfoProvider : ISakiExtensionInfoProvider
    {
        public const string BaseExtensionName = "BaseSakiExtension";
        public string ExtensionName => BaseExtensionName;

    }
}
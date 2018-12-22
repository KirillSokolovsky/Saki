using Saki.Framework.Attributes;

[assembly:SakiFrameworkExtensionInfo(typeof(Saki.Framework.Extensions.WebElements.ExtensionInfoProvider))]

namespace Saki.Framework.Extensions.WebElements
{
    using Saki.Framework.Internal.Interfaces;

    public class ExtensionInfoProvider : ISakiExtensionInfoProvider
    {
        public const string BaseExtensionName = "WebElements";
        public string ExtensionName => BaseExtensionName;

    }
}
using Saki.Framework.Attributes;
[assembly: SakiFrameworkExtensionInfo(typeof(SampleExtension.SampleExtensionInfoProvider))]

namespace SampleExtension
{
    using System;
    using Saki.Framework.Internal.Interfaces;

    public class SampleExtensionInfoProvider : ISakiExtensionInfoProvider
    {
        public virtual string ExtensionName => "SampleExtension";
    }
}

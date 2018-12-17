using Saki.Framework.Attributes;
[assembly: SakiFrameworkExtensionInfo(typeof(SampleExtensionWithDependency.SampleExtensionWithDependencyInfoProvider))]

namespace SampleExtensionWithDependency
{
    using System;
    using Saki.Framework.Internal.Interfaces;
    using SampleExtension;

    public class SampleExtensionWithDependencyInfoProvider : SampleExtensionInfoProvider
    {
        public override string ExtensionName => "SampleExtension";
    }
}

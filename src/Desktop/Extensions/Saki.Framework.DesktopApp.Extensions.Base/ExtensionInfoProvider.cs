using Saki.Framework.Attributes;

[assembly:SakiFrameworkDesktopExtensionInfo(typeof(Saki.Framework.DesktopApp.Extensions.Base.ExtensionInfoProvider))]

namespace Saki.Framework.DesktopApp.Extensions.Base
{
    using System.Collections.Generic;
    using Saki.Framework.Internal.Interfaces;

    public class ExtensionInfoProvider : ISakiDesktopExtensionInfoProvider
    {
        public const string BaseExtensionName = "BaseSakiExtension";
        public string ExtensionName => BaseExtensionName;

        public List<string> ResourceDictionariesPathes => new List<string>
        {
            "pack://application:,,,/Saki.Framework.DesktopApp.Extensions.Base;component/Tree/TreeResourceDictionary.xaml",
            "pack://application:,,,/Saki.Framework.DesktopApp.Extensions.Base;component/Project/ProjectResourceDictionary.xaml"
        };
    }
}
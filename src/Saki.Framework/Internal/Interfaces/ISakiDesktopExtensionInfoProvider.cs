namespace Saki.Framework.Internal.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiDesktopExtensionInfoProvider
    {
        string ExtensionName { get; }
        List<string> ResourceDictionariesPathes { get; }
    }
}

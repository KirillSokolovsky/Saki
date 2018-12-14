namespace Saki.Framework.Internal.Interfaces
{
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    public interface ISakiExtensionsLoadingService
    {
        SakiResult<IEnumerable<Assembly>> LoadExtensions();
    }
}

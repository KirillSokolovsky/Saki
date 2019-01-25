namespace Saki.Framework.DesktopApp.Core
{
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISakiDesktopExtensionsLoadingService : ISakiExtensionsLoadingService
    {
        SakiResult<IEnumerable<Assembly>> LoadSakiDesktopExtensions(string directoryPath, ILogger log);
    }
}

namespace Saki.Framework.NetCoreExtensionsLoader
{
    using Saki.Framework.Internal;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using Saki.Framework.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    public class SakiNetCoreExtensionsLoadingService : BaseSakiExtensionsLoadingService
    {
        public SakiNetCoreExtensionsLoadingService(Container container) 
            : base(container)
        {
        }

        public override SakiResult<IEnumerable<Assembly>> LoadExtensions()
        {
            Assembly.ReflectionOnlyLoadFrom("");

            return null;
        }
    }
}

namespace Saki.Framework.Internal
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using Saki.Framework.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    public abstract class BaseSakiExtensionsLoadingService : ISakiExtensionsLoadingService
    {
        protected readonly Container _container;
        protected readonly ILogger _log;

        public BaseSakiExtensionsLoadingService(Container container)
        {
            _container = container;
            _log = container.GetInstance<ILogger>()?.CreateChildLogger(GetType().Name);
        }

        public abstract SakiResult<IEnumerable<Assembly>> LoadExtensions();

        public virtual bool IsAssemblySakiExtension(Assembly assembly, ILogger log)
        {
            var extAttribute = assembly.GetCustomAttribute<SakiFrameworkExtensionInfoAttribute>();
            if (extAttribute != null)
            {
                var infoProviderType = extAttribute.InfoProviderType;
                log?.INFO($"Found possible extension in {assembly.FullName}");
                log?.INFO($"InfoProviderType: {infoProviderType.FullName}");

                if (!typeof(ISakiExtensionInfoProvider).IsAssignableFrom(infoProviderType))
                    log?.INFO($"{infoProviderType.FullName} doesn't implements ISakiExtensionInfoProvider");
                else if (!infoProviderType.IsClass)
                    log?.INFO($"{infoProviderType.FullName} is not a class");
                else if (!infoProviderType.IsAbstract)
                    log?.INFO($"{infoProviderType.FullName} is an abstract class");
                else if (infoProviderType.GetConstructor(Type.EmptyTypes) == null)
                    log?.INFO($"{infoProviderType.FullName} doesn't implement empty ctor");
                else
                {
                    log.INFO("All seems to be ok for futher loading");
                    return true;
                }
            }
            return false;
        }
    }
}

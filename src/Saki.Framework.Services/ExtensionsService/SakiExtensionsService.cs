namespace Saki.Framework.Services.ExtensionsService
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Internal.ExtensionsInfo;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using Saki.Framework.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class SakiExtensionsService : ISakiExtensionsService
    {
        private readonly Container _container;
        private readonly ILogger _log;
        private readonly ISakiExtensionsLoadingService _loadingService;

        public SakiExtensionsService(Container container)
        {
            _container = container;
            _log = container.GetInstance<ILogger>()?.CreateChildLogger(nameof(SakiExtensionsService));
            _loadingService = container.GetInstance<ISakiExtensionsLoadingService>();
        }

        public SakiResult LoadExtension(string extensionDirectory)
        {
            var loadedExtAssResult = _loadingService.LoadExtension(extensionDirectory, _log);

            if(loadedExtAssResult.Result != SakiResultType.Ok)
            {
                _log.ERROR("Error occurred during loading extensions");
                return new SakiResult(loadedExtAssResult);
            }

            var loadedAssemblies = loadedExtAssResult.Data.ToList();
            _log.INFO($"Scan {loadedAssemblies.Count} assemblies for requests and handlers:",
                loadedAssemblies.Select(a => a.FullName));

            foreach (var loadedAssembly in loadedAssemblies)
            {
                ScanAssembly(loadedAssembly, _log);
            }

            var allAss = AppDomain.CurrentDomain.GetAssemblies();
            _log?.INFO($"All loaded assemblies. Count {allAss.Length}",
                allAss.Select(a => a.FullName));

            return SakiResult.Ok;
        }

        private void ScanAssembly(Assembly assembly, ILogger log)
        {
            var att = assembly.GetCustomAttribute<SakiFrameworkExtensionInfoAttribute>();

            var type = att.InfoProviderType;

            var infoProvider = (ISakiExtensionInfoProvider)Activator.CreateInstance(type);
            log?.INFO($"Extension name: {infoProvider.ExtensionName}");
            

            var requestInterfaceType = typeof(ISakiRequestForScan);
            var requestHandlerInterfaceType = typeof(ISakiRequestHandlerForScan);

            log = log?.CreateChildLogger($"Scaning assembly: {assembly.FullName}");

            var requestsTypes = assembly.DefinedTypes
                .Where(t => t.IsClass && !t.IsAbstract && requestInterfaceType.IsAssignableFrom(t))
                .ToList();

            log?.INFO($"Found {requestsTypes.Count} requests types:",
                requestsTypes.Select(t => t.FullName));

            var requestHandlersTypes = assembly.DefinedTypes
                .Where(t => t.IsClass && !t.IsAbstract && requestHandlerInterfaceType.IsAssignableFrom(t))
                .ToList();

            log?.INFO($"Found {requestsTypes.Count} requests handlers types:",
                requestHandlersTypes.Select(t => t.FullName));
        }

        public SakiExtensionsInfo GetInfo()
        {
            throw new NotImplementedException();
        }
        public SakiResult<ISakiRequestHandler<ISakiRequest<TResult>, TResult>> FindHandler<TResult>(string extensionName, ISakiRequest<TResult> request) where TResult : ISakiResult
        {
            throw new NotImplementedException();
        }
    }
}

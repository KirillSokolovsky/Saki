namespace Saki.Framework.DesktopApp.Core
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using Saki.Framework.Result;
    using Saki.Framework.Services.ExtensionsService;
    using Saki.Framework.Services.ExtensionsService.Descriptors;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

    public class SakiDesktopExtensionsService : SakiExtensionsService, ISakiDesktopExtensionsService
    {
        private readonly ILogger _log;
        private readonly ISakiDesktopExtensionsLoadingService _loadingService;

        public SakiDesktopExtensionsService(Container container) : base(container)
        {
            _log = container.GetInstance<ILogger>()?.CreateChildLogger(nameof(SakiDesktopExtensionsService));
            _loadingService = container.GetInstance<ISakiDesktopExtensionsLoadingService>();
        }

        public SakiResult LoadDesktopExtension(string extensionDirectory)
        {
            List<Assembly> loadedAssemblies = null;
            if (extensionDirectory != null)
            {
                var loadedExtAssResult = _loadingService.LoadSakiDesktopExtensions(extensionDirectory, _log);

                if (loadedExtAssResult.Result != SakiResultType.Ok)
                {
                    _log.ERROR("Error occurred during loading desktop extensions");
                    return new SakiResult(loadedExtAssResult);
                }
                loadedAssemblies = loadedExtAssResult.Data.ToList();
            }
            else
            {
                var allAss = AppDomain.CurrentDomain.GetAssemblies();
                loadedAssemblies = allAss.Where(a => a.GetCustomAttribute<SakiFrameworkDesktopExtensionInfoAttribute>() != null)
                    .ToList();

                if (loadedAssemblies.Count == 0)
                    return SakiResult.Ok;
            }

            foreach (var loadedAssembly in loadedAssemblies)
            {
                var att = loadedAssembly.GetCustomAttribute<SakiFrameworkDesktopExtensionInfoAttribute>();
                var attType = att.InfoProviderType;
                var infoObj = Activator.CreateInstance(attType) as ISakiDesktopExtensionInfoProvider;

                foreach (var resPath in infoObj.ResourceDictionariesPathes)
                {
                    ResourceDictionary resources = new ResourceDictionary();
                    resources.Source = new Uri(resPath, UriKind.RelativeOrAbsolute);
                    Application.Current.Resources.MergedDictionaries.Add(resources);
                }
            }

            return SakiResult.Ok;
        }


        public ISakiResult<ISakiCommandProcessor<TCommand>> GetCommandProcessor<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ISakiCommand
        {
            throw new NotImplementedException();
        }
    }
}

namespace Saki.Framework.ExtensionsService
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Inerfaces;
    using Saki.Framework.Internal.ExtensionsInfo;
    using Saki.Framework.Internal.Inerfaces;
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

        public SakiExtensionsService(Container container)
        {
            _container = container;
            _log = container.GetInstance<ILogger>()?.CreateChildLogger(nameof(SakiExtensionsService));
        }

        public SakiResult LoadExtension(string extensionDirectory)
        {
            var log = _log?.CreateChildLogger($"TryLoadExtension: {extensionDirectory}");

            var di = new DirectoryInfo(extensionDirectory);
            if (!di.Exists)
            {
                var ex = new SakiExtensionsServiceException(
                    nameof(LoadExtension),
                    $"Directory {extensionDirectory} doesn't exists");
                log?.ERROR(ex.Message);
                return SakiResult.FromEx(ex);
            }

            var dllsFileInfos = di.GetFiles("*.dll");
            log?.INFO($"Found: {dllsFileInfos.Length} dlls:", dllsFileInfos.Select(f => f.Name));

            var scanLog = log.CreateChildLogger("Scan dlls for possible extensions");

            var possibleExtensionsFileInfos = new List<FileInfo>();

            foreach (var dllFileInfo in dllsFileInfos)
            {
                scanLog?.INFO($"Dll file: {dllFileInfo.Name}");
                var assembly = Assembly.ReflectionOnlyLoadFrom(dllFileInfo.FullName);
                var extAttribute = assembly.GetCustomAttribute<SakiFrameworkExtensionInfoAttribute>();
                if (extAttribute != null)
                {
                    var infoProviderType = extAttribute.InfoProviderType;
                    scanLog?.INFO($"Found possible extension in {dllFileInfo.Name}");
                    scanLog?.INFO($"InfoProviderType: {infoProviderType.FullName}");

                    if (!typeof(ISakiExtensionInfoProvider).IsAssignableFrom(infoProviderType))
                        scanLog?.INFO($"{infoProviderType.FullName} doesn't implements ISakiExtensionInfoProvider");
                    else if (!infoProviderType.IsClass)
                        scanLog?.INFO($"{infoProviderType.FullName} is not a class");
                    else if (!infoProviderType.IsAbstract)
                        scanLog?.INFO($"{infoProviderType.FullName} is an abstract class");
                    else if (infoProviderType.GetConstructor(Type.EmptyTypes) == null)
                        scanLog?.INFO($"{infoProviderType.FullName} doesn't implement empty ctor");
                    else
                    {
                        scanLog.INFO("All seems to be ok for futher loading");
                        possibleExtensionsFileInfos.Add(dllFileInfo);
                    }
                }
            }

            if (possibleExtensionsFileInfos.Count == 0)
            {
                var ex = new SakiExtensionsServiceException(
                    nameof(LoadExtension),
                    $"Directory {extensionDirectory} doesn't contains any posible extension dlls");
                log?.ERROR(ex.Message);
                return SakiResult.FromEx(ex);
            }

            log?.INFO($"Try Load assemblies from dll. Count: {possibleExtensionsFileInfos.Count}:", possibleExtensionsFileInfos.Select(f => f.Name));

            var isAnyLoaded = false;
            foreach (var possibleExtensionsFileInfo in possibleExtensionsFileInfos)
            {
                var loadResult = LoadExtension(possibleExtensionsFileInfo, log);
                isAnyLoaded |= loadResult.Result == SakiResultType.Ok;
            }

            if (!isAnyLoaded)
            {
                var ex = new SakiExtensionsServiceException(
                    nameof(LoadExtension),
                    $"Extensions dlls for loading were not found in directory {extensionDirectory}");
                log?.ERROR(ex.Message);
                return SakiResult.FromEx(ex);
            }

            return SakiResult.Ok;
        }
        public SakiResult LoadExtension(FileInfo dllFileInfo, ILogger log)
        {
            log = log?.CreateChildLogger($"Try load dll: {dllFileInfo.Name}");
            //var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllFileInfo.FullName);
            var assembly = Assembly.Load(dllFileInfo.FullName);
            var attr = assembly.GetCustomAttribute<SakiFrameworkExtensionInfoAttribute>();

            var provider = (ISakiExtensionInfoProvider)Activator.CreateInstance(attr.InfoProviderType);
            var name = provider.ExtensionName;

            return null;
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

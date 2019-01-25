namespace Saki.Framework.DesktopApp.Core
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Internal;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using Saki.Framework.NetExtensionsLoader;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class SakiDesktopExtensionsLoadingService : SakiNetExtensionsLoadingService, ISakiDesktopExtensionsLoadingService
    {
        public bool IsDllSakiDesktopExtension(FileInfo dllFileInfo, ILogger scanLog)
        {
            var reqAttTypeName = typeof(SakiFrameworkDesktopExtensionInfoAttribute).FullName;
            Assembly assembly = null;

            try
            {
                assembly = Assembly.ReflectionOnlyLoadFrom(dllFileInfo.FullName);
            }
            catch (FileLoadException)
            {
                scanLog?.INFO($"Same assembly to {dllFileInfo.Name} has been already loaded");
                return false;
            }

            var customAtts = assembly.GetCustomAttributesData();

            var extAttribute = customAtts.FirstOrDefault(ca => ca.AttributeType.FullName == reqAttTypeName);

            if (extAttribute != null)
            {
                scanLog?.INFO($"Found possible desktop extension in {dllFileInfo.Name}");
                return true;
            }
            else
            {
                return false;
            }
        }

        public SakiResult<IEnumerable<Assembly>> LoadSakiDesktopExtensions(string extensionDirectory, ILogger log)
        {
            log = log?.CreateChildLogger($"TryLoadExtension from directory: {extensionDirectory}");

            var di = new DirectoryInfo(extensionDirectory);
            if (!di.Exists)
            {
                var ex = new SakiExtensionsServiceException(
                    nameof(LoadSakiDesktopExtensions),
                    $"Directory {extensionDirectory} doesn't exists");
                log?.ERROR(ex.Message);
                return SakiResult<IEnumerable<Assembly>>.FromEx(ex);
            }

            var dllsFileInfos = di.GetFiles("*.dll").ToList();
            log?.INFO($"Found: {dllsFileInfos.Count} dlls:", dllsFileInfos.Select(f => f.Name));

            var scanLog = log?.CreateChildLogger("Scan dlls for possible desktop extensions");

            var possibleExtensionsFileInfos = new List<FileInfo>();

            foreach (var dllFileInfo in dllsFileInfos)
            {
                scanLog?.INFO($"Dll file: {dllFileInfo.Name}");
                if (IsDllSakiDesktopExtension(dllFileInfo, scanLog))
                    possibleExtensionsFileInfos.Add(dllFileInfo);
            }

            if (possibleExtensionsFileInfos.Count == 0)
            {
                var ex = new SakiExtensionsServiceException(
                    nameof(LoadExtension),
                    $"Directory {extensionDirectory} doesn't contains any posible desktop extension dlls");
                log?.ERROR(ex.Message);
                return SakiResult<IEnumerable<Assembly>>.FromEx(ex);
            }

            log?.INFO($"Try Load assemblies from dll. Count: {possibleExtensionsFileInfos.Count}:", possibleExtensionsFileInfos.Select(f => f.Name));

            Func<Assembly, ILogger, bool> loadingConditions = (loadedAssembly, loadingLog) =>
            {
                Type infoProviderType = loadedAssembly.GetCustomAttribute<SakiFrameworkDesktopExtensionInfoAttribute>()
                    .InfoProviderType;
                loadingLog?.INFO($"InfoProviderType: {infoProviderType.FullName}");

                if (!typeof(ISakiDesktopExtensionInfoProvider).IsAssignableFrom(infoProviderType))
                    loadingLog?.INFO($"{infoProviderType.FullName} doesn't implements ISakiDesktopExtensionInfoProvider");
                else if (!infoProviderType.IsClass)
                    loadingLog?.INFO($"{infoProviderType.FullName} is not a class");
                else if (infoProviderType.IsAbstract)
                    loadingLog?.INFO($"{infoProviderType.FullName} is an abstract class");
                else if (infoProviderType.GetConstructor(Type.EmptyTypes) == null)
                    loadingLog?.INFO($"{infoProviderType.FullName} doesn't implement empty ctor");
                else
                    return true;
                return false;
            };

            var loadResult = LoadAssembliesWithResolving(loadingConditions, possibleExtensionsFileInfos, dllsFileInfos, log);
            return loadResult;
        }
    }
}

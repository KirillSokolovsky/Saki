namespace Saki.Framework.Internal
{
    using Saki.Framework.Attributes;
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

    public abstract class BaseSakiExtensionsLoadingService : ISakiExtensionsLoadingService
    {
        public SakiResult<IEnumerable<Assembly>> LoadExtension(string extensionDirectory, ILogger log)
        {
            log = log?.CreateChildLogger($"TryLoadExtension from directory: {extensionDirectory}");

            var di = new DirectoryInfo(extensionDirectory);
            if (!di.Exists)
            {
                var ex = new SakiExtensionsServiceException(
                    nameof(LoadExtension),
                    $"Directory {extensionDirectory} doesn't exists");
                log?.ERROR(ex.Message);
                return SakiResult<IEnumerable<Assembly>>.FromEx(ex);
            }

            var dllsFileInfos = di.GetFiles("*.dll").ToList();
            log?.INFO($"Found: {dllsFileInfos.Count} dlls:", dllsFileInfos.Select(f => f.Name));

            var scanLog = log?.CreateChildLogger("Scan dlls for possible extensions");

            var possibleExtensionsFileInfos = new List<FileInfo>();

            foreach (var dllFileInfo in dllsFileInfos)
            {
                scanLog?.INFO($"Dll file: {dllFileInfo.Name}");
                if (IsAssemblySakiExtension(dllFileInfo, scanLog))
                    possibleExtensionsFileInfos.Add(dllFileInfo);
            }

            if (possibleExtensionsFileInfos.Count == 0)
            {
                var ex = new SakiExtensionsServiceException(
                    nameof(LoadExtension),
                    $"Directory {extensionDirectory} doesn't contains any posible extension dlls");
                log?.ERROR(ex.Message);
                return SakiResult<IEnumerable<Assembly>>.FromEx(ex);
            }

            log?.INFO($"Try Load assemblies from dll. Count: {possibleExtensionsFileInfos.Count}:", possibleExtensionsFileInfos.Select(f => f.Name));


            var loadResult = LoadAssembliesWithResolving(possibleExtensionsFileInfos, dllsFileInfos, log);
            return loadResult;
        }


        protected abstract bool IsAssemblySakiExtension(FileInfo dllFileInfo, ILogger scanLog);

        protected abstract SakiResult<IEnumerable<Assembly>> LoadAssembliesWithResolving(List<FileInfo> assembliesToLoad, List<FileInfo> allAssemblies, ILogger log);
    }
}

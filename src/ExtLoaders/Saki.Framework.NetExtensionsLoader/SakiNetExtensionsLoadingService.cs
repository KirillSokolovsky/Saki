namespace Saki.Framework.NetExtensionsLoader
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Internal;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class SakiNetExtensionsLoadingService : BaseSakiExtensionsLoadingService
    {
        protected override bool IsAssemblySakiExtension(FileInfo dllFileInfo, ILogger scanLog)
        {
            var reqAttTypeName = typeof(SakiFrameworkExtensionInfoAttribute).FullName;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
            Assembly assembly = null;

            try
            {
                assembly = Assembly.ReflectionOnlyLoadFrom(dllFileInfo.FullName);
            }
            catch(FileLoadException)
            {
                scanLog?.INFO($"Same assembly to {dllFileInfo.Name} has been already loaded");
                return false;
            }

            var customAtts = assembly.GetCustomAttributesData();

            var extAttribute = customAtts.FirstOrDefault(ca => ca.AttributeType.FullName == reqAttTypeName);

            if(extAttribute != null)
            {
                scanLog?.INFO($"Found possible extension in {dllFileInfo.Name}");
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override SakiResult<IEnumerable<Assembly>> LoadAssembliesWithResolving(List<FileInfo> assembliesToLoad, List<FileInfo> allAssemblies, ILogger log)
        {
            _allDllFileInfos = allAssemblies;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            var loadedAssemblies = new List<Assembly>();

            foreach (var assemblyToLoad in assembliesToLoad)
            {
                var loadingLog = log?.CreateChildLogger($"Loading: {assemblyToLoad.Name}");
                var loadedAssembly = Assembly.LoadFrom(assemblyToLoad.FullName);

                Type infoProviderType = loadedAssembly.GetCustomAttribute<SakiFrameworkExtensionInfoAttribute>()
                    .InfoProviderType;
                loadingLog?.INFO($"InfoProviderType: {infoProviderType.FullName}");

                if (!typeof(ISakiExtensionInfoProvider).IsAssignableFrom(infoProviderType))
                    loadingLog?.INFO($"{infoProviderType.FullName} doesn't implements ISakiExtensionInfoProvider");
                else if (!infoProviderType.IsClass)
                    loadingLog?.INFO($"{infoProviderType.FullName} is not a class");
                else if (infoProviderType.IsAbstract)
                    loadingLog?.INFO($"{infoProviderType.FullName} is an abstract class");
                else if (infoProviderType.GetConstructor(Type.EmptyTypes) == null)
                    loadingLog?.INFO($"{infoProviderType.FullName} doesn't implement empty ctor");
                else
                {
                    loadingLog?.INFO($"Assembly: {loadedAssembly.FullName} loaded and available for futher scanning");
                    loadedAssemblies.Add(loadedAssembly);
                }
            }

            if(loadedAssemblies.Count == 0)
            {
                var ex = new SakiExtensionsServiceException(
                    nameof(LoadAssembliesWithResolving),
                    $"There are no loaded extension assemblies");
                log?.ERROR(ex.Message);
                return SakiResult<IEnumerable<Assembly>>.FromEx(ex);
            }

            return SakiResult<IEnumerable<Assembly>>.Ok(loadedAssemblies);
        }

        private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                var resolved = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().FirstOrDefault(a => a.FullName == args.Name);

                if (resolved != null)
                    return resolved;

                resolved = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
                if (resolved != null)
                {
                    var reflectionOnly = Assembly.ReflectionOnlyLoadFrom(resolved.CodeBase);
                    return reflectionOnly;
                }

                return null;
            }
            catch(Exception)
            {
                return null;
            }
        }

        private List<FileInfo> _allDllFileInfos;
        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var resolved = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            return resolved;
        }
    }
}

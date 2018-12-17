﻿namespace Saki.Framework.NetCoreExtensionsLoader
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Internal;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using Saki.Framework.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Loader;
    using System.Text;
    using Dnlib = dnlib.DotNet;

    public class SakiNetCoreExtensionsLoadingService : BaseSakiExtensionsLoadingService
    {
        protected override bool IsAssemblySakiExtension(FileInfo dllFileInfo, ILogger scanLog)
        {
            var reqAttTypeName = typeof(SakiFrameworkExtensionInfoAttribute).FullName;

            var module = Dnlib.ModuleDefMD.Load(dllFileInfo.FullName);

            if (AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName == module.Assembly.FullName))
                return false;

            var extAttribute = module.Assembly.CustomAttributes.Find(reqAttTypeName);
            if (extAttribute != null)
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
            var loadedAssemblies = new List<Assembly>();

            foreach (var assemblyToLoad in assembliesToLoad)
            {
                var loadingLog = log?.CreateChildLogger($"Loading: {assemblyToLoad.Name}");
                var loadedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyToLoad.FullName);


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

            if (loadedAssemblies.Count == 0)
            {
                var ex = new SakiExtensionsServiceException(
                    nameof(LoadAssembliesWithResolving),
                    $"There are no loaded extension assemblies");
                log?.ERROR(ex.Message);
                return SakiResult<IEnumerable<Assembly>>.FromEx(ex);
            }

            return SakiResult<IEnumerable<Assembly>>.Ok(loadedAssemblies);
        }
    }
}

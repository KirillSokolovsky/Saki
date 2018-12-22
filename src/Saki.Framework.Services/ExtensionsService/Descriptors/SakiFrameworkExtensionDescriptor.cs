namespace Saki.Framework.Services.ExtensionsService.Descriptors
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    internal class SakiFrameworkExtensionDescriptor
    {
        private static readonly Type RequestInterfaceHandlerType = typeof(ISakiRequestHandlerForScan);
        private static readonly Type ItemDataType = typeof(ISakiTreeItemData);

        private readonly Assembly _assembly;

        private readonly Type _descriptorType;
        private ISakiExtensionInfoProvider _extInfoProvider;

        public string ExtensionName { get; private set; }
        public List<SakiRequestHandlerDescriptor> RequestHandlersDescriptors { get; private set; }
        public List<Type> ItemDataTypes { get; private set; }

        public SakiFrameworkExtensionDescriptor(Assembly assembly, SakiFrameworkExtensionInfoAttribute infoAttribute)
        {
            _assembly = assembly;
            _descriptorType = infoAttribute.InfoProviderType;

            RequestHandlersDescriptors = new List<SakiRequestHandlerDescriptor>();
            ItemDataTypes = new List<Type>();
        }

        public void ScanExtensionAssembly(ILogger log)
        {
            log = log?.CreateChildLogger($"Scanning extension assembly: {_assembly.FullName}");

            log?.INFO("Creating info provider object");
            _extInfoProvider = (ISakiExtensionInfoProvider)Activator.CreateInstance(_descriptorType);
            ExtensionName = _extInfoProvider.ExtensionName;
            log?.INFO($"Extension name: {ExtensionName}");

            ScanForRequestsHandlers(log);
            ScanForItemDataTypes(log);
        }

        private void ScanForRequestsHandlers(ILogger log)
        {
            log = log?.CreateChildLogger("Scan for requests handlers");

            var requestsHandlerTypes = _assembly.DefinedTypes
                .Where(t => t.IsClass && !t.IsAbstract && RequestInterfaceHandlerType.IsAssignableFrom(t))
                .ToList();

            log?.INFO($"Found {requestsHandlerTypes.Count} possible request handlers types");

            foreach (var requestHandlerType in requestsHandlerTypes)
            {
                log?.INFO($"Scan type: {requestHandlerType}");
                var infoAtt = requestHandlerType.GetCustomAttribute<SakiRequestHandlerInfoAttribute>();
                if (infoAtt == null)
                {
                    log?.INFO($"There is no requrired attribute {nameof(SakiRequestHandlerInfoAttribute)}");
                    continue;
                }

                var descriptor = new SakiRequestHandlerDescriptor(requestHandlerType, infoAtt);
                RequestHandlersDescriptors.Add(descriptor);
            }
        }

        private void ScanForItemDataTypes(ILogger log)
        {
            log = log?.CreateChildLogger("Scan for item data types");

            var itemDataTypes = _assembly.DefinedTypes
                .Where(t => t.IsClass && !t.IsAbstract && ItemDataType.IsAssignableFrom(t))
                .ToList();

            log?.INFO($"Found {itemDataTypes.Count} possible item data types:",
                itemDataTypes.Select(t => t.FullName));

            ItemDataTypes.AddRange(itemDataTypes);
        }
    }
}

namespace Saki.Framework.Services.ExtensionsService
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Exceptions;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Internal;
    using Saki.Framework.Internal.ExtensionsInfo;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using Saki.Framework.Result;
    using Saki.Framework.Services.ExtensionsService.Descriptors;
    using Saki.Framework.Services.ExtensionsService.Registered;
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

        private RegisteredItemDataTypes _itemDataTypes;
        private RegisteredExtensions _extensions;
        private RegisteredHandlers _handlers;

        public SakiExtensionsService(Container container)
        {
            _container = container;
            _log = container.GetInstance<ILogger>()?.CreateChildLogger(nameof(SakiExtensionsService));
            _loadingService = container.GetInstance<ISakiExtensionsLoadingService>();

            _itemDataTypes = new RegisteredItemDataTypes();
            _handlers = new RegisteredHandlers(_itemDataTypes);
            _extensions = new RegisteredExtensions();
        }

        public SakiResult LoadExtension(string extensionDirectory)
        {
            List<Assembly> loadedAssemblies = null;
            if (extensionDirectory != null)
            {
                var loadedExtAssResult = _loadingService.LoadExtension(extensionDirectory, _log);

                if (loadedExtAssResult.Result != SakiResultType.Ok)
                {
                    _log.ERROR("Error occurred during loading extensions");
                    return new SakiResult(loadedExtAssResult);
                }
                loadedAssemblies = loadedExtAssResult.Data.ToList();
            }
            else
            {
                var allAss = AppDomain.CurrentDomain.GetAssemblies();
                loadedAssemblies = allAss.Where(a => a.GetCustomAttribute<SakiFrameworkExtensionInfoAttribute>() != null)
                    .ToList();

                if (loadedAssemblies.Count == 0)
                    return SakiResult.Ok;
            }

            var result = new SakiResult();
            var log = _log.CreateChildLogger($"Scan {loadedAssemblies.Count} assemblies for requests and handlers");
            foreach (var loadedAssembly in loadedAssemblies)
            {
                var scanResult = ScanAssembly(loadedAssembly, log);
                if (scanResult.Result != SakiResultType.Ok)
                {
                    result.AddResult(scanResult);
                    continue;
                }

                var registeringResult = RegisterExtensionItems(scanResult.Data, log);
            }

            _itemDataTypes.LogFullTree(_log.CreateChildLogger("Registered item data types"));

            return result;
        }

        private SakiResult<SakiFrameworkExtensionDescriptor> ScanAssembly(Assembly assembly, ILogger log)
        {
            var att = assembly.GetCustomAttribute<SakiFrameworkExtensionInfoAttribute>();
            var descriptor = new SakiFrameworkExtensionDescriptor(assembly, att);

            try
            {
                descriptor.ScanExtensionAssembly(log);
                return SakiResult<SakiFrameworkExtensionDescriptor>.Ok(descriptor);
            }
            catch (Exception ex)
            {
                var sakiEx = new SakiExtensionsServiceException(nameof(ScanAssembly),
                    $"Error occurred during scaning extension assembly: {assembly}", ex);
                return SakiResult<SakiFrameworkExtensionDescriptor>.FromEx(sakiEx);
            }
        }

        private SakiResult RegisterExtensionItems(SakiFrameworkExtensionDescriptor descriptor, ILogger log)
        {
            log = log?.CreateChildLogger($"Register extension items: {descriptor.ExtensionName}");
            var result = new SakiResult();

            _itemDataTypes.RegisterNewItemDataTypes(descriptor.ItemDataTypes);

            var regExtension = _extensions.GetOrAdd(descriptor.ExtensionName);

            var regResult = _handlers.RegisterHandlers(regExtension, descriptor.RequestHandlersDescriptors, log);
            result.AddResult(regResult);

            return result;
        }

        public SakiResult<object> FindHandler<TResult>(ISakiRequest<TResult> request)
            where TResult : ISakiResult
        {
            var result = new SakiResult<object>();

            var requestType = request.GetType();
            var itemDataType = Type.GetType(request.ItemDataType);

            if (itemDataType == null)
            {
                var ex = new SakiHandlerResolvingException(nameof(FindHandler),
                    $"Couldn't find type: {itemDataType} for request with type: {requestType} | {request.ExtensionName} | {request.CommandName}");
                result.AddError(new SakiError(ex));
                return result;
            }

            var regDataType = _itemDataTypes.GetRegisteredItemDataTypeForType(itemDataType);
            if (regDataType == null)
            {
                var ex = new SakiHandlerResolvingException(nameof(FindHandler),
                    $"Request with type: {requestType} has not registered ItemDataType: {itemDataType}");
                result.AddError(new SakiError(ex));
                return result;
            }

            RegisteredHandler handler = null;
            while (regDataType != null)
            {
                RegisteredCommand regCommand = regDataType.GetCommandOrDefault(request.CommandName);
                if (regCommand == null)
                {
                    regDataType = regDataType.ParentRegisteredType;
                    continue;
                }

                handler = regCommand.GetHandler(request.ExtensionName);

                if (handler == null)
                {
                    regDataType = regDataType.ParentRegisteredType;
                    continue;
                }

                break;
            }

            if (handler == null)
            {
                var ex = new SakiHandlerResolvingException(nameof(FindHandler),
                    $"Couldn't find handler for request with type: {requestType}");
                result.AddError(new SakiError(ex));
                return result;
            }

            var handlerType = handler.HandlerType;
            object handlerObj = null;

            if (handlerType.IsGenericType)
            {
                handlerType = handlerType.MakeGenericType(requestType.GetGenericArguments().First());
                handlerObj = _container.GetInstance(handlerType);
            }
            else
            {
                handlerObj = _container.GetInstance(handlerType);
            }

            result.SetData(handlerObj);

            return result;
        }
    }
}

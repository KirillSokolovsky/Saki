using Saki.Framework.Internal;
using Saki.Framework.Logging;
using Saki.Framework.Result;
using Saki.Framework.Services.ExtensionsService.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saki.Framework.Services.ExtensionsService.Registered
{
    public class RegisteredHandlers
    {
        private readonly RegisteredItemDataTypes _itemDataTypes;

        public RegisteredHandlers(RegisteredItemDataTypes registeredItemDataTypes)
        {
            _itemDataTypes = registeredItemDataTypes;
        }

        public SakiResult RegisterHandlers(RegisteredExtension extension, List<SakiRequestHandlerDescriptor> handlers, ILogger log)
        {
            log = log?.CreateChildLogger($"Register handlers for extension: {extension}");
            var result = new SakiResult();

            foreach (var handler in handlers)
            {
                var regResult = RegisterHandler(extension, handler, log);
                result.AddResult(regResult);
            }

            return result;
        }

        private SakiResult RegisterHandler(RegisteredExtension extension, SakiRequestHandlerDescriptor handler, ILogger log)
        {
            log = log?.CreateChildLogger($"Register handler: {handler.HandlerType}");
            var result = new SakiResult();

            var regItemDataType = _itemDataTypes.GetRegisteredItemDataTypeForType(handler.ProcessingRequestItemDataType);
            if (regItemDataType == null)
            {
                var sakiEx = new SakiExtensionsServiceException(nameof(RegisterHandler),
                    $"There is no registered item data type for handler: {handler}");
                log?.ERROR($"Erro occurred during registering handler", sakiEx);
                result.AddError(new SakiError(sakiEx));
                return result;
            }

            var regCommand = regItemDataType.GetOrAddRequest(handler.HandledCommandName);

            var regHandler = new RegisteredHandler(handler.HandlerType, extension, regItemDataType);

            var regHandlerResult = regCommand.AddHandlerToRequest(regHandler, log);
            result.AddResult(regHandlerResult);

            return result;
        }
    }
}

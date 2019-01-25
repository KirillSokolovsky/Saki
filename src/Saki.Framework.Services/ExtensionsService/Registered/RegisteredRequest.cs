namespace Saki.Framework.Services.ExtensionsService.Registered
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Saki.Framework.Internal;
    using Saki.Framework.Logging;
    using Saki.Framework.Result;

    public class RegisteredRequest
    {
        public string RequestName { get; private set; }
        public RegisteredItemDataType ItemDataType { get; private set; }

        private List<RegisteredHandler> _handlers = new List<RegisteredHandler>();


        public RegisteredRequest(string commandName, RegisteredItemDataType itemDataType)
        {
            RequestName = commandName;
            ItemDataType = itemDataType;
        }

        public SakiResult AddHandlerToRequest(RegisteredHandler regHandler, ILogger log)
        {
            var existed = _handlers.FirstOrDefault(h => h.Extension == regHandler.Extension);
            if (existed != null)
            {
                var ex = new SakiExtensionsServiceException(nameof(AddHandlerToRequest),
                    $"Couldn't add handler for request, another handler for the same extension exists.{Environment.NewLine}" +
                    $"Handler to add:{regHandler}" +
                    $"Existed handler:{existed}");
                log?.ERROR(ex.Message);
                return SakiResult.FromEx(ex);
            }

            regHandler.SetRequest(this);
            _handlers.Add(regHandler);

            return SakiResult.Ok;
        }

        public void LogFullTree(ILogger log)
        {
            log = log?.CreateChildLogger($"Request: {RequestName}");

            foreach (var handler in _handlers)
            {
                log?.INFO($"{handler.Extension.ExtensionName} | {handler.HandlerType}");
            }
        }

        internal RegisteredHandler GetHandler(string extensionName)
        {
            return _handlers.FirstOrDefault(h => h.Extension.ExtensionName == extensionName);
        }
    }
}

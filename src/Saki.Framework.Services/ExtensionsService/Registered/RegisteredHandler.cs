namespace Saki.Framework.Services.ExtensionsService.Registered
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RegisteredHandler
    {
        public Type HandlerType { get; private set; }
        public RegisteredExtension Extension { get; private set; }
        public RegisteredItemDataType ItemDataType { get; private set; }
        public RegisteredRequest Request { get; private set; }

        public RegisteredHandler(Type handlerType, RegisteredExtension extension, RegisteredItemDataType itemDataType)
        {
            HandlerType = handlerType;
            Extension = extension;
            ItemDataType = itemDataType;
        }

        public void SetRequest(RegisteredRequest registeredCommand)
        {
            Request = registeredCommand;
        }
    }
}

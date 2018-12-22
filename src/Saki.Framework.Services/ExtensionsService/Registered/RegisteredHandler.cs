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
        public RegisteredCommand Command { get; private set; }

        public RegisteredHandler(Type handlerType, RegisteredExtension extension, RegisteredItemDataType itemDataType)
        {
            HandlerType = handlerType;
            Extension = extension;
            ItemDataType = itemDataType;
        }

        public void SetCommand(RegisteredCommand registeredCommand)
        {
            Command = registeredCommand;
        }
    }
}

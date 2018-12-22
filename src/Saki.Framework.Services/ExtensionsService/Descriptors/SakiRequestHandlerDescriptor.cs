namespace Saki.Framework.Services.ExtensionsService.Descriptors
{
    using Saki.Framework.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiRequestHandlerDescriptor
    {
        public string HandledCommandName { get; private set; }
        public Type HandlerType { get; private set; }
        public Type ProcessingRequestItemDataType { get; private set; }

        public SakiRequestHandlerDescriptor(Type handlerType, SakiRequestHandlerInfoAttribute infoAttribute)
        {
            HandlerType = handlerType;
            HandledCommandName = infoAttribute.HandledCommandName;
            ProcessingRequestItemDataType = infoAttribute.ProcessingDataType;
        }

        public override string ToString()
        {
            return $"HandledCommandName: {HandledCommandName}" +
                $"{Environment.NewLine}HandlerType: {HandlerType}" +
                $"{Environment.NewLine}ProcessingRequestItemDataType: {ProcessingRequestItemDataType}";
        }
    }
}

namespace Saki.Framework.ExtensionsService
{
    using Saki.Framework.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class SakiRequestHandlerDescriptor
    {
        private readonly Type _handlerType;
        private readonly string _handledCommandName;
        private readonly Type _processingRequestItemDataType;

        public SakiRequestHandlerDescriptor(Type handlerType, SakiRequestHandlerInfoAttribute infoAttribute)
        {
            _handlerType = handlerType;
            _handledCommandName = infoAttribute.HandledCommandName;
            _processingRequestItemDataType = infoAttribute.ProcessingDataType;
        }
    }
}

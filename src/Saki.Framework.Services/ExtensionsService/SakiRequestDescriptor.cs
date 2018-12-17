namespace Saki.Framework.Services.ExtensionsService
{
    using Saki.Framework.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class SakiRequestDescriptor
    {
        private readonly Type _requestType;
        private readonly string _commandName;
        private readonly Type _requestItemDataType;

        public SakiRequestDescriptor(Type requestType, SakiRequestInfoAttribute infoAttribute)
        {
            _requestType = requestType;
            _commandName = infoAttribute.CommandName;
            _requestItemDataType = infoAttribute.DataType;
        }
    }
}

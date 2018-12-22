namespace Saki.Framework.Services.ExtensionsService.Registered
{
    using Saki.Framework.Interfaces;
    using Saki.Framework.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RegisteredItemDataTypes
    {
        public static readonly Type BaseSakiTreeItemDataType = typeof(ISakiTreeItemData);
        private readonly RegisteredItemDataType _rootType;

        public RegisteredItemDataTypes()
        {
            _rootType = new RegisteredItemDataType(BaseSakiTreeItemDataType);
        }

        public void RegisterNewItemDataTypes(List<Type> itemDataTypes)
        {
            var itemDataTypesToAdd = itemDataTypes
                .SelectMany(t => t.GetParentTypesTillParent(BaseSakiTreeItemDataType))
                .ToList();
            itemDataTypes.ForEach(idt => _rootType.RegisterItemDataType(idt));
        }

        public RegisteredItemDataType GetRegisteredItemDataTypeForType(Type itemDataType)
        {
            return _rootType.GetRegisteredItemDataTypeForType(itemDataType);
        }

        public void LogTree(ILogger log)
        {
            _rootType.LogTypesHierarchy(log);
        }

        public void LogFullTree(ILogger log)
        {
            _rootType.LogFullTree(log);
        }
    }
}

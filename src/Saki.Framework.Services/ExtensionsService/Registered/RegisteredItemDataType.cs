namespace Saki.Framework.Services.ExtensionsService.Registered
{
    using Saki.Framework.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RegisteredItemDataType
    {
        public Type ItemDataType { get; set; }
        public RegisteredItemDataType ParentRegisteredType { get; set; }
        private List<RegisteredItemDataType> _childRegisteredTypes { get; set; } = new List<RegisteredItemDataType>();

        private RegisteredCommands _commands { get; set; }

        public RegisteredItemDataType(Type type)
        {
            ItemDataType = type;
            _commands = new RegisteredCommands(this);
        }

        public RegisteredItemDataType RegisterItemDataType(Type newType)
        {
            if (ItemDataType == newType) return this;
            if (!ItemDataType.IsAssignableFrom(newType)) return null;

            RegisteredItemDataType added = null;
            foreach (var node in _childRegisteredTypes)
            {
                added = node.RegisterItemDataType(newType);
                if (added != null) return added;
            }

            added = new RegisteredItemDataType(newType);
            added.ParentRegisteredType = this;

            var childNodes = _childRegisteredTypes.Where(n => newType.IsAssignableFrom(n.ItemDataType))
                .ToList();

            foreach (var childNode in childNodes)
            {
                childNode.ParentRegisteredType = added;
                added._childRegisteredTypes.Add(childNode);
                _childRegisteredTypes.Remove(childNode);
            }

            _childRegisteredTypes.Add(added);

            return added;
        }

        public RegisteredItemDataType GetRegisteredItemDataTypeForType(Type type)
        {
            if (ItemDataType == type) return this;

            return _childRegisteredTypes.FirstOrDefault(n => n.ItemDataType.IsAssignableFrom(type))
                ?.GetRegisteredItemDataTypeForType(type);
        }

        public RegisteredCommand GetOrAddCommand(string commandName)
        {
            return _commands.GetOrAddCommand(commandName);
        }


        public void LogTypesHierarchy(ILogger log)
        {
            log = log.CreateChildLogger($"Type: {ItemDataType}");
            _childRegisteredTypes.ForEach(n => n.LogTypesHierarchy(log));
        }

        public void LogFullTree(ILogger log)
        {
            log = log.CreateChildLogger($"Type: {ItemDataType}");
            _commands.LogFullTree(log);

            log = log?.CreateChildLogger("Child Types:");
            _childRegisteredTypes.ForEach(n => n.LogFullTree(log));
        }

        internal RegisteredCommand GetCommandOrDefault(string commandName)
        {
            return _commands.GetCommandOrDefault(commandName);
        }
    }
}

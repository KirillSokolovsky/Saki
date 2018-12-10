namespace Saki.Core
{
    using Saki.Common.Interfaces;
    using Saki.Core.ExtensionManagement;
    using Saki.Result;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Linq;
    using Saki.Common;

    public class SakiExtensionService : ISakiExtensionsService
    {
        private readonly Container _container;
        private readonly ConcurrentDictionary<string, ISakiFrameworkExension> _extensions;
        private static Type _commandBaseType = typeof(ISakiCommandExecutor<,>);

        public SakiExtensionService(Container container)
        {
            _container = container;
            _extensions = new ConcurrentDictionary<string, ISakiFrameworkExension>();
        }

        public void LoadFrameworkExtension(SakiExtenstionDescriptor extenstionDescriptor)
        {

        }

        public ISakiFrameworkExension FindExtensionWithDerivedExecutor(Type itemType, string commandName)
        {
            ISakiFrameworkExension extension = null;
            var currentCommandType = itemType;
            while (extension == null)
            {
                extension = _extensions.Values.FirstOrDefault(ext => ext.AcceptCommand(currentCommandType));
                if (extension != null) break;

                currentCommandType = itemType.BaseType;
                if (!_commandBaseType.IsAssignableFrom(currentCommandType)) break;
            }

            return extension;
        }

        public ISakiCommandExecutor<TCommand, TResult> GetCommandExecutor<TCommand, TResult>(string itemCategory, string itemType, string commandName)
            where TCommand : ISakiCommand<TResult>
            where TResult : IBaseSakiResult
        {
            ISakiCommandExecutor<TCommand, TResult> executor = null;

            if (_extensions.TryGetValue(itemCategory, out var extension))
            {
                executor = extension.GetCommandExecutor<TCommand, TResult>(itemType, commandName);
            }
            if(executor == null)
            {
                var commandType = Type.GetType(itemType);
                extension = FindExtensionWithDerivedExecutor(commandType, commandName);
                executor = extension.GetCommandExecutor<TCommand, TResult>(itemType, commandName);
            }

            return executor;
        }

        public ISakiAvailableCommandsProvider GetAvailableCommandsProvider(SakiTreeState treeState)
        {
            var targetItem = treeState.TargetItem;
            //if null -> do smt on root

        }
    }
}

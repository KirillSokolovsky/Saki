namespace Saki.Framework.Services.ExtensionsService.Registered
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Saki.Framework.Logging;

    public class RegisteredCommands
    {
        public RegisteredItemDataType ItemDataType { get; private set; }
        private List<RegisteredCommand> _commands = new List<RegisteredCommand>();

        public RegisteredCommands(RegisteredItemDataType itemDataType)
        {
            ItemDataType = itemDataType;
        }

        public RegisteredCommand GetOrAddCommand(string commandName)
        {
            var command = _commands.FirstOrDefault(c => c.CommandName == commandName);
            if (command == null)
            {
                command = new RegisteredCommand(commandName, ItemDataType);
                _commands.Add(command);
            }

            return command;
        }

        public void LogFullTree(ILogger log)
        {
            log = log.CreateChildLogger("Commands:");

            foreach (var command in _commands)
            {
                command.LogFullTree(log);
            }
        }

        internal RegisteredCommand GetCommandOrDefault(string commandName)
        {
            return _commands.FirstOrDefault(c => c.CommandName == commandName);
        }
    }
}

namespace Saki.Core
{
    using Saki.Common;
    using Saki.Common.Interfaces;
    using Saki.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Reflection;
    using System.Threading;

    public class SakiLogicService : ISakiLogicService
    {
        private readonly Container _container;

        public SakiLogicService(Container container)
        {
            _container = container;
        }

        public async Task<TResult> ExecuteCommand<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ISakiCommand<TResult>
            where TResult : IBaseSakiResult
        {
            var extensionService = _container.GetInstance<ISakiExtensionsService>();

            var executor = extensionService.GetCommandExecutor<TCommand, TResult>(command.ItemCategory, command.ItemType, command.CommandName);

            if (executor == null)
            {
                var error = new SakiError("");
                var errorResult = Activator.CreateInstance<TResult>();
                errorResult.AddError(error);
                return errorResult;
            }

            var result = await executor.Execute(command, cancellationToken);

            return result;
        }

        public async Task<ISakiResult<IEnumerable<ISakiAvailableCommand>>> GetAvailableCommands(SakiTreeItem item, SakiTreeState treeState)
        {
            var extensionService = _container.GetInstance<ISakiExtensionsService>();
            var provider = extensionService.GetAvailableCommandsProvider(treeState);

            if (provider == null)
            {
                var error = new SakiError("");
                return new SakiResult<IEnumerable<ISakiAvailableCommand>>(error);
            }

            var result = await provider.GetAvailableCommands(treeState);

            return result;
        }
    }
}

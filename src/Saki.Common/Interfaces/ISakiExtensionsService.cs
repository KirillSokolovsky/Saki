namespace Saki.Common.Interfaces
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiExtensionsService
    {
        ISakiCommandExecutor<TCommand, TResult> GetCommandExecutor<TCommand, TResult>(string itemCategory, string itemType, string commandName)
            where TCommand : ISakiCommand<TResult>
            where TResult : IBaseSakiResult;

        ISakiAvailableCommandsProvider GetAvailableCommandsProvider(SakiTreeState treeState);
    }
}

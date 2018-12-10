namespace Saki.Common.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Saki.Result;

    public interface ISakiFrameworkExension
    {
        ISakiCommandExecutor<TCommand, TResult> GetCommandExecutor<TCommand, TResult>(string itemType, string commandName)
            where TCommand : ISakiCommand<TResult>
            where TResult : IBaseSakiResult;

        bool AcceptCommand(Type commandType);
    }
}

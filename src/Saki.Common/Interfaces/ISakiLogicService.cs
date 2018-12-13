namespace Saki.Common.Interfaces
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISakiLogicService
    {
        Task<TResult> ExecuteCommand<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ISakiCommand<TResult>
            where TResult : IBaseSakiResult;

        Task<ISakiResult<IEnumerable<ISakiAvailableCommand>>> GetAvailableCommands(SakiTreeItem item, SakiTreeState treeState);
    }
}

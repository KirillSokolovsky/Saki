namespace Saki.Framework.Interfaces
{
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISakiMediatorService
    {
        Task<TResult> ProcessRequest<TResult>(ISakiRequest<TResult> request, CancellationToken cancellationToken)
            where TResult : ISakiResult;

        Task<SakiResult<IEnumerable<ISakiCommand>>> GetAvailableCommands(ISakiTreeState treeState, CancellationToken cancellationToken);
    }
}

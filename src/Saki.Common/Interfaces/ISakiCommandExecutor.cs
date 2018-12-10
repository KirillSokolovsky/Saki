namespace Saki.Common.Interfaces
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISakiCommandExecutor<in TCommand, TResult>
        where TCommand : ISakiCommand<TResult>
        where TResult : IBaseSakiResult
    {
        Task<TResult> Execute(TCommand command, CancellationToken cancellationToken);
    }
}

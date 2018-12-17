namespace Saki.Framework.Interfaces
{
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISakiRequestHandler<in TRequest, TResult> : ISakiRequestHandlerForScan
        where TResult : ISakiResult
        where TRequest : ISakiRequest<TResult>
    {
        Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
    }
}

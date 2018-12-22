namespace Saki.Framework.Interfaces
{
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISakiCoreService
    {
        Task<TResult> ProcessRequest<TResult>(ISakiRequest<TResult> request, CancellationToken cancellationToken)
            where TResult : ISakiResult;
    }
}

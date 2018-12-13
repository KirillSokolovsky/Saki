using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    public interface IBaseSakiResult { }
    public interface ISakiResult<out TResult> : IBaseSakiResult { };
    public class SakiResult<TResult> : ISakiResult<TResult> { }

    public interface ISakiRequest<out TResult>
            where TResult : IBaseSakiResult
    {
    }

    public interface ISakiRequestHandler<in TRequest, TResult>
        where TRequest : ISakiRequest<TResult>
        where TResult : IBaseSakiResult
    {
        Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
    }
}

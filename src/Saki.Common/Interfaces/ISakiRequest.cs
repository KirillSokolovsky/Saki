namespace Saki.Common.Interfaces
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISakiRequest<out TResult>
        where TResult : IBaseSakiResult
    {
    }

    public interface ISakiRequestHandler<in TRequest, TResult>
        where TRequest : ISakiRequest<TResult>
        where TResult : IBaseSakiResult
    {
        Task<TResult> Handle(TRequest request);
    }

    public class CreateItemRequest : ISakiRequest<IBaseSakiResult> { };
    public class CreateWebElementRequest : CreateItemRequest { };

    public class CreateItemRequestHandler : ISakiRequestHandler<CreateItemRequest, IBaseSakiResult>
    {
        public Task<IBaseSakiResult> Handle(CreateItemRequest request)
        {
            throw new NotImplementedException();
        }
    };
    public class CreateWebElementRequestHandler : CreateItemRequestHandler, ISakiRequestHandler<CreateWebElementRequest, IBaseSakiResult>
    {
        public Task<IBaseSakiResult> Handle(CreateWebElementRequest request)
        {
            
            throw new NotImplementedException();
        }
    };
}

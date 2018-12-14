using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    public class Mediator
    {
        private Container _container;

        public Mediator()
        {
            _container = new Container();
        }

        public async Task<TResult> ProcessRequest<TResult>(ISakiRequest<TResult> request, CancellationToken cancellationToken)
            where TResult : IBaseSakiResult
        {
            var findHandlerResult = GetHandler(request);
            
            if(findHandlerResult.Result != null)
            {
                var handler = findHandlerResult.Result;
                var requestResult = await handler.Handle(request, cancellationToken);

                return requestResult;
            }

            return default(TResult);
        }

        public ISakiRequestHandler<TRequest, TResult> FindRequestHadnlerFor<TRequest, TResult>(TRequest request)
            where TRequest : ISakiRequest<TResult>
            where TResult : IBaseSakiResult
        {
            //what i need
            //command type
            //command data argument type


            return null;
        }

        public SakiResult<ISakiRequestHandler<ISakiRequest<TResult>, TResult>> GetHandler<TResult>(ISakiRequest<TResult> request)
            where TResult : IBaseSakiResult
        {
            return null;
        }

        private Dictionary<string, List<Type>> _commandTypeToHandlers = new Dictionary<string, List<Type>>();

        public void ScanAssembly(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();

            var requestTypes = allTypes.Where(t => t.GetCustomAttribute<SakiRequestInfoAttribute>() != null)
                .ToList();

            var hadnlerTypes = allTypes.Where(t => t.GetCustomAttribute<SakiRequestHandlerInfoAttribute>() != null)
                .ToList();

            foreach (var handlerType in hadnlerTypes)
            {

                _container.Register(handlerType);
            }
        }
    }
}

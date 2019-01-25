namespace Saki.Framework.Services.MediatorService
{
    using Saki.Framework.Interfaces;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class SakiMediatorService : ISakiMediatorService
    {
        private readonly Container _container;
        private readonly ISakiExtensionsService _extensionsService;

        public SakiMediatorService(Container container)
        {
            _container = container;
            _extensionsService = _container.GetInstance<ISakiExtensionsService>();
        }

        public async Task<TResult> ProcessRequest<TResult>(ISakiRequest<TResult> request, CancellationToken cancellationToken) 
            where TResult : ISakiResult
        {
            var handlerResult = _extensionsService.FindHandler(request);
            if (handlerResult.Result != SakiResultType.Ok)
            {
                var result = Activator.CreateInstance<TResult>();
                result.AddResult(handlerResult);
                return result;
            }

            var handleMethod = handlerResult.Data.GetType().GetMethod("Handle");

            var processingTask = (Task<TResult>)handleMethod.Invoke(handlerResult.Data, new object[] { request, cancellationToken });
            var processingResult = await processingTask;

            return processingResult;
        }
        

        public async Task<SakiResult<IEnumerable<ISakiCommand>>> GetAvailableCommands(ISakiTreeState treeState, CancellationToken cancellationToken)
        {
            var commandProvider = _extensionsService.FindCommandsProvider(treeState.CurrentItem);

            return await commandProvider.Data.GetAvailableCommands(treeState, cancellationToken);
        }
    }
}

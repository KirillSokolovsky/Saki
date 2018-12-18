namespace Saki.Framework.SakiTree.Commands.Create
{
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class CreateTreeItemRequestHandler 
        : ISakiRequestHandler<ICreateTreeItemRequest<ISakiTreeItem<ISakiTreeItemData>>, SakiResult<int>>
    {
        protected readonly ISakiTreeRepositoryService _repositoryService;

        public CreateTreeItemRequestHandler(ISakiTreeRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public Task<SakiResult<int>> Handle(ICreateTreeItemRequest<ISakiTreeItem<ISakiTreeItemData>> request, CancellationToken cancellationToken)
        {
            
            _repositoryService.CreateItem(request.Item)
        }
    }
}

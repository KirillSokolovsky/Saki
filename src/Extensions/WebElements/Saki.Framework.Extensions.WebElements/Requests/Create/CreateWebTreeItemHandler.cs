namespace Saki.Framework.Extensions.WebElements.Requests.Create
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Base.SakiTree.Requests;
    using Saki.Framework.Base.SakiTree.Requests.Create;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    [SakiRequestHandlerInfo(TreeItemRequestsNames.CreateTreeItemRequestName, typeof(BaseWebTreeItemData))]
    public class CreateWebTreeItemHandler : BaseTreeItemRequestHandler,
        ISakiRequestHandler<ICreateWebTreeItemRequest<ISakiTreeItem<IWebTreeItemData>>, ISakiResult<int>>
    {
        private readonly CreateTreeItemRequestHandler _createTreeItemRequestHandler;

        public CreateWebTreeItemHandler(Container container) 
            : base(container)
        {
            _createTreeItemRequestHandler = _container.GetInstance<CreateTreeItemRequestHandler>();
        }

        public Task<ISakiResult<int>> Handle(ICreateWebTreeItemRequest<ISakiTreeItem<IWebTreeItemData>> request, CancellationToken cancellationToken)
        {
            return _createTreeItemRequestHandler.Handle(request, cancellationToken);
        }
    }
}

namespace Saki.Framework.Extensions.WebElements.Commands.Create
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Base.SakiTree.Commands;
    using Saki.Framework.Base.SakiTree.Commands.Create;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    [SakiRequestHandlerInfo(TreeItemCommandNames.CreateTreeItemCommandName, typeof(BaseWebTreeItemData))]
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

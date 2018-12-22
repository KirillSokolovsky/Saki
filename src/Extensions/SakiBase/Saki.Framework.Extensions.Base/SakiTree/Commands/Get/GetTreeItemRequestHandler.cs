namespace Saki.Framework.Base.SakiTree.Commands.Get
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using Saki.Framework.SakiTree;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    [SakiRequestHandlerInfo(TreeItemCommandNames.GetTreeItemCommandName, typeof(ISakiTreeItemData))]
    public class GetTreeItemRequestHandler<TData> : BaseTreeItemRequestHandler,
        ISakiRequestHandler<GetTreeItemRequest<TData>, SakiResult<SakiTreeItem<TData>>>
        where TData : ISakiTreeItemData
    {
        public GetTreeItemRequestHandler(Container container)
            : base(container)
        {
        }

        public async Task<SakiResult<SakiTreeItem<TData>>> Handle(GetTreeItemRequest<TData> request, CancellationToken cancellationToken)
        {
            var result = await _repositoryService.GetItem<TData>(request.ItemId);
            return result;
        }
    }
}

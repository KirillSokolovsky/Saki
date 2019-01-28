namespace Saki.Framework.Base.SakiTree.Requests.GetChildren
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Saki.Framework.Attributes;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using SimpleInjector;

    [SakiRequestHandlerInfo(TreeItemRequestsNames.GetChildTreeItemsRequestName, typeof(ISakiTreeItemData))]
    public class GetChildTreeItemsRequestHandler : BaseTreeItemRequestHandler,
        ISakiRequestHandler<GetChildTreeItemsRequest, SakiResult<IEnumerable<ISakiResult<ISakiTreeItem<ISakiTreeItemData>>>>>
    {
        public GetChildTreeItemsRequestHandler(Container container) : base(container)
        {
        }

        public async Task<SakiResult<IEnumerable<ISakiResult<ISakiTreeItem<ISakiTreeItemData>>>>> 
            Handle(GetChildTreeItemsRequest request, CancellationToken cancellationToken)
        {
            var result = await _repositoryService.GetChildItems(request.ParentItemId);
            return result;
        }
    }
}

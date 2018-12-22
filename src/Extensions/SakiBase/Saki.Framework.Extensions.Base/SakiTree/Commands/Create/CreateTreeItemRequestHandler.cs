namespace Saki.Framework.Base.SakiTree.Commands.Create
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Exceptions;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using Saki.Framework.SakiTree;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    [SakiRequestHandlerInfo(TreeItemCommandNames.CreateTreeItemCommandName, typeof(ISakiTreeItemData))]
    public class CreateTreeItemRequestHandler : BaseTreeItemRequestHandler,
        ISakiRequestHandler<ICreateTreeItemRequest<ISakiTreeItem<ISakiTreeItemData>>, ISakiResult<int>>
    {
        public CreateTreeItemRequestHandler(Container container)
            : base(container)
        {
        }

        public async Task<ISakiResult<int>> Handle(ICreateTreeItemRequest<ISakiTreeItem<ISakiTreeItemData>> request, CancellationToken cancellationToken)
        {
            var itemNamesResult = await _repositoryService.GetChildItemNames(request.ParentItemId);

            if (itemNamesResult.Result != SakiResultType.Ok)
                return new SakiResult<int>(itemNamesResult);

            var name = request.Item.Data.Name;
            if (string.IsNullOrWhiteSpace(name))
            {
                var ex = new SakiException($"{nameof(CreateTreeItemRequestHandler)}.{nameof(Handle)}",
                    $"Item with name: {name} already exists");
                return SakiResult<int>.FromEx(ex);
            }

            var createResult = await _repositoryService
                .CreateItem(request.ExtensionName, request.Item.Data, request.ParentItemId);

            return createResult;
        }
    }
}

namespace Saki.Framework.Extensions.WebElements.WebElement.Commands.Create
{
    using Saki.Framework.Base.SakiTree.Commands;
    using Saki.Framework.Extensions.WebElements.Commands.Create;
    using Saki.Framework.Extensions.WebElements.WebElementsRoot;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using Saki.Framework.SakiTree;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateWebElementTreeItemHandler : BaseTreeItemRequestHandler,
        ISakiRequestHandler<CreateWebElementTreeItemRequest, ISakiResult<int>>
    {
        private readonly CreateWebTreeItemHandler _createWebTreeItemHandler;

        public CreateWebElementTreeItemHandler(Container container)
            : base(container)
        {
            _createWebTreeItemHandler = container.GetInstance<CreateWebTreeItemHandler>();
        }

        public async Task<ISakiResult<int>> Handle(CreateWebElementTreeItemRequest request, CancellationToken cancellationToken)
        {
            var ascendtants = await _repositoryService.GetAscendantItems(request.ParentItemId, typeof(WebElementsRootTreeItemData).GetSakiTypeName());



            return await _createWebTreeItemHandler.Handle(request, cancellationToken);
        }
    }
}

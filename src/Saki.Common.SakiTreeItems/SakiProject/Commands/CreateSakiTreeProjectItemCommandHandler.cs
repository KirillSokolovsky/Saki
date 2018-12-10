namespace Saki.Common.SakiTreeItems.SakiProject.Commands
{
    using Saki.Common.Commands;
    using Saki.Common.Validators;
    using Saki.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateSakiTreeProjectItemCommandHandler : CreateTreeItemCommandExecutor
    {
        public CreateSakiTreeProjectItemCommandHandler(Container container) 
            : base(container)
        {
        }

        public override async Task<ISakiResult<SakiTreeItem>> Handle(CreateTreeItemCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentId != 0)
                return new SakiResult<SakiTreeItem>(new SakiError($"Item with type: {request.ItemType} couldn't have parent"));

            return await base.Handle(request, cancellationToken);
        }
    }
}

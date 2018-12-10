namespace Saki.Common.Commands
{
    using Saki.Common.Interfaces;
    using Saki.Common.Validators;
    using Saki.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateTreeItemCommandExecutor
        : BaseTreeItemCommandExecutor,
        ISakiCommandExecutor<CreateTreeItemCommand, ISakiResult<SakiTreeItem>>
    {
        public CreateTreeItemCommandExecutor(Container container) 
            : base(container)
        {
        }

        public virtual async Task<ISakiResult<SakiTreeItem>> Execute(CreateTreeItemCommand command, CancellationToken cancellationToken)
        {
            var validator = new UniqueNameValidator(_treeRepository);
            var validationResult = await validator.ValidateForCreating(command.Name, command.ParentId, cancellationToken);
            if (validationResult.HasErrors)
                return new SakiResult<SakiTreeItem>(validationResult.Errors);

            var item = new SakiTreeItem
            {
                Name = command.Name,
                Description = command.Description,
                InnerType = command.ItemType,
                ItemCategory = command.ItemCategory
            };

            var response = await _treeRepository.CreateItem(item, command.ParentId, cancellationToken);

            return response;
        }
    }
}

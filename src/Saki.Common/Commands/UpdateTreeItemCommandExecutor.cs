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

    public class UpdateTreeItemCommandExecutor
        : BaseTreeItemCommandExecutor, 
        ISakiCommandExecutor<UpdateTreeItemCommand, ISakiResult<SakiTreeItem>>
    {
        public UpdateTreeItemCommandExecutor(Container container) 
            : base(container)
        {
        }

        public async Task<ISakiResult<SakiTreeItem>> Execute(UpdateTreeItemCommand command, CancellationToken cancellationToken)
        {
            var validator = new UniqueNameValidator(_treeRepository);
            var validationResult = await validator.ValidateForUpdating(command.UpdatedName, command.ItemId, command.ParentId, cancellationToken);
            if (validationResult.HasErrors)
                return new SakiResult<SakiTreeItem>(validationResult.Errors);

            var updatedItem = new SakiTreeItem
            {
                Name = command.UpdatedName,
                Description = command.UpdatedDescription,
                InnerType = command.ItemType,
                ItemCategory = command.ItemCategory
            };

            var response = await _treeRepository.UpdateItem(command.ItemId, updatedItem, cancellationToken);

            return response;
        }
    }
}

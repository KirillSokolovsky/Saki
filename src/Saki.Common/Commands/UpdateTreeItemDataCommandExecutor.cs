namespace Saki.Common.Commands
{
    using Saki.Common.Interfaces;
    using Saki.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateTreeItemDataCommandExecutor<TData>
        : BaseTreeItemCommandExecutor,
        ISakiCommandExecutor<UpdateTreeItemDataCommand<TData>, ISakiResult>
        where TData : ISakiTreeItemData
    {
        public UpdateTreeItemDataCommandExecutor(Container container) 
            : base(container)
        {
        }

        public async Task<ISakiResult> Execute(UpdateTreeItemDataCommand<TData> command, CancellationToken cancellationToken)
        {
            var item = new SakiTreeItem<TData>
            {
                Id = command.ItemId,
                InnerType = command.ItemType,
                ItemCategory = command.ItemCategory,
                Data = command.Data
            };

            var result = await _treeRepository.UpdateItemData(item, cancellationToken);

            return result;
        }
    }
}

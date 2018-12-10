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

    public class GetTreeItemDataCommandExecutor<TData>
        : BaseTreeItemCommandExecutor,
        ISakiCommandExecutor<GetTreeItemDataCommand<TData>, ISakiResult<TData>>
        where TData : ISakiTreeItemData
    {
        public GetTreeItemDataCommandExecutor(Container container)
            : base(container)
        {
        }

        public async Task<ISakiResult<TData>> Execute(GetTreeItemDataCommand<TData> command, CancellationToken cancellationToken)
        {
            var item = new SakiTreeItem<TData>
            {
                Id = command.ItemId
            };

            var result = await _treeRepository.GetItemData(item, cancellationToken);

            return result;
        }
    }
}

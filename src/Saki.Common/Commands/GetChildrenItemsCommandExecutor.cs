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

    public class GetChildrenItemsCommandExecutor
        : BaseTreeItemCommandExecutor,
        ISakiCommandExecutor<GetChildrenItemsCommand, ISakiResult<IEnumerable<SakiTreeItem>>>
    {
        public GetChildrenItemsCommandExecutor(Container container)
            : base(container)
        {
        }

        public async Task<ISakiResult<IEnumerable<SakiTreeItem>>> Execute(GetChildrenItemsCommand command, CancellationToken cancellationToken)
        {
            var result = await _treeRepository.GetChildrenItems(command.ParentItemId, cancellationToken);
            return result;
        }
    }
}

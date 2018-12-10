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

    public class DeleteTreeItemCommandExecutor
        : BaseTreeItemCommandExecutor,
        ISakiCommandExecutor<DeleteTreeItemCommand, ISakiResult>
    {
        public DeleteTreeItemCommandExecutor(Container container)
            : base(container)
        {
        }

        public async Task<ISakiResult> Execute(DeleteTreeItemCommand command, CancellationToken cancellationToken)
        {
            var result = await _treeRepository.DeleteItem(command.ItemId, command.Recursive, cancellationToken);
            return result;
        }
    }
}

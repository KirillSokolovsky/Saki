namespace Saki.Framework.DesktopApp.Core
{
    using Saki.Framework.DesktopApp.Extensions.Base.Tree;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISakiDesktopExtensionsService : ISakiExtensionsService
    {
        ISakiResult<ISakiCommandProcessor<TCommand>> GetCommandProcessor<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ISakiCommand;

        ISakiResult<ISakiTreeItem<ISakiTreeItemData>> ConvertFromModel(TreeItemViewModel model);
        ISakiResult<TreeItemViewModel> ConvertToModel(ISakiTreeItem<ISakiTreeItemData> treeItem);
    }
}

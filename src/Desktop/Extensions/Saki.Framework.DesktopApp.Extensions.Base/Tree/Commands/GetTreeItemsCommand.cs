namespace Saki.Framework.DesktopApp.Extensions.Base.Tree.Commands
{
    using Saki.Framework.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GetTreeItemsCommand : ISakiCommand
    {
        public string CommandName => "GetTreeItems";

        public string Description => "Retrieve items from storage";

        public ISakiTreeState TreeState { get; private set; }

        public GetTreeItemsCommand(ISakiTreeState treeState)
        {
            TreeState = treeState;
        }
    }
}

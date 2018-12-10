namespace Saki.Common.Commands
{
    using Saki.Common.Interfaces;
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GetChildrenItemsCommand : BaseTreeItemCommand, ISakiCommand<ISakiResult<IEnumerable<SakiTreeItem>>>
    {
        public const string GetChildrenItemsCommandName = "GetChildrenItems";
        public override string CommandName => GetChildrenItemsCommandName;

        public int ParentItemId { get; set; }
    }
}

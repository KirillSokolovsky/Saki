namespace Saki.Common.Commands
{
    using Saki.Common.Interfaces;
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DeleteTreeItemCommand : BaseTreeItemCommand, ISakiCommand<ISakiResult>
    {
        public const string DeleteTreeItemCommandName = "DeleteTreeItem";
        public override string CommandName => DeleteTreeItemCommandName;

        public int ItemId { get; set; }
        public bool Recursive { get; set; } = false;
    }
}

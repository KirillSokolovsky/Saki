namespace Saki.Common.Commands
{
    using Saki.Common.Interfaces;
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UpdateTreeItemCommand : BaseTreeItemCommand, ISakiCommand<ISakiResult<SakiTreeItem>>
    {
        public const string UpdateItemCommandName = "UpdateTreeItem";
        public override string CommandName => UpdateItemCommandName;

        public string UpdatedName { get; set; }
        public string UpdatedDescription { get; set; }
        public int ItemId { get; set; }
        public int ParentId { get; set; }
    }
}

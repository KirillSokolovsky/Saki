namespace Saki.Common.Commands
{
    using Saki.Common.Interfaces;
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateTreeItemCommand : BaseTreeItemCommand, ISakiCommand<ISakiResult<SakiTreeItem>>
    {
        public const string CreateTreeItemCommandName = "CreateTreeItem";
        public override string CommandName => CreateTreeItemCommandName;

        public string Name { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; } = 0;
    }
}

namespace Saki.Common.Commands
{
    using Saki.Common.Interfaces;
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GetTreeItemDataCommand<TData> : BaseTreeItemCommand, ISakiCommand<ISakiResult<TData>>
        where TData : ISakiTreeItemData
    {
        public const string GetTteeItemCommandName = "GetTreeItemData";
        public override string CommandName => GetTteeItemCommandName;

        public int ItemId { get; set; }
    }
}

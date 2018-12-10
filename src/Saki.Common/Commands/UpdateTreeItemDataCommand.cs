namespace Saki.Common.Commands
{
    using Saki.Common.Interfaces;
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UpdateTreeItemDataCommand<TData> : BaseTreeItemCommand, ISakiCommand<ISakiResult>
        where TData : ISakiTreeItemData
    {
        public const string UpdateTreeItemDataCommandName = "UpdateTreeItemData";
        public override string CommandName => UpdateTreeItemDataCommandName;

        public TData Data { get; set; }
        public int ItemId { get; set; }
    }
}

namespace Saki.Framework.Base.SakiTree.Commands.Get
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using Saki.Framework.SakiTree;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GetTreeItemRequest<TData> : ISakiRequest<SakiResult<SakiTreeItem<TData>>>
        where TData : ISakiTreeItemData
    {
        public int ItemId { get; protected set; }
        public string ExtensionName { get; protected set; }
        public string ItemDataType => typeof(TData).GetSakiTypeName();
        public string CommandName => TreeItemCommandNames.GetTreeItemCommandName;

        public GetTreeItemRequest(string extensionName, int itemId)
        {
            ExtensionName = extensionName;
            ItemId = itemId;
        }
    }
}

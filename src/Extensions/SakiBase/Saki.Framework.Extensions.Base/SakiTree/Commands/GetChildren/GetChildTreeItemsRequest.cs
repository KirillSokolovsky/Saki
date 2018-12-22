namespace Saki.Framework.Base.SakiTree.Commands.GetChildren
{
    using Saki.Framework.Attributes;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GetChildTreeItemsRequest 
        : ISakiRequest<SakiResult<IEnumerable<ISakiResult<ISakiTreeItem<ISakiTreeItemData>>>>>
    {
        public int ParentItemId { get; protected set; }
        public string ExtensionName { get; protected set; }
        public string ItemDataType => typeof(ISakiTreeItemData).GetSakiTypeName();
        public string CommandName => TreeItemCommandNames.GetChildTreeItemsCommandName;

        public GetChildTreeItemsRequest(string extensionName, int parentItemId)
        {
            ParentItemId = parentItemId;
            ExtensionName = extensionName;
        }
    }
}

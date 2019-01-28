namespace Saki.Framework.Base.SakiTree.Requests.Create
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Saki.Framework.Attributes;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;

    public abstract class CreateTreeItemRequest<TItem> : ICreateTreeItemRequest<ISakiTreeItem<ISakiTreeItemData>>
        where TItem : class, ISakiTreeItem<ISakiTreeItemData>
    {
        public TItem Item { get; protected set; }
        public string ExtensionName { get; protected set; }
        public string ItemDataType => Item.Data.GetType().GetSakiTypeName();
        public string CommandName => TreeItemRequestsNames.CreateTreeItemRequestName;
        public int ParentItemId { get; protected set; }

        ISakiTreeItem<ISakiTreeItemData> ICreateTreeItemRequest<ISakiTreeItem<ISakiTreeItemData>>.Item => Item;


        public CreateTreeItemRequest(string extensionName, TItem item, int parentItemId)
        {
            ExtensionName = extensionName;
            ParentItemId = parentItemId;
            Item = item;
        }
    }
}

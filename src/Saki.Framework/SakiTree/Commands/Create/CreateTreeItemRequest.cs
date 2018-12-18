namespace Saki.Framework.SakiTree.Commands.Create
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;

    public class CreateTreeItemRequest<TData> : ICreateTreeItemRequest<ISakiTreeItem<TData>>
        where TData : ISakiTreeItemData
    {
        public string ExtensionName { get; protected set; }

        public string ItemDataType { get; protected set; }

        public SakiTreeItem<TData> Item { get; protected set; }

        public CreateTreeItemRequest(string extensionName, TItem item)
        {
            ExtensionName = extensionName;
            Item = item;
        }
    }
}

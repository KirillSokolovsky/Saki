namespace Saki.Framework.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiTreeItem<out TData>
        where TData : ISakiTreeItemData
    {
        int ItemId { get; }
        int ParentId { get; }

        string ExtensionName { get; }
        string ItemDataType { get; }

        TData Data { get; }
    }
}

namespace Saki.Common.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiTreeItem<out TData>
        where TData : ISakiTreeItemData
    {
        int ItemId { get; }
        string ItemCategory { get; }
        string ItemType { get; }

        int ParentId { get; }

        TData Data { get; }
    }
}

namespace Saki.Framework.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiTreeItem<out TData>
        where TData : ISakiTreeItemData
    {
        int ItemId { get; }
        string ItemCategory { get; }
        string ItemDataType { get; }

        int ParentId { get; }

        int DataId { get; }
        TData Data { get; }
    }
}

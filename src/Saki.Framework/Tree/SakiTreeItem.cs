namespace Saki.Framework.Tree
{
    using Saki.Framework.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiTreeItem<TData> : ISakiTreeItem<TData>
        where TData : ISakiTreeItemData
    {
        public int ItemId { get; set; }

        public string ItemCategory { get; set; }

        public string ItemDataType { get; set; }

        public int ParentId { get; set; }

        public int DataId { get; set; }

        public TData Data { get; set; }
    }
}

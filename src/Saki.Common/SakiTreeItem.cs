namespace Saki.Common
{
    using Saki.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiTreeItem<TData> : ISakiTreeItem<TData>
        where TData : ISakiTreeItemData
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string ItemCategory { get; set; }
        public string ItemType { get; set; }

        public int ParentId { get; set; }
        public int DataId { get; set; }
        public TData Data { get; set; }
    }

    public class SakiTreeItem : SakiTreeItem<SakiTreeItemData>
    {

    }
}

namespace Saki.Common
{
    using Saki.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiTreeItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string ItemCategory { get; set; }
        public string InnerType { get; set; }

        public int? DataId { get; set; }
        public int? ParentId { get; set; }
    }

    public class SakiTreeItem<TItemData> : SakiTreeItem
        where TItemData : ISakiTreeItemData
    {
        public TItemData Data { get; set; }
    }
}

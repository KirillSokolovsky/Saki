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


        public int ItemCategory { get; set; }
        public int ItemType { get; set; }

        public int? DataId { get; set; }
        public int? ParentId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class SakiTreeItem<TItemData> : SakiTreeItem
        where TItemData : ISakiTreeItemData
    {
        public TItemData Data { get; set; }
    }
}

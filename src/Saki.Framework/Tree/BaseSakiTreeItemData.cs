namespace Saki.Framework.Tree
{
    using Saki.Framework.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class BaseSakiTreeItemData : ISakiTreeItemData
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

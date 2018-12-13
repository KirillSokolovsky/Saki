namespace Saki.Common
{
    using Saki.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiTreeItemData : ISakiTreeItemData
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

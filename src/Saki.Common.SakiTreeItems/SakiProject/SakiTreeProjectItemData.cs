namespace Saki.Common.SakiTreeItems.SakiProject
{
    using Saki.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiTreeProjectItemData : ISakiTreeItemData
    {
        public int ItemId { get; set; }
        public string ProjectDescription { get; set; }
    }
}

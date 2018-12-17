namespace SampleExtensionWithDependency
{
    using Saki.Framework.Interfaces;
    using Saki.Framework.SakiTree;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SampleWdItemData : BaseSakiTreeItemData
    {
        public string SampleWdStrProp { get; set; }
        public double SampleWdDoubProp { get; set; }
    }
}

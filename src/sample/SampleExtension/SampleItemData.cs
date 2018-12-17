namespace SampleExtension
{
    using Saki.Framework.Interfaces;
    using Saki.Framework.SakiTree;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SampleItemData : BaseSakiTreeItemData
    {
        public string SampleStrProp { get; set; }
        public bool SampleBoolProp { get; set; }
    }
}

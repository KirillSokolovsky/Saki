namespace Saki.Framework.Extensions.WebElements
{
    using Saki.Framework.SakiTree;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BaseWebTreeItemData : BaseSakiTreeItemData, IWebTreeItemData
    {
        public WebElementType ElementType { get; set; }
    }
}

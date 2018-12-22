namespace Saki.Framework.Extensions.WebElements
{
    using Saki.Framework.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IWebTreeItemData : ISakiTreeItemData
    {
        WebElementType ElementType { get; }
    }
}

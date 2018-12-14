namespace Saki.Framework.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiTreeItemData
    {
        int ItemId { get; }
        string Name { get; }
        string Description { get; }
    }
}

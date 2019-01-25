namespace Saki.Framework.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiTreeState
    {
        int CurrentItemId { get; }
        ISakiTreeItem<ISakiTreeItemData> CurrentItem { get; }

        int ItemInBufferId { get; }
    }
}

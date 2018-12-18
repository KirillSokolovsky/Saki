using Saki.Framework.Interfaces;
using Saki.Framework.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saki.Framework.SakiTree.Commands.Create
{
    public interface ICreateTreeItemRequest<out TItem> : ISakiRequest<SakiResult<int>>
        where TItem : ISakiTreeItem<ISakiTreeItemData>
    {
        string ExtensionName { get; }
        TItem Item { get; }
    }
}

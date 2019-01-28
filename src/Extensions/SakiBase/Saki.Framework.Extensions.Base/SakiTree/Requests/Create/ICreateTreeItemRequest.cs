using Saki.Framework.Interfaces;
using Saki.Framework.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saki.Framework.Base.SakiTree.Requests.Create
{
    public interface ICreateTreeItemRequest<out TItem> : ISakiRequest<ISakiResult<int>>
        where TItem : ISakiTreeItem<ISakiTreeItemData>
    {
        TItem Item { get; }
        int ParentItemId { get; }
    }
}

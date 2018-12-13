namespace Saki.Common.Interfaces
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISakiTreeRepository
    {
        Task<ISakiResult<ISakiTreeItem<TData>>> GetItem<TData>(int itemId, CancellationToken token)
            where TData : ISakiTreeItemData;

        Task<ISakiResult<TItem>> CreateItem<TItem, TData>(TItem itemInfo, int parentItemId, CancellationToken token)
            where TItem : ISakiTreeItem<TData>
            where TData : ISakiTreeItemData;

        Task<ISakiResult<TItem>> UpdateItem<TItem, TData>(int itemId, TItem updatedItemInfo, CancellationToken token)
            where TItem : ISakiTreeItem<TData>
            where TData : ISakiTreeItemData;

        Task<ISakiResult> DeleteItem(int itemId, bool recursive, CancellationToken token);

        Task<ISakiResult<IEnumerable<ISakiTreeItem<ISakiTreeItemData>>>> GetChildItems(int parentItemId, CancellationToken token);

        Task<ISakiResult<TItem>> Transfer<TItem, TData>(int itemId, TItem itemInfo, int targetParentId, bool removeFromSource)
            where TItem : ISakiTreeItem<TData>
            where TData : ISakiTreeItemData;
    }
}

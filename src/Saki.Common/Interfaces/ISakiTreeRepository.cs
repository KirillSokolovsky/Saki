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
        Task<ISakiResult<SakiTreeItem>> GetItem(int itemId, CancellationToken token);
        Task<ISakiResult<SakiTreeItem<TItemData>>> GetItem<TItemData>(int itemId, CancellationToken token)
            where TItemData : ISakiTreeItemData;

        Task<ISakiResult<SakiTreeItem>> CreateItem(SakiTreeItem itemInfo, int parentItemId, CancellationToken token);

        Task<ISakiResult<SakiTreeItem>> UpdateItem(int itemId, SakiTreeItem updatedItemInfo, CancellationToken token);

        Task<ISakiResult> UpdateItemData<TItemData>(SakiTreeItem<TItemData> item, CancellationToken token)
            where TItemData : ISakiTreeItemData;

        Task<ISakiResult<TItemData>> GetItemData<TItemData>(SakiTreeItem<TItemData> item, CancellationToken token)
            where TItemData : ISakiTreeItemData;

        Task<ISakiResult> DeleteItem(int itemId, bool recursive, CancellationToken token);

        Task<ISakiResult<IEnumerable<SakiTreeItem>>> GetChildrenItems(int parentItemId, CancellationToken token);
    }
}

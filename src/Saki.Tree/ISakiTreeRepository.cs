namespace Saki.Tree
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISakiTreeRepository<T>
    {
        Task<SingleTreeItemResult> GetItemAsync(int itemId);
        Task<SingleTreeItemResult> AddItemAsync(SakiTreeItem item, int parentId);
        Task<SingleTreeItemResult> UpdateItemAsync(int itemId, SakiTreeItem updatedItem);

        Task<ISakiResult> DeleteItemAsync(int itemId);
        Task<MultiTreeItemResult> GetChildrenAsync(int parentItemId);

        Task<ISakiResult> MoveItemAsync(int itemId, int targetParentItemId);
        Task<ISakiResult> CopyItemAsync(int itemId, int targetParentItemId);

        Task<ISakiResult<object>> GetItemDataAsync(int itemId);
    }
}

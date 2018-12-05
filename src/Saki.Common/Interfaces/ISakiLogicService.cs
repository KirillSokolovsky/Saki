namespace Saki.Common.Interfaces
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISakiLogicService
    {
        Task<ISakiResult<IEnumerable<TCommand>>> GetOnItemCommands<TCommand>(int itemId)
            where TCommand : ISakiTreeItemCommand;

        Task<ISakiResult<SakiTreeItem>> CreateItem(SakiTreeItem itemInfo, int parentItemId);

        Task<ISakiResult<SakiTreeItem>> UpdateItem(int itemId, SakiTreeItem updatedItemInfo);

        Task<ISakiResult> UpdateItemData<TItemData>(SakiTreeItem<TItemData> item)
            where TItemData : ISakiTreeItemData;

        Task<ISakiResult<TItemData>> GetItemData<TItemData>(SakiTreeItem<TItemData> item)
            where TItemData : ISakiTreeItemData;

        Task<ISakiResult> DeleteItem(int itemId);
    }
}

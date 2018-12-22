namespace Saki.Framework.Interfaces
{
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using Saki.Framework.SakiTree;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISakiTreeRepositoryService
    {
        Task<SakiResult<SakiTreeItem<TData>>> GetItem<TData>(int itemId)
            where TData : ISakiTreeItemData;

        Task<SakiResult<int>> CreateItem(string extensionName, ISakiTreeItemData itemData, int parentItemId);

        Task<SakiResult<IEnumerable<ISakiResult<ISakiTreeItem<ISakiTreeItemData>>>>> GetChildItems(int parentItemId);

        Task<SakiResult<IEnumerable<string>>> GetChildItemNames(int parentItemId);
        Task<SakiResult<IEnumerable<SakiTreeItem<ISakiTreeItemData>>>> GetAscendantItems(int fromParentId, string tillItemDataType);
    }
}

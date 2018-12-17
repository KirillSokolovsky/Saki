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

        Task<SakiResult<int>> CreateItem(ISakiTreeItem<ISakiTreeItemData> treeItem, int parentItemId);

        Task<SakiResult<IEnumerable<ISakiTreeItem<ISakiTreeItemData>>>> GetChildItems(int parentItemId);
    }
}

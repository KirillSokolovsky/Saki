namespace Saki.Framework.Interfaces
{
    using Saki.Framework.Result;
    using Saki.Framework.SakiTree.Storage;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISakiTreeStorage
    {
        Task<SakiResult<StoredSakiTreeItemModel>> GetItem(int itemId);
        Task<SakiResult<int>> CreateItem(CreateSakiTreeItemModel createSakiTreeItemModel);
        Task<SakiResult> UpdatedItem(int itemId, UpdateSakiTreeItemModel updateSakiTreeItemModel);
        Task<SakiResult<IEnumerable<StoredSakiTreeItemModel>>> GetChildItems(int parentItemId);
    }
}

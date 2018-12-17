namespace Saki.Framework.TreeRepositoryService
{
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class SakiTreeRepositoryService : ISakiTreeRepositoryService
    {
        private readonly ISakiTreeItemDataSerializaionService _serializaionService;
        private readonly ISakiTreeStorage _storage;

        public SakiTreeRepositoryService(
            ISakiTreeItemDataSerializaionService serializaionService,
            ISakiTreeStorage sakiTreeStorage)
        {
            _serializaionService = serializaionService;
            _storage = sakiTreeStorage;
        }

        public Task<SakiResult<int>> CreateItem(ISakiItemTypeInfo itemTypeInfo, ISakiTreeItemData itemData, int parentItemId)
        {
            throw new NotImplementedException();
        }

        public Task<ISakiResult<IEnumerable<ISakiTreeItem<ISakiTreeItemData>>>> GetChildItems(int parentItemId)
        {
            throw new NotImplementedException();
        }

        public Task<ISakiResult<TItem>> GetItem<TItem>(int itemId) where TItem : ISakiTreeItem<ISakiTreeItemData>
        {
            throw new NotImplementedException();
        }
    }
}

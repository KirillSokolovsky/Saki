namespace Saki.Framework.Services.TreeRepositoryService
{
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using Saki.Framework.SakiTree;
    using Saki.Framework.SakiTree.Storage;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<SakiResult<int>> CreateItem(ISakiTreeItem<ISakiTreeItemData> treeItem, int parentItemId)
        {
            var itemType = treeItem.Data.GetType();
            var itemDataType = itemType.GetSakiTypeName();

            var dataSerResult = _serializaionService.SerializeTreeItemData(treeItem.Data);

            if (dataSerResult.Result != SakiResultType.Ok)
                return new SakiResult<int>(dataSerResult);

            var createModel = new CreateSakiTreeItemModel
            {
                ParentItemId = parentItemId,

                ExtensionName = treeItem.ExtensionName,
                ItemDataType = itemDataType,

                Name = treeItem.Data.Name,
                Description = treeItem.Data.Description,

                Data = dataSerResult.Data
            };

            var storeResult = await _storage.CreateItem(createModel);

            return storeResult;
        }

        public async Task<SakiResult<SakiTreeItem<TData>>> GetItem<TData>(int itemId)
            where TData : ISakiTreeItemData
        {
            var storeResult = await _storage.GetItem(itemId);

            if (storeResult.Result != SakiResultType.Ok)
                return new SakiResult<SakiTreeItem<TData>>(storeResult);

            var storedItem = storeResult.Data;

            return CreateTreeItemFromStoredModel<TData>(storedItem);
        }

        public async Task<SakiResult<IEnumerable<ISakiResult<ISakiTreeItem<ISakiTreeItemData>>>>> GetChildItems(int parentItemId)
        {
            var storeResult = await _storage.GetChildItems(parentItemId);

            if (storeResult.Result != SakiResultType.Ok)
                return new SakiResult<IEnumerable<ISakiResult<ISakiTreeItem<ISakiTreeItemData>>>>(storeResult);

            var deserialized = storeResult.Data.Select(sr => CreateTreeItemFromStoredModel<ISakiTreeItemData>(sr))
                .ToList();

            return new SakiResult<IEnumerable<ISakiResult<ISakiTreeItem<ISakiTreeItemData>>>>(deserialized);
        }

        private SakiResult<SakiTreeItem<TData>> CreateTreeItemFromStoredModel<TData>(StoredSakiTreeItemModel model)
            where TData : ISakiTreeItemData
        {
            var itemSerResult = _serializaionService.DeserializeTreeItemData<TData>(model.ItemDataType, model.Data);

            if (itemSerResult.Result != SakiResultType.Ok)
                return new SakiResult<SakiTreeItem<TData>>(itemSerResult);

            var item = new SakiTreeItem<TData>
            {
                ItemId = model.ItemId,
                ExtensionName = model.ExtensionName,
                ItemDataType = model.ItemDataType,
                ParentId = model.ParentId,
                Data = itemSerResult.Data
            };

            return SakiResult<SakiTreeItem<TData>>.Ok(item);
        }
    }
}

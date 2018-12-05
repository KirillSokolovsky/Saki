namespace Saki.Core
{
    using Saki.Common;
    using Saki.Common.Interfaces;
    using Saki.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class SakiLogicService : ISakiLogicService
    {
        private readonly Container _container;

        public SakiLogicService(Container container)
        {
            _container = container;

        }

        public Task<ISakiResult<SakiTreeItem>> CreateItem(SakiTreeItem itemInfo, int parentItemId)
        {
            //get handler for create item command for concret item type
            throw new NotImplementedException();
        }

        public Task<ISakiResult> DeleteItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<ISakiResult<TItemData>> GetItemData<TItemData>(SakiTreeItem<TItemData> item) where TItemData : ISakiTreeItemData
        {
            throw new NotImplementedException();
        }

        public Task<ISakiResult<IEnumerable<TCommand>>> GetOnItemCommands<TCommand>(int itemId) where TCommand : ISakiTreeItemCommand
        {
            throw new NotImplementedException();
        }

        public Task<ISakiResult<SakiTreeItem>> UpdateItem(int itemId, SakiTreeItem updatedItemInfo)
        {
            throw new NotImplementedException();
        }

        public Task<ISakiResult> UpdateItemData<TItemData>(SakiTreeItem<TItemData> item) where TItemData : ISakiTreeItemData
        {
            throw new NotImplementedException();
        }
    }
}

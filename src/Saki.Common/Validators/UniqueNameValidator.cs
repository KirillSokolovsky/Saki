namespace Saki.Common.Validators
{
    using Saki.Common.Interfaces;
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    public class UniqueNameValidator
    {
        private readonly ISakiTreeRepository _repository;
        public UniqueNameValidator(ISakiTreeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ISakiResult> ValidateForCreating(string name, int parentId, CancellationToken token)
        {
            var items = await _repository.GetChildrenItems(parentId, token);
            if (items.HasErrors)
                return new SakiResult(items.Errors);

            if (items.Data.Any(i => i.Name == name))
            {
                var error = new SakiError($"Item with name: {name} already exists in parent item with id: {parentId}");
                return new SakiResult(error);
            }

            return new SakiResult();
        }

        public async Task<ISakiResult> ValidateForUpdating(string newName, int itemId, int parentId, CancellationToken token)
        {
            var items = await _repository.GetChildrenItems(parentId, token);
            if (items.HasErrors)
                return new SakiResult(items.Errors);

            var currentItem = items.Data.FirstOrDefault(i => i.Id == itemId);

            if(currentItem == null)
            {
                var error = new SakiError($"Item with id: {itemId} is not found in parent item with id: {parentId}");
                return new SakiResult(error);
            }

            if (items.Data.Any(i => i.Id != currentItem.Id && i.Name == newName))
            {
                var error = new SakiError($"Item with name: {newName} already exists in parent item with id: {parentId}");
                return new SakiResult(error);
            }

            return new SakiResult();
        }
    }
}

namespace Saki.TreeStorage.FileSystemStorage
{
    using Newtonsoft.Json;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class FileSystemSakiTreeStorageService : ISakiTreeRepositoryService
    {
        private static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            TypeNameHandling = TypeNameHandling.Arrays | TypeNameHandling.Objects,
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };
        private List<TreeNode> _nodes;
        private List<TreeNode> _allNodes;
        private string _pathToStorageJsonFile;

        public FileSystemSakiTreeStorageService(string pathToStorageJsonFile)
        {
            _pathToStorageJsonFile = pathToStorageJsonFile;
            _nodes = new List<TreeNode>();
            _allNodes = new List<TreeNode>();
        }

        public Task<SakiResult<int>> CreateItem(ISakiItemTypeInfo itemTypeInfo, ISakiTreeItemData itemData, int parentItemId)
        {
            var newId = _allNodes.Max(n => n.NodeId) + 1;
            var nodeToAdd = new TreeNode
            {
                NodeId = newId,
                ExtensionName = itemTypeInfo.ItemCategory,
                ItemType = itemTypeInfo.ItemDataType,
                Name = itemData.Name,
                Description = itemData.Description
            };

            var serializationResult = SerializeData(itemData);

            if (serializationResult.Result != SakiResultType.Ok)
                return Task.FromResult(new SakiResult<int>(serializationResult));

            nodeToAdd.DataJson = serializationResult.Data;

            if (parentItemId > 1)
            {
                var parentNode = _allNodes.FirstOrDefault(n => n.NodeId == parentItemId);
                if (parentNode == null)
                {
                    var ex = new FileSystemSakiTreeStorageException(nameof(CreateItem),
                        $"There is no parent item with id: {parentItemId}");
                    return Task.FromResult(SakiResult<int>.FromEx(ex));
                }

                nodeToAdd.ParentNodeId = parentItemId;
                nodeToAdd.ParentNode = parentNode;

                if (parentNode.Nodes == null)
                    parentNode.Nodes = new List<TreeNode>();

                parentNode.Nodes.Add(nodeToAdd);
            }
            else
            {
                _nodes.Add(nodeToAdd);
            }

            _allNodes.Add(nodeToAdd);

            return Task.FromResult(SakiResult<int>.Ok(newId));
        }

        public Task<ISakiResult<IEnumerable<ISakiTreeItem<ISakiTreeItemData>>>> GetChildItems(int parentItemId)
        {
            throw new NotImplementedException();
        }

        public Task<ISakiResult<TItem>> GetItem<TItem>(int itemId) where TItem : ISakiTreeItem<ISakiTreeItemData>
        {
            var item = _allNodes.FirstOrDefault
        }




        private SakiResult<string> SerializeData(ISakiTreeItemData itemData)
        {
            try
            {
                var dataJson = JsonConvert.SerializeObject(itemData, JsonSerializerSettings);
                return SakiResult<string>.Ok(dataJson);
            }
            catch (Exception ex)
            {
                var sakiException = new FileSystemSakiTreeStorageException(nameof(SerializeData),
                    $"Error occurred during serelization of item data",
                    ex);

                return SakiResult<string>.FromEx(sakiException);
            }
        }

        private SakiResult<TItemData> Deserialize<TItemData>(string json, string itemDataType)
            where TItemData : ISakiTreeItemData
        {
            var dataType = Type.GetType(itemDataType);
            var requestedType = typeof(TItemData);
            if (dataType == null)
            {
                var ex = new FileSystemSakiTreeStorageException(nameof(Deserialize),
                    $"Couldn't find item data type: {itemDataType}" +
                    $"{Environment.NewLine}Requested type: {requestedType.FullName}");
                return SakiResult<TItemData>.FromEx(ex);
            }

            if (!requestedType.IsAssignableFrom(dataType))
            {
                var ex = new FileSystemSakiTreeStorageException(nameof(Deserialize),
                    $"Item data type: {itemDataType}" +
                    $"{Environment.NewLine}couldn't be assigned to requested type." +
                    $"{Environment.NewLine}Requested type: {requestedType.FullName}");
                return SakiResult<TItemData>.FromEx(ex);
            }

            Object rawData = null;

            try
            {
                rawData = JsonConvert.DeserializeObject(json, dataType, JsonSerializerSettings);
            }
            catch (Exception ex)
            {
                var sakiException = new FileSystemSakiTreeStorageException(nameof(Deserialize),
                    $"Error occurred during deserelization of item data with type: {itemDataType}",
                    ex);

                return SakiResult<TItemData>.FromEx(sakiException);
            }


            return SakiResult<TItemData>.Ok((TItemData)rawData);
        }

        public async Task<ISakiResult> Save()
        {
            try
            {
                var fileInfo = new FileInfo(_pathToStorageJsonFile);
                if (!fileInfo.Directory.Exists)
                    fileInfo.Directory.Create();

                var json = JsonConvert.SerializeObject(_nodes);
                using (var sw = new StreamWriter(_pathToStorageJsonFile))
                {
                    await sw.WriteAsync(json);
                }

                return SakiResult.Ok;
            }
            catch (Exception ex)
            {
                var sakiEx = new FileSystemSakiTreeStorageException(nameof(Save),
                    "Error occurred during saving items", ex);
                return SakiResult.FromEx(sakiEx);
            }
        }
        public async Task<ISakiResult> Load()
        {
            try
            {
                var fileInfo = new FileInfo(_pathToStorageJsonFile);
                if (!fileInfo.Exists) return SakiResult.Ok;

                string json = null;
                using (var sr = new StreamReader(_pathToStorageJsonFile))
                {
                    json = await sr.ReadToEndAsync();
                }

                _nodes = JsonConvert.DeserializeObject<List<TreeNode>>(json);

                FillAllNodes(_nodes);

                return SakiResult.Ok;
            }
            catch (Exception ex)
            {
                var sakiEx = new FileSystemSakiTreeStorageException(nameof(Load),
                    "Error occurred during loading items", ex);
                return SakiResult.FromEx(sakiEx);
            }
        }

        private void FillAllNodes(List<TreeNode> nodes)
        {
            if (nodes == null) return;

            foreach (var node in nodes)
            {
                _allNodes.Add(node);
                FillAllNodes(node.Nodes);
            }
        }
    }
}

namespace Saki.FileSystemTreeRepository
{
    using Newtonsoft.Json;
    using Saki.Common;
    using Saki.Common.Interfaces;
    using Saki.Exceptions;
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class SakiTreeFileSystemRepository : ISakiTreeRepository
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

        public SakiTreeFileSystemRepository(string pathToStorageJsonFile)
        {
            _pathToStorageJsonFile = pathToStorageJsonFile;
            _nodes = new List<TreeNode>();
            _allNodes = new List<TreeNode>();
        }

        public Task<ISakiResult<SakiTreeItem>> CreateItem(SakiTreeItem itemInfo, int parentItemId, CancellationToken token)
        {
            var newId = _allNodes.Max(n => n.NodeId) + 1;
            var nodeToAdd = new TreeNode
            {
                NodeId = newId,
                Name = itemInfo.Name,
                Description = itemInfo.Description,
                ItemCategory = itemInfo.ItemCategory,
                ItemType = itemInfo.InnerType
            };

            if (parentItemId > 1)
            {
                var parentNode = _allNodes.FirstOrDefault(n => n.NodeId == parentItemId);
                if (parentNode == null)
                {
                    var error = new SakiError($"There is no parent item with id: {parentItemId}");
                    return Task.FromResult<ISakiResult<SakiTreeItem>>(new SakiResult<SakiTreeItem>(error));
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

            itemInfo.Id = nodeToAdd.NodeId;

            _allNodes.Add(nodeToAdd);

            return Task.FromResult<ISakiResult<SakiTreeItem>>(new SakiResult<SakiTreeItem>(itemInfo));
        }

        public Task<ISakiResult> DeleteItem(int itemId, bool recursive, CancellationToken token)
        {
            var node = _allNodes.FirstOrDefault(n => n.NodeId == itemId);
            if (node == null)
            {
                var error = new SakiError($"There is no item with id: {itemId}");
                return Task.FromResult<ISakiResult>(new SakiResult(error));
            }

            if(!recursive)
            {
                if(node.Nodes != null && node.Nodes.Count > 0)
                {
                    var error = new SakiError($"There are child items under item with id: {itemId}");
                    return Task.FromResult<ISakiResult>(new SakiResult(error));
                }
            }
            if (!recursive)
            {
                RemoveNode(node);
            }
            else
            {
                var nodesToRemove = new Stack<TreeNode>();
                var nodesToScan = new Stack<TreeNode>();
                nodesToScan.Push(node);

                while (nodesToScan.Count > 0)
                {
                    var nodeToScan = nodesToScan.Pop();
                    nodesToRemove.Push(nodeToScan);

                    if(nodeToScan.Nodes != null && nodeToScan.Nodes.Count > 0)
                    {
                        foreach (var childNode in nodeToScan.Nodes)
                        {
                            nodesToScan.Push(childNode);
                        }
                    }
                }

                while(nodesToRemove.Count > 0)
                {
                    var nodeToRemove = nodesToRemove.Pop();
                    RemoveNode(nodeToRemove);
                }
            }

            return Task.FromResult<ISakiResult>(new SakiResult());
        }

        private void RemoveNode(TreeNode node)
        {
            if (node.ParentNode != null)
            {
                node.ParentNode.Nodes.Remove(node);
                node.ParentNode = null;
            }
            else
            {
                _nodes.Remove(node);
            }
            _allNodes.Remove(node);
        }

        public Task<ISakiResult<IEnumerable<SakiTreeItem>>> GetChildrenItems(int parentItemId, CancellationToken token)
        {
            TreeNode parentNode = null;
            if (parentItemId > 0)
            {
                parentNode = _allNodes.FirstOrDefault(n => n.NodeId == parentItemId);

                if (parentNode == null)
                    return Task.FromResult<ISakiResult<IEnumerable<SakiTreeItem>>>(new SakiResult<IEnumerable<SakiTreeItem>>(
                        new SakiError($"There is no parent item with id: {parentItemId}")));
            }

            var childrenNodes = parentItemId > 0
                ? parentNode.Nodes ?? new List<TreeNode>()
                : _nodes;

            var childItems = childrenNodes.Select(node => new SakiTreeItem
            {
                Name = node.Name,
                Description = node.Description,
                Id = node.NodeId,
                ParentId = node.ParentNodeId,
                ItemCategory = node.ItemCategory,
                InnerType = node.ItemType,
                DataId = node.DataJson == null ? 0 : node.NodeId
            });
            return Task.FromResult<ISakiResult<IEnumerable<SakiTreeItem>>>(new SakiResult<IEnumerable<SakiTreeItem>>(
                        childItems));
        }

        public Task<ISakiResult<SakiTreeItem>> GetItem(int itemId, CancellationToken token)
        {
            if (itemId < 1)
                return Task.FromResult<ISakiResult<SakiTreeItem>>(new SakiResult<SakiTreeItem>(
                    new SakiError($"Incorrect item id: {itemId}")));

            var node = _allNodes.FirstOrDefault(n => n.NodeId == itemId);

            if (node == null)
                return Task.FromResult<ISakiResult<SakiTreeItem>>(new SakiResult<SakiTreeItem>(
                    new SakiError($"There is no item with id: {itemId}")));

            var item = new SakiTreeItem
            {
                Name = node.Name,
                Description = node.Description,
                Id = node.NodeId,
                ParentId = node.ParentNodeId,
                ItemCategory = node.ItemCategory,
                InnerType = node.ItemType,
                DataId = node.DataJson == null ? 0 : node.NodeId
            };

            return Task.FromResult<ISakiResult<SakiTreeItem>>(new SakiResult<SakiTreeItem>(item));
        }

        public Task<ISakiResult<SakiTreeItem<TItemData>>> GetItem<TItemData>(int itemId, CancellationToken token) where TItemData : ISakiTreeItemData
        {
            if (itemId < 1)
                return Task.FromResult<ISakiResult<SakiTreeItem<TItemData>>>(new SakiResult<SakiTreeItem<TItemData>>(
                    new SakiError($"Incorrect item id: {itemId}")));

            var node = _allNodes.FirstOrDefault(n => n.NodeId == itemId);

            if (node == null)
                return Task.FromResult<ISakiResult<SakiTreeItem<TItemData>>>(new SakiResult<SakiTreeItem<TItemData>>(
                    new SakiError($"There is no item with id: {itemId}")));

            var item = new SakiTreeItem<TItemData>
            {
                Name = node.Name,
                Description = node.Description,
                Id = node.NodeId,
                ParentId = node.ParentNodeId,
                ItemCategory = node.ItemCategory,
                InnerType = node.ItemType,
                DataId = node.DataJson == null ? 0 : node.NodeId
            };

            var itemDataResult = GetItemData(item, token).Result;

            if (itemDataResult.HasErrors)
            {
                return Task.FromResult<ISakiResult<SakiTreeItem<TItemData>>>(
                    new SakiResult<SakiTreeItem<TItemData>>(itemDataResult.Errors));
            }

            item.Data = itemDataResult.Data;

            return Task.FromResult<ISakiResult<SakiTreeItem<TItemData>>>(new SakiResult<SakiTreeItem<TItemData>>(item));
        }

        public Task<ISakiResult<TItemData>> GetItemData<TItemData>(SakiTreeItem<TItemData> item, CancellationToken token)
            where TItemData : ISakiTreeItemData
        {
            if (item.Id < 1)
                return Task.FromResult<ISakiResult<TItemData>>(new SakiResult<TItemData>(
                    new SakiError($"Incorrect item id: {item.Id}")));

            var node = _allNodes.FirstOrDefault(n => n.NodeId == item.Id);

            if (node == null)
                return Task.FromResult<ISakiResult<TItemData>>(new SakiResult<TItemData>(
                    new SakiError($"There is no item with id: {item.Id}")));

            if (node.DataJson == null)
                return Task.FromResult<ISakiResult<TItemData>>(new SakiResult<TItemData>(
                    new SakiError($"There is no item data for item with id: {item.Id}")));

            try
            {
                var itemData = JsonConvert.DeserializeObject<TItemData>(node.DataJson, JsonSerializerSettings);

                return Task.FromResult<ISakiResult<TItemData>>(new SakiResult<TItemData>(itemData));
            }
            catch (Exception ex)
            {
                var sakiException = new SakiException(nameof(SakiTreeFileSystemRepository),
                    $"Error occurred during deserelization of item data for item id: {item.Id}",
                    ex);

                return Task.FromResult<ISakiResult<TItemData>>(new SakiResult<TItemData>(
                    new SakiError(sakiException)));
            }
        }

        public Task<ISakiResult<SakiTreeItem>> UpdateItem(int itemId, SakiTreeItem updatedItemInfo, CancellationToken token)
        {
            if (itemId < 1)
                return Task.FromResult<ISakiResult<SakiTreeItem>>(new SakiResult<SakiTreeItem>(
                    new SakiError($"Incorrect item id: {itemId}")));

            var node = _allNodes.FirstOrDefault(n => n.NodeId == itemId);

            if (node == null)
                return Task.FromResult<ISakiResult<SakiTreeItem>>(new SakiResult<SakiTreeItem>(
                    new SakiError($"There is no item with id: {itemId}")));

            if (updatedItemInfo.Name != null)
                node.Name = updatedItemInfo.Name;
            if (updatedItemInfo.Description != null)
                node.Description = updatedItemInfo.Description;

            return GetItem(itemId, token);
        }

        public Task<ISakiResult> UpdateItemData<TItemData>(SakiTreeItem<TItemData> item, CancellationToken token) where TItemData : ISakiTreeItemData
        {
            if (item.Data == null)
                return Task.FromResult<ISakiResult>(new SakiResult(
                    new SakiError($"There is no item data to do update for item with id: {item.Id}")));

            if (item.Id < 1)
                return Task.FromResult<ISakiResult>(new SakiResult(
                    new SakiError($"Incorrect item id: {item.Id}")));

            var node = _allNodes.FirstOrDefault(n => n.NodeId == item.Id);

            if (node == null)
                return Task.FromResult<ISakiResult>(new SakiResult(
                    new SakiError($"There is no item with id: {item.Id}")));

            if (node.DataJson == null)
                return Task.FromResult<ISakiResult>(new SakiResult(
                    new SakiError($"There is no item data for item with id: {item.Id}")));

            try
            {
                var dataJson = JsonConvert.SerializeObject(item.Data, JsonSerializerSettings);
                node.DataJson = dataJson;

                return Task.FromResult<ISakiResult>(new SakiResult());
            }
            catch (Exception ex)
            {
                var sakiException = new SakiException(nameof(SakiTreeFileSystemRepository),
                    $"Error occurred during serelization of item data for item id: {item.Id}",
                    ex);

                return Task.FromResult<ISakiResult>(new SakiResult(
                    new SakiError(sakiException)));
            }
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

                return new SakiResult();
            }
            catch (Exception ex)
            {
                var sakiEx = new SakiException(nameof(SakiTreeFileSystemRepository),
                    "Error occurred during saving items", ex);
                return new SakiResult(new SakiError(sakiEx));
            }
        }

        public async Task<ISakiResult> Load()
        {
            try
            {
                var fileInfo = new FileInfo(_pathToStorageJsonFile);
                if (!fileInfo.Exists) return new SakiResult();

                string json = null;
                using (var sr = new StreamReader(_pathToStorageJsonFile))
                {
                    json = await sr.ReadToEndAsync();
                }

                _nodes = JsonConvert.DeserializeObject<List<TreeNode>>(json);

                FillAllNodes(_nodes);

                return new SakiResult();
            }
            catch (Exception ex)
            {
                var sakiEx = new SakiException(nameof(SakiTreeFileSystemRepository),
                    "Error occurred during loading items", ex);
                return new SakiResult(new SakiError(sakiEx));
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

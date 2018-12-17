namespace Saki.TreeStorage.FileSystemStorage
{
    using Newtonsoft.Json;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using Saki.Framework.SakiTree.Storage;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FileSystemSakiTreeStorage : ISakiTreeStorage
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
        
        public FileSystemSakiTreeStorage(string pathToStorageJsonFile)
        {
            _pathToStorageJsonFile = pathToStorageJsonFile;
            _nodes = new List<TreeNode>();
            _allNodes = new List<TreeNode>();
        }

        public Task<SakiResult<int>> CreateItem(CreateSakiTreeItemModel createSakiTreeItemModel)
        {
            var newId = _allNodes.Max(n => n.NodeId) + 1;
            var nodeToAdd = new TreeNode
            {
                NodeId = newId,
                ExtensionName = createSakiTreeItemModel.ExtensionName,
                ItemType = createSakiTreeItemModel.ItemDataType,
                Name = createSakiTreeItemModel.Name,
                Description = createSakiTreeItemModel.Description,
                Data = createSakiTreeItemModel.Data
            };

            var parentItemId = createSakiTreeItemModel.ParentItemId;
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

        public Task<SakiResult<IEnumerable<StoredSakiTreeItemModel>>> GetChildItems(int parentItemId)
        {
            throw new NotImplementedException();
        }

        public Task<SakiResult<StoredSakiTreeItemModel>> GetItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public Task<SakiResult> UpdatedItem(int itemId, UpdateSakiTreeItemModel updateSakiTreeItemModel)
        {
            throw new NotImplementedException();
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

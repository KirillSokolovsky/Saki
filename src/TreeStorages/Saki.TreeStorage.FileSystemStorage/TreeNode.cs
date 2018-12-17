namespace Saki.TreeStorage.FileSystemStorage
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class TreeNode
    {
        public int NodeId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string ItemType { get; set; }
        public string ExtensionName { get; set; }

        public int ParentNodeId { get; set; }
        public TreeNode ParentNode { get; set; }

        public List<TreeNode> Nodes { get; set; }

        public byte[] Data { get; set; }
    }
}

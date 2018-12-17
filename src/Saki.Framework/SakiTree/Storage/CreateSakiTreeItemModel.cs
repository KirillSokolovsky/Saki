﻿namespace Saki.Framework.SakiTree.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateSakiTreeItemModel
    {
        public int ParentItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ItemDataType { get; set; }
        public string ExtensionName { get; set; }
        public byte[] Data { get; set; }
    }
}

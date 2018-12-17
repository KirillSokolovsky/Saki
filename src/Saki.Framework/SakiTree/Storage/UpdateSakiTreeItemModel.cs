namespace Saki.Framework.SakiTree.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UpdateSakiTreeItemModel
    {
        public string UpdatedName { get; set; }
        public string UpdatedDescription { get; set; }
        public byte[] UpdatedData { get; set; }
    }
}

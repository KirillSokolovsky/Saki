using System;
using System.Collections.Generic;
using System.Text;

namespace Saki.Tree
{
    public class SakiTreeItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int? ParentId { get; set; }

        public int ItemCategoryId { get; set; }
        public int ItemTypeId { get; set; }

        public int? DataId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}

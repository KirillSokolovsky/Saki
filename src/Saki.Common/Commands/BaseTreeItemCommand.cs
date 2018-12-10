using System;
using System.Collections.Generic;
using System.Text;

namespace Saki.Common.Commands
{
    public abstract class BaseTreeItemCommand
    {
        public abstract string CommandName { get; }
        public string ItemCategory { get; set; }
        public string ItemType { get; set; }
    }
}

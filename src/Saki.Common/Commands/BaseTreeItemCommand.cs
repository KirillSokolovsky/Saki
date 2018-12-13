using Saki.Common.Interfaces;
using Saki.Result;
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

    public abstract class C1 : ISakiCommand<ISakiResult<ISakiTreeItem<ISakiTreeItemData>>>
    {
        public abstract string ItemCategory { get; }
        public abstract string ItemType { get; }
        public abstract string CommandName { get; }
    };
}

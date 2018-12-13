using System;
using System.Collections.Generic;
using System.Text;

namespace Saki.Common.Interfaces
{
    public interface ISakiTreeItemData
    {
        int ItemId { get; }
        string Name { get; }
        string Description { get; }
    }
}

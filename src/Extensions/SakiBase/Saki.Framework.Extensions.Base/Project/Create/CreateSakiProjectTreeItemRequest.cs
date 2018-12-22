namespace Saki.Framework.Extensions.Base.Project.Create
{
    using Saki.Framework.Base.SakiTree.Commands.Create;
    using Saki.Framework.SakiTree;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateSakiProjectTreeItemRequest : CreateTreeItemRequest<SakiTreeItem<SakiProjectTreeItemData>>
    {
        public CreateSakiProjectTreeItemRequest(string extensionName, SakiTreeItem<SakiProjectTreeItemData> item) 
            : base(extensionName, item, 0)
        {
        }
    }
}

using Saki.Framework.Base.SakiTree.Commands.Create;
using Saki.Framework.Extensions.WebElements.Commands.Create;
using Saki.Framework.Interfaces;
using Saki.Framework.SakiTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saki.Framework.Extensions.WebElements.WebElement.Commands.Create
{
    public class CreateWebElementTreeItemRequest : CreateTreeItemRequest<SakiTreeItem<WebElementTreeItemData>>,
        ICreateWebTreeItemRequest<ISakiTreeItem<IWebTreeItemData>>
    {
        ISakiTreeItem<IWebTreeItemData> ICreateTreeItemRequest<ISakiTreeItem<IWebTreeItemData>>.Item => Item;

        public CreateWebElementTreeItemRequest(string extensionName, SakiTreeItem<WebElementTreeItemData> item, int parentItemId)
            : base(extensionName, item, parentItemId)
        {
        }
    }
}

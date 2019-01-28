using Saki.Framework.Base.SakiTree.Requests.Create;
using Saki.Framework.Extensions.WebElements.Requests.Create;
using Saki.Framework.Interfaces;
using Saki.Framework.SakiTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saki.Framework.Extensions.WebElements.WebElement.Requests.Create
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

namespace Saki.Framework.Extensions.WebElements.Commands.Create
{
    using Saki.Framework.Base.SakiTree.Commands.Create;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICreateWebTreeItemRequest<TWebItem> : ICreateTreeItemRequest<TWebItem>
        where TWebItem : ISakiTreeItem<IWebTreeItemData>
    {
    }
}

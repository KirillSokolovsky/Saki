namespace Saki.Framework.Internal.Interfaces
{
    using Saki.Framework.Interfaces;
    using Saki.Framework.Internal.ExtensionsInfo;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiExtensionsService
    {
        SakiResult LoadExtension(string extensionDirectory);

        SakiResult<object> FindHandler<TResult>(ISakiRequest<TResult> request)
            where TResult : ISakiResult;

        SakiResult<ISakiCommandsProvider> FindCommandsProvider(ISakiTreeItem<ISakiTreeItemData> item);
    }
}

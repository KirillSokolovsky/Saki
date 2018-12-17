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

        SakiExtensionsInfo GetInfo();

        SakiResult<ISakiRequestHandler<ISakiRequest<TResult>, TResult>> FindHandler<TResult>(string extensionName, ISakiRequest<TResult> request)
            where TResult : ISakiResult;
    }
}

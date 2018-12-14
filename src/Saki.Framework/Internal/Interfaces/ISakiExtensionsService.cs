namespace Saki.Framework.Internal.Inerfaces
{
    using Saki.Framework.Inerfaces;
    using Saki.Framework.Internal.ExtensionsInfo;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiExtensionsService
    {
        SakiExtensionsInfo GetInfo();

        SakiResult<ISakiRequestHandler<ISakiRequest<TResult>, TResult>> FindHandler<TResult>(string extensionName, ISakiRequest<TResult> request)
            where TResult : ISakiResult;
    }
}

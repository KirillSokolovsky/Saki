namespace Saki.Framework.Interfaces
{
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiRequest<out TResult> : ISakiRequestForScan
        where TResult : ISakiResult
    {
    }
}

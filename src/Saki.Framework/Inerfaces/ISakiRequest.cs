namespace Saki.Framework.Inerfaces
{
    using Saki.Framework.Internal.Inerfaces;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiRequest<out TResult> : ISakiRequestForScan
        where TResult : ISakiResult
    {
    }
}

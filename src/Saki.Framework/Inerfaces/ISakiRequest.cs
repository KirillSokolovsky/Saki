namespace Saki.Framework.Inerfaces
{
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiRequest<TResult>
        where TResult : ISakiResult
    {
    }
}

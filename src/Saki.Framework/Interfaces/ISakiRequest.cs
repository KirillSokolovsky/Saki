namespace Saki.Framework.Interfaces
{
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiRequest<out TResult>
        where TResult : ISakiResult
    {
        string CommandName { get; }
        string ExtensionName { get; }
        string ItemDataType { get; }
    }
}

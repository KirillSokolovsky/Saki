namespace Saki.Common.Interfaces
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiCommand<out TResult>
        where TResult : IBaseSakiResult
    {
        string ItemCategory { get; }
        string ItemType { get; }
    }
}

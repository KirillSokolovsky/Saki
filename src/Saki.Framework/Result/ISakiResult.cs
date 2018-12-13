namespace Saki.Framework.Result
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiResult<out T> : ISakiResult
    {
        T Data { get; }
    }

    public interface ISakiResult
    {
        void AddError(ISakiError error);
        IEnumerable<ISakiError> Errors { get; }
        bool HasErrors { get; }
    }
}

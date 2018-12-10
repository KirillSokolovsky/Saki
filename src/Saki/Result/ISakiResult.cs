namespace Saki.Result
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiResult<out T> : IBaseSakiResult
    {
        T Data { get; }
    }

    public interface ISakiResult : ISakiResult<SakiUnit>
    {
    }

    public interface IBaseSakiResult
    {
        void AddError(ISakiError error);
        IEnumerable<ISakiError> Errors { get; }
        bool HasErrors { get; }
    }
}

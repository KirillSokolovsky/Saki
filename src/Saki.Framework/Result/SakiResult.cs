namespace Saki.Framework.Result
{
    using Saki.Framework.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiResult<T> : SakiResult, ISakiResult<T>
    {
        public new static SakiResult<T> Ok(T result) => new SakiResult<T>(result);

        public new static SakiResult<T> FromEx(SakiException sakiException)
        {
            return new SakiResult<T>(new SakiError(sakiException));
        }

        public T Data { get; set; }

        public SakiResult()
        {
            Result = SakiResultType.Unknown;
        }

        public SakiResult(T data)
            : base()
        {
            SetData(data);
        }

        public SakiResult(ISakiError error)
            : base(error)
        {
        }

        public SakiResult(IEnumerable<ISakiError> errors)
            : base(errors)
        {
        }

        public SakiResult(SakiResult<T> sakiResult)
            : base(sakiResult)
        {
            Data = sakiResult.Data;
        }

        public SakiResult(SakiResult sakiResult)
            : base(sakiResult)
        {

        }

        public void SetData(T data)
        {
            Data = data;
            if (Result == SakiResultType.Unknown)
                Result = SakiResultType.Ok;
        }

        public new SakiResult<T> AddResult(ISakiResult result)
        {
            return (SakiResult<T>)base.AddResult(result);
        }
    }

    public class SakiResult : ISakiResult
    {
        public static SakiResult Ok => new SakiResult();

        public static SakiResult FromEx(SakiException sakiException)
        {
            return new SakiResult(new SakiError(sakiException));
        }

        private List<ISakiError> _errors;
        public IEnumerable<ISakiError> Errors => _errors;
        public SakiResultType Result { get; set; }

        public SakiResult()
        {
            Result = SakiResultType.Ok;
        }

        public SakiResult(ISakiError error)
            : this()
        {
            AddError(error);
        }

        public SakiResult(IEnumerable<ISakiError> errors)
            : this()
        {
            if (errors == null) return;

            foreach (var error in errors)
                AddError(error);
        }

        public SakiResult(SakiResult sakiResult)
            : this(sakiResult._errors)
        {

        }

        public void AddError(ISakiError error)
        {
            if (error == null) return;

            if (_errors == null)
                _errors = new List<ISakiError>();

            Result = SakiResultType.Error;
            _errors.Add(error);
        }

        public SakiResult AddResult(ISakiResult result)
        {
            if (result.Errors == null) return this;

            foreach (var error in result.Errors)
                AddError(error);

            return this;
        }

        ISakiResult ISakiResult.AddResult(ISakiResult result)
        {
            return AddResult(result);
        }
    }
}

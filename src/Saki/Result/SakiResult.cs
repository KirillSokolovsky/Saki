namespace Saki.Result
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiResult<T> : BaseSakiResult, ISakiResult<T>
    {
        public T Data { get; set; }

        public SakiResult()
        {
            Data = default(T);
        }

        public SakiResult(T data)
        {
            Data = data;
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
    }

    public class SakiResult : BaseSakiResult, ISakiResult
    {
        public SakiResult()
        {
        }

        public SakiResult(ISakiError error) : base(error)
        {
        }

        public SakiResult(IEnumerable<ISakiError> errors) : base(errors)
        {
        }

        public SakiResult(BaseSakiResult sakiResult) : base(sakiResult)
        {
        }

        public SakiUnit Data => SakiUnit.Value;
    }

    public class BaseSakiResult : IBaseSakiResult
    {
        private HashSet<ISakiError> _errors;
        public IEnumerable<ISakiError> Errors => _errors;
        public bool HasErrors => _errors == null || _errors.Count == 0;

        public BaseSakiResult()
        {
        }

        public BaseSakiResult(ISakiError error)
        {
            AddError(error);
        }

        public BaseSakiResult(IEnumerable<ISakiError> errors)
        {
            if (errors == null) return;

            foreach (var error in errors)
                AddError(error);
        }

        public BaseSakiResult(BaseSakiResult sakiResult)
            : this(sakiResult._errors)
        {

        }

        public void AddError(ISakiError error)
        {
            if (error == null) return;

            if (_errors == null)
                _errors = new HashSet<ISakiError>();

            _errors.Add(error);
        }
    }
}

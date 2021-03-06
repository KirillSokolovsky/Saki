﻿namespace Saki.Result
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiResult<T> : SakiResult, ISakiResult<T>
    {
        public T Data { get; set; }

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

    public class SakiResult : ISakiResult
    {
        private HashSet<ISakiError> _errors;
        public IEnumerable<ISakiError> Errors => _errors;
        public bool HasErrors => _errors == null || _errors.Count == 0;

        public SakiResult()
        {
        }

        public SakiResult(ISakiError error)
        {
            AddError(error);
        }

        public SakiResult(IEnumerable<ISakiError> errors)
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
                _errors = new HashSet<ISakiError>();

            _errors.Add(error);
        }
    }

    public class Test
    {
        public void Do()
        {
        }
    }
}

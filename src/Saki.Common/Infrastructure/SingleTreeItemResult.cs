namespace Saki.Common.Infrastructure
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SingleTreeItemResult<T> : SakiResult<T>
        where T : SakiTreeItem
    {
        public SingleTreeItemResult(T data) : base(data)
        {
        }

        public SingleTreeItemResult(ISakiError error) : base(error)
        {
        }

        public SingleTreeItemResult(IEnumerable<ISakiError> errors) : base(errors)
        {
        }

        public SingleTreeItemResult(SakiResult<T> sakiResult) : base(sakiResult)
        {
        }
    }
}

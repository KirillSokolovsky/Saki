namespace Saki.Common.Infrastructure
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MultiTreeItemResult<T> : SakiResult<IEnumerable<T>>
        where T : SakiTreeItem
    {
        public MultiTreeItemResult(IEnumerable<T> data) : base(data)
        {
        }

        public MultiTreeItemResult(ISakiError error) : base(error)
        {
        }

        public MultiTreeItemResult(IEnumerable<ISakiError> errors) : base(errors)
        {
        }

        public MultiTreeItemResult(SakiResult<IEnumerable<T>> sakiResult) : base(sakiResult)
        {
        }
    }
}

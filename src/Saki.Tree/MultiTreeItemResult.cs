namespace Saki.Tree
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MultiTreeItemResult : SakiResult<IEnumerable<SakiTreeItem>>
    {
        public MultiTreeItemResult(IEnumerable<SakiTreeItem> data) : base(data)
        {
        }

        public MultiTreeItemResult(ISakiError error) : base(error)
        {
        }

        public MultiTreeItemResult(IEnumerable<ISakiError> errors) : base(errors)
        {
        }

        public MultiTreeItemResult(SakiResult<IEnumerable<SakiTreeItem>> sakiResult) : base(sakiResult)
        {
        }
    }
}

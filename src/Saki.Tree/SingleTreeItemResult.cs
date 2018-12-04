namespace Saki.Tree
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SingleTreeItemResult : SakiResult<SakiTreeItem>
    {
        public SingleTreeItemResult(SakiTreeItem data) : base(data)
        {
        }

        public SingleTreeItemResult(ISakiError error) : base(error)
        {
        }

        public SingleTreeItemResult(IEnumerable<ISakiError> errors) : base(errors)
        {
        }

        public SingleTreeItemResult(SakiResult<SakiTreeItem> sakiResult) : base(sakiResult)
        {
        }
    }
}

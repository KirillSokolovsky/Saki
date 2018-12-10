namespace Saki.Common.SakiTreeItems
{
    using Saki.Common.Interfaces;
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiTreeItemsFrameworkExtension : ISakiFrameworkExension
    {
        public ISakiCommandExecutor<TCommand, TResult> GetCommandExecutor<TCommand, TResult>(string itemType, string commandName)
            where TCommand : ISakiCommand<TResult>
            where TResult : IBaseSakiResult
        {
            throw new NotImplementedException();
        }
    }
}

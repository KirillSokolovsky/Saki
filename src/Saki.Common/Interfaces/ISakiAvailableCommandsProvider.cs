namespace Saki.Common.Interfaces
{
    using Saki.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISakiAvailableCommandsProvider
    {
        Task<ISakiResult<IEnumerable<ISakiAvailableCommand>>> GetAvailableCommands(SakiTreeState treeState);
    }
}

namespace Saki.Framework.Interfaces
{
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISakiCommandsProvider
    {
        Task<SakiResult<IEnumerable<ISakiCommand>>> GetAvailableCommands(ISakiTreeState treeState, CancellationToken cancellationToken);
    }
}

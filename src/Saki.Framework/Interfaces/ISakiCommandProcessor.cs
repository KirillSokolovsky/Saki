namespace Saki.Framework.Interfaces
{
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISakiCommandProcessor<in TCommand>
        where TCommand : ISakiCommand
    {
        Task<SakiResult> ProcessCommand(TCommand command, CancellationToken cancellationToken);
    }
}

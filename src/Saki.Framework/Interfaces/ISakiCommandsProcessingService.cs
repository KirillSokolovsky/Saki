namespace Saki.Framework.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISakiCommandsProcessingService
    {
        Task ProcessCommand(ISakiCommand command, CancellationToken cancellationToken);
    }
}

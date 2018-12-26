namespace Saki.Framework.DesktopApp
{
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDesktopCommandProcessingService
    {
        Task<SakiResult> ProcessCommand(ISakiCommand command, CancellationToken cancellationToken);
    }
}

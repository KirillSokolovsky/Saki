namespace Saki.Framework.Result
{
    using Saki.Framework.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiError
    {
        string Message { get; }
        SakiException Exception { get; }
    }
}

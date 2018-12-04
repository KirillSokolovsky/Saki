namespace Saki.Result
{
    using Saki.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiError
    {
        string Message { get; }
        SakiException Exception { get; }
    }
}

namespace Saki.Result
{
    using Saki.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiError : ISakiError
    {
        public string Message { get; set; }

        public SakiException Exception { get; set; }

        public SakiError(string message, SakiException sakiException = null)
        {
            Message = message;
            Exception = sakiException;
        }
    }
}

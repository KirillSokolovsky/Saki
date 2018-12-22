namespace Saki.Framework.Internal
{
    using Saki.Framework.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiHandlerResolvingException : SakiException
    {
        public SakiHandlerResolvingException(string exType, string message, Exception innerException = null) 
            : base($"SakiHandlerResolving.{exType}", message, innerException)
        {
        }
    }
}

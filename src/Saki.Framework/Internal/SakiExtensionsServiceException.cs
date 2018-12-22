namespace Saki.Framework.Internal
{
    using Saki.Framework.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiExtensionsServiceException : SakiException
    {
        public SakiExtensionsServiceException(string exType, string message, Exception innerException = null) 
            : base($"SakiExtensionsService.{exType}", message, innerException)
        {
        }
    }
}

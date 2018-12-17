namespace Saki.Framework.Services.SerializationService
{
    using Saki.Framework.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiTreeItemDataSerializationException : SakiException
    {
        public SakiTreeItemDataSerializationException(string exType, string message, Exception innerException = null)
            : base($"SakiTreeItemDataSerialization.{exType}", message, innerException)
        {
        }
    }
}

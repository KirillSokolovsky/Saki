namespace Saki.TreeStorage.FileSystemStorage
{
    using Saki.Framework.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FileSystemSakiTreeStorageException : SakiException
    {
        public FileSystemSakiTreeStorageException(string exType, string message, Exception innerException = null) 
            : base($"FileSystemSakiTreeStorage.{exType}", message, innerException)
        {
        }
    }
}

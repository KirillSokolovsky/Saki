namespace Saki.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiException : Exception
    {
        public string SakiExType { get; set; }

        public SakiException(string exType, string message, Exception innerException)
            : base(message, innerException)
        {
            SakiExType = exType;
        }

        public override string ToString()
        {
            return $"SakiExType: {SakiExType}{Environment.NewLine}{GetExceptionInfoWithNewLineEnd()}{base.ToString()}";
        }

        protected virtual string GetExceptionInfoWithNewLineEnd()
        {
            return null;
        }
    }
}

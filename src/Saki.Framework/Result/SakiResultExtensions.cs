namespace Saki.Framework.Result
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    public static class SakiResultExtensions
    {
        public static string GetFullErrorsString(this ISakiResult result)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Errors cound: {result.Errors?.Count() ?? 0}");

            if(result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    sb.AppendLine(error.Message);
                    sb.AppendLine(error.Exception?.ToString());
                }
            }

            return sb.ToString();
        }
    }
}

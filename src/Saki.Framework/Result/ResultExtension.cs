namespace Saki.Framework.Result
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ResultExtension
    {
        public static bool TryGetData<T>(this ISakiResult<T> sakiResult, out T data)
        {
            if (sakiResult.Errors == null || sakiResult.Errors.Count() == 0)
            {
                data = sakiResult.Data;
                return true;
            }
            data = default(T);
            return false;
        }
    }
}

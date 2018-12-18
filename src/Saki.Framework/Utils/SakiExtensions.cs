namespace Saki.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class SakiExtensions
    {
        public static string GetSakiTypeName(this Type type)
        {
            return $"{type.FullName}, {type.Assembly.GetName().Name}";
        }
    }
}

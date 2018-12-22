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

        public static List<Type> GetParentTypesTillParent(this Type childType, Type parentType)
        {
            if (!parentType.IsAssignableFrom(childType)) return new List<Type>();
            var list = new List<Type>();

            while(parentType.IsAssignableFrom(childType) && childType != parentType)
            {
                list.Add(childType);
                childType = childType.BaseType;
            }

            return list;
        }
    }
}

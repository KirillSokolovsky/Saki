namespace Saki.Framework.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiCommandsProviderAttribute : Attribute
    {
        public Type ItemDataType { get; private set; }

        public SakiCommandsProviderAttribute(Type itemDataType)
        {
            ItemDataType = itemDataType;
        }
    }
}

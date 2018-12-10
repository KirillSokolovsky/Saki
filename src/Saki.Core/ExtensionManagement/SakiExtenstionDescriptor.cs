namespace Saki.Core.ExtensionManagement
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiExtenstionDescriptor
    {
        public string ExtensionName { get; set; }
        public List<Type> ProvidedItemTypes { get; set; }
    }
}

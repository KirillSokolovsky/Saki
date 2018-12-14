namespace Saki.Framework.Internal.ExtensionsInfo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiRequestInfo
    {
        public string CommandName { get; set; }
        public string ItemDataTypeName { get; set; }

        public SakiRequestHandlerInfo ResolvedHandlerInfo { get; set; }
    }
}

namespace Saki.Framework.Internal.ExtensionsInfo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiRequestHandlerInfo
    {
        public string HandledCommandName { get; set; }
        public string ProcessingItemDataType { get; set; }

        public List<SakiRequestInfo> RegisteredRequests { get; set; }
    }
}

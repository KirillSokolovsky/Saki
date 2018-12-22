namespace Saki.Framework.Extensions.WebElements
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WebLocatorInfo
    {
        public string LocatorValue { get; set; }
        public WebLocatorType LocatorType { get; set; }
        public bool IsRelative { get; set; }
    }
}

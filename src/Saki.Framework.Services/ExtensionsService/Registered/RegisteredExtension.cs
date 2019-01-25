namespace Saki.Framework.Services.ExtensionsService.Registered
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RegisteredExtension
    {
        public string ExtensionName { get; protected set; }
        public string AssemblyName { get; protected set; }

        public RegisteredExtension(string extensionName, string assemblyName)
        {
            ExtensionName = extensionName;
            AssemblyName = assemblyName;
        }
    }
}

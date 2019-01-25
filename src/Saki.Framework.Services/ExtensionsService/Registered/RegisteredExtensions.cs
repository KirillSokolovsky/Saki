namespace Saki.Framework.Services.ExtensionsService.Registered
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RegisteredExtensions
    {
        private List<RegisteredExtension> _extensions = new List<RegisteredExtension>();

        public RegisteredExtensions()
        {

        }

        public RegisteredExtension GetOrAdd(string extensionName, string assemblyName)
        {
            var extension = _extensions.FirstOrDefault(e => e.ExtensionName == extensionName);
            if(extension == null)
            {
                extension = new RegisteredExtension(extensionName, assemblyName);
                _extensions.Add(extension);
            }

            return extension;
        }

        public IReadOnlyCollection<RegisteredExtension> AllExtensions()
        {
            return _extensions;
        }
    }
}

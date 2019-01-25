namespace Net.TestConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Reflection;
    using Saki.Framework.Attributes;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.NetExtensionsLoader;
    using SimpleInjector;
    using Saki.Framework.Logging;
    using Saki.Framework.Logging.Simple;
    using Saki.Framework.Services.ExtensionsService;

    class Program
    {
        static void Main(string[] args)
        {
            var baseExtDir = @"C:\Dev\SaKi\Saki\src\Extensions\SakiBase\Saki.Framework.Extensions.Base\bin\Debug\netstandard2.0";
            var weExtDir = @"C:\Dev\SaKi\Saki\src\Extensions\WebElements\Saki.Framework.Extensions.WebElements\bin\Debug\netstandard2.0";

            var container = new Container();
            container.Register<ISakiExtensionsLoadingService, SakiNetExtensionsLoadingService>();
            container.Register<ISakiExtensionsService, SakiExtensionsService>();
            container.Register<ILogger, SimpleLogger>();

            container.Verify();

            var extService = container.GetInstance<ISakiExtensionsService>();

            extService.LoadExtension(baseExtDir);
            extService.LoadExtension(weExtDir);
            
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}

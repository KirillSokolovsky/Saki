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
            var ext1directory = @"C:\Dev\SaKi\Saki\src\sample\SampleExtension\bin\Debug\netstandard2.0";
            var ext2directory = @"C:\Dev\SaKi\Saki\src\sample\SampleExtensionWithDependency\bin\Debug\netstandard2.0";

            var container = new Container();
            container.Register<ISakiExtensionsLoadingService, SakiNetExtensionsLoadingService>();
            container.Register<ISakiExtensionsService, SakiExtensionsService>();
            container.Register<ILogger, SimpleLogger>();

            container.Verify();

            var extService = container.GetInstance<ISakiExtensionsService>();

            var extAss = extService.LoadExtension(ext1directory);
            var extAss1 = extService.LoadExtension(ext2directory);

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}

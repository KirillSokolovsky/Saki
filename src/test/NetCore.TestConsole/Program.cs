﻿namespace NetCore.TestConsole
{
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using Saki.Framework.Logging.Simple;
    using Saki.Framework.NetCoreExtensionsLoader;
    using Saki.Framework.Services.ExtensionsService;
    using SimpleInjector;
    using System;
    using System.Linq;
    using System.Runtime.Loader;

    class Program
    {
        static void Main(string[] args)
        {
            var ext1directory = @"C:\Dev\SaKi\Saki\src\sample\SampleExtension\bin\Debug\netstandard2.0";
            var ext2directory = @"C:\Dev\SaKi\Saki\src\sample\SampleExtensionWithDependency\bin\Debug\netstandard2.0";

            var container = new Container();
            container.Register<ISakiExtensionsLoadingService, SakiNetCoreExtensionsLoadingService>();
            container.Register<ISakiExtensionsService, SakiExtensionsService>();
            container.Register<ILogger, SimpleLogger>();

            container.Verify();

            var extService = container.GetInstance<ISakiExtensionsService>();

            var extAss = extService.LoadExtension(ext1directory);
            var extAss1 = extService.LoadExtension(ext2directory);

            var t = Type.GetType("SampleItemData");

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}

namespace NetCore.TestConsole
{
    using Saki.Framework.Base.SakiTree.Commands.Get;
    using Saki.Framework.Extensions.Base;
    using Saki.Framework.Extensions.Base.Project;
    using Saki.Framework.Extensions.Base.Project.Create;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using Saki.Framework.Logging.Simple;
    using Saki.Framework.NetCoreExtensionsLoader;
    using Saki.Framework.SakiTree;
    using Saki.Framework.Services.MediatorService;
    using Saki.Framework.Services.ExtensionsService;
    using Saki.Framework.Services.SerializationService;
    using Saki.Framework.Services.TreeRepositoryService;
    using Saki.TreeStorage.FileSystemStorage;
    using SimpleInjector;
    using System;
    using System.Linq;
    using System.Runtime.Loader;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            var ext1directory = @"C:\Dev\SaKi\Saki\src\sample\SampleExtension\bin\Debug\netstandard2.0";
            var ext2directory = @"C:\Dev\SaKi\Saki\src\sample\SampleExtensionWithDependency\bin\Debug\netstandard2.0";
            var baseExtDir = @"C:\Dev\SaKi\Saki\src\Extensions\SakiBase\Saki.Framework.Extensions.Base\bin\Debug\netstandard2.0";

            var container = new Container();
            container.Register<ILogger, SimpleLogger>(Lifestyle.Singleton);
            container.Register<ISakiTreeItemDataSerializaionService, SakiTreeItemDataSerializationService>(Lifestyle.Singleton);
            container.Register<ISakiTreeStorage>(() => new FileSystemSakiTreeStorage("tree.json"), Lifestyle.Singleton);
            container.Register<ISakiTreeRepositoryService, SakiTreeRepositoryService>(Lifestyle.Singleton);
            container.Register<ISakiExtensionsLoadingService, SakiNetCoreExtensionsLoadingService>(Lifestyle.Singleton);
            container.Register<ISakiExtensionsService, SakiExtensionsService>(Lifestyle.Singleton);
            container.Register<ISakiMediatorService, SakiMediatorService>(Lifestyle.Singleton);
                                 
            container.Verify();

            var extService = container.GetInstance<ISakiExtensionsService>();
            var extAss = extService.LoadExtension(null);

            var core = container.GetInstance<ISakiMediatorService>();

            var data = new SakiProjectTreeItemData();
            var item = new SakiTreeItem<SakiProjectTreeItemData>(data);

            var req = new CreateSakiProjectTreeItemRequest(ExtensionInfoProvider.BaseExtensionName, item);

            var h = core.ProcessRequest(req, CancellationToken.None);
            var r = h.Result;

            var gItem = new GetTreeItemRequest<SakiProjectTreeItemData>(ExtensionInfoProvider.BaseExtensionName, r.Data);
            var h1 = core.ProcessRequest(gItem, CancellationToken.None);
            var r1 = h1.Result;

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}

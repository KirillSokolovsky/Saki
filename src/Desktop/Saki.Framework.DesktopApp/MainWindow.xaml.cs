namespace Saki.Framework.DesktopApp
{
    using MahApps.Metro.Controls;
    using Saki.Framework.DesktopApp.Extensions.Base.Project;
    using Saki.Framework.DesktopApp.Extensions.Base.Tree;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Internal.Interfaces;
    using Saki.Framework.Logging;
    using Saki.Framework.Logging.Simple;
    using Saki.Framework.NetExtensionsLoader;
    using Saki.Framework.Services.MediatorService;
    using Saki.Framework.Services.ExtensionsService;
    using Saki.Framework.Services.SerializationService;
    using Saki.Framework.Services.TreeRepositoryService;
    using Saki.TreeStorage.FileSystemStorage;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Saki.Framework.DesktopApp.Core;
    using Saki.Framework.Result;
    using Saki.Framework.Base.SakiTree.Requests.Get;
    using Saki.Framework.Base.SakiTree.Requests.GetChildren;
    using System.Threading;

    public partial class MainWindow : MetroWindow
    {
        public class ViewModel
        {
            public ObservableCollection<TreeItemViewModel> Items { get; set; } = new ObservableCollection<TreeItemViewModel>();
        }

        private ViewModel _model;
        private readonly Container _container;

        public MainWindow()
        {
            _model = new ViewModel();

            _container = new Container();

            _container.Register<ILogger, SimpleLogger>(Lifestyle.Singleton);
            _container.Register<ISakiTreeItemDataSerializaionService, SakiTreeItemDataSerializationService>(Lifestyle.Singleton);
            _container.Register<ISakiTreeStorage>(() => new FileSystemSakiTreeStorage("tree.json"), Lifestyle.Singleton);
            _container.Register<ISakiTreeRepositoryService, SakiTreeRepositoryService>(Lifestyle.Singleton);

            _container.Register<ISakiDesktopExtensionsLoadingService, SakiDesktopExtensionsLoadingService>(Lifestyle.Singleton);
            _container.Register<ISakiDesktopExtensionsService, SakiDesktopExtensionsService>(Lifestyle.Singleton);

            _container.Register<ISakiExtensionsLoadingService>(() => _container.GetInstance<ISakiDesktopExtensionsLoadingService>(), Lifestyle.Singleton);
            _container.Register<ISakiExtensionsService>(() => _container.GetInstance<ISakiDesktopExtensionsService>(), Lifestyle.Singleton);

            _container.Register<ISakiMediatorService, SakiMediatorService>(Lifestyle.Singleton);

            _container.Verify();

            DataContext = _model;

            InitializeComponent();
        }

        private async void DoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var items = _model.Items;


            var projItem = new ProjectTreeItemViewModel { Name = "MyProject", Description = "My project desc", ItemId = 1 };

            items.Add(projItem);

            for (int i = 1; i <= 4; i++)
            {
                var item = new TreeItemViewModel { Name = $"Item {i}", Description = $"Desc for item {i}", ItemId = i + 1 };
                projItem.Items.Add(item);

                for (int j = 1; j <= 3; j++)
                {
                    var citem = new TreeItemViewModel { Name = $"Child Item {i}_{j}", Description = $"Desc for child item {i}_{j}", ItemId = 5 + j };
                    item.Items.Add(citem);
                }
            }
        }

        private async void LoadTree_Click(object sender, RoutedEventArgs e)
        {
            var st = _container.GetInstance<ISakiTreeStorage>() as FileSystemSakiTreeStorage;
            var loadResult = await st.Load().ConfigureAwait(true);
            if (loadResult.Result != SakiResultType.Ok)
            {
                MessageBox.Show(loadResult.GetFullErrorsString(), "Error occured during loading tree.");
                return;
            }

            var treeRepo = _container.GetInstance<ISakiTreeRepositoryService>();
            var desktopService = _container.GetInstance<ISakiDesktopExtensionsService>();

            var mediator = _container.GetInstance<ISakiMediatorService>();

            var request = new GetChildTreeItemsRequest(Extensions.Base.ExtensionInfoProvider.BaseExtensionName, 0);

            var topLevelTreeItemsResult = await mediator.ProcessRequest<ISakiResult<IEnumerable<ISakiResult<ISakiTreeItem<ISakiTreeItemData>>>>>
                (request, CancellationToken.None).ConfigureAwait(true);

            var items = topLevelTreeItemsResult.Data.ToList();
        }

        private async void SaveTree_Click(object sender, RoutedEventArgs e)
        {
            var st = _container.GetInstance<ISakiTreeStorage>() as FileSystemSakiTreeStorage;
            var saveResult = await st.Save().ConfigureAwait(true);

            if (saveResult.Result != SakiResultType.Ok)
            {
                MessageBox.Show(saveResult.GetFullErrorsString(), "Error occured during saving tree.");
                return;
            }
        }
    }
}

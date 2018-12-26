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
    using Saki.Framework.Services.CoreService;
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

    public partial class MainWindow : MetroWindow
    {
        public class ViewModel
        {
            public ObservableCollection<TreeItemViewModel> Items { get; set; } = new ObservableCollection<TreeItemViewModel>();
        }

        private ViewModel _model;

        public MainWindow()
        {
            _model = new ViewModel();

            var container = new Container();
            container.Register<ILogger, SimpleLogger>(Lifestyle.Singleton);
            container.Register<ISakiTreeItemDataSerializaionService, SakiTreeItemDataSerializationService>(Lifestyle.Singleton);
            container.Register<ISakiTreeStorage>(() => new FileSystemSakiTreeStorage("tree.json"), Lifestyle.Singleton);
            container.Register<ISakiTreeRepositoryService, SakiTreeRepositoryService>(Lifestyle.Singleton);
            container.Register<ISakiExtensionsLoadingService, SakiNetExtensionsLoadingService>(Lifestyle.Singleton);
            container.Register<ISakiExtensionsService, SakiExtensionsService>(Lifestyle.Singleton);
            container.Register<ISakiCoreService, SakiCoreService>(Lifestyle.Singleton);

            container.Verify();

            DataContext = _model;

            InitializeComponent();
        }

        private void DoMenuItem_Click(object sender, RoutedEventArgs e)
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
    }
}

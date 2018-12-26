namespace Saki.Framework.DesktopApp.Extensions.Base.Tree
{
    using ReactiveUI;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TreeItemViewModel : ReactiveObject
    {
        public TreeItemViewModel()
        {
            _items = new ObservableCollection<TreeItemViewModel>();
        }

        private int _itemId;
        public int ItemId
        {
            get => _itemId;
            set => this.RaiseAndSetIfChanged(ref _itemId, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        private ObservableCollection<TreeItemViewModel> _items;
        public ObservableCollection<TreeItemViewModel> Items
        {
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }
    }
}

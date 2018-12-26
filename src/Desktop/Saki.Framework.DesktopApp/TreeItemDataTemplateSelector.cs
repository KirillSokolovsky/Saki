namespace Saki.Framework.DesktopApp
{
    using Saki.Framework.DesktopApp.Extensions.Base.Tree;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    public class TreeItemDataTemplateSelector : DataTemplateSelector
    {
        private static readonly Type _baseType = typeof(TreeItemViewModel);

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (container is FrameworkElement element && item != null && item is TreeItemViewModel model)
            {
                var itemType = item.GetType();

                while (_baseType.IsAssignableFrom(itemType))
                {
                    var resourceName = $"{itemType.Name}TreeItemDataTemplate";
                    var res = element.TryFindResource(resourceName);
                    if (res != null)
                        return res as DataTemplate;
                    itemType = itemType.BaseType;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}

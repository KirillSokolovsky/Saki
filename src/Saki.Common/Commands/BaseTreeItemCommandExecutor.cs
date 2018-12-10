namespace Saki.Common.Commands
{
    using Saki.Common.Interfaces;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class BaseTreeItemCommandExecutor
    {
        protected readonly ISakiTreeRepository _treeRepository;
        protected readonly Container _container;

        public BaseTreeItemCommandExecutor(Container container)
        {
            _container = container;
            _treeRepository = container.GetInstance<ISakiTreeRepository>();
        }
    }
}

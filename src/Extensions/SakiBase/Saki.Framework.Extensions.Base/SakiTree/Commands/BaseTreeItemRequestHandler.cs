namespace Saki.Framework.Base.SakiTree.Commands
{
    using Saki.Framework.Interfaces;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class BaseTreeItemRequestHandler
    {
        protected readonly Container _container;
        protected readonly ISakiTreeRepositoryService _repositoryService;

        public BaseTreeItemRequestHandler(Container container)
        {
            _container = container;
            _repositoryService = _container.GetInstance<ISakiTreeRepositoryService>();
        }
    }
}

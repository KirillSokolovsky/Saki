namespace Saki.Console
{
    using Saki.Common;
    using Saki.Common.Interfaces;
    using Saki.Result;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var ass = new List<Assembly>
            {
                typeof(Program).Assembly,
                //typeof(Common.SakiTreeItem).Assembly,
                //typeof(Extensions.SakiProject.Commands.CreateSakiProjectCommandHandler).Assembly
            };

            var container = new Container();

            var r = Do<SakiResult<int>>();
        }

        public static TResult Do<TResult>()
            where TResult : IBaseSakiResult
        {
            var error = new SakiError("");
            var errorResult = Activator.CreateInstance<TResult>();
            errorResult.AddError(error);
            return errorResult;
        }
               
        public async Task<IEnumerable<ISakiTreeItem<ISakiTreeItemData>>> GetChildItems(int parentItemId, CancellationToken token)
        {
            await Task.Delay(1000);

            var list = new List<ISakiTreeItem<ISakiTreeItemData>>();

            list.Add(new SakiTreeItem<Data1>());

            return list;
        }

        public class Data1 : SakiTreeItemData
        {
            public bool Test { get; set; }
        }
    }
}

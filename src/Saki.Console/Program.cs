namespace Saki.Console
{
    using MediatR;
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
    }
}

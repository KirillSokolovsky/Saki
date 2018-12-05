namespace Saki.Console
{
    using MediatR;
    using SimpleInjector;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var thisAssembly = typeof(Program).Assembly;

            var container = new Container();

            var types = container.GetTypesToRegister(typeof(IRequestHandler<,>), thisAssembly);
        }
    }

    public class A { };
    public class B : A { };

    public class R1 : IRequest<A> { }

    public class H1 : IRequestHandler<R1, A>
    {
        public Task<A> Handle(R1 request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

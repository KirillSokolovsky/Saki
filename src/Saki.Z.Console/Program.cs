using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using SimpleInjector;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;

            var container = new Container();
            //container.Register(typeof(Generic<>));

            container.Verify();

            var ga = container.GetInstance<Generic<A>>();
            ga.Data = new A();
            
            Console.WriteLine("Done");
            Console.ReadLine();
        }        
    }

    public interface Ia { };

    public class A : Ia { };
    public class A1 : Ia { };

    public class Generic<T>
        where T : Ia
    {
        public T Data { get; set; }
    }
}

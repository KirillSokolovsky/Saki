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

            var mediator = new Mediator();
            mediator.ScanAssembly(assembly);


            var createWebTreeItemRequest = new CreateTreeWebItemRequest<WebElementData>();

            var result = mediator.ProcessRequest(createWebTreeItemRequest, CancellationToken.None).Result;


            var result1 = new CreateTreeItemHandler().Handle(createWebTreeItemRequest, CancellationToken.None).Result;

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        //static async Task<TResult> ProcessCommand<TResult>(Container container, ISakiRequest<TResult> request)
        //    where TResult : IBaseSakiResult
        //{
        //    var commandType = request.GetType();
        //    var commandImplementedRequestInterface = commandType.FindInterfaces(
        //        (t, o) => t.Name.StartsWith("ISakiRequest"), null
        //        ).First();

        //    var resultType = commandImplementedRequestInterface.GetGenericArguments()[0];

        //    var handlerType = typeof(ISakiRequestHandler<,>).MakeGenericType(commandType, resultType);


        //    var handler = container.GetInstance(handlerType);

        //    var mi = handler.GetType().GetMethod("Handle");
        //    var result = mi.Invoke(handler, new object[] { request, CancellationToken.None });

        //    var casted = (Task<TResult>)result;

        //    var realResult = await casted;

        //    return realResult;
        //}

        //static void Register<TRequest, THandler, TResult>(Container container)
        //    where TResult : IBaseSakiResult
        //    where TRequest : ISakiRequest<TResult>
        //    where THandler : ISakiRequestHandler<TRequest, TResult>
        //{
        //    var commandType = typeof(TRequest);
        //    var resultType = typeof(TResult);

        //    var handlerType = typeof(ISakiRequestHandler<,>)
        //        .MakeGenericType(commandType, resultType);

        //    container.Register(handlerType, typeof(THandler));
        //}
    }






}

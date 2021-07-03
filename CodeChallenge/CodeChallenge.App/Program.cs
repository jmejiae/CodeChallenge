using Autofac;
using CodeChallenge.Core;
using CodeChallenge.Core.Helpers;
using CodeChallenge.Core.Type;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallenge.App
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IConsoleOutput>();
            builder.RegisterType<ConsoleUtilities>();
            builder.RegisterType<CalculateMEX>().As<ICalculate>();
            builder.RegisterType<CalculateUSA>().As<ICalculate>();

            Container = builder.Build();

            CalculateCoins();
        }

        public static void CalculateCoins()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var console = scope.Resolve<IConsoleOutput>();
                console.CalculateItemPrice();
            }
        }
    }
}

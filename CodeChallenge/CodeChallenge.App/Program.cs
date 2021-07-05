using Autofac;
using Autofac.Configuration;
using AutofacSerilogIntegration;
using CodeChallenge.Core;
using CodeChallenge.Core.Currency;
using CodeChallenge.Core.Helpers;
using CodeChallenge.Core.Type;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration;
using Serilog;
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
            // Serilog configuration
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            // Add configuration
            var config = new ConfigurationBuilder();
            config.AddJsonFile("appSettings.json");
            var module = new ConfigurationModule(config.Build());

            // Creating builder
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);
            builder.RegisterType<AmericanDolar>().As<ICurrency>();
            builder.RegisterType<MexicanPeso>().As<ICurrency>();
            builder.RegisterType<ConsoleOutput>().As<IConsoleOutput>();
            builder.RegisterType<ConsoleUtilities>();
            builder.Register<ICalculate>(ctx =>
            {
                if (module.Configuration["CurrencyType"] == "MEX")
                {
                    return new CalculateMEX(ctx.Resolve<ILogger>(), new MexicanPeso());
                }

                return new CalculateUSA(ctx.Resolve<ILogger>(), new AmericanDolar());
            }).As<ICalculate>()
            .SingleInstance();

            builder.RegisterLogger();
            Container = builder.Build();

            // Execute program
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

using Autofac;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using ScreenApi.Serilog;

namespace ScreenApi
{
    public static class Autofac
    {
        public static IContainer GetContainer() {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(SerilogConfiguration.CreateLogger())
                .As<ILogger>()
                .SingleInstance();

            var container = builder.Build();

            return container;
        }
    }
}

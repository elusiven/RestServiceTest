using Autofac;
using Autofac.Core;
using Optimation.Service.Abstractions;
using System;
using System.Linq;

namespace Optimation.Service.ModuleRegistration
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<EmailProcessingService>()
                .As<IEmailProcessingService>()
                .InstancePerLifetimeScope();
        }
    }
}

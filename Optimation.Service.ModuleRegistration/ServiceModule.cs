﻿using Autofac;
using Optimation.Service.Abstractions;

namespace Optimation.Service.ModuleRegistration
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailProcessingService>().As<IEmailProcessingService>().InstancePerLifetimeScope();
        }
    }
}

﻿using Autofac;
using WebAppServer.Autofac.Extensions;
using WebAppServer.Domain;
using WebAppServer.Domain.Services;
using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.Repository;
using Module = Autofac.Module;

namespace WebAppServer.Autofac.Modules;

public class AutofacCommonModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterTypesEndingWith(typeof(RepositoryConfig).Assembly, "Repository").AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterTypesEndingWith(typeof(DomainConfig).Assembly, "Service").AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterTypesEndingWith(typeof(DomainConfig).Assembly, "Mapper").AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterType<SubscriptionHandler>().As<ISubscriptionHandler>().SingleInstance();

        return;
    }
}

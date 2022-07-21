using Autofac;
using WebAppServer.Common.Extensions;
using WebAppServer.Domain.Services;
using WebAppServer.Domain.Services.Interfaces;

namespace WebAppServer.Domain;

public static class AutofacDomainConfiguration
{
    public static ContainerBuilder AddDomain(this ContainerBuilder builder)
    {
        var assembly = typeof(AutofacDomainConfiguration).Assembly;

        builder.RegisterTypesEndingWith(assembly, "Service").AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterType<SubscriptionHandler>().As<ISubscriptionHandler>().SingleInstance();

        return builder;
    }
}

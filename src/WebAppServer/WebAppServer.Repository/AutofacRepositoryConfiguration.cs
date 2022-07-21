using Autofac;
using WebAppServer.Common.Extensions;

namespace WebAppServer.Repository;

public static class AutofacRepositoryConfiguration
{
    public static ContainerBuilder AddRepository(this ContainerBuilder builder)
    {
        var assembly = typeof(AutofacRepositoryConfiguration).Assembly;

        builder.RegisterTypesEndingWith(assembly, "Repository").AsImplementedInterfaces().InstancePerLifetimeScope();

        return builder;
    }
}

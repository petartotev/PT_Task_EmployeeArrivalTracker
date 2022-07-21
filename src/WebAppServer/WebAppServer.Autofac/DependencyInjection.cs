using Autofac;
using WebAppServer.Autofac.Modules;

namespace WebAppServer.Autofac;

public static class DependencyInjection
{
    public static ContainerBuilder ConfigureHost(this ContainerBuilder builder)
    {
        builder.RegisterModule<AutofacCommonModule>();

        return builder;
    }
}

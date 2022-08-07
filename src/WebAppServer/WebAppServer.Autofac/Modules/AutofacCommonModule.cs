using Autofac;
using WebAppServer.Api.Policies;
using WebAppServer.Autofac.Extensions;
using WebAppServer.Domain;
using WebAppServer.Domain.Services;
using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.Repository;
using WebAppServer.Repository.Interfaces;
using Module = Autofac.Module;

namespace WebAppServer.Autofac.Modules;

public class AutofacCommonModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterTypesEndingWith(typeof(RepositoryConfig).Assembly, "Repository").AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterTypesEndingWith(typeof(DomainConfig).Assembly, "Validator").AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterTypesEndingWith(typeof(DomainConfig).Assembly, "Service").AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterTypesEndingWith(typeof(DomainConfig).Assembly, "Mapper").AsImplementedInterfaces().InstancePerLifetimeScope();

        builder.RegisterType<DbContext>().As<IDbContext>();
        builder.RegisterType<SubscriptionHandler>().As<ISubscriptionHandler>().SingleInstance();
        builder.RegisterType<ClientPolicy>().AsSelf().SingleInstance();

        return;
    }
}

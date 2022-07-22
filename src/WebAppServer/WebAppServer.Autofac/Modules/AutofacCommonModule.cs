using Autofac;
using Dapperer;
using Dapperer.DbFactories;
using Dapperer.QueryBuilders.MsSql;
using WebAppServer.Autofac.Extensions;
using WebAppServer.Common.Configuration;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Domain;
using WebAppServer.Domain.Services;
using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.Repository;
using WebAppServer.Repository.DbUp;
using WebAppServer.Repository.DbUp.Interfaces;
using WebAppServer.Repository.Seeder;
using WebAppServer.Repository.Seeder.Interfaces;
using Module = Autofac.Module;

namespace WebAppServer.Autofac.Modules;

public class AutofacCommonModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterDatabase(builder);

        builder.RegisterTypesEndingWith(typeof(RepositoryConfig).Assembly, "Repository").AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterTypesEndingWith(typeof(DomainConfig).Assembly, "Service").AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterTypesEndingWith(typeof(DomainConfig).Assembly, "Mapper").AsImplementedInterfaces().InstancePerLifetimeScope();

        builder.RegisterType<SubscriptionHandler>().As<ISubscriptionHandler>().SingleInstance();

        return;
    }

    private static void RegisterDatabase(ContainerBuilder builder)
    {
        builder.RegisterType<EnvironmentSettings>().As<IDbSettings>().SingleInstance();
        builder.RegisterType<DatabaseUpgrader>().As<IDatabaseUpgrader>().SingleInstance();
        builder.RegisterType<DatabaseSeeder>().As<IDatabaseSeeder>().SingleInstance();

        builder.RegisterType<SqlQueryBuilder>().As<IQueryBuilder>();
        builder.RegisterType<SqlDbFactory>().As<IDbFactory>();
        builder.RegisterType<DappererSettings>().As<IDappererSettings>();
    }
}

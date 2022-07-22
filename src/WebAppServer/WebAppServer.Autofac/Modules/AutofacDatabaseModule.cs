using Autofac;
using Dapperer;
using Dapperer.DbFactories;
using Dapperer.QueryBuilders.MsSql;
using WebAppServer.Common.Configuration;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Repository;
using WebAppServer.Repository.DbUp;
using WebAppServer.Repository.DbUp.Interfaces;
using WebAppServer.Repository.Seeder;
using WebAppServer.Repository.Seeder.Interfaces;

namespace WebAppServer.Autofac.Modules;

public class AutofacDatabaseModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EnvironmentSettings>().As<IDbSettings>().SingleInstance();
        builder.RegisterType<DatabaseUpgrader>().As<IDatabaseUpgrader>().SingleInstance();
        builder.RegisterType<DatabaseSeeder>().As<IDatabaseSeeder>().SingleInstance();

        builder.RegisterType<SqlQueryBuilder>().As<IQueryBuilder>();
        builder.RegisterType<SqlDbFactory>().As<IDbFactory>();
        builder.RegisterType<DappererSettings>().As<IDappererSettings>();
    }
}

using Autofac;
using WebAppServer.Common.Configuration;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Domain;
using WebAppServer.Repository;
using WebAppServer.Repository.DbUp;
using WebAppServer.Repository.DbUp.Interfaces;
using WebAppServer.Repository.Seeder;
using WebAppServer.Repository.Seeder.Interfaces;

namespace WebAppServer.Autofac.Modules;

public class AutofacCommonModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterDatabase(builder);

        builder.AddRepository();
        builder.AddDomain();
    }

    private static void RegisterDatabase(ContainerBuilder builder)
    {
        builder.RegisterType<EnvironmentSettings>().As<IDbSettings>().SingleInstance();
        builder.RegisterType<DatabaseUpgrader>().As<IDatabaseUpgrader>().SingleInstance();
        builder.RegisterType<DatabaseSeeder>().As<IDatabaseSeeder>().SingleInstance();
    }
}

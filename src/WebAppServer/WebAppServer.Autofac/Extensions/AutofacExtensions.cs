using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;

namespace WebAppServer.Autofac.Extensions;

public static class AutofacExtensions
{
    public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterTypesEndingWith(
        this ContainerBuilder builder,
        Assembly assembly,
        string typeNameEndsWith)
    {
        return builder.RegisterAssemblyTypes(assembly).Where(x => x.IsClass && x.Name.EndsWith(typeNameEndsWith));
    }
}

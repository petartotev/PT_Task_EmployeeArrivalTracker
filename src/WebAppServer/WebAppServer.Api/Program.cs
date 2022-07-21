using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Serilog;
using Serilog.Events;
using WebAppServer.Api.Extensions;
using WebAppServer.Api.Filters;
using WebAppServer.Api.Middlewares;
using WebAppServer.Autofac;
using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.Repository.DbUp.Interfaces;
using WebAppServer.Repository.Seeder.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(builder => builder.ConfigureHost()));

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day));

builder.Services.UseHangfire(connectionString);

builder.Services.AddControllers(options => options.Filters.Add<FourthTokenHeaderRequiredFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHangfireDashboard();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});

// Build database and seed from file if empty:
app.Services.GetService<IDatabaseUpgrader>().Upgrade();
await app.Services.GetService<IDatabaseSeeder>().SeedFromFileAsync(builder.Configuration.GetSection("DatabaseSettings")["SeederFilePath"]);

// Subscribe to WebService:
var url = $"http://localhost:51396/api/clients/subscribe?date={DateTime.Now:yyyy-MM-dd}&callback=https://localhost:7168/reports";
await app.Services.GetService<ISubscriptionHandler>().SubscribeAsync(url);

app.Run();

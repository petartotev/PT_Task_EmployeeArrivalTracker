using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Serilog;
using Serilog.Events;
using WebAppServer.Api.Extensions;
using WebAppServer.Api.Middlewares;
using WebAppServer.Autofac;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.Repository.DbUp.Interfaces;
using WebAppServer.Repository.Seeder.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(builder => builder.ConfigureHost()));
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day));

builder.Services.UseHangfire(connectionString);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHangfireDashboard();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});

// Upgrade database:
app.Services
    .GetService<IDatabaseUpgrader>()
    .Upgrade();

// Seed database from file (if empty):
await app.Services
    .GetService<IDatabaseSeeder>()
    .SeedFromFileAsync(builder.Configuration.GetSection("DatabaseSettings")["SeederFilePath"]);

// Subscribe to WebService:
await app.Services
    .GetService<ISubscriptionHandler>()
    .SubscribeAsync(app.Services.GetService<IDbSettings>().ConnectionUrlWebService);

// TODO: Add retry mechanism with Polly!!!

app.Run();

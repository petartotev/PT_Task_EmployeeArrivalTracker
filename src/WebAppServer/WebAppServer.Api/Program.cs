using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Serilog;
using Serilog.Events;
using WebAppServer.Api.Extensions;
using WebAppServer.Api.Middlewares;
using WebAppServer.Api.Policies;
using WebAppServer.Autofac;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Common.Constants;
using WebAppServer.Domain.Services.Interfaces;
using WebAppServer.Repository.DbUp.Interfaces;
using WebAppServer.Repository.Seeder.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Use Autofac for DI
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(builder => builder.ConfigureHost()));
// Use Serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day));

// Use Polly
builder.Services
    .AddHttpClient(CommonConstants.Application.HttpClientName)
    .AddPolicyHandler(request => new ClientPolicy().ExponentialHttpRetry);
// Use Hangfire
builder.Services.UseHangfire(builder.Configuration.GetConnectionString("DefaultConnection"));
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
// TODO: Find a way to remove CORS, as it is not secure!!!
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
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

// Seed database from file (only if empty):
await app.Services
    .GetService<IDatabaseSeeder>()
    .SeedFromFileAsync(builder.Configuration.GetSection("DatabaseSettings")["SeederFilePath"]);

// Subscribe to WebService:
await app.Services
    .GetService<ISubscriptionHandler>()
    .SubscribeAsync(app.Services.GetService<IDbSettings>().ConnectionUrlWebService);

app.Run();

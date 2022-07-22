using Hangfire;
using Newtonsoft.Json;
using Serilog;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Domain.Models;
using WebAppServer.Domain.Services.Interfaces;

namespace WebAppServer.Domain.Services;

public class SubscriptionHandler : ISubscriptionHandler
{
    private string _token;
    private readonly IDbSettings _settings;
    public SubscriptionHandler(IDbSettings settings)
    {
        _settings = settings;
    }

    public bool ValidateToken(string token)
    {
        return _token == token;
    }

    public async Task SubscribeAsync(string callbackUrl)
    {
        try
        {
            await SubscribeToWebServiceAsync(callbackUrl);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            Log.Error("WebService is probably off.");
        }

        BackgroundJob.Schedule(() => AddDailyJob(callbackUrl), DateTime.Today.AddDays(1).AddMinutes(5));
    }

    public void AddDailyJob(string callbackUrl)
    {
        RecurringJob.AddOrUpdate(() => SubscribeToWebServiceAsync(callbackUrl), "59 6 * * *");
    }

    public async Task SubscribeToWebServiceAsync(string callbackUrl)
    {
        HttpClient client = new ();
        client.DefaultRequestHeaders.Add("Accept-Client", "Fourth-Monitor");
        var response = await client.GetAsync(_settings.ConnectionUrlWebService);
        response.EnsureSuccessStatusCode();
        var obj = JsonConvert.DeserializeObject<WebServiceResponse>(await response.Content.ReadAsStringAsync());
        _token = obj.Token;
    }
}

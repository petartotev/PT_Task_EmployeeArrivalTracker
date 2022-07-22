using Hangfire;
using Newtonsoft.Json;
using Serilog;
using WebAppServer.Common;
using WebAppServer.Common.Configuration.Interfaces;
using WebAppServer.Common.Constants;
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

    public bool ValidateIncomingToken(string token)
    {
        return _token == token;
    }

    public async Task SubscribeAsync(string callbackUrl = null)
    {
        try
        {
            await SubscribeToWebServiceAsync(callbackUrl);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            Log.Error(LoggerMessages.ExternalApi.WebService.ServiceUnavailable);
        }

        ScheduleSingleJobForTomorrowToDailySubscribeToWebServiceAsync(callbackUrl);
    }

    public async Task SubscribeToWebServiceAsync(string callbackUrl = null)
    {
        callbackUrl ??= _settings.ConnectionUrlWebService;

        HttpClient client = new();
        client.DefaultRequestHeaders.Add(Header.ExternalApi.WebService.AcceptClientKey, Header.ExternalApi.WebService.AcceptClientValue);

        var response = await client.GetAsync(callbackUrl);
        response.EnsureSuccessStatusCode();

        _token = JsonConvert.DeserializeObject<WebServiceResponse>(await response.Content.ReadAsStringAsync()).Token;
    }

    // Single, one-time Hangfire job at 00:01 AM tomorrow:
    public void ScheduleSingleJobForTomorrowToDailySubscribeToWebServiceAsync(string callbackUrl = null)
    {
        BackgroundJob.Schedule(() => ScheduleDailyRecurringJobToSubscribeToWebServiceAsync(callbackUrl), DateTime.Today.AddDays(1).AddMinutes(1));
    }

    // Recurring Hangfire job every day at 07:00 AM:
    public void ScheduleDailyRecurringJobToSubscribeToWebServiceAsync(string callbackUrl = null)
    {
        RecurringJob.AddOrUpdate(() => SubscribeToWebServiceAsync(callbackUrl), "0 7 * * *");
    }
}

using Hangfire;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using WebAppServer.Common;
using WebAppServer.Common.Constants;
using WebAppServer.Domain.Models;
using WebAppServer.Domain.Services.Interfaces;

namespace WebAppServer.Domain.Services;

public class SubscriptionHandler : ISubscriptionHandler
{
    private string _token;
    private readonly string _urlWebService;
    private readonly IConfiguration _settings;
    private readonly IHttpClientFactory _clientFactory;

    public SubscriptionHandler(IConfiguration settings, IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _settings = settings;
        _urlWebService = string.Format(_settings.GetSection("WebService")["Url"], DateTime.Now.ToString("yyyy-MM-dd"));
    }

    public bool ValidateIncomingToken(string token)
    {
        return _token == token;
    }

    public async Task SubscribeAsync(string callbackUrl = null)
    {
        try
        {
            await SubscribeToWebServiceAsync(callbackUrl ?? _urlWebService);
        }
        catch (Exception ex)
        {
            Log.Fatal(LoggerMessages.ExternalApi.WebService.ServiceDown + " " + ex.Message);
        }

        ScheduleSingleJobForTomorrowToDailySubscribeToWebServiceAsync(callbackUrl);
    }

    public async Task SubscribeToWebServiceAsync(string callbackUrl = null)
    {
        var client = _clientFactory.CreateClient(CommonConstants.Application.HttpClientName);
        client.DefaultRequestHeaders.Add(Header.ExternalApi.WebService.AcceptClientKey, Header.ExternalApi.WebService.AcceptClientValue);

        var response = await client.GetAsync(callbackUrl ?? _urlWebService);

        if (response.IsSuccessStatusCode)
        {
            _token = JsonConvert.DeserializeObject<WebServiceResponse>(await response.Content.ReadAsStringAsync()).Token;

            Log.Information(LoggerMessages.ExternalApi.WebService.SuccessfulSubscription);
            Log.Information($"Token: {_token}");
        }
        else
        {
            Log.Warning(LoggerMessages.ExternalApi.WebService.ServiceUnavailable);
        }
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

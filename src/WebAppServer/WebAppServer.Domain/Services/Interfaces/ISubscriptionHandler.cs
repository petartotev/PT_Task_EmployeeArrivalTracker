namespace WebAppServer.Domain.Services.Interfaces;

public interface ISubscriptionHandler
{
    Task SubscribeAsync(string callbackUrl = null);

    bool ValidateIncomingToken(string token);
}

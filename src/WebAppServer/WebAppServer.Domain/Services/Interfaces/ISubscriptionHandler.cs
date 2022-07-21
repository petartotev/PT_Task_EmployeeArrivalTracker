namespace WebAppServer.Domain.Services.Interfaces;

public interface ISubscriptionHandler
{
    Task SubscribeAsync(string callbackUrl);

    bool ValidateToken(string token);
}

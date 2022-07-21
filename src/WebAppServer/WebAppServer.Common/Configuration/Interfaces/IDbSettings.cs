namespace WebAppServer.Common.Configuration.Interfaces;

public interface IDbSettings
{
    string ConnectionString { get; }

    string ConnectionUrlWebService { get; }
}

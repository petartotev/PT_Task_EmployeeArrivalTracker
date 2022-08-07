namespace WebAppServer.Tests.Infrastructure.Asserts;

public class Asserter
{
    public Asserter()
    {
        Response = new ();
    }

    public AsserterResponse Response { get; private set; }
}

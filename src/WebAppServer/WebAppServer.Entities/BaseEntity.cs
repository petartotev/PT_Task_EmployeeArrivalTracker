using Dapperer;

namespace WebAppServer.Entities;

public abstract class BaseEntity : IIdentifier<int>
{
    [Column("Id", IsPrimary = true, AutoIncrement = true)]
    public int Id { get; set; }

    public int GetIdentity()
    {
        return Id;
    }

    public void SetIdentity(int identity)
    {
        Id = identity;
    }
}

namespace WebAppServer.Repository.Seeder.Interfaces;

public interface IDatabaseSeeder
{
    Task SeedFromFileAsync(string path);
}

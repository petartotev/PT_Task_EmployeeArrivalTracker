using Newtonsoft.Json;
using WebAppServer.Repository.Seeder.Models;

namespace WebAppServer.Repository.Seeder;

public static class JsonManager
{
    public static async Task<List<Employee>> GetEmployeesFromJsonFileAsync(string path)
    {
        var json = await LoadJsonAsync(path);

        return JsonConvert.DeserializeObject<List<Employee>>(json);
    }

    private static async Task<string> LoadJsonAsync(string path)
    {
        using (StreamReader reader = new(path))
        {
            return await reader.ReadToEndAsync();
        }
    }
}

using Newtonsoft.Json;
using WebAppServer.Repository.Seeder.Models;

namespace WebAppServer.Repository.Seeder;

public static class JsonManager
{
    public static async Task<List<EmployeeSeederModel>> GetEmployeesFromJsonFileAsync(string path)
    {
        var jsonFile = await LoadJsonFromFileAsync(path);

        return JsonConvert.DeserializeObject<List<EmployeeSeederModel>>(jsonFile);
    }

    private static async Task<string> LoadJsonFromFileAsync(string path)
    {
        using (StreamReader reader = new(path))
        {
            return await reader.ReadToEndAsync();
        }
    }
}

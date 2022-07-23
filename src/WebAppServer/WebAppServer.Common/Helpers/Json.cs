using Newtonsoft.Json;

namespace WebAppServer.Common.Helpers;

public class Json
{
    public static string? Serialize<T>(T source, JsonSerializerSettings? settings = null)
    {
        if (source == null)
        {
            return null;
        }

        if (settings != null)
        {
            return JsonConvert.SerializeObject(source, settings);
        }

        return JsonConvert.SerializeObject(source);
    }

    public static T? Deserialize<T>(string source, JsonSerializerSettings? settings = null)
    {
        if (source == null)
        {
            return default;
        }

        if (settings != null)
        {
            return JsonConvert.DeserializeObject<T>(source, settings);
        }

        return JsonConvert.DeserializeObject<T>(source);
    }
}

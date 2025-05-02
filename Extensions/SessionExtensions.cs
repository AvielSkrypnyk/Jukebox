using Newtonsoft.Json;

namespace MPAJukebox.Extensions;

public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        // Prevents errors caused by circular references during JSON serialization, bug fix for genre loading
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        session.SetString(key, JsonConvert.SerializeObject(value, settings));
    }

    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }
}
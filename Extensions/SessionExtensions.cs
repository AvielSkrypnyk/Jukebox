using Newtonsoft.Json;

namespace MPAJukebox.Extensions;

public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var jsonString = session.GetString(key);
        return jsonString == null ? default(T) : JsonConvert.DeserializeObject<T>(jsonString);
    }
}
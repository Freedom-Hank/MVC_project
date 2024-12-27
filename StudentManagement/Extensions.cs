using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace StudentManagement
{
    public static class SessionExtensions
    {
        // 將對象轉換為 JSON 字符串並存儲在 Session 中
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // 從 Session 中讀取 JSON 字符串並反序列化為對象
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}


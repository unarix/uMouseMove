using System;
using Newtonsoft.Json;

namespace uMouseMove.Entities.Helpers
{
    static class JsonSerializer
    {
        /// <summary>
        /// Serialize an object to Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Serialized object as Json</returns>
        internal static string ObjectToJson(object obj)
        {
            try
            {
                if (obj == null) return null;
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Deserialize Json to be convertd to a specific object
        /// </summary>
        /// <typeparam name="T">Object type to be converted</typeparam>
        /// <param name="json">Serialized object as json</param>
        /// <returns>Specified object type deserialized from Json</returns>
        internal static T JsonToObject <T>(string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json)) return default(T);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception) { throw; }
        }
    }
}

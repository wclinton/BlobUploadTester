using Newtonsoft.Json;

namespace BlobUploadTester
{
    public static class Serializer
    {
        public static string Serialize(dynamic data)
        {
            return data == null ? null : JsonConvert.SerializeObject(data);
        }

        public static dynamic Deserialize(string serialized)
        {
            return serialized == null ? null : JsonConvert.DeserializeObject(serialized);
        }
        public static T Deserialize<T>(string serialized)
        {
            return serialized == null ? default(T) : JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}

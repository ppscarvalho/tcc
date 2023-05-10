#nullable disable

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Loja.Inspiracao.Util.Contracts;
using Loja.Inspiracao.Util.Extensions;

namespace Loja.Inspiracao.Util.Extensions
{
    public static class SerializeObjectExtensions
    {
        public static string SerializeObject(this object obj, bool ignoreJsonProperty = false)
        {
            return JsonConvert.SerializeObject(obj, GetSettings(ignoreJsonProperty));
        }

        public static T SerializeDeserializeObject<T>(this object obj, bool ignoreJsonProperty = false)
        {
            return DeserializeObject<T>(JsonConvert.SerializeObject(obj, GetSettings(ignoreJsonProperty)), ignoreJsonProperty);
        }

        public static T DeserializeObject<T>(this string json, bool ignoreJsonProperty = false)
        {
            return JsonConvert.DeserializeObject<T>(json, GetSettings(ignoreJsonProperty));
        }

        public static T TryDeserialize<T>(this string value, T anonymous)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        private static JsonSerializerSettings GetSettings(bool ignoreJsonProperty)
        {
            var settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Formatting = Formatting.Indented;

            if (ignoreJsonProperty is false)
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            else
                settings.ContractResolver = new ExplicitNameContractResolver();

            return settings;
        }
    }
}

#nullable disable

using Newtonsoft.Json;
using System.Text;
using Loja.Inspiracao.RabbitMQ.Extensions;

namespace Loja.Inspiracao.RabbitMQ.Extensions
{
    public static class ByteExtensions
    {
        public static bool TryDecodeToObject<T>(this ReadOnlyMemory<byte> bytes, out T obj, Encoding encoding = default)
            where T : class, new() => TryDecodeToObject(bytes.ToArray(), out obj, encoding);

        private static bool TryDecodeToObject<T>(this byte[] bytes, out T obj, Encoding encoding = default)
            where T : class, new()
        {
            obj = default;

            if (bytes.Length == 0)
                return false;

            try
            {
                var decodedString = (encoding ?? Encoding.UTF8)
                    .GetString(bytes);

                if (decodedString.IsNullOrDefault())
                    return false;

                obj = JsonConvert.DeserializeObject<T>(decodedString);

                return obj != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

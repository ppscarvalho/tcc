using System.Text.Json;

namespace Loja.Inspiracao.Core.Util
{
    public class DefaultResult
    {
        public object? Result { get; set; }
        public bool Success { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

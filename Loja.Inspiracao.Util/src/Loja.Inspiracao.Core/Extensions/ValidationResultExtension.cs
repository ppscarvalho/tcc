using FluentValidation.Results;

namespace Loja.Inspiracao.Core.Extensions
{
    public static class ValidationResultExtension
    {
        public static IEnumerable<string> GetMessage(this ValidationResult result)
        {
            return result.Errors.Select(e => e.ErrorMessage);
        }
    }
}

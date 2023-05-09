using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Loja.Inspiracao.Resources.Extensions
{
    public static class ValidationResultExtension
    {
        public static IEnumerable<string> GetMessage(this ValidationResult result)
        {
            return result.Errors.Select(e => e.ErrorMessage);
        }
    }
}

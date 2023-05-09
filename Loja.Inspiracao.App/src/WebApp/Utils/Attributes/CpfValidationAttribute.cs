using System.ComponentModel.DataAnnotations;
using WebApp.Utils.Extensions;

namespace WebApp.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CpfValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object cpf)
        {
            if (string.IsNullOrEmpty(cpf?.ToString()) is false)
                return cpf.ToString().CpfValidation();

            return true;
        }
    }
}

using System.ComponentModel.DataAnnotations;
using WebApp.Utils.Extensions;

namespace WebApp.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object email)
        {
            if (string.IsNullOrEmpty(email?.ToString()) is false)
                return email.ToString().EmailValidation();

            return true;
        }
    }
}

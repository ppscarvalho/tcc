#nullable disable

using Loja.Inspiracao.Util.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Loja.Inspiracao.Util.Attributes
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

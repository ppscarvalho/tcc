using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Loja.Inspiracao.Util.Extensions
{
    public static class TextExtensions
    {
        public static string FormatValue(this string message, params string[] value)
        {
            return string.Format(message, value);
        }
        public static int ToInt(this string number)
        {
            int.TryParse(number, out int result);

            return result;
        }
        public static bool IsValid(this object obj)
        {
            if (obj is null) return false;

            var type = obj.GetType();

            var dictionaryValidacao = DicionaryValidation();

            if (dictionaryValidacao.ContainsKey(type))
            {
                return dictionaryValidacao[type](obj);
            }

            return true;
        }

        private static Dictionary<Type, Func<object, bool>> DicionaryValidation()
        {
            return new Dictionary<Type, Func<object, bool>>
            {
                { typeof(int), x => (int)x != default },
                { typeof(long), x => (long)x != default },
                { typeof(double), x => (double)x != default },
                { typeof(float), x => (float)x != default },
                { typeof(decimal), x => (decimal)x != default },
                { typeof(DateTime), x => (DateTime)x != default },
                { typeof(byte), x => (byte)x != default(byte) },
                { typeof(string), x => !string.IsNullOrWhiteSpace((string)x) },
                { typeof(char), x => (char)x != default(char) },
                { typeof(bool), x => true },
            };
        }

        public static string ToCamelCase(this string text)
        {
            return text.Substring(0, 1).ToLower() + text.Substring(1);
        }

        public static string EncodeToBase64(this string input)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(input));
        }

        public static string EncodeToBase64UTF8(this string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        static public string ToCapitalCase(this string extension)
        {
            var textInfo = new CultureInfo("pt-BR", false).TextInfo;

            return textInfo.ToTitleCase(extension.ToLower());
        }
    }
}

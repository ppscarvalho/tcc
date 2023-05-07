#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using Loja.Inspiracao.RabbitMQ.Extensions;

namespace Loja.Inspiracao.RabbitMQ.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Applies System.ComponentModel validations on the object.
        /// https://docs.microsoft.com/pt-br/dotnet/api/system.componentmodel.dataannotations
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool Validate<T>(this T value, out IEnumerable<ValidationResult> validationResults)
        {
            ICollection<ValidationResult> contextValidationResults = new List<ValidationResult>();

            Validator.TryValidateObject(value, new ValidationContext(value), contextValidationResults, true);
            validationResults = contextValidationResults;

            return validationResults.IsNullOrEmpty();
        }

        /// <summary>
        /// Serialize object in json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string Serialize<T>(this T value, JsonSerializerOptions options = null)
            => JsonSerializer.Serialize(value, options);

        /// <summary>
        /// Attempts to serialize the object into json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool TrySerialize<T>(this T value, out string result, JsonSerializerOptions options = null)
        {
            result = default;

            try
            {
                result = value.Serialize(options);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To
        /// </summary>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static object To(this object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();
                var destinationConverter = GetTypeConverter(destinationType);
                var sourceConverter = GetTypeConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsAssignableFrom(value.GetType()))
                    return Convert.ChangeType(value, destinationType, culture);
            }

            return value;
        }

        /// <summary>
        /// To
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T To<T>(this object value)
        {
            return (T)To(value, typeof(T));
        }

        /// <summary>
        /// To
        /// </summary>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public static object To(this object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// GetTypeConverter
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeConverter GetTypeConverter(this Type type)
        {
            return TypeDescriptor.GetConverter(type);
        }

        /// <summary>
        /// Unanonymize Properties
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDictionary<string, object> UnanonymizeProperties(this object obj)
        {
            Type type = obj?.GetType();

            var properties = type?.GetProperties()
                ?.Select(n => n.Name)
                ?.ToDictionary(k => k, k => type.GetProperty(k).GetValue(obj, null));

            return properties;
        }

        /// <summary>
        /// GetDescription Enums
        /// </summary>
        /// <param name="enumerationValue"></param>
        /// <returns></returns>
        public static string GetDescription(this object enumerationValue)
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }

            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumerationValue.ToString();
        }
    }
}

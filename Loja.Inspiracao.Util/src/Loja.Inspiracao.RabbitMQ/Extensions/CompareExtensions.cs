#nullable disable

using JetBrains.Annotations;

namespace Loja.Inspiracao.RabbitMQ.Extensions
{
    public static class CompareExtensions
    {
        /// <summary>
        /// Determines if the object instance is null or equal to its' default value.
        /// </summary>
        /// <typeparam name="T">The type of instance.</typeparam>
        /// <param name="value">The value to be compared.</param>
        /// <returns>True, if the provided value is default. False otherwise.</returns>
        [ContractAnnotation("value:null => true")]
        public static bool IsNullOrDefault<T>(this T value)
        {
            var isDefault = value == null || EqualityComparer<T>.Default.Equals(value, default);
            return isDefault;
        }
    }
}

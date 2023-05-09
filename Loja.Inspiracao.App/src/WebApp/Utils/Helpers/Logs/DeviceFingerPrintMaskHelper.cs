using Serilog.Enrichers.Sensitive;
using System.Text.RegularExpressions;

namespace WebApp.Utils.Helpers.Logs
{
    public class DeviceFingerPrintMaskHelper : IMaskingOperator
    {
        private const string ReplacePattern = "(\"(Device)?FingerPrint\":\\s?(\".*?\"))";

        private readonly Regex _regex;

        public DeviceFingerPrintMaskHelper() : this(ReplacePattern, RegexOptions.IgnoreCase | RegexOptions.Compiled)
        {
        }

        protected DeviceFingerPrintMaskHelper(string regexString, RegexOptions options)
        {
            _regex = new Regex(regexString ?? throw new ArgumentNullException(nameof(regexString)), options);
            if (string.IsNullOrWhiteSpace(regexString))
            {
                throw new ArgumentOutOfRangeException(nameof(regexString), "Regex pattern cannot be empty or whitespace.");
            }
        }

        public MaskingResult Mask(string input, string mask)
        {
            var preprocessedInput = PreprocessInput(input);
            if (!ShouldMaskInput(preprocessedInput))
            {
                return MaskingResult.NoMatch;
            }

            var matches = _regex.Matches(preprocessedInput);

            var maskedResult = matches.Count > 0 ? preprocessedInput.Replace(matches[0].Groups[3].Value, PreprocessMask(mask)) : preprocessedInput;
            var result = new MaskingResult
            {
                Result = maskedResult,
                Match = maskedResult != input
            };

            return result;
        }

        protected virtual bool ShouldMaskInput(string input) => true;

        protected virtual string PreprocessInput(string input) => input;

        protected virtual string PreprocessMask(string mask) => $"{mask}";
    }
}

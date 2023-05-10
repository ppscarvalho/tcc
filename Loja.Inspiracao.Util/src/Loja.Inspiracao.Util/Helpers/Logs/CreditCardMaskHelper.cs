using Serilog.Enrichers.Sensitive;
using System.Text.RegularExpressions;

namespace Loja.Inspiracao.Util.Helpers.Logs
{
	public class CreditCardMaskHelper : RegexMaskingOperator
	{
		private const string ReplacePattern = "(\"adyenjs.*?\")";

		private readonly string _replacementPattern;

		public CreditCardMaskHelper() : base(ReplacePattern, RegexOptions.IgnoreCase | RegexOptions.Compiled)
		{
			_replacementPattern = "{0}";
		}

		protected override string PreprocessMask(string mask) => string.Format(_replacementPattern, mask);
	}
}

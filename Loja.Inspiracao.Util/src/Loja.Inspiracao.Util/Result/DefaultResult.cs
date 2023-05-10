using Newtonsoft.Json;
using System.Collections.Generic;

namespace Loja.Inspiracao.Util.Result
{
	public class DefaultResult
	{
		[JsonConstructor]
		public DefaultResult(object result, string message, bool success, Dictionary<string, string[]> errors)
		{
			Result = result;
			Message = message;
			Errors = errors;
			Success = success;
		}
		public DefaultResult(string message, bool success, Dictionary<string, string[]> errors)
		{
			Message = message;
			Errors = errors;
			Success = success;
		}
		public DefaultResult(object result, string message, bool success = true)
		{
			Result = result;
			Message = message;
			Success = success;
		}
		public DefaultResult(string message, bool success = true)
		{
			Success = success;
			Message = message;
		}
		public DefaultResult(bool success = true)
		{
			Success = success;
		}
		public bool Success { get; private set; }

		public string Message { get; private set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public object Result { get; private set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, string[]> Errors { get; private set; }
	}
}

using System.Collections.Generic;
using System.ComponentModel;

namespace Loja.Inspiracao.Util.Result
{
	public interface IDefaultResultSuccess<T> where T : class
	{
		[DefaultValue(true)]
		bool Success { get; }

		string Message { get; }

		T Result { get; }
	}

	public interface IDefaultResultPartialSuccess<T> where T : class
	{
		[DefaultValue(true)]
		bool Success { get; }

		string Message { get; }

		T Result { get; }
		Dictionary<string, string[]> Errors { get; }
	}

	public interface IDefaultResultSuccess
	{
		[DefaultValue(true)]
		bool Success { get; }

		string Message { get; }

	}

	public interface IDefaultResultFail
	{

		[DefaultValue(false)]
		bool Success { get; }

		string Message { get; }

		Dictionary<string, string[]> Errors { get; }
	}
}

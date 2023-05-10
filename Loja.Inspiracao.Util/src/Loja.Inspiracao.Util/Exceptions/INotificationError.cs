using System.Net;
using Loja.Inspiracao.Util.Exceptions;

namespace Loja.Inspiracao.Util.Exceptions
{
	public interface INotificationError
	{
		void AddError(string error);

		void AddError(Error error);

		void RemoveError(Error error);

		void Clear();

		void Publish(HttpStatusCode statusCode = HttpStatusCode.BadRequest);
	}
}

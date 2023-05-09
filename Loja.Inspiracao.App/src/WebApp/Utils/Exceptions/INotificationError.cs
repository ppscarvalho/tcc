using System.Net;
using WebApp.Utils.Exceptions;

namespace WebApp.Utils.Exceptions
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

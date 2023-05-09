using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebApp.Utils.Exceptions;

namespace WebApp.Utils.Exceptions
{
	public class NotificationError : INotificationError, IDisposable
	{
		private List<Error> _notification;

		public void AddError(string error)
		{
			_notification ??= new List<Error>();
			_notification.Add(new Error(error));
		}

		public void AddError(Error error)
		{
			_notification ??= new List<Error>();
			_notification.Add(error);
		}

		public void RemoveError(Error error)
		{
			_notification?.Remove(error);
		}

		public void Clear()
		{
			_notification?.Clear();
		}

		public void Publish(HttpStatusCode statusCode = HttpStatusCode.BadRequest)
		{
			if (_notification?.Any() != true) return;

			throw new ApiException(statusCode, _notification.Select(x => x.Message));
		}

		public void Dispose()
		{
			Clear();
		}
	}
}

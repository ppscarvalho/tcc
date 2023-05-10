using System;

namespace Loja.Inspiracao.MQ.Configuration
{
	public class Retry
	{
		public Retry(int retryCount, TimeSpan interval)
		{
			RetryCount = retryCount;
			Interval = interval;
		}

		public int RetryCount { get; private set; }
		public TimeSpan Interval { get; private set; }
	}
}

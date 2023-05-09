namespace WebApp.Utils.Result
{
	public class ApiResult<T>
	{
		public string Message { get; set; }

		public T Result { get; set; }
	}
}

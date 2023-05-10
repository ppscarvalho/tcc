using System.Net;

namespace Loja.Inspiracao.Util.Helpers
{
    public static class StatusCodeHelper
    {
        public static bool IsStatusCodeSuccessRange(HttpStatusCode httpStatusCode)
        {
            return httpStatusCode >= HttpStatusCode.OK && httpStatusCode <= HttpStatusCode.IMUsed;
        }

        public static bool IsStatusCodeError(HttpStatusCode httpStatusCode)
        {
            return httpStatusCode >= HttpStatusCode.BadRequest;
        }
    }
}

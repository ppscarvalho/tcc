#nullable disable

using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;

namespace Loja.Inspiracao.Util.Extensions
{
    public static class PollyExtensions
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(response => response.StatusCode >= HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}

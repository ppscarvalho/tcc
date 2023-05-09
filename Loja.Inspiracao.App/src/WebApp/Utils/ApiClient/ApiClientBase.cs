#nullable disable

using System.Net;
using System.Net.Http.Headers;
using System.Text;
using WebApp.Utils.Exceptions;
using WebApp.Utils.Extensions;
using WebApp.Utils.Helpers;
using WebApp.Utils.Helpers.Logs;
using WebApp.Utils.Result;

namespace WebApp.Utils.ApiClient
{
    public abstract class ApiClientBase
    {
        protected string Login;
        protected string Password;
        private readonly HttpClient _httpClient;

        private Dictionary<string, string> _headers;

        protected ApiClientBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual async Task<string> Post(string urlServico, object obj, Dictionary<string, string> headers = null)
        {
            _headers = headers;

            var request = new HttpRequestMessage(HttpMethod.Post, urlServico);

            return await SendRequest(request, obj);
        }

        public virtual async Task<string> Put(string urlServico, object obj, Dictionary<string, string> headers = null)
        {
            _headers = headers;

            var request = new HttpRequestMessage(HttpMethod.Put, urlServico);

            return await SendRequest(request, obj);
        }

        public virtual async Task<string> Get(string urlServico, object obj = null, Dictionary<string, string> headers = null)
        {
            _headers = headers;

            var request = new HttpRequestMessage(HttpMethod.Get, urlServico);

            return await SendRequest(request, obj);
        }
        public virtual async Task<string> Delete(string urlServico, object obj = null, Dictionary<string, string> headers = null)
        {
            _headers = headers;

            var request = new HttpRequestMessage(HttpMethod.Delete, urlServico);

            return await SendRequest(request, obj);
        }
        protected virtual void AddHeaders(HttpRequestMessage request)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (_headers?.Any() == true)
            {
                foreach (var header in _headers)
                {
                    if (string.IsNullOrWhiteSpace(header.Value)) continue;
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }
        protected virtual void AddContent(HttpRequestMessage request, object obj)
        {
            if (obj != null)
            {
                request.Content = new StringContent(obj.SerializeObject(), Encoding.UTF8, "application/json");
            }
        }

        protected virtual void AddContent(HttpRequestMessage request, List<KeyValuePair<string, string>> obj)
        {
            if (obj?.Any() is false) { throw new ApiException("Erro", HttpStatusCode.BadRequest); }

            var @params = string.Join("&", obj.Select(kv => $"{kv.Key}={kv.Value}"));
            request.Content = new StringContent(@params, Encoding.UTF8, "application/x-www-form-urlencoded");
        }

        protected virtual async Task<string> SendRequest(HttpRequestMessage request, object obj)
        {
            AddContent(request, obj);
            AddHeaders(request);

            var response = await _httpClient.SendAsync(request);

            await ErrorsResponseHandler(response);

            return CreateResponse(await RegisterLog(request, response));
        }

        protected virtual string CreateResponse(string contentResponse)
        {
            return contentResponse;
        }

        protected virtual async Task ErrorsResponseHandler(HttpResponseMessage response)
        {
            if (StatusCodeHelper.IsStatusCodeSuccessRange(response.StatusCode))
            {
                response.EnsureSuccessStatusCode();
            }
            else
            {
                var messageError = string.Empty;
                var erros = new Dictionary<string, string[]>();

                try
                {
                    var contentResponse = await ReadContent(response);
                    var result = contentResponse.Item1.DeserializeObject<DefaultResult>();
                    messageError = result.Message;
                    erros = result.Errors;

                    if (result.Result != null)
                    {
                        erros = erros ?? new Dictionary<string, string[]>();
                        erros.Add("Erro", new string[] { result.Result.SerializeObject() });
                    }
                }
                catch
                {
                    messageError = String.IsNullOrWhiteSpace(response.ReasonPhrase) ? "Erro" : response.ReasonPhrase;
                }

                throw new ApiException(messageError, response.StatusCode, erros);
            }

            await Task.CompletedTask;
        }

        protected virtual async Task<string> RegisterLog(HttpRequestMessage request, HttpResponseMessage response)
        {
            (string contentResponse, string contentRequest) = await ReadContent(response, request);

            await CreateLog(request, contentRequest, contentResponse);

            return contentResponse;
        }

        protected virtual async Task<Tuple<string, string>> ReadContent(HttpResponseMessage response, HttpRequestMessage request = null)
        {
            var contentRequest = string.Empty;

            if (request != null && request.Content.IsValid())
                contentRequest = await request.Content.ReadAsStringAsync();

            var contentResponse = await response.Content.ReadAsStringAsync();

            return Tuple.Create(contentResponse, contentRequest);
        }

        protected virtual async Task CreateLog(HttpRequestMessage request, string contentRequest, string contentResponse)
        {
            var log = (new
            {
                request.RequestUri.AbsoluteUri,
                request.RequestUri.Query,
                request.Method,
                ContentRequest = contentRequest,
                ContentResponse = contentResponse
            }).
             SerializeObject();

            LogHelper.AddLog(Helpers.Logs.LogLevel.Information, ConstantsLogs.LOG_API, log);

            await Task.CompletedTask;
        }
    }
}

#nullable disable

using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Loja.Inspiracao.Util.Helpers.Logs
{
    public static class LogHelper
    {
        private static List<string> _listHeadersParams = new List<string> { "requestId", "userId", "userSystemId" };
        private static HttpContext _httpContext;

        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            try
            {
                _httpContext = httpContext;

                ProcessLogs();
            }
            catch (Exception ex)
            {
                Log.Error($"Algum erro ocorreu! - {ex.Message}"); ;
            }
        }

        private static void ProcessLogs()
        {
            if (ValidateLog()) return;

            DiagnosticContextAddDefaultMetadata();
            DiagnosticContextAddHeaders();
            DiagnosticContextAddQueries();
            DiagnosticContextAddBodies();
        }

        private static bool ValidateLog()
        {
            string requestPath = _httpContext.Request.Path.Value.ToLower();

            return (requestPath.Contains(ConstantsLogs.PATH_SWAGGER) && !StatusCodeHelper.IsStatusCodeError((HttpStatusCode)_httpContext.Response.StatusCode)) || requestPath.Contains(ConstantsLogs.HEALTH_CHECK);
        }

        private static void DiagnosticContextAddDefaultMetadata()
        {
            SetLog(ConstantsLogs.PARAM_HOST, _httpContext.Request.Host.ToString());
            SetLog(ConstantsLogs.PARAM_PROTOCOL, _httpContext.Request.Protocol);
            SetLog(ConstantsLogs.PARAM_SCHEME, _httpContext.Request.Scheme);
        }

        private static void DiagnosticContextAddHeaders()
        {
            foreach (var value in _listHeadersParams)
            {
                if (_httpContext.Request.Headers.ContainsKey(value))
                    SetLog(value, _httpContext.Request.Headers[value]);
            }
        }

        private static void DiagnosticContextAddQueries()
        {
            if (_httpContext.Request.QueryString.HasValue)
                SetLog(ConstantsLogs.QUERY_STRING, _httpContext.Request.QueryString.Value);
        }

        private static void DiagnosticContextAddBodies()
        {
            (string responseBodyPayload, string requestBodyPayload) = GetBodies();

            if (!string.IsNullOrWhiteSpace(responseBodyPayload))
                SetLog(ConstantsLogs.PAYLOAD_RESPONSE, responseBodyPayload);

            if (!string.IsNullOrWhiteSpace(requestBodyPayload))
                SetLog(ConstantsLogs.PAYLOAD_REQUEST, requestBodyPayload);
        }

        private static Tuple<string, string> GetBodies()
        {
            return Tuple.Create(ReadResponseBody(_httpContext.Response), ReadRequestBody(_httpContext.Request));
        }

        private static string ReadResponseBody(HttpResponse response)
        {
            return ReadBody(response.Body);
        }

        private static string ReadRequestBody(HttpRequest request)
        {
            return ReadBody(request.Body);
        }

        private static string ReadBody(Stream body)
        {
            body.Seek(0, SeekOrigin.Begin);
            var requestBody = new StreamReader(body).ReadToEndAsync().Result;
            body.Seek(0, SeekOrigin.Begin);

            return requestBody;
        }

        private static void SetLog(string key, string value)
        {
            if (StatusCodeHelper.IsStatusCodeError((HttpStatusCode)_httpContext.Response.StatusCode))
                Log.Error($"{key}:{value}");
            else
                Log.Information($"{key}:{value}");
        }

        public static void AddLog(LogLevel level, string key, string log)
        {
            Log.Write((LogEventLevel)(int)level, $"{key}:{log}");
        }

        public static void AddLog(LogLevel level, string log)
        {
            Log.Write((LogEventLevel)(int)level, log);
        }

        public static void AddRollback(string log)
        {
            AddLog(LogLevel.Error, ConstantsLogs.ROLLBACK, log);
        }

        public static void SaveLog(List<string> logs)
        {
            foreach (var log in logs)
                AddLog(LogLevel.Information, log);
        }
    }
}

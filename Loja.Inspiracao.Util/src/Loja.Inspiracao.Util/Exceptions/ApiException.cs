#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Loja.Inspiracao.Util.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public Dictionary<string, string[]> Errors { get; private set; }

        public ApiException() : base() { }

        public ApiException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public ApiException(string message, HttpStatusCode httpStatusCode, Dictionary<string, string[]> errors) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
        }

        public ApiException(HttpStatusCode httpStatusCode, IEnumerable<string> errors, string message = "", string keyError = "") : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Errors = new Dictionary<string, string[]>
            {
                { string.IsNullOrEmpty(keyError) ?  "Erro" : keyError, errors.ToArray() }
            };
        }
    }
}

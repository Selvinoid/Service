using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace HotelApi.Filters
{
    public class BaseActionHandler : DelegatingHandler
    {
        protected bool ValidRequestBody(HttpRequestMessage request, string body)
        {
            try
            {
                if (request.Method.Method == "POST")
                {
                    JToken.Parse(body);
                }
                return true;
            }
            catch(Exception exception)
            {
                return false;
            }
        }

        protected Task<HttpResponseMessage> TaskOnError(HttpRequestMessage request, int httpStatusCode, string errorMessage)
        {
            var task = new TaskCompletionSource<HttpResponseMessage>();
            task.SetResult(request.CreateErrorResponse(
                (HttpStatusCode)httpStatusCode,
                new HttpError(errorMessage)
            ));
            return task.Task;
        }

    }
}
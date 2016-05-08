using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HotelApi.Filters
{
    public class RequestValidationHandler : BaseActionHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (ValidRequestBody(request, await request.Content.ReadAsStringAsync()))
            {
                // let other handlers process the request
                return await base.SendAsync(request, cancellationToken);
            }
            else
            {
                return await TaskOnError(request, 422, "Can't parse JSON in the request body");
            }
        }
    }

}
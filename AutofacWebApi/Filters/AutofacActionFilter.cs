using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AutofacWebApi.Filters
{
    public class AutofacActionFilter : IAutofacActionFilter
    {
        public Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Request.RequestUri.OriginalString.Contains("Get"))
            {
                return Task.FromResult(0);
            }
            else
            {
                return Task.FromResult(100);
            }
        }

        public Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if(actionContext.Request.RequestUri.OriginalString.Contains("Get"))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Request not valid");
                return Task.FromResult(0);
            }
            else
            {
                return Task.FromResult(100);
            }
        }
    }
}
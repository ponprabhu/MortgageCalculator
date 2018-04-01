using System;
using System.Web.Http.Filters;
using System.Net.Http.Headers;

namespace MortgageCalculator.Api.Helpers
{
    //CacheWebApiAttribute helps to cache 
    public class CacheWebApiAttribute : ActionFilterAttribute
    {
        public int Duration { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            filterContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(Duration),
                MustRevalidate = true,
                Private = true
            };
        }
    }
}
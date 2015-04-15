using Auditor.Features;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Threading.Tasks;

namespace Auditor.Middleware
{
    public abstract class RouteNotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public RouteNotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.SetFeature<IRouteNotFoundFeature>(new RouteNotFoundFeature());

            if (context.Response.HeadersSent)
            {
                await _next(context);
                return;
            }

            OnNotFound(context);
            await _next(context);
        }

        protected abstract Task OnNotFound(HttpContext context);
    }
}
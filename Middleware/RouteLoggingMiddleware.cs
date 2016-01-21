using Auditor.Features;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using System;
using System.Threading.Tasks;

namespace Auditor.Middleware
{
    public abstract class RouteLoggingMiddleware
    {
        public RouteLoggingMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        protected RequestDelegate Next { get; }

        public async Task Invoke(HttpContext context)
        {
            await Next(context);

            var routeFeature = context.Features.Get<IRouteInformationFeature>();
            var routeName = context.Request.Method + " " + context.Request.PathBase.Add(context.Request.Path).ToString();
            
            if (context.WasNotFound())
                LogNotFound(context, routeFeature ?? new RouteInformationFeature { RouteName = routeName }, context.Features.Get<IRouteNotFoundFeature>());
            else
                LogRoute(context, routeFeature ?? new RouteInformationFeature { RouteName = routeName });
        }

        protected abstract Task LogRoute(HttpContext context, IRouteInformationFeature routeInformation);
        protected abstract Task LogNotFound(HttpContext context, IRouteInformationFeature routeInformation, IRouteNotFoundFeature notFound);
    }
}
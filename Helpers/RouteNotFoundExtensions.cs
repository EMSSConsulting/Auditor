using Auditor.Features;
using Auditor.Middleware;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Threading.Tasks;

namespace Auditor
{
    public static class RouteNotFoundExtensions
    {
        public static IApplicationBuilder UseNotFound<T>(this IApplicationBuilder builder) where T : RouteNotFoundMiddleware
        {
            return builder.Use(next => ((T)typeof(T).GetConstructor(new[] { next.GetType() }).Invoke(new[] { next })).Invoke);
        }

        public static bool WasNotFound(this HttpContext context)
        {
            return context.GetFeature<IRouteNotFoundFeature>() != null;
        }
    }
}
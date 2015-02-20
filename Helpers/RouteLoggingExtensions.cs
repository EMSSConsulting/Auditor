using Auditor.Features;
using Auditor.Middleware;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Threading.Tasks;

namespace Auditor
{
    public static class RouteLoggingExtensions
    {
        public static IApplicationBuilder UseRouteLogger<T>(this IApplicationBuilder builder) where T : RouteLoggingMiddleware
        {
            return builder.Use(next => ((T)typeof(T).GetConstructor(new[] { next.GetType() }).Invoke(new[] { next })).Invoke);
        }
    }
}
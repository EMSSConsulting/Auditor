using Auditor.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Auditor
{
    public static class ErrorReportingExtensions
    {
        public static IApplicationBuilder UseErrorReporter<T>(this IApplicationBuilder builder, bool stopProcessing = true) where T : ErrorReporterMiddleware
        {
            var constructor = typeof(T).GetConstructor(new[] { typeof(RequestDelegate), typeof(bool) });
            return builder.Use(next => ((T)constructor.Invoke(new object[] { next, stopProcessing })).Invoke);
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Auditor.Middleware
{
    public static class ErrorReportingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorReporter<T>(this IApplicationBuilder builder, bool stopProcessing = true) where T : ErrorReporterMiddleware
        {
            var constructor = typeof(T).GetConstructor(new[] { typeof(RequestDelegate), typeof(bool) });
            return builder.Use(next => ((T)constructor.Invoke(new object[] { next, stopProcessing })).Invoke);
        }
    }

    public abstract class ErrorReporterMiddleware
    {
        public ErrorReporterMiddleware(RequestDelegate next, bool stopProcessing)
        {
            Next = next;
            StopProcessing = stopProcessing;
        }

        protected RequestDelegate Next { get; }

        protected virtual bool StopProcessing { get; }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                ReportException(context, ex);
                if(StopProcessing) throw;
            }
        }

        protected abstract Task ReportException(HttpContext context, Exception ex);
    }
}
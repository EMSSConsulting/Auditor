﻿using Auditor.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Auditor
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class RouteInformationAttribute : ActionFilterAttribute
    {
        public RouteInformationAttribute(string routeName)
        {
            RouteName = routeName;
        }

        public string RouteName { get; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Features.Set<IRouteInformationFeature>(new RouteInformationFeature() {
                RouteName = RouteName,
                Arguments = context.ActionArguments
            });
            base.OnActionExecuting(context);
        }
    }
}
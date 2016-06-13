using Microsoft.AspNetCore.Mvc;
using System;

namespace Auditor.Features
{
    public interface IRouteInformationFeature
    {
        string RouteName { get; set; }
        object Arguments { get; set; }
    }

    internal class RouteInformationFeature : IRouteInformationFeature
    {
        public string RouteName { get; set; }
        public object Arguments { get; set; }
    }
}
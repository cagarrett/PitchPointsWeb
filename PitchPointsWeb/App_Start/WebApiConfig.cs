using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PitchPointsWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "PitchPointsAPI",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = RouteParameter.Optional, value = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "PitchPointsAPIv2",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = RouteParameter.Optional, value = RouteParameter.Optional });

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

        }
    }
}

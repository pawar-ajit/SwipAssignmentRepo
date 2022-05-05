using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using UserSwipeAssignment.Filters;

namespace UserSwipeAssignment
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:5000", "*", "GET,POST");
            config.EnableCors(cors);

            config.Filters.Add(new MyExceptionFilter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

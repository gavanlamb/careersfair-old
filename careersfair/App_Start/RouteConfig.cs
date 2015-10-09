using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace careersfair
{
    /// <summary>
    /// contains the default route information
    /// Edited by: Gavan
    /// Version: 2.0
    /// Edit: set controller and action to Form and Create respectively
    /// </summary>
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Form", action = "Create", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ViewForms",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Form", action = "ViewForm", id = UrlParameter.Optional }
            );
        }
    }
}

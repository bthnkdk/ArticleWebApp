using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Article.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           
            routes.MapRoute(
                name: "Default3",
                url: "PostImageView/{id}/{w}/{h}", // makale resim
                defaults: new { controller = "Site", action = "PostImageView", id = UrlParameter.Optional,w = UrlParameter.Optional,h = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default2",
                url: "ProfileImageView/{id}/{w}/{h}", // profil resim
                defaults: new { controller = "Site", action = "ProfileImageView", id = UrlParameter.Optional,w = UrlParameter.Optional,h = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "PostList",
                url: "{categoryName}-{categoryId}", //navbar menu
                defaults: new { controller = "Site", action = "PostList"} 
            );

            routes.MapRoute(
                name: "PostDetail",
                url: "{id}/{categoryName}/{slug}-{categoryId}", //makale detayı url
                defaults: new { controller = "Site", action = "PostDetail"}
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

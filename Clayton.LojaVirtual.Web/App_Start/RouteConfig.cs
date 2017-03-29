using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Clayton.LojaVirtual.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // 1 - Inicio

            routes.MapRoute(null, "", new { controller = "Vitrine", action = "ListaProdutos", categoria = (string)null, pagina = 1 });

            // 2
            routes.MapRoute(null, "Pagina{pagina}", new { controller = "Vitrine", action = "ListaProdutos", categoria = (string)null }, new { pagina = @"\d+" });


            //3
            routes.MapRoute(null, "{categoria}", new { controller = "Vitrine", action = "ListaProdutos", pagina = 1 });

            //4
            routes.MapRoute(null, "{categoria}/Pagina{pagina}", new { controller = "Vitrine", action = "ListaProdutos" }, new { pagina = @"\d+" });

            //routes.MapRoute("ObterImagem", "Vitrine/Img/{ProdutoId}", new { controller = "Vitrine", action = "ObterImagem", produtoId = UrlParameter.Optional });

            routes.MapRoute(null, "{controller}/{action}");



            /* exemplo do ToSeoUrl em HtmlHelpers*/

            //routes.MapRoute(
            //    "NamedProduct",
            //    "product/{id}/{name}",
            //    new { controller = "Product", action = "Details", name = UrlParameter.Optional },
            //    new { id = @"^\d+$" }
            //);
        }
    }
}

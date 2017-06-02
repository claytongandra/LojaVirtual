using System.Web;
using System.Web.Optimization;

namespace Clayton.LojaVirtual.Web.V2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/bundles/OlsonKart").Include(
                     "~/Scripts/ddlevelsmenu.js",
                     "~/Scripts/jquery.navgoco.min.js",
                     "~/Scripts/custom.js",
                      "~/Scripts/default.js"
                     ));


            bundles.Add(new StyleBundle("~/Content/css/bootstrap").Include(
                      "~/Content/bootstrap.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/css/OlsonKart").Include(
                       "~/Content/revolution.slider.settings.css",
                        "~/Content/animate.min.css",
                        "~/Content/ddlevelsmenu-base.css",
                        "~/Content/ddlevelsmenu-topbar.css",
                        "~/Content/jquery.countdown.css",
                        "~/Content/ionicons.min.css",
                         "~/Content/font-awesome.min.css",
                        "~/Content/style.css",
                        "~/Content/site.css"
                        ));


            bundles.Add(new ScriptBundle("~/Scripts/detalhesproduto").Include(
                        "~/Scripts/jquery.elevatezoom.js",
                        "~/Scripts/detalhesProduto.js"
                        ));
        }
    }
}

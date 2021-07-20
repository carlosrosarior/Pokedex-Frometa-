using System.Web;
using System.Web.Optimization;

namespace Pokedex
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            //Add location files to the project - no default
            bundles.Add(new ScriptBundle("~/bundles/jquerySelectOption").Include(
                "~/Scripts/JquerySelect.js"));
            bundles.Add(new StyleBundle("~/bundles/CssSelectOption").Include(
                "~/Content/estilos.css"));
            bundles.Add(new StyleBundle("~/bundles/style").Include(
                "~/Content/Style.css"));
            bundles.Add(new StyleBundle("~/bundles/descstyle").Include(
               "~/Content/descstyle.css"));
        }
    }
}

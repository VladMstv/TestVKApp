using System.Web;
using System.Web.Optimization;

namespace TestSegmento
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //angular
            bundles.Add(new ScriptBundle("~/bundles/angular/js").Include(
                        "~/AngularTestSegmento/dist/AngularTestSegmento/polyfills.*",
                        "~/AngularTestSegmento/dist/AngularTestSegmento/main.*",
                        "~/AngularTestSegmento/dist/AngularTestSegmento/runtime.*",
                        "~/AngularTestSegmento/dist/AngularTestSegmento/vendor.*",
                        "~/AngularTestSegmento/dist/AngularTestSegmento/styles.js"
                ));
            bundles.Add(new StyleBundle("~/bundles/angular/css").Include(
                        "~/AngularTestSegmento/dist/AngularTestSegmento/styles.{version}.css"
                ));
        }
    }
}

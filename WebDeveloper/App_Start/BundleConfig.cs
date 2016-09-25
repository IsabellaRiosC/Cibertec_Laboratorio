using System.Web;
using System.Web.Optimization;

namespace WebDeveloper
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/unobtrusive-ajax").Include(
                     "~/Scripts/jquery.unobtrusive-ajax.js"));


            bundles.Add(new ScriptBundle("~/bundles/modal").Include(
                   "~/Scripts/Shared/modal.js"));

            bundles.Add(new ScriptBundle("~/bundles/paginator").Include(
                  "~/Scripts/jquery.bootpag.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/person").Include(
                  "~/Areas/Personnel/Scripts/Person.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css")
                      .Include("~/Css/site.css"));

           /*******MINIFICAR CODIGO**************/
            bundles.Add(
                new DynamicFolderBundle("js", "*.js", false, new JsMinify())
                );


            bundles.Add(
                new DynamicFolderBundle("css", "*.css", false, new CssMinify())
             );

#if DEBUG

            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations =  true;
#endif


        }
    }
}

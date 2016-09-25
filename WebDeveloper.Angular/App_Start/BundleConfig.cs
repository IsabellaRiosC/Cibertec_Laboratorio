using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebDeveloper.Angular.App_Start
{
    public class BundleConfig

    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/Scripts/jquery-3.1.0.js")
                .Include("~/Scripts/angular.js"));


            bundles.Add(new ScriptBundle("~/bundles/dependencies")
              .IncludeDirectory("~/Scripts/angular","*.js",true));

            bundles.Add(new ScriptBundle("~/bundles/app")
              .Include("~/app/app.js")
              .Include("~/app/app.routes.js"));

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/bootstrap.css")
                 .Include("~/Content/Site.css"));

            bundles.Add(
                new DynamicFolderBundle("js", "*.js", false, new JsMinify())
                );
            bundles.Add(
                new DynamicFolderBundle("css", "*.css", false, new CssMinify())
                );

            BundleTable.EnableOptimizations = true;



        }

    }
}
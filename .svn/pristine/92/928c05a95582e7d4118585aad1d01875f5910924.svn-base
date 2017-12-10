using System.Web;
using System.Web.Optimization;

namespace AMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"

                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap-datepicker.min.js"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/extra").Include(
                     "~/Scripts/respond.min.js",
                      "~/Scripts/moment.min.js",
                      "~/Scripts/icheck.min.js",
                      "~/Scripts/Chart.min.js",
                      "~/Scripts/nprogress.min.js"    
                     // "~/Scripts/jquery-ui-1.9.0.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/manage").Include(
                 "~/Scripts/select2.min.js",
                 "~/Scripts/jquery.dataTables.min.js",
                 "~/Scripts/swiper.min.js"
                //"~/Scripts/jquery-ui.min.js"
                ));
            
             bundles.Add(new ScriptBundle("~/Scripts/adminScripts").Include(
                      "~/Scripts/dest/js.cookie.min.js",
                      "~/Scripts/v9.js",
                      "~/Scripts/datatableEntry.js",
                      "~/Scripts/merchant.js",
                      "~/Scripts/orderDetails.js",
                      "~/Scripts/manageLocation.js"
                     ));

            bundles.Add(new ScriptBundle("~/Scripts/signalR").Include(
                 "~/Scripts/jquery.signalR-2.2.2.min.js",
                  "~/Scripts/jquery.signalR.push.js"
                ));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-datepicker.min.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/jquery.dataTables.css",
                       "~/Content/progressbar.css",
                      //"~/Content/dest/sidebar.min.css",
                      //"~/Content/font-awesome.min.css",
                      "~/Content/jquery-ui.min.css",
                      "~/Content/swiper.min.css"
                      //"~/Content/select2.min.css"
                      //"~/Content/site.css"
                      ));



            BundleTable.EnableOptimizations = false;
        }
    }
}

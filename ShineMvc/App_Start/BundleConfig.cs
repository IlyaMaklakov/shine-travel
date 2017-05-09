#region Usings

using System.Web.Optimization;

#endregion

namespace ShineMvc
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/assets/vendors/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/assets/vendors/jquery-1.12.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/assets/js/videurls.js", "~/assets/js/shine.js"));

            bundles.Add(
                new StyleBundle("~/bundles/css").Include("~/assets/css/site.css", "~/assets/css/components.css"));
        }
    }
}
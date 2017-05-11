#region Usings

using System.Web.Optimization;

#endregion

namespace ShineMvc
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/assets/vendors/jquery-1.12.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/assets/js/videurls.js", "~/assets/js/shine.js"));
        }
    }
}
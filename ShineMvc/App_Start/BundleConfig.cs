#region Usings

using System.Web.Optimization;

#endregion

namespace ShineMvc
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/bundles/globalStyles")
                    .Include("~/assets/css/bootstrap.css", new CssRewriteUrlTransform())
                    .Include("~/assets/css/bootstrap-theme.css", new CssRewriteUrlTransform())
                    .Include("~/assets/vendors/sweetalert2/sweetalert2.css", new CssRewriteUrlTransform())
                    .Include("~/assets/css/style.css", new CssRewriteUrlTransform()));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/assets/vendors/jquery-1.12.0.min.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/js").Include(
                    "~/assets/js/videurls.js",
                    "~/assets/js/shine.js",
                    "~/assets/vendors/bootstrap/bootstrap.js",
                    "~/assets/vendors/jquery.validate.min.js",
                    "~/assets/vendors/inputmask/jquery.inputmask.bundle.js",
                    "~/assets/vendors/inputmask/inputmask/phone-codes/phone.js",
                    "~/assets/vendors/inputmask/inputmask/phone-codes/phone-be.js",
                    "~/assets/vendors/inputmask/inputmask/phone-codes/phone-ru.js",
                    "~/assets/vendors/inputmask/inputmask/bindings/inputmask.binding.js"));
        }
    }
}
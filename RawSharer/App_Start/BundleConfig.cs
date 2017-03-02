using System.Web;
using System.Web.Optimization;

namespace RawSharer
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.color-{version}.js",
                        "~/Scripts/jquery.easing.{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/winjs").Include(
                      "~/Content/frameworks/WinJS/js/base.min.js",
                      "~/Content/frameworks/WinJS/js/ui.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/fine-uploader").Include(
                    "~/Content/frameworks/fine-uploader/jquery.fine-uploader.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/rawsharer").Include(
                "~/Content/js/*.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/frameworks/WinJS/css/ui-dark.min.css",
                      "~/Content/frameworks/bootstrap/bootstrap.min.css",
                      "~/Content/frameworks/fine-uploader/fine-uploader-gallery.css",
                      "~/Content/css/*.css",
                      "~/Content/css/common/*.css"));
        }
    }
}

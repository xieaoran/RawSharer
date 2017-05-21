using System.Web;
using System.Web.Optimization;

namespace RawSharer
{
    public class BundleConfig
    {
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
                      "~/WinJS/js/base.min.js",
                      "~/WinJS/js/ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/rawsharer")
                .IncludeDirectory("~/Content/ts/Common", "*.js", true)
                .IncludeDirectory("~/Content/ts/PlayBack/Utils", "*.js")
                .IncludeDirectory("~/Content/ts/PlayBack/UI", "*.js", true)
                .IncludeDirectory("~/Content/ts/PlayBack/Core", "*.js")
                .IncludeDirectory("~/Content/js", "*.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/WinJS/css/ui-dark.min.css",
                      "~/Content/frameworks/bootstrap/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/css/*.css",
                      "~/Content/css/common/*.css"));
        }
    }
}

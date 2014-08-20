using System.Web;
using System.Web.Optimization;

namespace CurseTeamBrowserUI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/init").Include(
                        "~/Scripts/jquery.js",
                        "~/Scripts/require.js"));

            bundles.Add(new ScriptBundle("~/scripts/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/styles/site").Include("~/Content/site.css"));
        }
    }
}
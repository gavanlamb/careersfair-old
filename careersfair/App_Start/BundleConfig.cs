using System.Web;
using System.Web.Optimization;

namespace careersfair.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Script bundle for the ribbon menu
            bundles.Add(new ScriptBundle("~/Bundles/ribbon").Include("~/Scripts/jquery-2.0.0.min.js", "~/Scripts/jquery.smartWizard.js"));
            //CSS bundle for the ribbon menu
            bundles.Add(new StyleBundle("~/Content/style").Include("~/Content/css/ribbon_smart_wizard.css", "~/Content/css/menu.css", "~/Content/css/Site.css"));
        }
    }
}
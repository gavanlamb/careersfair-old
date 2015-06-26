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
            //Script bundle for the form builder
            bundles.Add(new ScriptBundle("~/Bundles/formbuilder").Include("~/plugin/formbuilder/assets/js/main-built.js", "~/plugin/formbuilder/assets/js/lib/require.js"));
            //CSS bundle for the ribbon menu
            bundles.Add(new StyleBundle("~/Content/style").Include("~/Content/css/ribbon_smart_wizard.css", "~/Content/css/menu.css", "~/Content/css/Site.css"));
            bundles.Add(new StyleBundle("~/Content/styles").Include("~/plugin/formbuilder/assets/css/lib/bootstrap.min.css", "~/plugin/formbuilder/assets/css/lib/bootstrap-responsive.min.css", "~/plugin/formbuilder/assets/css/custom.css"));
        } 
    }
}
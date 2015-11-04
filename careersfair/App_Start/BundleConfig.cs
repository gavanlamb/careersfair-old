using System.Web;
using System.Web.Optimization;

namespace careersfair.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
<<<<<<< HEAD
            //script bundles
            //Bundles for Bootstrap
            bundles.Add(new ScriptBundle("~/Bundles/Bootstrap").Include("~/Scripts/jquery-2.1.4.min.js", "~/Scripts/bootstrap.min.js"));
            //jquery
            //bundles.Add(new ScriptBundle("~/Bundles/JQuery").Include("~/Scripts/jquery-2.1.4.min.js"));
            //formbuilder ribbon
            bundles.Add(new ScriptBundle("~/Bundles/FormBuilderTabs").Include("~/Scripts/bootstrap-formbuilder-tabs.js"));
            //jquery validation
            bundles.Add(new ScriptBundle("~/Bundles/JQueryValidation").Include("~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.validate.unobtrusive.min.js"));


            //css bundles
            bundles.Add(new StyleBundle("~/Content/Bootstrap").Include("~/Content/css/bootstrap-theme.min.css", "~/Content/css/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Content/FormBuilder").Include("~/Content/css/custom.css"));
            bundles.Add(new StyleBundle("~/Content/Site").Include("~/Content/css/Site.css"));
        }
=======
            //Script bundle for the ribbon menu
            bundles.Add(new ScriptBundle("~/Bundles/ribbon").Include("~/Scripts/jquery-2.0.0.min.js", "~/Scripts/jquery.smartWizard.js"));
            //Script bundle for the form builder
            bundles.Add(new ScriptBundle("~/Bundles/formbuilder").Include("~/plugin/formbuilder/assets/js/main-built.js", "~/plugin/formbuilder/assets/js/lib/require.js"));
            //CSS bundle for the ribbon menu
            bundles.Add(new StyleBundle("~/Content/style").Include("~/Content/css/ribbon_smart_wizard.css", "~/Content/css/menu.css", "~/Content/css/Site.css"));
            bundles.Add(new StyleBundle("~/Content/styles").Include("~/plugin/formbuilder/assets/css/lib/bootstrap.min.css", "~/plugin/formbuilder/assets/css/lib/bootstrap-responsive.min.css", "~/plugin/formbuilder/assets/css/custom.css"));
        } 
>>>>>>> master
    }
}
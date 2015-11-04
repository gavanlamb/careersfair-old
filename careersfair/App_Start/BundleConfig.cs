using System.Web;
using System.Web.Optimization;

namespace careersfair.App_Start
{
    /// <summary>
    /// This class creates bundles of scripts and stylesheets so they 
    /// can be called individually whereever they are needed in the program
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// This method sets up and assigns the bundles to a BundleCollection item
        /// </summary>
        /// <param name="bundles">The BundleCollection item that all of these bundles are being added to</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
<<<<<<< HEAD
            //********************************************
            //***************SCRIPT BUNDLES***************
            //********************************************
            bundles.Add(
                new ScriptBundle("~/script/bootstrap")
                    .Include("~/Scripts/library/bootstrap.min.js"));

            bundles.Add(
                new ScriptBundle("~/script/buttons")
                    .Include("~/Scripts/app/button.js"));

            bundles.Add(
                new ScriptBundle("~/script/popup")
                    .Include("~/Scripts/app/popup.js"));

            bundles.Add(
                new ScriptBundle("~/script/formbuildertabs")
                    .Include("~/Scripts/app/bootstrap-formbuilder-tabs.js"));

            bundles.Add(
                new ScriptBundle("~/script/jquery_1102")
                    .Include("~/Scripts/library/jquery-1.10.2.min.js"));

            bundles.Add(
                new ScriptBundle("~/script/jquery_200")
                    .Include("~/Scripts/library/jquery-2.0.0.min.js"));

            bundles.Add(
                new ScriptBundle("~/script/jquery")
                    .Include("~/Scripts/library/jquery-2.1.4.min.js"));

            bundles.Add(
                new ScriptBundle("~/script/jqueryvalidation")
                    .Include("~/Scripts/library/jquery.validate.*"));

            bundles.Add(
                new ScriptBundle("~/script/jqueryui")
                    .Include("~/Scripts/library/jquery-ui-1.11.4.js"));

            bundles.Add(
                new ScriptBundle("~/script/jqueryajax")
                    .Include("~/Scripts/library/jquery.unobtrusive-ajax.js"));

            bundles.Add(
                new ScriptBundle("~/script/modernizr")
                    .Include("~/Scripts/library/modernizr-2.6.2.js"));

            bundles.Add(
                new ScriptBundle("~/script/linkedin")
                    .Include("~/Scripts/app/linkedin.js"));

            bundles.Add(
                new ScriptBundle("~/script/formwizard")
                    .Include("~/Scripts/app/formToWizard.js"));

            bundles.Add(
                new ScriptBundle("~/script/validator")
                    .Include("~/Scripts/app/validation.js"));

            //*****************************************
            //***************CSS BUNDLES***************
            //*****************************************
            //CSS bundle for the bootstrap theme
            bundles.Add(
                new StyleBundle("~/style/bootstrap").Include(
                    "~/Content/css/bootstrap.min.css",
                    "~/Content/css/bootstrap-theme.min.css"
                )
            );

            //CSS bundle for the bootstrap theme
            bundles.Add(
                new StyleBundle("~/style/main").Include(
                    "~/Content/css/main.css"
                )
            );

            //CSS bundle for the site
            bundles.Add(
                new StyleBundle("~/style/menu").Include(
                    "~/Content/css/menu.css"
                )
            );

            //CSS bundle for form create page
            bundles.Add(
                new StyleBundle("~/style/form-create").Include(
                    "~/Content/css/form.css",
                    "~/Content/css/form-create.css",
                    "~/Content/css/form-style.css"
                )
            );

            //CSS bundle for form index page
            bundles.Add(
                new StyleBundle("~/style/form-index").Include(
                    "~/Content/css/form.css",
                    "~/Content/css/form-index.css"
                )
            );

            //CSS bundle for form index page
            bundles.Add(
                new StyleBundle("~/style/formresults-thankyou").Include(
                    "~/Content/css/formresults-thankyou.css"
                )
            );

            //CSS bundle for formstyle
            bundles.Add(
                new StyleBundle("~/style/form-style").Include(
                    "~/Content/css/form.css",
                    "~/Content/css/form-style.css",
                    "~/Content/css/form-view.css"
                )
            );

            //CSS bundle for formstyle
            bundles.Add(
                new StyleBundle("~/style/formresults-detail").Include(
                    "~/Content/css/formresults-details.css"
                )
            );

            //CSS bundle for formstyle
            bundles.Add(
                new StyleBundle("~/style/form-disabled").Include(
                    "~/Content/css/form-disabled.css"
                )
            );
            
            //*****************************************
            //***************LESS BUNDLES**************
            //*****************************************
            //LESS bundle for the icons
            bundles.Add(
                new LessBundle("~/style/ionicons").Include(
                    "~/Content/ionicons/ionicons.less"
                )
            );
=======
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
>>>>>>> master
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
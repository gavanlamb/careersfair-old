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
            //********************************************
            //***************SCRIPT BUNDLES***************
            //********************************************
            bundles.Add(
                new ScriptBundle("~/script/bootstrap")
                    .Include("~/Scripts/bootstrap.min.js")); 
            
            bundles.Add(
                new ScriptBundle("~/script/formbuildertabs")
                    .Include("~/Scripts/bootstrap-formbuilder-tabs.js"));
            
            bundles.Add(
                new ScriptBundle("~/script/jquery_1102")
                    .Include("~/Scripts/jquery-1.10.2.min.js"));

            bundles.Add(
                new ScriptBundle("~/script/jquery_200")
                    .Include("~/Scripts/jquery-2.0.0.min.js"));

            bundles.Add(
                new ScriptBundle("~/script/jquery")
                    .Include("~/Scripts/jquery-2.1.4.min.js"));

            bundles.Add(
                new ScriptBundle("~/script/jqueryvalidation")
                    .Include("~/Scripts/jquery.validate.*"));

            bundles.Add(
                new ScriptBundle("~/script/jqueryui")
                    .Include("~/Scripts/jquery-ui-1.11.4.js"));

            bundles.Add(
                new ScriptBundle("~/script/jqueryajax")
                    .Include("~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(
                new ScriptBundle("~/script/modernizr")
                    .Include("~/Scripts/modernizr-2.6.2.js"));


            //*****************************************
            //***************CSS BUNDLES***************
            //*****************************************
            //CSS bundle for the bootstrap theme
            bundles.Add(
                new StyleBundle("~/style/main")
                    .Include("~/Content/css/main.css"));

            //CSS bundle for the bootstrap theme
            bundles.Add(
                new StyleBundle("~/style/bootstrap")
                    .Include("~/Content/css/bootstrap-theme.min.css")
                    .Include("~/Content/css/bootstrap.min.css"));

            //CSS bundle for the icons
            bundles.Add(
                new StyleBundle("~/style/ionicons")
                    .Include("~/Content/css/ionicons.css"));

            //CSS bundle for formbuilder
            bundles.Add(
                new StyleBundle("~/style/formbuilder")
                    .Include("~/Content/css/formbuilder.css"));

            //CSS bundle for formstyle
            bundles.Add(
                new StyleBundle("~/style/formstyle")
                    .Include("~/Content/css/formstyle.css"));

            //CSS bundle for the site
            bundles.Add(
                new StyleBundle("~/style/menu")
                    .Include("~/Content/css/menu.css"));
        }
    }
}
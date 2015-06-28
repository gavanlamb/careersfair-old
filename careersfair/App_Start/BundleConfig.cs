using System.Web;
using System.Web.Optimization;

namespace careersfair.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Bundles for Bootstrap
            bundles.Add(new ScriptBundle("~/Bundles/Bootstrap").Include("~/Scripts/bootstrap.min.js"));
            bundles.Add(new StyleBundle("~/Content/Bootstrap").Include("~/Content/css/bootstrap-theme.min.css", "~/Content/css/bootstrap334.min.css"));

            //jquery
            bundles.Add(new ScriptBundle("~/Bundles/JQuery").Include("~/Scripts/jquery-2.1.4.min.js"));

            //jquery validation
            bundles.Add(new ScriptBundle("~/Bundles/JQueryValidation").Include("~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.validate.unobtrusive.min.js"));
        } 
    }
}
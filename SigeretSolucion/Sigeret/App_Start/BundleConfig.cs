using System.Web;
using System.Web.Optimization;

namespace Sigeret
{
    public class BundleConfig
    {
        // Para obtener más información acerca de Bundling, consulte http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //referencia para scripts globales de javascript en el cliente -- Saúl H. Sánchez.
            bundles.Add(new ScriptBundle("~/bundles/global").Include(
                        "~/Scripts/ClientGlobal.js"));

            // Agregando referencia de Boostrap para el Layaout de la aplicación -- Saúl H. S.
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap*", "~/Scripts/bootstrapmodal/bootstrap*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/timepicker").Include(
                        "~/Scripts/jquery-ui-timepicker-addon.js",
                        "~/Scripts/jquery-ui-sliderAccess.js",
                        "~/Scripts/jquery-ui-timepicker-es.js"));

            bundles.Add(new ScriptBundle("~/bundles/maskedinput").Include(
                        "~/Scripts/jquery.inputmask/jquery.inputmask-{version}.js",
                        "~/Scripts/jquery.inputmask/jquery.inputmask.extensions-{version}.js",
                        "~/Scripts/jquery.inputmask/jquery.inputmask.date.extensions-{version}.js",
                        "~/Scripts/jquery.inputmask/jquery.inputmask.numeric.extensions-{version}.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de creación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            // Adicionando referencia para los estilos. -- Saúl H. S.
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrapmodal").Include("~/Content/bootstrap-modal-bs3patch.css", "~/Content/bootstrap-modal.css"));

            bundles.Add(new ScriptBundle("~/bundles/chosen").Include("~/Scripts/chosen*"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css",
                        "~/Content/chosen.css"));
        }
    }
}
using Sigeret.CustomCode;
using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace Sigeret.Controllers
{
    /// <summary>
    /// Clase controlador base utilizada para establecer objetos y métodos comunes
    /// a ser utilizados en todos los controladores de la aplicación.
    /// </summary>
    [Authorize]
    [CustomAuthorize]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class BaseController : Controller
    {
        /// <summary>
        /// objeto de instancia de entity framework para las solicitudes
        /// a la base de datos.
        /// </summary>
        protected SigeretContext db = new SigeretContext();

        /// <summary>
        /// Método utilizado en el momento que se van a liberar los recursos
        /// de los objetos de las clases que hereden de BaseController
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            // Custom error page display
            dynamic returnData;
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (!filterContext.HttpContext.IsCustomErrorEnabled)
                {
                    returnData = filterContext.Exception.Message + " " + filterContext.Exception.StackTrace;
                }
                else
                {
                    returnData = "Ha ocurrido un error al procesar la solicitud\n" + filterContext.Exception.Message + " " + filterContext.Exception.StackTrace; 
                }

                // TODO: Decide what to do if ajax
                filterContext.HttpContext.Response.StatusCode = 401;
                Json(returnData, JsonRequestBehavior.AllowGet).ExecuteResult(this.ControllerContext);
            }
            else
            {
                filterContext.ExceptionHandled = true;
                this.View(
                    "Error",
                    new HandleErrorInfo(
                        filterContext.Exception,
                        RouteData.GetRequiredString("controller"),
                        RouteData.GetRequiredString("action")
                    )
                ).ExecuteResult(this.ControllerContext);
            }
        }

        protected override void HandleUnknownAction(string actionName)
        {
            // If controller is ErrorController dont 'nest' exceptions
            if (this.GetType() != typeof(CustomErrorsController))
                this.InvokeHttp404(HttpContext);
        }

        public ActionResult InvokeHttp404(HttpContextBase httpContext)
        {
            Response.TrySkipIisCustomErrors = true;
            IController errorController = DependencyResolver.Current.GetService<CustomErrorsController>();
            var errorRoute = new RouteData();
            errorRoute.Values.Add("controller", "CustomErrors");
            errorRoute.Values.Add("action", "NotFound");
            errorRoute.Values.Add("url", httpContext.Request.Url.OriginalString);
            errorController.Execute(new RequestContext(
                 httpContext, errorRoute));

            return new EmptyResult();
        }

        public string PartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}



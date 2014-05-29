using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebMatrix.WebData;

namespace Sigeret.CustomCode
{
    /// <summary>
    /// Clase utilizada para manejar los permisos
    /// de los usuarios, a las acciones de la apli-
    /// cación en base a sus roles.
    /// </summary>
    public class CustomAuthorize : AuthorizeAttribute
    {
        /// <summary>
        /// Objeto entity framework para gestionar las consultas
        /// a la base de datos.
        /// </summary>
        private SigeretContext db;

        /// <summary>
        /// Método utilizado para verificar que el usuario de la aplicación
        /// tiene permiso sobre la acción que intente ejecutar, y si dicho
        /// rol no tiene permiso es redireccionado a una página que le muestra
        /// al usuario un error indicando que no tiene permisos.
        /// </summary>
        /// <param name="filterContext">
        /// Encapsula la información usasa para el mecanismo de autorización.
        /// </param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            db = new SigeretContext();
            var descriptor = filterContext.ActionDescriptor;
            bool isAView = descriptor.GetCustomAttributes(typeof(VistaAttribute), true).Count() > 0;
            if (!isAView)
                return;
            if (filterContext.ActionDescriptor.ActionName.ToLower() == "login") return;

            if (WebSecurity.IsAuthenticated) // si el usuario inició sesion
            {
                var attributes = descriptor.GetCustomAttributes(typeof(VistaAttribute), false).FirstOrDefault() as VistaAttribute;
                var a = db.Accions
                    .FirstOrDefault(ac => ac.Descriptor == attributes.Descriptor );
                if (a != null)
                {
                    //si el usuario tiene permisos.
                    if (new MenuAuthorize().HasPermission(a.Name, a.Controlador.Name))
                    {
                        return;
                    }
                }
                filterContext.Result = new RedirectToRouteResult(
                 new RouteValueDictionary
                 {
                     { "controller", "Unauthorize" },
                     { "action", "ErrorUnauthorized" }
                 });
            }
            else // si el usuario no ha iniciado sesión.
            {
                bool isAllowAnonymous = descriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Count() > 0;
                if (isAllowAnonymous) return;

                filterContext.Result = new RedirectToRouteResult(
                     new RouteValueDictionary
                 {
                     { "controller", "Account" },
                     { "action", "Login" }
                 });
            }
        }
    }
}
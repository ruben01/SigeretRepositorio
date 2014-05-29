using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using WebMatrix.WebData;

namespace Sigeret.CustomCode
{
    public class MenuAuthorize
    {
        SigeretContext db = new SigeretContext();
        /// <summary>
        /// Constructor para sacar los permisos que tiene un usuario y almacenarlos en la variable Menu
        /// </summary>
        public MenuAuthorize()
        {
            try
            {
                if (WebSecurity.IsAuthenticated)
                {
                    var user = db.UserProfiles.FirstOrDefault(usr => usr.UserId == WebSecurity.CurrentUserId);

                    foreach (webpages_Roles role in user.webpages_Roles)
                    {
                        foreach (Sigeret.Models.Accion action in role.Accions)
                        {
                            if (!Menu.ContainsKey(
                                        string.Format("{0}-{1}", action.Controlador.Name,
                                        action.Name)
                                ))
                            {
                                Menu.Add(
                                    string.Format("{0}-{1}", action.Controlador.Name, action.Name),
                                    action.Name
                                );
                            }
                        }
                    }
                }
            }
            catch
            {
                new AuthorizationContext().Result = new RedirectToRouteResult(
                     new RouteValueDictionary
                 {
                     { "controller", "Account" },
                     { "action", "Login" }
                 });
            }
        }

        //Variables privadas
        Dictionary<string, string> Menu = new Dictionary<string, string>();

        /// <summary>
        /// Retorna true si el usuario loged acutal tiene permiso al controlador-acción
        /// </summary>
        /// <param name="actionName">Nombre de la acción</param>
        /// <param name="ControllerName">Nombre del controlador</param>
        /// <returns></returns>
        public bool HasPermission(string actionName, string ControllerName)
        {
            if (Menu.Where(d => d.Key == (ControllerName + "-" + actionName) && d.Value == actionName).Count() > 0)
                return true;
            return false;
        }

        public bool HasPermission(string actionName, string ControllerName, String ActionDescriptor)
        {
            if (Menu.Where(d => d.Key == (ControllerName + "-" + actionName) && d.Value == actionName).Count() > 0)
                return true;
            else
            {
                var Action = db.Accions.FirstOrDefault(a => a.Descriptor == ActionDescriptor);
                if (Action != null)
                {
                    if (Menu.Where(d => d.Key == (Action.Controlador.Name + "-" + Action.Name) && d.Value == Action.Name).Count() > 0)
                        return true;
                }
            }

            return false;
        }
    }
}


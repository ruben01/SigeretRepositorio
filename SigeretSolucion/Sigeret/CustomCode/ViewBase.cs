using Sigeret.CustomCode;
using System;
using System.Resources;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Sigeret.CustomCode
{
    /// <summary>
    /// Clase base utilizada para encapsular genéricamente métodos y propiedades
    /// que serán utilizados en todos los Views de la apliación.
    /// </summary>
    /// <typeparam name="TModel">Clase modelo abstracta del View en cuestión en tiempo de ejecución</typeparam>
    public abstract class ViewBase<TModel> : System.Web.Mvc.WebViewPage<TModel> where TModel : class
    {
        public MenuAuthorize menu = new MenuAuthorize();

        private static SimpleRoleProvider roleProvider = (SimpleRoleProvider)Roles.Provider;

        /// <summary>
        /// propiedad utilizada para saber si el usuario activo es un estudiante.
        /// </summary>
        public bool EsEstudiante
        {
            get
            {
                return roleProvider.IsUserInRole(WebSecurity.CurrentUserName, "Estudiante");
            }
        }

        /// <summary>
        /// propiedad utilizada para saber si el usuario activo es un profesor.
        /// </summary>
        public bool EsProfesor
        {
            get
            {
                return roleProvider.IsUserInRole(WebSecurity.CurrentUserName, "Profesor");
            }
        }

        /// <summary>
        /// propiedad utilizada para saber si el usuario activo es un administrador.
        /// </summary>
        public bool EsAdministrador
        {
            get
            {
                return roleProvider.IsUserInRole(WebSecurity.CurrentUserName, "Administrador");
            }
        }
    }
}
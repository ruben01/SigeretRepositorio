using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sigeret.Models.ViewModels
{

    /// <summary>
    /// Clase modelo utilizada para la pantalla
    /// de editar los permisos de los diferentes
    /// roles de la aplicáción.
    /// </summary>
    public class PermisosViewModel
    {
        /// <summary>
        /// propiedad que hace referencia al rol
        /// al cual se desea editar los permisos.
        /// </summary>
        [Display(Name = "Rol a Editar")]
        public int RoleId { get; set; }

        /// <summary>
        /// propiedad que establece las acciones
        /// a las cuales tendrá permiso el rol que
        /// se está editando.
        /// </summary>
        public int[] Acciones { get; set; }
    }

    /// <summary>
    /// Clase utilizada para mostrar los datos
    /// de los controladores y las acciones para editar los
    /// permisos en la vista.
    /// </summary>
    public class PermisosViewModelPartial
    {
        /// <summary>
        /// propiedad de instancia con los datos
        /// del rol al cual se le editarán los
        /// permisos.
        /// </summary>
        public webpages_Roles Rol { get; set; }

        /// <summary>
        /// propiedad 
        /// </summary>
        public int[] AccionesSeleccionadas { get; set; }

        /// <summary>
        /// propiedad con las acciones de la aplicación.
        /// </summary>
        public List<Action> Acciones { get; set; }

        /// <summary>
        /// propiedad con los controladores principales de la
        /// aplicación.
        /// </summary>
        public List<Controlador> Controladores { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace Sigeret.Models.ViewModels
{
    public class ContactoViewModel
    {
        [Required]
        public int Id { get; set; }
        [Display(Name="Descripción")]
        [Required]
        public string Descripcion { get; set; }
        public int IdUserProfile { get; set; }
        [Required]
        [Display(Name="Tipo de Contacto")]
        public int IdTipoContacto { get; set; }

        public ContactoViewModel()
        {
            IdUserProfile = WebSecurity.CurrentUserId;
        }
    }
}
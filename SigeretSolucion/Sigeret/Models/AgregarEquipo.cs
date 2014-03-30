using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sigeret.Models
{
    public class AgregarEquipo
    {

        
        public int IdModelo { get; set; }
        public bool IsSelected { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


    }
}
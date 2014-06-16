using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sigeret.Models.ViewModels
{
    public class MarcaModel
    {
        [Required]
        public string Marca { get; set; }
    }
}
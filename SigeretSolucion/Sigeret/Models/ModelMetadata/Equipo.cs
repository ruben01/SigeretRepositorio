using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sigeret.Models
{
    [MetadataType(typeof(EquipoMetadata))]
    public partial class Equipo
    {
    }

    class EquipoMetadata
    {
        public int Id { get; set; }
        [Display(Name="Número de Serie")]
        [Required]
        public string Serie { get; set; }
        [Display(Name = "Estatus del Equipo")]
        [Required]
        public int EstatusEquipo { get; set; }
        public int IdModeloEquipo { get; set; }
    }
}

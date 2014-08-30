using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sigeret.Models
{
    [MetadataType(typeof(AulaEdificioMetadata))]
    public partial class AulaEdificio
    {
        
    }

    class AulaEdificioMetadata
    {
        public int Id { get; set; }
        public string Aula { get; set; }
        [Display(Name="Edificio")]
        public int IdLugar { get; set; }
        public virtual Lugar Lugar { get; set; }
        public virtual ICollection<Solicitud> Solicituds { get; set; }
    }

}

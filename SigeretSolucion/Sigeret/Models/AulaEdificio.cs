using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class AulaEdificio
    {
        public AulaEdificio()
        {
            this.Solicituds = new List<Solicitud>();
        }

        public int Id { get; set; }
        public string Aula { get; set; }
        public int IdLugar { get; set; }
        public virtual Lugar Lugar { get; set; }
        public virtual ICollection<Solicitud> Solicituds { get; set; }
    }
}

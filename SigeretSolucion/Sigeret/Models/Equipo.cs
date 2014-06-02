using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class Equipo
    {
        public Equipo()
        {
            this.Solicituds = new List<Solicitud>();
        }

        public int Id { get; set; }
        public string Serie { get; set; }
        public int EstatusEquipo { get; set; }
        public int IdModeloEquipo { get; set; }
        public virtual ModeloEquipo ModeloEquipo { get; set; }
        public virtual ICollection<Solicitud> Solicituds { get; set; }
    }
}

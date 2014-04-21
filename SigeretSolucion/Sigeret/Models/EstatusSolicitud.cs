using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class EstatusSolicitud
    {
        public EstatusSolicitud()
        {
            this.Solicituds = new List<Solicitud>();
        }

        public int Id { get; set; }
        public string Estatus { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Solicitud> Solicituds { get; set; }
    }
}

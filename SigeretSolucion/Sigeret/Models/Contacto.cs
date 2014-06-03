using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class Contacto
    {
        public Contacto()
        {
            this.Solicituds = new List<Solicitud>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdUserProfile { get; set; }
        public int IdTipoContacto { get; set; }
        public virtual TipoContacto TipoContacto { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<Solicitud> Solicituds { get; set; }
    }
}

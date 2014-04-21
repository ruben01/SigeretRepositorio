using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class TipoContacto
    {
        public TipoContacto()
        {
            this.Contactoes = new List<Contacto>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Contacto> Contactoes { get; set; }
    }
}

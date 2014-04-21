using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class EstatusEquipo
    {
        public EstatusEquipo()
        {
            this.Equipoes = new List<Equipo>();
        }

        public int Id { get; set; }
        public string Estatus { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Equipo> Equipoes { get; set; }
    }
}

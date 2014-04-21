using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class Equipo
    {
        public Equipo()
        {
            this.SolicitudEquipoes = new List<SolicitudEquipo>();
        }

        public int Id { get; set; }
        public string Serie { get; set; }
        public int IdEstatusEquipo { get; set; }
        public int IdModelo { get; set; }
        public virtual EstatusEquipo EstatusEquipo { get; set; }
        public virtual ModeloEquipo ModeloEquipo { get; set; }
        public virtual ICollection<SolicitudEquipo> SolicitudEquipoes { get; set; }
    }
}

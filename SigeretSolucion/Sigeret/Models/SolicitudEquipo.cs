using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class SolicitudEquipo
    {
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public int idEquipo { get; set; }
        public virtual Equipo Equipo { get; set; }
        public virtual Solicitud Solicitud { get; set; }
    }
}

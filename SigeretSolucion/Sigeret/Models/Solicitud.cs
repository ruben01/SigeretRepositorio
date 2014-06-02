using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class Solicitud
    {
        public Solicitud()
        {
            this.Equipoes = new List<Equipo>();
            this.Contactoes = new List<Contacto>();
        }

        public int Id { get; set; }
        public System.TimeSpan HoraInicio { get; set; }
        public System.TimeSpan HoraFin { get; set; }
        public string Descripcion { get; set; }
        public int IdUserProfile { get; set; }
        public int IdAulaEdificio { get; set; }
        public int EstatusSolicitud { get; set; }
        public System.DateTime Fecha { get; set; }
        public int TipoSolicitud { get; set; }
        public virtual AulaEdificio AulaEdificio { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<Equipo> Equipoes { get; set; }
        public virtual ICollection<Contacto> Contactoes { get; set; }
    }
}

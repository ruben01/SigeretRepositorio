using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class Solicitud
    {
        public Solicitud()
        {
            this.SolicitudEquipoes = new List<SolicitudEquipo>();
            this.SolicitudSms = new List<SolicitudSm>();
        }

        public int Id { get; set; }
        public System.TimeSpan HoraInicio { get; set; }
        public System.TimeSpan HoraFin { get; set; }
        public string Descripcion { get; set; }
        public int IdUserProfile { get; set; }
        public int IdLugar { get; set; }
        public int IdEstatusSolicitud { get; set; }
        public System.DateTime Fecha { get; set; }
        public virtual AulaEdificio AulaEdificio { get; set; }
        public virtual EstatusSolicitud EstatusSolicitud { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<SolicitudEquipo> SolicitudEquipoes { get; set; }
        public virtual ICollection<SolicitudSm> SolicitudSms { get; set; }
    }
}

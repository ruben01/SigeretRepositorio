using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class SolicitudSm
    {
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public int IdContacto { get; set; }
        public virtual Contacto Contacto { get; set; }
        public virtual Solicitud Solicitud { get; set; }
    }
}

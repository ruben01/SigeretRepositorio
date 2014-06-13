using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.Contactoes = new List<Contacto>();
            this.Solicituds = new List<Solicitud>();
            this.webpages_Roles = new List<webpages_Roles>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public byte[] Foto { get; set; }
        public string Matricula { get; set; }
        public int EstatusUsuario { get; set; }
        public string NipSms { get; set; }
        public virtual ICollection<Contacto> Contactoes { get; set; }
        public virtual ICollection<Solicitud> Solicituds { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}

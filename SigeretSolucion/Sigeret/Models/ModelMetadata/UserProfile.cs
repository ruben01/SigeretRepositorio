using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sigeret.Models
{
    [MetadataType(typeof(UserProfileMetadata))]
    public partial class UserProfile
    {
        
    }

    class UserProfileMetadata
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [System.Web.Mvc.Remote("ValidarUserName", "Account", AdditionalFields = "UserId")]
        public string UserName { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [System.Web.Mvc.Remote("ValidarCedula", "Account", AdditionalFields="UserId")]
        public string Cedula { get; set; }
        public byte[] Foto { get; set; }
        [Required]
        [System.Web.Mvc.Remote("ValidarMatricula", "Account", AdditionalFields="UserId")]
        public string Matricula { get; set; }
        public int IdEstatusUsuario { get; set; }
        public virtual ICollection<Contacto> Contactoes { get; set; }
        public virtual EstatusUsuario EstatusUsuario { get; set; }
        public virtual ICollection<Solicitud> Solicituds { get; set; }
        public virtual ICollection<UsuarioNipSm> UsuarioNipSms { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}

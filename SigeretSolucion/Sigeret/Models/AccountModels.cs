using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Security;

namespace Sigeret.Models
{
    
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
     
    
    //[Table("UserProfile")]
    //public class UserProfile
    //{
    //    [Key]
    //    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]        
    //    public int UserId { get; set; }
    //    public string UserName { get; set; }
    //    public string Apellido { get; set; }
    //    public string Nombre { get; set; }
    //    public string Cedula { get; set; }
    //    public string Matricula { get; set; }
    //    public int IdEstatusUsuario { get; set; }
    //}
     

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar la nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {

        
        [Required]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordar cuenta?")]
        public bool RememberMe { get; set; }
    }
    
    public class RegisterModel
    {
        
        [Required]
        [Display(Name = "Nombres")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string Apellido { get; set; }

        [Required]
        [Display(Name = "Nombre de usuario")]
        [System.Web.Mvc.Remote("ValidarUserName", "Account", AdditionalFields = "UserId")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Matricula")]
        [System.Web.Mvc.Remote("ValidarMatricula", "Account", AdditionalFields = "UserId")]
        public string Matricula { get; set; }

        [Required]
        [Display(Name = "Cedula")]
        [System.Web.Mvc.Remote("ValidarCedula", "Account", AdditionalFields = "UserId")]
        public string Cedula { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required]
        [EmailAddress(ErrorMessage="Introduzca una dirección de correo válida")]
        public string Correo { get; set; }

        [Required]
        [Display(Name = "Rol de Usuario")]
        public int RoleId { get; set; }

        [Display(Name = "Nip Sms")]
        public string NipSms { get; set; }
    }
     

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}

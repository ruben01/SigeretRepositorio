using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sigeret.Models.ViewModels
{
    public class SolicitudViewModel
    {
        public int Id { get; set; }

        [DisplayName("Hora Inicio")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        [ValidarHora(TipoValidacion.ValidarRango, "Hora fuera del Horario laboral 7:00am-10:00pm")]
        public System.TimeSpan HoraInicio { get; set; }

        [DisplayName("Hora Final")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        [ValidarHora(TipoValidacion.Comparar, "La hora Final debe ser mayor a la inicial", compararCon: "HoraInicio")]
        [ValidarHora(TipoValidacion.ValidarRango, "Hora fuera del Horario laboral 7:00am-10:00pm")]
        public System.TimeSpan HoraFin { get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public int IdUserProfile { get; set; }
        public int IdLugar { get; set; }
        public int IdEstatusSolicitud { get; set; }

        [validarFecha]
        public System.DateTime Fecha { get; set; }
        [Display(Name = "Edificio")]
        [Required]
        public int EdificioId { get; set; }
        [Required]
        [Display(Name="Salón")]
        public int SalonId { get; set; }
    }
}
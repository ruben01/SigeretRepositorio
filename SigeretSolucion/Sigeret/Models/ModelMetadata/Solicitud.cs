using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Sigeret.Models
{

     [MetadataType(typeof(SolicitudMetadata))]
    public partial class Solicitud
    {
    }


    class SolicitudMetadata
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
        public int IdAulaEdificio { get; set; }
        [DisplayName("Id Estatus Solicitud")]
        public int EstatusSolicitud { get; set; }

        
        [validarFecha]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Fecha { get; set; }

        public virtual AulaEdificio AulaEdificio { get; set; }
        public virtual UserProfile UserProfile { get; set; }

    }
}
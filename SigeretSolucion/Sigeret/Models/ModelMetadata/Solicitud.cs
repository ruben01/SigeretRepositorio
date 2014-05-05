﻿using System;
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
        public int IdLugar { get; set; }
        public int IdEstatusSolicitud { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [validarFecha]
        public System.DateTime Fecha { get; set; }

        public virtual AulaEdificio AulaEdificio { get; set; }
        public virtual EstatusSolicitud EstatusSolicitud { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<SolicitudEquipo> SolicitudEquipoes { get; set; }
        public virtual ICollection<SolicitudSm> SolicitudSms { get; set; }

    }
}
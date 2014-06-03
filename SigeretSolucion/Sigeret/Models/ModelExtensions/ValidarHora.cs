using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Sigeret.Models
{

    public enum TipoValidacion
    {
        ValidarRango,
        Comparar
    }
   [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
                AllowMultiple = true, Inherited = true)]
    public class ValidarHora:ValidationAttribute
    {

        private TipoValidacion tipoValidacion;
        private TimeSpan horaInicio;
        private TimeSpan horaFin;
        private string error;
        private string nombreCampoComparar;

        public ValidarHora() { }

        public ValidarHora(TipoValidacion _tipoValidacion, string mensaje, string compararCon="", string _horaInicio="")
        {

                tipoValidacion = _tipoValidacion;

                switch(_tipoValidacion){

                    case TipoValidacion.Comparar:{

                         nombreCampoComparar= compararCon;
                         error = mensaje;
                         break;
                       }

                    case TipoValidacion.ValidarRango:{
                        
                        error = mensaje;
                        break;
                        }

                }
                       
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            switch (tipoValidacion)
            {
                case TipoValidacion.Comparar:
                    {

                        var baseProperyInfo = validationContext.ObjectType.GetProperty(nombreCampoComparar);
                        var horaInicio = (TimeSpan)baseProperyInfo.GetValue(validationContext.ObjectInstance, null);
                       
                        if (value!= null)
                        {
                            TimeSpan horaFin = (TimeSpan)value;

                            if (horaFin <= horaInicio)
                            {

                                string message = string.Format(error, validationContext.DisplayName);
                                return new ValidationResult(message);
                            }
                            else if (horaFin < horaInicio.Add(new TimeSpan(1,0,0)))
                            {
                                string message = string.Format(error, validationContext.DisplayName);
                                return new ValidationResult("El tiempo mínimo entre las horas debe ser 1h");
                            }

                        }

                        break;
                    }


               case TipoValidacion.ValidarRango:
              {
                  horaInicio=(TimeSpan)value;
                  if (horaInicio < new TimeSpan(07, 00, 00) || horaInicio > new TimeSpan(22, 00, 00))
                  {
                      string message = string.Format(error, validationContext.DisplayName);
                      return new ValidationResult(message);
                  }
                  break;
              }
            }



           
            return ValidationResult.Success;
        }
    }
}
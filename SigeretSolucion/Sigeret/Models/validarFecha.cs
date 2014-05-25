using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sigeret.Models
{
    public class validarFecha:ValidationAttribute
    {
        

        public validarFecha():base("{0} Fuera de rango."+String.Format("  {0:dd/MM/yy}", DateTime.Now)+" - "+String.Format("{0:dd/MM/yy}", DateTime.Now.AddMonths(3)))
        {
            
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                if ((DateTime)value < DateTime.Now.AddDays(-1))
                {
                    var error = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(error);
                }
                else if ((DateTime)value > DateTime.Now.AddMonths(3))
                {
                    var error = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(error);
                }
            }
            

           
            return ValidationResult.Success;
        }

    }
}
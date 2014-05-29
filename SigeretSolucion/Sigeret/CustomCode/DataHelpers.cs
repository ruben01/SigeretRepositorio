using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Sigeret.CustomCode
{
    public static class DataHelpers
    {
        public static string phoneRegex = @"(\d{3})(\d{3})(\d{4})";
        public static string phoneMask = "($1) $2-$3";
        public static string dniRegex = @"(\d{3})(\d{7})(\d{1})";
        public static string dniMask = "$1-$2-$3";
        public static string rncRegex = @"(\d{3})(\d{5})(\d{1})";
        public static string rncMask = "$1-$2-$3";

        public static string PhoneFormat(this string phone)
        {
            try
            {
                return Regex.Replace(phone, phoneRegex, phoneMask);
            }
            catch (Exception)
            {
                return phone;
            }
        }

        public static string DniFormat(this string dni)
        {
            try
            {
                return Regex.Replace(dni, dniRegex, dniMask);
            }
            catch (Exception)
            {
                return dni;
            }
        }

        /// <summary>
        /// Version mejorada de la funcion para formatear cadenas en formatos específicos
        /// </summary>
        /// <param name="rawValue">Cadena con los datos a ser formateados</param>
        /// <param name="formatType">Tipo de formato a utilizar (phone,dni,rnc,date,datetime)</param>
        /// <returns></returns>
        public static string CFormat(this string rawValue, string formatType)
        {
            try
            {
                switch (formatType.ToLower())
                {
                    case "phone": return Regex.Replace(rawValue, phoneRegex, phoneMask);
                    case "dni": return Regex.Replace(rawValue, dniRegex, dniMask);
                    case "rnc": return Regex.Replace(rawValue, rncRegex, rncMask);
                    default: return rawValue;
                }
            }
            catch (Exception)
            {
                return rawValue;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace Sigeret.App_Start
{
    public static class WebSecurityStart
    {

        /// <summary>
        /// Saúl H. Sánchez.
        /// 
        /// Metodo utilizado para inicializar la configuración del
        /// plugin WebSecurity, y la inicialización de la conexión
        /// de la base de datos, tomando en cuenta la tabla de la 
        /// base de datos que almacenará los datos de los usuarios
        /// de la aplicación. También en este método se insertan los
        /// datos por defecto requeridos para el mínimo funcionamiento
        /// de la aplicación, tales como roles y un usuario por defecto.
        /// </summary>
        public static void Register()
        {
            try
            {
                WebSecurity.InitializeDatabaseConnection
                    (
                     "SigeretContext",
                     "UserProfile",
                     "UserId",
                     "username",
                     autoCreateTables: true
                    );
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No se pudo inicializar la conexión con la base de datos.", ex);
            }
        }
    }
}
using Sigeret.CustomCode;
using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

                //verificacion y creacion de roles de la aplicacion
                var roles = (SimpleRoleProvider)Roles.Provider;
                if (roles.GetAllRoles().Count() == 0)
                {
                    roles.CreateRole("Administrador");
                    roles.CreateRole("Profesor");
                    roles.CreateRole("Estudiante");
                }

                //Creacion y gestion de parametros para cuenta administrador
                if (!WebSecurity.UserExists("SigeretAdmin"))
                {                   
                    WebSecurity.CreateUserAndAccount(
                        "SigeretAdmin", "000000",
                        propertyValues: new { Nombre = "Administrador", Apellido = "Sigeret", Cedula = "00000000000", Matricula = "7777777777" });
                }

                if (!roles.GetRolesForUser("SigeretAdmin").Contains("Administrador"))
                {
                    roles.AddUsersToRoles(new[] { "SigeretAdmin" }, new[] { "Administrador" });
                }

                using (SigeretContext db = new SigeretContext())
                {
                    //Almacenando datos de los controladores y la acciones en la bd, para el modulo de permisos.
                    var types =
                        from a in AppDomain.CurrentDomain.GetAssemblies()
                        from t in a.GetTypes()
                        where typeof(IController).IsAssignableFrom(t)
                        select t;

                    foreach (var ctrlType in types)
                    {
                        ReflectedControllerDescriptor ctrlDescriptor = new ReflectedControllerDescriptor(ctrlType);
                        if (ctrlDescriptor.GetCustomAttributes(typeof(EsControllerAttribute), false).Any())
                        {
                            var attribute = ctrlDescriptor.GetCustomAttributes(typeof(EsControllerAttribute), false).FirstOrDefault() as EsControllerAttribute;
                            Controlador ctrl = db.Controladors.FirstOrDefault(c => c.Descriptor == attribute.Descriptor);

                            if (ctrl == null)
                            {
                                ctrl = new Controlador();
                                ctrl.Name = attribute.ControllerName;
                                ctrl.Descriptor = attribute.Descriptor;
                                db.Controladors.Add(ctrl);
                            }
                            else
                            {
                                if (ctrl.Name != attribute.ControllerName)
                                {
                                    ctrl.Name = attribute.ControllerName;
                                }
                                db.Entry(ctrl).State = System.Data.EntityState.Modified;
                            }

                            foreach (ActionDescriptor ad in ctrlDescriptor.GetCanonicalActions())
                            {
                                if (ad.GetCustomAttributes(typeof(VistaAttribute), false).Any())
                                {
                                    var ActionAttribute = ad.GetCustomAttributes(typeof(VistaAttribute), false).FirstOrDefault() as VistaAttribute;
                                    Accion acc = db.Accions.FirstOrDefault(a => a.Descriptor == ActionAttribute.Descriptor);

                                    if (acc == null)
                                    {
                                        acc = new Accion();
                                        acc.Name = ActionAttribute.ViewName;
                                        acc.Descriptor = ActionAttribute.Descriptor;
                                        ctrl.Accions.Add(acc);
                                        db.Accions.Add(acc);
                                    }
                                    else
                                    {
                                        if (acc.Name != ActionAttribute.ViewName)
                                        {
                                            acc.Name = ActionAttribute.ViewName;
                                        }
                                        db.Entry(acc).State = System.Data.EntityState.Modified;
                                    }
                                }
                            }
                            db.SaveChanges();
                        }
                    }

                    //Asignando permisos sobre todas las acciones a la cuenta de Administrador por defecto.
                    var adminRole = db.webpages_Roles.FirstOrDefault(r => r.RoleName == "Administrador");
                    foreach (Accion a in db.Accions)
                    {
                        if (!adminRole.Accions.Contains(a))
                            adminRole.Accions.Add(a);
                    }
                    db.Entry(adminRole).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No se pudo inicializar la conexión con la base de datos.", ex);
            }
        }
    }
}
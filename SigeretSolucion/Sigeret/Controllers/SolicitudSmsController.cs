using Sigeret.Models;
using Sigeret.CustomCode;
using Sigeret.Models.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;

namespace Sigeret.Controllers
{
    [AllowAnonymous]
    public class SolicitudSmsController : BaseController
    {

        /*
        public ActionResult prueba(){        

            return View();
        }
        [HttpPost]
        public ActionResult prueba(string fecha,string horaInicio, string horaFin)
        {
            DateTime obj = new DateTime();
            obj = DateTime.Parse(fecha);

            fecha = obj.ToString("yyyy-MM-dd");

            var query = db.Database.SqlQuery<int>("EXEC EquiposDisponibles {0},{1},{2}", fecha, horaInicio, horaFin).ToList();

            return View();
        }
         */
        //
        // GET: /SolicitudSms/
   /*     public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
    */

        public ActionResult Index(string body, string From)
        {
            string opcion = "";
            string sender = "2766011354";
            string solicitud = "";
            string respuesta;

            if (body.ToLower() == "ayuda" || body.ToLower() == "c")
            {
                Session["opcion"] = "";
                Session["spp"] = "";
                Session["sppOpcion"] = "";
            }

            if (Session["opcion"] != null) { opcion = (String)Session["opcion"]; }

            if (Session["opcion"] != null)
            {
                if (opcion == "spp")
                {
                    string spp = (string)Session["spp"];

                    //almacena cada valor de la solicitud paso a paso
                    Session["spp"] = spp + "*" + body;
                    opcion = (String)Session["sppOpcion"];

                }
                else if (opcion == "spp*")
                {
                    string sol = (string)Session["spp"];
                    body = "*" + sol+"*"+ body + "*";
                    Session["opcion"] = "";
                }


            }


            //si la longitud del mensaje enviado es mayor a 2 entonces procedemos a sacar una subcadena para verificar si es una solicitud
            if (body.Length > 2)
            {
                solicitud = body.Substring(0, 2);
            }

            if (solicitud == "**")
            {
                //enviamos la solicitud al metodo ProcesarSolicitud
                respuesta = ProcesarSolicitud(body, "5088863180");

            }
            else if (opcion == "aes")
            {
                respuesta = agregarEquipoSolicitud(body, "5088863180");
                Session["opcion"] = "";
                Session["spp"] = "";
                Session["sppOpcion"] = "";
            }
            else if (opcion == "cs")
            {
                respuesta = cancelarSolicitud(body, "5088863180");
                Session["opcion"] = "";

            }
            else if (opcion == "ees")
            {
                respuesta = eliminarEquipoSolicitud(body, "5088863180");
                Session["opcion"] = "";

            }
            else if (opcion == "es")
            {
                respuesta = estatusSolicitud(body, "5088863180");
                Session["opcion"] = "";

            }
            else if (opcion == "de")
            {
                respuesta = getCodigoEquipos(body);
                Session["opcion"] = "";

            }
            else
            {
                if ((String)Session["opcion"] != "spp")
                {
                    opcion = opcion + body;
                    Session["opcion"] = opcion;
                }

                switch (opcion)
                {

                    case "ayuda":
                        respuesta = "\n1 Solicitud\n2 Equipos\n3 NipSMS\n4 Salones \n5 Estatus Solicitud";
                        break;

                    case "":
                        respuesta = "\n1 Solicitud\n2 Equipos\n3 NipSMS\n4 Salones \n5 Estatus Solicitud";
                        break;

                    case "menu":
                        respuesta = "\n1 Solicitud\n2 Equipos\n3 NipSMS\n4 Salones \n5 Estatus Solicitud";
                        Session["opcion"] = "";
                        break;

                    case "1":
                        respuesta = "\n1 Solicitud\n2 Equipos\n3 NipSMS\n4 Salones \n5 Estatus Solicitud";
                        break;


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    case "11":
                        respuesta = "\nMenu Solicitud\n1 Formatos Solicitud \n2 Agregar Equipos \n3 Eliminar Equipos \n4 Cancelar Solicitud";
                        break;

                    case "12":
                        respuesta = "\nEquipos\n1 Codigos Equipos\n2 Descripcion Equipo";
                        break;

                    case "13":
                        respuesta = "\nNipSms \nCodigo utilizado para confirmar la solicitud\n Esta disponible via web o personalmente.\n1 mas detalles";//Codigo para confirmar la solicitud que sera entregado al usuario
                        //al crear su cuenta y luego cada vez q haga una solicitud cuando pase a entregar el equipo se le entregara este codigo personal

                        break;

                    case "112":
                        respuesta = "Digite el codigo de la solicitud, el equipo y la cantidad con el sgt fomato:\ncodigoSolicitud*codigoEquipo*Cantidad";
                        Session["opcion"] = "aes";
                        break;

                    case "113":
                        respuesta = "Ingrese el codigo de la solicitud,codigo equipo y cantidad. \nCon el formato codigoSolicitud*codigoEquipo*Cantidad ";
                        Session["opcion"] = "ees";
                        break;

                    case "14":
                        respuesta = "\n" + getSalones(null);
                        Session["opcion"] = "";
                        break;

                    case "15":
                        respuesta = "\nIngrese el Codigo de la Solicitud";
                        Session["opcion"] = "es";
                        break;

                    case "121":
                        respuesta = "\n" + getCodigoEquipos(null);//Funcion para devolver todos lo equipos disponibles por modelos
                        Session["opcion"] = "";
                        break;

                    case "122":
                        respuesta = "\nEnvie el Codigo Equipo: Ejmp:001";
                        Session["opcion"] = "de";
                        break;

                    case "131":
                        respuesta = "\nNipSMS\n Codigo de 4 digitos generado al momento de crear su cuenta.\nDebe proporcionarlo para una solicitud SMS.\nEj. 9999";
                        Session["opcion"] = "";
                        break;

                    case "111":
                        respuesta = "\n1 Solicitud paso a paso \n2 Formato Solicitud un paso\n3 Formato Fecha\n4 Fomato Hora";
                        break;

                    case "1111":
                        respuesta = "\nIngrese la fecha \nC para cancelar";
                        Session["opcion"] = "spp";
                        Session["sppOpcion"] = "11111";
                        break;

                    case "11111":
                        respuesta = "\nIngrese la Hora inicio.\nEjemplo: 07:00 formato 24h \nC para cancelar";
                        Session["opcion"] = "spp";
                        Session["sppOpcion"] = "111111";
                        break;

                    case "111111":
                        respuesta = "\nIngrese la Hora final.\nEjemplo: 09:00 formato 24h \nC para cancelar";
                        Session["opcion"] = "spp";
                        Session["sppOpcion"] = "1111111";
                        break;

                    case "1111111":
                        respuesta = "\nIngrese el NipSms. \nCodigo utilizado para validar la solicitud \nEnvie ayuda para mas informacion";
                        Session["opcion"] = "spp";
                        Session["sppOpcion"] = "11111111";
                        break;

                    case "11111111":
                        respuesta = "\nIngrese el codigo del Salon. \nEj:001 \nEnvie ayuda y luego seleccione la opcion salon para mas informacion";
                        Session["opcion"] = "spp";
                        Session["sppOpcion"] = "111111111";
                        break;

                    case "111111111":
                        respuesta = "\nIngrese el codigo del Equipo.\nEj:001 \nEnvie ayuda y luego seleccione la opcion Equipo para mas informacion";
                        Session["opcion"] = "spp";
                        Session["sppOpcion"] = "1111111111";
                        break;

                    case "1111111111":
                        respuesta = "\nIngrese la cantidad necesaria del Equipo. \nC para cancelar";
                        Session["opcion"] = "spp*";
                        Session["sppOpcion"] = "";
                        break;

                    case "1112":
                        respuesta = "\nFormato Solicitud:\n *fecha*horaInicio*horaFin*NipSMS*IdSalon*CodigoEquipo1*cantidad*CodigoEquipo2*cantidad*";
                        Session["opcion"] = "";
                        break;

                    case "1113":
                        respuesta = "\nFormato Fecha\nDia/Mes/año \nEjemplo 24/12/1999";
                        Session["opcion"] = "";
                        break;

                    case "1114":
                        respuesta = "\nFormato Hora\n24H Ejemplo \n07:00  \n20:00 \nhora fin mayor a la hora inicio";
                        Session["opcion"] = "";
                        break;

                    case "114":
                        respuesta = "\nCancelar Solicitud\nIngrese el codigo de la Solicitud \nEjemplo 001 ";
                        Session["opcion"] = "cs";
                        break;
                    default:
                        respuesta = "\nNo se Reconoce la Instruccion\n 1 Menu Principal";
                        Session["opcion"] = null;
                        Session["sppOpcion"] = null;
                        break;

                }
            }

             var twilio = new TwilioRestClient("AC7329769855ac2319f51129e29352294c","30b5abfcedeec6ec14586780e880fc88");
             var sms = twilio.SendSmsMessage(sender,From,respuesta);

               return Content(sms.Sid);
         //   ViewBag.resp = respuesta + " Opcion=" + opcion;
          //  ViewBag.leng = respuesta.Length;
           // return View();
        }

        private string estatusSolicitud(string body, string telefono)
        {
            int codigo = 0;
            try
            {
                codigo = Int32.Parse(body);
            }
            catch
            {
                return "Error con el codigo verifique y vuelva a intentarlo";
            }

            var solicitud = db.Solicituds.Find(codigo);
            if (solicitud != null)
            {
                if (db.Contactoes.SingleOrDefault(c => c.Descripcion == telefono) != null)
                {

                    int idUsuario = db.Contactoes.SingleOrDefault(c => c.Descripcion == telefono).IdUserProfile;

                    if (idUsuario != solicitud.IdUserProfile)
                    {
                        return "Usted no tiene solicitudes con este codigo. \nGracias";
                    }

                    return "Estatus Solicitud \n" + ((EstatusSolicitudes)solicitud.EstatusSolicitud).EnumToStr();
                }
                else
                {
                    return "Su numero telefonico No esta registrado. \nGracias";
                }

            }
            else
            {
                return "Error con el codigo verifique y vuelva a intentarlo";
            }

        }

        private string agregarEquipoSolicitud(string body, string telefono)
        {
            int indice1 = 0;
            int indice2 = 0;
            int contador = 0;
            int idSolicitud = 0;
            int idEquipo = 0;
            int cantidad = 0;
            List<Equipo> disponibles = new List<Equipo>();
            Solicitud solicitud = new Solicitud();

            for (int i = 0; i < body.Length; i++)
            {

                if (body.ElementAt(i) == '*' && contador == 0)
                {
                    indice1 = i;
                    contador++;
                }
                else if (contador == 1 && body.ElementAt(i) == '*')
                {
                    indice2 = i;
                }
            }
            try
            {
                idSolicitud = Int32.Parse(body.Substring(0, indice1));
                idEquipo = Int32.Parse(body.Substring(indice1 + 1, indice2 - indice1 - 1));
                cantidad = Int32.Parse(body.Substring(indice2 + 1, body.Length - indice2 - 1));
            }
            catch
            {
                return "Ha ocurido un problema.\nVerifique el formato y vuelva a intentarlo";
            }

            solicitud = db.Solicituds.Find(idSolicitud);
            if (solicitud == null)
            {
                return "Codigo Solicitud invalido. Vuelva a intentarlo.";
            }
            if (solicitud.EstatusSolicitud == 3 || solicitud.EstatusSolicitud == 2 && solicitud.Fecha > DateTime.Now)
            {

                disponibles = EquiposDisponibles(solicitud.Fecha.ToString(), solicitud.HoraInicio.ToString(), solicitud.HoraFin.ToString());

                if (disponibles.Where(e => e.IdModeloEquipo == idEquipo).Count() == 0)
                {
                    return "Equipo no esta disponible.";
                }
                else if (disponibles.Where(e => e.IdModeloEquipo == idEquipo).Count() < cantidad)
                {
                    return "La cantidad seleccionada no esta disponible. \nIntente con una cantidad menor";
                }

                foreach (var item in disponibles)
                {
                    if (cantidad > 0 && item.IdModeloEquipo == idEquipo)
                    {
                        solicitud.Equipoes.Add(item);
                        cantidad--;
                    }
                }
            }
            else
            {
                return "La solicitud ya no puede ser editada";
            }

            db.SaveChanges();
            return "Equipo agregado. gracias";
        }

        private string eliminarEquipoSolicitud(string body, string p)
        {
            int indice1 = 0;
            int indice2 = 0;
            int contador = 0;
            int idSolicitud = 0;
            int idModeloEquipo = 0;
            int cantidad = 0;

            Solicitud solicitud = new Solicitud();

            for (int i = 0; i < body.Length; i++)
            {

                if (body.ElementAt(i) == '*' && contador == 0)
                {
                    indice1 = i;
                    contador++;
                }
                else if (contador == 1 && body.ElementAt(i) == '*')
                {
                    indice2 = i;
                }
            }
            try
            {
                idSolicitud = Int32.Parse(body.Substring(0, indice1));
                idModeloEquipo = Int32.Parse(body.Substring(indice1 + 1, indice2 - indice1 - 1));
                cantidad = Int32.Parse(body.Substring(indice2 + 1, body.Length - indice2 - 1));
            }
            catch
            {
                return "Ha ocurido un problema.\nVerifique el formato y vuelva a intentarlo";
            }

            if (db.Solicituds.SingleOrDefault(s => s.Id == idSolicitud) != null)
            {
                solicitud = db.Solicituds.SingleOrDefault(s => s.Id == idSolicitud);
            }
            else
            {
                return "Codigo Solicitud invalido. Vuelva a intentarlo.";
            }
            if (solicitud.EstatusSolicitud == 3 || solicitud.EstatusSolicitud == 2 && solicitud.Fecha > DateTime.Now)
            {
                List<Equipo> equipos = solicitud.Equipoes.ToList();

                if (equipos.Where(e => e.IdModeloEquipo == idModeloEquipo).Count() == 0)
                {
                    return "No existen equipos con ese codigo.";
                }

                foreach (var equipo in equipos)
                {
                    if (solicitud.Equipoes.Count() == 1)
                    {
                        return "No se pueden eliminar Todos los equipos de la solicitud, de lo contrario sera cancelada.";
                    }
                    if (cantidad > 0 && equipo.IdModeloEquipo == idModeloEquipo)
                    {
                        solicitud.Equipoes.Remove(equipo);
                        cantidad--;
                    }
                }
            }
            else
            {
                return "La solicitud ya no puede ser editada";
            }

            db.SaveChanges();
            return "Equipos eliminados. gracias";
        }



        public string ProcesarSolicitud(string solicitud, string telefono)
        {
            string fecha;
            string horaInicio;
            string horaFin;
            string equiposStr;
            int lugar = 0;
            string nipSMS = null;
            List<ModeloEquipo> modelosDisponibles = new List<ModeloEquipo>();
            List<Equipo> equiposDisponibles = new List<Equipo>();

            //almacena la nueva Solicitud
            Solicitud nuevaSolicitud = new Solicitud();

            //Seleccionando los id de las Solicitudes anteriores para obtener la ultima
            var SolicitudsId = db.Solicituds.Select(s => s.Id).ToList();

            try
            {
                //Validando que el usuario este registrado para proceder con la solicitud
                int codigoUsuario = 0;
                var contacto = db.Contactoes.SingleOrDefault(c => c.Descripcion == telefono);
                if (contacto != null)
                {
                    codigoUsuario = db.Contactoes.SingleOrDefault(c => c.Descripcion == telefono).IdUserProfile;

                    //Validando que el usurario solo pueda Registrar 5 solucitudes maximas por sms cada semestre

                    var solicitudesSMS = from s in contacto.Solicituds
                                         where contacto.IdUserProfile == codigoUsuario && s.Fecha >= DateTime.Now.AddMonths(-3)
                                              && s.Fecha <= DateTime.Now.AddMonths( 3)
                                         select s.Id;

                    if (solicitudesSMS.Count() > 8)
                    {
                        Session["opcion"] = "";
                        Session["spp"] = "";
                        Session["sppOpcion"] = "";
                        return "Usted ha excedido el maximo de solicitud sms por semestre.";
                    }

                }
                else
                {
                    Session["opcion"] = "";
                    Session["spp"] = "";
                    Session["sppOpcion"] = "";
                    return "Su numero No esta registrado como Contacto de algun usuario.\n Registrelo o cree una cuenta. ";
                }




                fecha = solicitud.Substring(2, 10);
                horaInicio = solicitud.Substring(13, 5);
                horaFin = solicitud.Substring(19, 5);
                nipSMS = solicitud.Substring(25, 4);
                equiposStr = solicitud.Substring(30, solicitud.Length - 30);

                //validando que el nipsms enviado sea igual al que esta registrado y pertenece al usuario para confirmar la solicitud
                if (db.UserProfiles.SingleOrDefault(n => n.UserId == codigoUsuario).NipSms != nipSMS)
                {
                    Session["opcion"] = "";
                    Session["spp"] = "";
                    Session["sppOpcion"] = "";
                    return "NipSMS invalido. \nFavor vuelva a intentarlo.\nPuede Solicitarlo via web.";
                }

                try
                {
                    nuevaSolicitud.Fecha = DateTime.Parse(fecha);
                    nuevaSolicitud.HoraInicio = TimeSpan.Parse(horaInicio);
                    nuevaSolicitud.HoraFin = TimeSpan.Parse(horaFin);
                    nuevaSolicitud.Descripcion = "Solicitud SMS";
                    nuevaSolicitud.IdUserProfile = codigoUsuario;
                    nuevaSolicitud.EstatusSolicitud = (int)EstatusSolicitudes.En_Proceso;
                }
                catch
                {
                    Session["opcion"] = "";
                    Session["spp"] = "";
                    Session["sppOpcion"] = "";
                    return "Error en el formato de la hora o la fecha";
                }


                //Obteniendo los equipos disponibles a partir de la fecha horaInicio y horaFin de la solicitudSms
                equiposDisponibles = EquiposDisponibles(fecha, horaInicio, horaInicio);

                if (equiposDisponibles.Count == 0)
                {
                    Session["opcion"] = "";
                    Session["spp"] = "";
                    Session["sppOpcion"] = "";
                    return "No hay equipos disponibles para esta fecha";
                }


                List<Tuple<string, string>> equipos = new List<Tuple<string, string>>();

                int cont = 50;
                int indice = 0;
                int indice2 = 0;

                for (int i = 0; i < equiposStr.Length; i++)
                {
                    //sacando el id del salon para agregarlo a la solicitud
                    if (equiposStr.ElementAt(i) == '*' && cont == 50)
                    {
                        lugar = Int32.Parse(equiposStr.Substring(0, i));
                        equiposStr = equiposStr.Substring(i + 1, equiposStr.Length - i - 1);
                        cont = 0;
                        i = 0;

                        //validando que el salon sea valido
                        if (db.AulaEdificios.SingleOrDefault(a => a.Id == lugar) != null)
                        {
                            nuevaSolicitud.IdAulaEdificio = lugar;
                        }
                        else
                        {
                            Session["opcion"] = "";
                            Session["spp"] = "";
                            Session["sppOpcion"] = "";

                            return "Error con el codigo Lugar.\nVuelva a intentarlo.\nEnviar Lu para saber mas.";
                        }
                    }
                    //sacando los equipos y la cantidad para agregarlos a la solicitud
                    if (equiposStr.ElementAt(i) == '*' && cont == 0)
                    {
                        indice2 = i;
                        cont++;
                    }
                    else if (equiposStr.ElementAt(i) == '*' && cont != 0)
                    {
                        equipos.Add(new Tuple<string, string>(equiposStr.Substring(indice, indice2), equiposStr.Substring((indice2) + 1, i - indice2 - 1)));

                        indice = 0;
                        cont = 0;

                        equiposStr = equiposStr.Substring(i + 1, equiposStr.Length - i - 1);
                        i = 0;

                    }


                }




                //Verificando si el modelo seleccionado esta disponible con relacion a los modelos disponibles por fecha y hora
                foreach (var item in equipos)
                {
                    int cantidadXModelo = equiposDisponibles.Where(e => e.IdModeloEquipo == Int32.Parse(item.Item1)).Count();

                    if (cantidadXModelo == 0)
                    {
                        Session["opcion"] = "";
                        Session["spp"] = "";
                        Session["sppOpcion"] = "";
                        return "Equipo " + item.Item1 + " no esta disponible.\nSolicitud cancelada\n Vuelva a intentarlo";
                    }
                    else if (cantidadXModelo < Int32.Parse(item.Item2))
                    {
                        Session["opcion"] = "";
                        Session["spp"] = "";
                        Session["sppOpcion"] = "";
                        return "Cantidad Equipo " + item.Item1 + " solo hay " + cantidadXModelo + " disponible.\nSolicitud cancelada\n Vuelva a intentarlo";
                    }
                    else
                    {
                        int cantidad = Int32.Parse(item.Item2);
                        int cantidadXModelo2 = equiposDisponibles.Where(e => e.IdModeloEquipo == Int32.Parse(item.Item1)).Count();

                        foreach (var equipo in equiposDisponibles)
                        {
                            if (cantidad > 0 && equipo.IdModeloEquipo == Int32.Parse(item.Item1) && cantidad <= cantidadXModelo2)
                            {
                                
                               //Actualizamos el estatus del equipo para que se refleje su primer uso en caso de que no haya sido utilizado
                                db.Equipoes.SingleOrDefault(e => e.Id == equipo.Id).EstatusEquipo = 1;
                                nuevaSolicitud.Equipoes.Add(equipo);

                                cantidad--;
                            }

                        }
                    }

                }


                //nuevaSolSms.IdContacto = db.Contactoes.SingleOrDefault(c => c.Descripcion == telefono).Id;


                try
                {
                    db.Solicituds.Add(nuevaSolicitud);
                    db.SaveChanges();

                }
                catch (DbEntityValidationException e)
                {
                    return e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage;
                }

            }
            catch (Exception e)
            {
                Session["opcion"] = "";
                Session["spp"] = "";
                Session["sppOpcion"] = "";
                return "Error procesando su solicitud Revise el formato";
            }

            Session["opcion"] = "";
            Session["spp"] = "";
            Session["sppOpcion"] = "";
            return "Solicitud Procesada.\nSu codigo:" +nuevaSolicitud.Id + "\n!Gracias!\n!Favor guardar Codigo!";
        }


        public string getCodigoEquipos(string ce)
        {

            try
            {
                string respuesta = null;
                if (ce == null)
                {
                    respuesta = "\nCodigos Equipos";

                    foreach (var modelo in db.ModeloEquipoes)
                    {

                        respuesta = respuesta + "\n" + modelo.Nombre + "=" + modelo.Id;
                    }


                    return respuesta;
                }
                else
                {

                    int codigoEquipo = Int32.Parse(ce);
                    respuesta = db.ModeloEquipoes.SingleOrDefault(e => e.Id == codigoEquipo).Descripcion;

                    return respuesta;
                }
            }
            catch
            {
                return "Problema al procesar el equipo !verifique Formato instruccion!";
            }
        }

        public string getSalones(string cs)
        {

            try
            {
                string respuesta = null;
                if (cs == null)
                {
                    respuesta = "\nCodigos Salones";

                    foreach (var modelo in db.AulaEdificios)
                    {

                        respuesta = respuesta + "\n" + modelo.Aula + "=" + modelo.Id;
                    }


                    return respuesta;
                }
                else
                {

                    int codigoSalon = Int32.Parse(cs);
                    respuesta = db.AulaEdificios.SingleOrDefault(e => e.Id == codigoSalon).Aula;

                    return respuesta;
                }
            }
            catch
            {
                return "Problema al procesar el Salon !verifique Formato instruccion!";
            }
        }

        public string cancelarSolicitud(string cs, string telefono)
        {
            int codigo;
            int idContacto;

            try
            {

                codigo = Int32.Parse(cs);
            }
            catch
            {
                return "Codigo Solicitud no valido vuelva a intentarlo";
            }

            if (db.Solicituds.SingleOrDefault(s => s.Id == codigo) != null)
            {
                idContacto = db.Solicituds.SingleOrDefault(s => s.Id == codigo).IdUserProfile;
            }
            else
            {
                return "Codigo Solicitud no valido vuelva a intentarlo";
            }

            if (db.Contactoes.SingleOrDefault(c => c.Descripcion == telefono) != null)
            {
                if (idContacto != db.Contactoes.SingleOrDefault(c => c.Descripcion == telefono).IdUserProfile)
                {
                    return "Lo Sentimos usted no tiene Solicitud con ese Codigo.";
                }
                else
                {

                    db.Solicituds.SingleOrDefault(s => s.Id == codigo).EstatusSolicitud = 4;
                    db.SaveChanges();
                }
            }
            else
            {
                return "Lo sentimos. \nEste numero de telefono no pertenece a ningun usuario registrado.";
            }


            return "Solicitud cancelada. \nGracias.";
        }


        //Consultando los equipos disponibles por modelos
        public List<Equipo> EquiposDisponibles(string fecha, string horaInicio, string horaFin)
        {

            DateTime fechaObj = new DateTime();
            fechaObj = DateTime.Parse(fecha);
            fecha = fechaObj.ToString("yyyy-MM-dd");

            //Almacena los equipos que estan disponibles para ser solicitado
            List<Equipo> EquiposDisponibles = new List<Equipo>();

            //Consultando los equipos que podrian estar disponible para la solicitud
            EquiposDisponibles = db.Equipoes.Where(e => e.EstatusEquipo == 5 || e.EstatusEquipo == 1).ToList();

            //Consultando los equipos que no han sido solicitado para la fecha indicada para eliminarlos
            //de la lista de equipos disponibles


            var query = db.Database.SqlQuery<int>("EXEC EquiposNoDisponibles {0},{1},{2}", fecha, horaInicio, horaFin);

            //Eliminando los equipos no disponibles de la lista de equipos disponibles

            foreach (var item in query)
            {

                EquiposDisponibles.Remove(EquiposDisponibles.SingleOrDefault(e => e.Id == item));

            }

            return EquiposDisponibles;
        }




    }

}

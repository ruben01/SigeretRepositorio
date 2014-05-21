using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigeret.Controllers
{
    public class SolicitudSmsController : BaseController
    {
        //
        // GET: /SolicitudSms/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string body, string From)
        {
            string sender = "2766011354";
            if (!string.IsNullOrEmpty(body))
            {
                body = body.ToLower();
            }
            string solicitud = "";
            string respuesta;
            //si la longitud del mensaje enviado es mayor a 2 entonces procedemos a sacar una subcadena para verificar si es una solicitud
            if (body.Length > 2)
            {
                solicitud = body.Substring(0, 2);
            }

            if (solicitud == "**")
            {
                respuesta = ProcesarSolicitud(body);

            }else if(body.Length>3 && body.Substring(0,3)=="de*"){

                respuesta = getCodigoEquipos(body);
            }
            else
            {

                switch (body)
                {
                    case "ayuda":
                        respuesta = "\n1 Nueva Solicitud(Formato)\n2 Equipos\n3 Formato Fecha\n4 Fomato Hora\n5 Confirmar Solicitud\n6 Cancelar Solicitud";
                        break;

                    case "1":

                        respuesta = "\nFormato Solicitud:\n *fecha*horaInicio*horaFin*CodigoEquipo1*cantidad*CodigoEquipo2*cantidad*";
                        break;
                    case "2":

                        respuesta = "Equipos\nCE Codigos Equipos\nDE Descripcion Equipo";
                        break;
                    case "3":

                        respuesta = "\nFormato Fecha\nDia/Mes/año \nEjemplo 24/12/1999";
                        break;
                    case "4":

                        respuesta = "\nFormato Hora\n24H Ejemplo \n07:00  \n20:00 \nhora fin mayor a la hora inicio";
                        break;
                    case "5":
                        respuesta = "Confirmar Solicitud\ncs*codigoSolicitud \nEjmplo cs*001";//Codigo para confirmar la solicitud que sera entregado al usuario
                        //al crear su cuenta y luego cada vez q haga una solicitud cuando pase a entregar el equipo se le entregara este codigo personal
                        break;
                    case "6":

                        respuesta = "\nCancelar Solicitud\n C*codigoSolicitud\nEjemplo C*2301 ";
                        break;
                    case "ce":

                        respuesta = getCodigoEquipos(null);//Funcion para devolver todos lo equipos disponibles por modelos
                        break;
                    case "de":

                        respuesta = "Descripcion Equipo\nde*codigoEquipo para ver la descripcion\nEj: de*001";
                        break;
                    default:
                        respuesta = "\nNo se Reconoce la Instruccion";
                        break;

                }
            }

            // var twilio = new TwilioRestClient("AC7329769855ac2319f51129e29352294c","30b5abfcedeec6ec14586780e880fc88");
            //  var sms = twilio.SendSmsMessage(sender,From,respuesta);

            // return Content(sms.Sid);
            ViewBag.resp = respuesta;
            ViewBag.leng = respuesta.Length;
            return View();
        }





        public string ProcesarSolicitud(string solicitud)
        {
            try
            {
                string fecha;
                string horaInicio;
                string horaFin;
                string equiposStr;


                fecha = solicitud.Substring(2, 10);
                horaInicio = solicitud.Substring(13, 5);
                horaFin = solicitud.Substring(19, 5);
                equiposStr = solicitud.Substring(25, solicitud.Length - 25);


                List<Tuple<string, string>> equipos = new List<Tuple<string, string>>();

                int cont = 0;
                int indice = 0;
                int indice2 = 0;

                for (int i = 0; i < equiposStr.Length; i++)
                {


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

                string eq = null;

                foreach (var item in equipos)
                {
                    eq = eq + " ID:" + item.Item1.ToString() + " Cantidad:" + item.Item2.ToString() + "||";
                }

                return eq;

            }
            catch (Exception e)
            {

                return "Error procesando su solicitud Revise el formato" + solicitud;
            }

        }


        public string getCodigoEquipos(string ce){

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
                    ce = ce.Substring(3, ce.Length - 3);
                    int codigoEquipo = Int32.Parse(ce);
                    respuesta = db.ModeloEquipoes.SingleOrDefault(e => e.Id == codigoEquipo).Descripcion;

                    return respuesta;
                }
            }
            catch (Exception e)
            {
                return "Problema al procesar el equipo !verifique Formato instruccion!";
            }
        }





        //Consultando los equipos disponibles por modelos
        public List<ModeloEquipo> EquiposDisponibles(string fecha, string horaInicio, string horaFin)
        {

            DateTime fechaObj = new DateTime();
            fechaObj = DateTime.Parse(fecha);

            //Almacena los equipos que estan disponibles para ser solicitado
            List<Equipo> EquiposDisponibles = new List<Equipo>();

            //Consultando los equipos que podrian estar disponible para la solicitud
            EquiposDisponibles = db.Equipoes.Where(e => e.IdEstatusEquipo == 5 || e.IdEstatusEquipo == 1).ToList();

            //Consultando los equipos que no han sido solicitado para la fecha indicada para eliminarlos
            //de la lista de equipos disponibles

            var query = db.Database.SqlQuery<int>("EXEC EquiposDisponibles {0},{1},{2}", fecha, horaInicio, horaFin).ToList();

            //Agregando los equipos a la lista de equipos disponibles

            foreach (var item in query)
            {

                EquiposDisponibles.Remove(EquiposDisponibles.SingleOrDefault(e => e.Id == item));

            }
            


            List<ModeloEquipo> modelosDisponibles = new List<ModeloEquipo>();

            foreach (var item in EquiposDisponibles.GroupBy(e => e.IdModelo))
            {
                modelosDisponibles.Add(db.ModeloEquipoes.SingleOrDefault(e => e.Id == item.Key));
            }



            return  modelosDisponibles;
        }

    }
}

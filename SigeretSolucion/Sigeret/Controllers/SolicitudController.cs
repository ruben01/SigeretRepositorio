using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Sigeret.Controllers
{
    public class SolicitudController : Controller
    {

        SigeretDBDataContext sigeretDb = new SigeretDBDataContext();
        
             
        //
        // GET: /Solicitud/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Solicitud/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }



        //
        // GET: /Solicitud/Create

        public ActionResult Nueva()
        {

           
            //seleccionando los equipos disponibles
          var modelosDisponibles  = from e in sigeretDb.Equipo
                                        where e.IdEstatusEquipo==1
                                        group e by e.IdModelo into equipo
                                     select equipo;

          List<ModeloEquipo> equiposDisponibles = new List<ModeloEquipo>();


            foreach( var modelos in modelosDisponibles){



                        equiposDisponibles.Add(sigeretDb.ModeloEquipo.SingleOrDefault(e => e.Id == modelos.Key));
                    
                
            }
          
            ViewBag.Edificio = new SelectList(sigeretDb.Lugar, "Id", "Edificio");
            ViewBag.Salon = new SelectList(sigeretDb.Lugar, "Id", "Descripcion");
            ViewBag.ModeloEquipo = equiposDisponibles;


            ViewBag.stateList = getState();
            ViewBag.cityList = new List<SelectListItem> { };  //blank dropdownlist
            ViewBag.areaList = new List<SelectListItem> { };  //blank no item


            return View();
        }

        //
        // POST: /Solicitud/Create

        [HttpPost]
        public ActionResult Nueva(Solicitud nuevaSolicitud, FormCollection form)
        {
           
            try
            {

                //Seleccionando los id de las solicitudes anteriores para obtener la ultima
                var solicitudId = from s in sigeretDb.Solicitud
                                  select s.Id;

                //variable para verificar que en la solicitud se ha seleccionado al menos un equipo
                bool contieneEquipos=false;

                //lista para almacenar los equipos seleccionados en la solicitud
                List<SolicitudEquipo> listaEquiposSelecionados=new List<SolicitudEquipo>();

                //Verificando si hay equipos seleccionado en la solicitud
                for (int i = 1; i < form.Count; i++ )
                {

                    if (form["chk" + i] != null)
                    {
                        var equipoSelecionado = from e in sigeretDb.Equipo
                                                where (e.IdModelo ==i && e.IdEstatusEquipo == 1)
                                                select e;

                        if (equipoSelecionado.Count() >= int.Parse(form["cant" + i]))
                        {

                            //buscando en la lista de equipos los equipos que estan disponibles
                            foreach (var item in equipoSelecionado)
                            {
                                //Instancia de equipo para almacenar los nuevos equipos
                                SolicitudEquipo nuevoEquipo = new SolicitudEquipo();


                                    nuevoEquipo.idEquipo = item.Id;


                                    //Actualizar estatus equipo selecionado

                                    sigeretDb.Equipo.SingleOrDefault(e => e.Id == item.Id).IdEstatusEquipo = 5;

                                    //Verificando si hay solicitudes Registrada
                                    if (solicitudId.Count() > 0)
                                    {
                                        nuevoEquipo.IdSolicitud = solicitudId.Max()+1;
                                    }
                                    else
                                    {
                                        nuevoEquipo.IdSolicitud = 1;
                                    }

                                    //agregando el equipo seleccionado a la lista de equipos seleccionados

                                    listaEquiposSelecionados.Add(nuevoEquipo);
                                
                               

                            }

                            contieneEquipos = true;

                        }
                        else
                        {

                            contieneEquipos = false;
                        }


                    }

                   /* int f=1;
                   while (f < form.Count)
                   {
                       if (form["chk" + i] == f.ToString())
                       {
                           contieneEquipos = true;
                       }
                       f++;
                   }
                    */
                }

               if(contieneEquipos){

                    //Registrando la nueva solicitud
                    nuevaSolicitud.IdEstatusSolicitud = 3;                
                    nuevaSolicitud.IdUserProfile = WebSecurity.GetUserId(User.Identity.Name);
                    nuevaSolicitud.IdLugar =Int32.Parse( form["Salon"]);
                    nuevaSolicitud.Fecha = DateTime.Today;

                    sigeretDb.Solicitud.InsertOnSubmit(nuevaSolicitud);
                    sigeretDb.SolicitudEquipo.InsertAllOnSubmit(listaEquiposSelecionados);
                    sigeretDb.SubmitChanges();

               }else{

                   ViewBag.Seleccionar = "Debe Seleccionar al menos un equipo!";

                   //seleccionando los equipos disponibles
                   var modelosDisponibles = from e in sigeretDb.Equipo
                                            where e.IdEstatusEquipo == 1
                                            group e by e.IdModelo into equipo
                                            select equipo;

                   List<ModeloEquipo> equiposDisponibles = new List<ModeloEquipo>();


                   foreach (var modelos in modelosDisponibles)
                   {



                       equiposDisponibles.Add(sigeretDb.ModeloEquipo.SingleOrDefault(e => e.Id == modelos.Key));


                   }

                   ViewBag.Edificio = new SelectList(sigeretDb.Lugar, "Id", "Edificio");
                   ViewBag.Salon = new SelectList(sigeretDb.Lugar, "Id", "Descripcion");
                   ViewBag.ModeloEquipo = equiposDisponibles;



                   return View(nuevaSolicitud);
               }




               

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// ReporteSolicitudes Adm
        /// </summary>
        /// <returns></returns>
        public ActionResult ReporteSolicitudesAdm()
        {
           return View(sigeretDb.Solicitud.ToList());
        }

        //
        // GET: /Solicitud/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Solicitud/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /Solicitud/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Solicitud/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public string CantidadDisponible(int Id)
        {

            var cantidadDisponible =( from e in sigeretDb.Equipo
                                     where e.IdEstatusEquipo == 1 && e.IdModelo==Id
                                     select e).Count();

            

    /*        if (cantidadDisponible > 0)
            {
                return "";
            }
            else
            {
                //add model error

                return "";
            }
     */
            return "Disponible";

   
           
        }


        ////////////////////////////////////////////
                    
        ///ajaxjson
        ///


        public JsonResult getStateJson(string selectStateId = null)
        {
            return Json(getState(selectStateId));
        }
        public SelectList getState(string selectStateId = null)
        {
            IEnumerable<SelectListItem> stateList = (from m in sigeretDb.Lugar select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.Edificio, Value = m.Id.ToString() });
            return new SelectList(stateList, "Value", "Text", selectStateId);

        }

        [HttpPost]
        public JsonResult getCityJson(string stateId, string selectCityId = null)
        {
            return Json(getCity(stateId, selectCityId));
        }
        public SelectList getCity(string stateId, string selectCityId = null)
        {
            IEnumerable<SelectListItem> cityList = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(stateId))
            {
                int _stateId = Convert.ToInt32(stateId);
                cityList = (from m in sigeretDb.AulaEdificio where m.IdLugar==int.Parse(stateId) select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.Aula, Value = m.Id.ToString() });
            }
            return new SelectList(cityList, "Value", "Text", selectCityId);

        }

       


        //State management during postback bind again
        [HttpPost]
        public ActionResult postyourad(FormCollection value)
        {
            ViewBag.stateList = getState(value["istateid"]);
            ViewBag.cityList = getCity(value["istateid"], value["icityid"]);
            

            return View();

        }

    }
}

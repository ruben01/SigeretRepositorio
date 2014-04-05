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

            ViewBag.Edificio = getEdificio();
            ViewBag.Salon = new List<SelectListItem> { };
            ViewBag.ModeloEquipo = equiposDisponibles;
            ViewBag.check = new List<String>();
            ViewBag.cantidad = new List<Tuple<String, String>>();
            ViewBag.lugar = new List<Tuple<string, string>>();




            return View();
        }

        //
        // POST: /Solicitud/Create

        [HttpPost]
        public ActionResult Nueva(Solicitud nuevaSolicitud, FormCollection form)
        {
           
            try
            {

                List<String> check = new List<string>();
                List<Tuple<String, String>> cantidad = new List<Tuple<string, string>>();
                 IEnumerable<SelectListItem>edificioList= new List<SelectListItem>();
                IEnumerable<SelectListItem> salonList = new List<SelectListItem>();

                //Seleccionando los id de las solicitudes anteriores para obtener la ultima
                var solicitudId = from s in sigeretDb.Solicitud
                                  select s.Id;

                //variable para verificar que en la solicitud se ha seleccionado al menos un equipo
                bool contieneEquipos=false;
                bool modelStateValido = true;


                //validando que el edificio haya sido seleccionado
                if (form["edificioId"] == null || form["edificioId"]=="")
                {

                       ViewBag.Edificio = getEdificio();

                       ModelState.AddModelError("edificioId", " !Debe Seleccionar el Edificio!");
                }
                else{
                        edificioList = (from l in sigeretDb.Lugar  select l).AsEnumerable().Select(e => new SelectListItem() { Text = e.Edificio, Value = e.Id.ToString() });
                        ViewBag.Edificio = new SelectList(edificioList, "Value", "Text", form["edificioId"]);

                     }

               
                //validando que el salon haya sido seleccionado
                if (form["salonId"] == null || form["salonId"]=="")
                {
                    ModelState.AddModelError("salonId", "  !Debe seleccionar el salon!");
                    modelStateValido = false;
                    ViewBag.Salon = new List<SelectListItem> { }; 
                }
                else
                {
                  
                   salonList = (from a in sigeretDb.AulaEdificio where a.IdLugar == int.Parse(form["edificioId"]) select a).AsEnumerable().Select(a => new SelectListItem() { Text = a.Aula, Value = a.Id.ToString() });

                   ViewBag.Salon = new SelectList(salonList, "Value", "Text", form["salonId"]);
                    ModelState.AddModelError("salonId", " ");
                }

                //lista para almacenar los equipos seleccionados en la solicitud
                List<SolicitudEquipo> listaEquiposSelecionados=new List<SolicitudEquipo>();

                //Verificando si hay equipos seleccionado en la solicitud
                for (int i = 1; i < form.Count; i++ )
                {

                    if (form["chk" + i] != null)
                    {
                        //agregando los check seleccionado a la lista para mantener el modelo en caso de error
                        check.Add("chk"+i);
                        cantidad.Add(new Tuple<string,string>("cant"+i, form["cant" + i]));

                        contieneEquipos = true;

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

                            
                      

                        }else{
                                //Mostramos un mensaje diciendo que la cantidad del equipo seleccionado no esta disponible
                                ViewBag.Seleccionar = "Cantidad "+sigeretDb.ModeloEquipo.SingleOrDefault(e=>e.Id==i).Nombre+" No disponible!";
                                
                                modelStateValido = false;
                                break;
                             }


                    }
                    

                }

               if(modelStateValido&&contieneEquipos){

                    //Registrando la nueva solicitud
                    nuevaSolicitud.IdEstatusSolicitud = 3;                
                    nuevaSolicitud.IdUserProfile = WebSecurity.GetUserId(User.Identity.Name);
                    nuevaSolicitud.IdLugar =Int32.Parse( form["Salon"]);
                    nuevaSolicitud.Fecha = DateTime.Today;

                    sigeretDb.Solicitud.InsertOnSubmit(nuevaSolicitud);
                    sigeretDb.SolicitudEquipo.InsertAllOnSubmit(listaEquiposSelecionados);
                    sigeretDb.SubmitChanges();

               }else{

                   //almacenar check activos por si hay error en el modelo
                   ViewBag.check = check;
                   ViewBag.cantidad = cantidad;

                   if (contieneEquipos==false)
                   {
                       ViewBag.Seleccionar = "Debe seleccionar al menos un equipo!";

                   }

                  
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






        ////////////////////////////////////////////
                    
        ///ajaxjson
        ///


        public JsonResult getLugarJson(string selectEdificioId = null)
        {
            return Json(getEdificio(selectEdificioId));
        }
        public SelectList getEdificio(string selectEdificioId = null)
        {
            IEnumerable<SelectListItem> edificioList = (from m in sigeretDb.Lugar select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.Edificio, Value = m.Id.ToString() });
            return new SelectList(edificioList, "Value", "Text", selectEdificioId);

        }

        [HttpPost]
        public JsonResult getSalonJson(string salonId, string selectSalonId = null)
        {
            return Json(getSalon(salonId, selectSalonId));
        }
        public SelectList getSalon(string salonId, string selectSalonId = null)
        {
            IEnumerable<SelectListItem> salonList = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(salonId))
            {
                int _stateId = Convert.ToInt32(salonId);
                salonList = (from m in sigeretDb.AulaEdificio where m.IdLugar==int.Parse(salonId) select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.Aula, Value = m.Id.ToString() });
            }
            return new SelectList(salonList, "Value", "Text", selectSalonId);

        }

       

        //NO funciona hasta ahora OJOOOOO
        //State management during postback bind again
        [HttpPost]
        public ActionResult postyourad(FormCollection value)
        {
            ViewBag.edificioList = getEdificio(value["edificioId"]);
            ViewBag.salonList = getSalon(value["edificioId"], value["salonId"]);
            

            return View();

        }

    }
}

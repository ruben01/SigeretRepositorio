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

        public ActionResult Detalles(int id)
        {


            List<Equipo> equiposSeleccionados = new List<Equipo>();



            var query = from e in sigeretDb.SolicitudEquipo
                        where e.IdSolicitud == id
                        select e.idEquipo;

            foreach (var equipoId in query)
            {

                equiposSeleccionados.Add(sigeretDb.Equipo.SingleOrDefault(e => e.Id == equipoId));
            }


            



           ViewBag.equiposSeleccionados = equiposSeleccionados;



            return View(sigeretDb.Solicitud.SingleOrDefault(s=>s.Id==id));
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
                if (form["salonId"] == null || form["salonId"]=="" || form["salonId"]=="--Seleccione Salon--")
                {
                    ModelState.AddModelError("salonId", "  !Debe seleccionar el salon!");
                    modelStateValido = false;
                    if (form["edificioId"] != null && form["edificioId"] != "")
                    {
                        salonList = (from a in sigeretDb.AulaEdificio where a.IdLugar == int.Parse(form["edificioId"]) select a).AsEnumerable().Select(a => new SelectListItem() { Text = a.Aula, Value = a.Id.ToString() });
                        ViewBag.Salon = new SelectList(salonList, "Value", "Text");
                    }
                    else
                    {
                        ViewBag.Salon = new List<SelectListItem> { };
                    }
                }
                else
                {
                  
                   salonList = (from a in sigeretDb.AulaEdificio where a.IdLugar == int.Parse(form["edificioId"]) select a).AsEnumerable().Select(a => new SelectListItem() { Text = a.Aula, Value = a.Id.ToString() });

                   ViewBag.Salon = new SelectList(salonList, "Value", "Text", form["salonId"]);
                    
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
                            int cantidadSeleccionada = int.Parse(form["cant" + i]);
                            //buscando en la lista de equipos los equipos que estan disponibles
                            foreach (var item in equipoSelecionado)
                            {
                                //Instancia de equipo para almacenar los nuevos equipos
                                SolicitudEquipo nuevoEquipo = new SolicitudEquipo();


                                    nuevoEquipo.idEquipo = item.Id;



                                    //Verificando si hay solicitudes Registrada
                                    if (solicitudId.Count() > 0)
                                    {
                                        nuevoEquipo.IdSolicitud = solicitudId.Max()+1;
                                    }
                                    else
                                    {
                                        nuevoEquipo.IdSolicitud = 1;
                                    }


                                    if (cantidadSeleccionada > 0)
                                    {
                                        //agregando el equipo seleccionado a la lista de equipos seleccionados

                                        listaEquiposSelecionados.Add(nuevoEquipo);

                                        //Actualizar estatus equipo selecionado

                                        sigeretDb.Equipo.SingleOrDefault(e => e.Id == item.Id).IdEstatusEquipo = 5;
                                        cantidadSeleccionada--;
                                    }
                                
                               

                            }

                            
                      

                        }else{
                                //Mostramos un mensaje diciendo que la cantidad del equipo seleccionado no esta disponible
                                ViewBag.Seleccionar = "Cantidad "+sigeretDb.ModeloEquipo.SingleOrDefault(e=>e.Id==i).Nombre+" No disponible!";
                                
                                modelStateValido = false;
                                break;
                             }


                    }
                    

                }

                var error = ModelState.Values.SelectMany(e => e.Errors);

               if(modelStateValido && contieneEquipos && ModelState.IsValid ){

                    //Registrando la nueva solicitud
                    nuevaSolicitud.IdEstatusSolicitud = 3;                
                    nuevaSolicitud.IdUserProfile = WebSecurity.GetUserId(User.Identity.Name);
                    nuevaSolicitud.IdLugar =Int32.Parse( form["SalonId"]);
                   

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

        public ActionResult Editar(int id)
        {

         //almacena la cantidad y el modelo del equipo seleccionado
            List<Tuple<String, String>> cantidadSel = new List<Tuple<string, string>>();

            var Lugar = from s in sigeretDb.Solicitud
                        join aula in sigeretDb.AulaEdificio on s.IdLugar equals aula.Id
                          join edificio in sigeretDb.Lugar on aula.IdLugar equals edificio.Id
                          where s.Id==id
                          select s;

            int aulaId=0;
            int edificioId=0;

            foreach (var ids in Lugar)
            {
                aulaId = ids.AulaEdificio.Id;
                edificioId = ids.AulaEdificio.IdLugar;

            }

            

            var LugarSeleccionado = sigeretDb.Lugar.SingleOrDefault(l => l.Id ==edificioId);
            ViewBag.edificioId = new SelectList(sigeretDb.Lugar, "Id", "Edificio", LugarSeleccionado.Id);


            var AulaSeleccionada = sigeretDb.AulaEdificio.SingleOrDefault(a => a.Id == aulaId);
            ViewBag.IdAula = new SelectList(sigeretDb.AulaEdificio.Where(x=>x.IdLugar==edificioId), "Id", "Aula", AulaSeleccionada.Id);

            Solicitud editar = sigeretDb.Solicitud.SingleOrDefault(x => x.Id == id);

            //seleccionando los equipos disponibles
            var modelosDisponibles = from mE in sigeretDb.ModeloEquipo
                                        join equipo in sigeretDb.Equipo on mE.Id equals equipo.IdModelo
                                        join solEquipo in sigeretDb.SolicitudEquipo on equipo.Id equals solEquipo.idEquipo
                                        join solicitud in sigeretDb.Solicitud on solEquipo.IdSolicitud equals solicitud.Id
                                                                             
                                     where solicitud.Id==id || equipo.IdEstatusEquipo==1
                                     group mE by mE.Id into equipo
                                     select equipo;




            List<ModeloEquipo> equiposDisponibles = new List<ModeloEquipo>();

            

            foreach (var modelos in modelosDisponibles)
            {
                
                equiposDisponibles.Add(sigeretDb.ModeloEquipo.SingleOrDefault(e => e.Id == modelos.Key));
                
            }




            //consultando para obtener la cantidad de equipos seleccionados por modelos
            var cantidadXModelo= from se in sigeretDb.SolicitudEquipo 
                                            join solEquipo in sigeretDb.Solicitud on se.IdSolicitud equals solEquipo.Id
                                            join equipo in sigeretDb.Equipo on se.idEquipo equals equipo.Id
                                            join modelo in sigeretDb.ModeloEquipo on equipo.IdModelo equals modelo.Id

                                            where solEquipo.Id==id

                                            group equipo by equipo.IdModelo into modelosSeleccionados

                                            select modelosSeleccionados;
            foreach (var modelo in cantidadXModelo)
            {

                
                
               cantidadSel.Add(new Tuple<string, string>(modelo.Key.ToString(), modelo.Count().ToString()));
            }
                                           

                                            



            ViewBag.ModeloEquipo = equiposDisponibles;
            ViewBag.cantidadSel = cantidadSel;
            

            return View(editar);
        }

        

        [HttpPost]
        public ActionResult Editar(int id, Solicitud editada,FormCollection form)
        {
            try
            {
                //almacena la cantidad y el modelo del equipo seleccionado
                List<Tuple<String, String>> cantidadSel = new List<Tuple<string, string>>();

                IEnumerable<SelectListItem> salonList = new List<SelectListItem>();

                bool contieneEquipo = false;

                var solicitudBD = sigeretDb.Solicitud.SingleOrDefault(s => s.Id == id);

                solicitudBD.Descripcion = editada.Descripcion;
                solicitudBD.HoraInicio = editada.HoraInicio;
                solicitudBD.HoraFin = editada.HoraFin;
                solicitudBD.Fecha = editada.Fecha;

                if(form["IdAula"]==null || form["IdAula"]==""|| form["IdAula"]=="--Seleccione Salon--"){

                    ModelState.AddModelError("IdAula", "Debe Seleccionar el Salon");

                    salonList = (from a in sigeretDb.AulaEdificio where a.IdLugar == int.Parse(form["edificioId"]) select a).AsEnumerable().Select(a => new SelectListItem() { Text = a.Aula, Value = a.Id.ToString() });
                    ViewBag.IdAula = new SelectList(salonList, "Value", "Text");
                    
                }
                else
                {

                    solicitudBD.IdLugar = Int32.Parse(form["IdAula"]);
                    salonList = (from a in sigeretDb.AulaEdificio where a.IdLugar == int.Parse(form["edificioId"]) select a).AsEnumerable().Select(a => new SelectListItem() { Text = a.Aula, Value = a.Id.ToString() });
                    ViewBag.IdAula = new SelectList(salonList, "Value", "Text",form["IdAula"]);


                }
                //lista para almacenar los equipos seleccionados en la solicitud
                List<SolicitudEquipo> listaEquiposSelecionados = new List<SolicitudEquipo>();

                //lista de equipos ya preseleccionados cuando se creo la solicitud
                List<SolicitudEquipo>listaEquiposPreseleccionados = new List<SolicitudEquipo>();

                listaEquiposPreseleccionados=sigeretDb.SolicitudEquipo.Where(e=>e.IdSolicitud==id).ToList();

                //Verificando si hay equipos seleccionado en la solicitud
                for (int i = 1; i < form.Count; i++)
                {
                    //Verificando si hay modelos de equipos eliminados
                    if (form["hChk" + i] != null)
                    {
                        if (form["chk"+i] == null)
                        {
                            //eliminamos los equipos con el modelo especifico
                            List<SolicitudEquipo> equiposEliminadosXmodelo = new List<SolicitudEquipo>();
                            equiposEliminadosXmodelo = sigeretDb.SolicitudEquipo.Where(e => e.IdSolicitud == id && e.Equipo.IdModelo == Int32.Parse(form["hChk" + i])).ToList();

                            foreach (var equipo in equiposEliminadosXmodelo)
                            {
                                sigeretDb.Equipo.SingleOrDefault(e => e.Id == equipo.idEquipo).IdEstatusEquipo = 1;
                            }

                            sigeretDb.SolicitudEquipo.DeleteAllOnSubmit(equiposEliminadosXmodelo);
                        }
                    }

                    if (form["chk" + i] != null)
                    {
                        //agregando los check seleccionado a la lista para mantener el modelo en caso de error

                        cantidadSel.Add(new Tuple<string, string>(i.ToString(), form["cant" + i]));

                        contieneEquipo = true;


                        //Equipos disponibles por modelos
                        var equiposDispXmodelos = from e in sigeretDb.Equipo
                                                  join solicitudEquipo in sigeretDb.SolicitudEquipo
                                                  on e.Id equals solicitudEquipo.idEquipo
                                                  join solicitud in sigeretDb.Solicitud
                                                  on solicitudEquipo.IdSolicitud equals solicitud.Id
                                                  where solicitud.Id == id && e.IdEstatusEquipo == 1//ojo aquiiiiiiiiiiiiiiiiiiii******/*/*/******
                                                        || e.IdModelo == Int32.Parse(form["chk" + i]) && solicitud.Id == id

                                                  select e;
                        //Equipos x modelo previamente seleccionados al momento de crear la solicitud

                        var equiposXmodelosPrevios = from e in sigeretDb.Equipo
                                                     join solicitudEquipo in sigeretDb.SolicitudEquipo
                                                     on e.Id equals solicitudEquipo.idEquipo
                                                     where e.IdModelo == Int32.Parse(form["chk" + i]) && solicitudEquipo.IdSolicitud == id
                                                     select e;

                        
                            String idModelo = form["chk" + i];
                            String cant = form["cant" + i];
                            int cants = equiposXmodelosPrevios.Count();
                            int cantdisponible = equiposDispXmodelos.Count();
                            //verificamos si la cantidad selecionada al momento de crear la solicitud
                            //es mayor que la cantidad seleccionada al momento de editar la solicitud
                            //para proceder a reducir los equipos previamente seleccionados
                            if (equiposXmodelosPrevios.Count() > Int32.Parse(form["cant" + i]))
                            {
                                List<SolicitudEquipo> equiposAEliminar = new List<SolicitudEquipo>();

                                int cantidadEliminar = equiposXmodelosPrevios.Count() - Int32.Parse(form["cant" + i]);

                                foreach (var equipo in equiposXmodelosPrevios)
                                {

                                    if (cantidadEliminar > 0)
                                    {
                                        //ojojojojojojojojojooojoooo
                                        equiposAEliminar.Add(sigeretDb.SolicitudEquipo.SingleOrDefault(e => e.idEquipo == equipo.Id && e.IdSolicitud==id));
                                        sigeretDb.Equipo.SingleOrDefault(e => e.Id == equipo.Id).IdEstatusEquipo = 1;
                                        cantidadEliminar--;
                                    }
                                }


                                sigeretDb.SolicitudEquipo.DeleteAllOnSubmit(equiposAEliminar);
                            }
                            else if (equiposXmodelosPrevios.Count() < Int32.Parse(form["cant" + i]) && equiposDispXmodelos.Count() >= Int32.Parse(form["cant" + i]) - equiposXmodelosPrevios.Count())
                            {
                                //Seleccionamos lod equipos disponibles para agregar a la solicitud
                                var equipoSelecionado = from e in sigeretDb.Equipo
                                                        where (e.IdModelo == i && e.IdEstatusEquipo == 1)
                                                        select e;

                                int cantidadSeleccionada = int.Parse(form["cant" + i]) - equiposXmodelosPrevios.Count();

                                //agregando los nuevos equipos
                                foreach (var item in equipoSelecionado)
                                {



                                    if (cantidadSeleccionada > 0)
                                    {
                                        //Instancia de equipo para almacenar los nuevos equipos
                                        SolicitudEquipo nuevoEquipo = new SolicitudEquipo();

                                        nuevoEquipo.idEquipo = item.Id;
                                        nuevoEquipo.IdSolicitud = id;
                                        //agregando el equipo seleccionado a la lista de equipos seleccionados
                                        listaEquiposSelecionados.Add(nuevoEquipo);

                                        //Actualizar estatus equipo selecionado

                                        sigeretDb.Equipo.SingleOrDefault(e => e.Id == item.Id).IdEstatusEquipo = 5;
                                        cantidadSeleccionada--;
                                    }



                                }
                                sigeretDb.SolicitudEquipo.InsertAllOnSubmit(listaEquiposSelecionados);



                            }
                            else if(equiposDispXmodelos.Count()<Int32.Parse(form["cant"+i]))
                            {
                                //Mostramos un mensaje diciendo que la cantidad del equipo seleccionado no esta disponible
                                ModelState.AddModelError("", "Cantidad de " + sigeretDb.ModeloEquipo.SingleOrDefault(e => e.Id == i).Nombre + " no disponible");

                            }
                            else
                            {

                            }

                        

                    }
                }

                if (!contieneEquipo)
                {
                    ViewBag.Seleccionar = "Debe seleccionar al menos un equipo";
                    ModelState.AddModelError("", "");
                }

        if (ModelState.IsValid)
        {

            sigeretDb.SubmitChanges();
            return RedirectToAction("Detalles", new { Id = editada.Id });
        }
        else
        {


            ViewBag.edificioId = new SelectList(sigeretDb.Lugar, "Id", "Edificio", form["edificioId"]);


            //seleccionando los equipos disponibles
            var modelosDisponibles = from mE in sigeretDb.ModeloEquipo
                                     join equipo in sigeretDb.Equipo on mE.Id equals equipo.IdModelo
                                     join solEquipo in sigeretDb.SolicitudEquipo on equipo.Id equals solEquipo.idEquipo
                                     join solicitud in sigeretDb.Solicitud on solEquipo.IdSolicitud equals solicitud.Id

                                     where solicitud.Id == id || equipo.IdEstatusEquipo == 1
                                     group mE by mE.Id into equipo
                                     select equipo;




            List<ModeloEquipo> equiposDisponibles = new List<ModeloEquipo>();



            foreach (var modelos in modelosDisponibles)
            {

                equiposDisponibles.Add(sigeretDb.ModeloEquipo.SingleOrDefault(e => e.Id == modelos.Key));

            }
            

            ViewBag.ModeloEquipo = equiposDisponibles;
            ViewBag.cantidadSel = cantidadSel;
            

            return View(editada);
        }

                
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

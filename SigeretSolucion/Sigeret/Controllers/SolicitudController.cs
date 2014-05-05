using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using SIGERET.CustomCode;


namespace Sigeret.Controllers
{
    public class SolicitudController : BaseController
    {

        //
        // GET: /Solicituds/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Solicituds/Details/5

        public ActionResult Detalles(int id)
        {


            List<Equipo> equiposSeleccionados = new List<Equipo>();

            var solicitados = db.SolicitudEquipoes.Where(se => se.IdSolicitud == id)
                .Select(se => se.idEquipo).ToList();

            var query = from e in db.SolicitudEquipoes
                        where e.IdSolicitud == id
                        select e.idEquipo;

            foreach (var equipoId in solicitados)
            {

                equiposSeleccionados.Add(db.Equipoes.FirstOrDefault(e => e.Id == equipoId));
            }

            ViewBag.equiposSeleccionados = equiposSeleccionados;

            return View(db.Solicituds.FirstOrDefault(s => s.Id == id));
        }

        //
        // GET: /Solicituds/Create

        public ActionResult Nueva()
        {
            //seleccionando los equipos disponibles
            var disponibles = db.Equipoes.Where(e => e.IdEstatusEquipo == 1)
                .GroupBy(e => e.IdModelo).ToList();

            List<ModeloEquipo> equiposDisponibles = new List<ModeloEquipo>();


            foreach (var modelos in disponibles)
            {
                equiposDisponibles.Add(db.ModeloEquipoes.SingleOrDefault(e => e.Id == modelos.Key));
            }

            ViewBag.Edificio = getEdificio();
            ViewBag.Salon = new List<SelectListItem> { };
            ViewBag.ModeloEquipo = equiposDisponibles;
            ViewBag.check = new List<String>();
            ViewBag.cantidad = new List<Tuple<String, String>>();
            ViewBag.Lugars = new List<Tuple<string, string>>();

            return View();
        }

        //
        // POST: /Solicituds/Create

        [HttpPost]
        public ActionResult Nueva(Solicitud nuevaSolicituds, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                List<String> check = new List<string>();
                List<Tuple<String, String>> cantidad = new List<Tuple<string, string>>();
                IEnumerable<SelectListItem> edificioList = new List<SelectListItem>();
                IEnumerable<SelectListItem> salonList = new List<SelectListItem>();

                int edificioId = int.Parse(form["edificioId"]);

                //Seleccionando los id de las Solicitudses anteriores para obtener la ultima
                var SolicitudsId = db.Solicituds.Select(s => s.Id).ToList();

                //variable para verificar que en la Solicituds se ha seleccionado al menos un equipo
                bool contieneEquipos = false;
                bool modelStateValido = true;

                //validando que el edificio haya sido seleccionado
                if (form["edificioId"] == null || form["edificioId"] == "")
                {
                    ViewBag.Edificio = getEdificio();
                    ModelState.AddModelError("edificioId", " !Debe Seleccionar el Edificio!");
                }
                else
                {
                    edificioList = db.Lugars.ToList().ToSelectListItems(e => e.Edificio, e => e.Id.ToString());
                    ViewBag.Edificio = new SelectList(edificioList, "Value", "Text", form["edificioId"]);
                }

                //validando que el salon haya sido seleccionado
                if (form["salonId"] == null || form["salonId"] == "" || form["salonId"] == "--Seleccione Salon--")
                {
                    ModelState.AddModelError("salonId", "  !Debe seleccionar el salon!");
                    modelStateValido = false;
                    if (form["edificioId"] != null && form["edificioId"] != "")
                    {
                        salonList = db.AulaEdificios
                            .Where(a => a.IdLugar == edificioId).ToList()
                            .ToSelectListItems(a => a.Aula, a => a.Id.ToString());
                        ViewBag.Salon = new SelectList(salonList, "Value", "Text");
                    }
                    else
                    {
                        ViewBag.Salon = new List<SelectListItem> { };
                    }
                }
                else
                {
                    salonList = db.AulaEdificios.Where(a => a.IdLugar == edificioId)
                        .ToList().ToSelectListItems(a => a.Aula, a => a.Id.ToString());
                    ViewBag.Salon = new SelectList(salonList, "Value", "Text", form["salonId"]);

                }

                //lista para almacenar los equipos seleccionados en la Solicituds
                List<SolicitudEquipo> listaEquiposSelecionados = new List<SolicitudEquipo>();

                //Verificando si hay equipos seleccionado en la Solicituds
                for (int i = 1; i < form.Count; i++)
                {

                    if (form["chk" + i] != null)
                    {
                        //agregando los check seleccionado a la lista para mantener el modelo en caso de error
                        check.Add("chk" + i);
                        cantidad.Add(new Tuple<string, string>("cant" + i, form["cant" + i]));
                        contieneEquipos = true;
                        var equipoSelecionado = db.Equipoes
                            .Where(e => e.IdModelo == i && e.IdEstatusEquipo == 1)
                            .ToList();

                        if (equipoSelecionado.Count() >= int.Parse(form["cant" + i]))
                        {
                            int cantidadSeleccionada = int.Parse(form["cant" + i]);
                            //buscando en la lista de equipos los equipos que estan disponibles
                            foreach (var item in equipoSelecionado)
                            {
                                //Instancia de equipo para almacenar los nuevos equipos
                                SolicitudEquipo nuevoEquipo = new SolicitudEquipo();
                                nuevoEquipo.idEquipo = item.Id;

                                //Verificando si hay Solicitudses Registrada
                                if (SolicitudsId.Count() > 0)
                                {
                                    nuevoEquipo.IdSolicitud = SolicitudsId.Max() + 1;
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
                                    db.Equipoes.SingleOrDefault(e => e.Id == item.Id).IdEstatusEquipo = 5;
                                    cantidadSeleccionada--;
                                }
                            }
                        }
                        else
                        {
                            //Mostramos un mensaje diciendo que la cantidad del equipo seleccionado no esta disponible
                            ViewBag.Seleccionar = "Cantidad " + db.ModeloEquipoes.FirstOrDefault(e => e.Id == i).Nombre + " No disponible!";
                            modelStateValido = false;
                            break;
                        }
                    }
                }

                var error = ModelState.Values.SelectMany(e => e.Errors);
                if (modelStateValido && contieneEquipos && ModelState.IsValid)
                {
                    //Registrando la nueva Solicituds
                    nuevaSolicituds.IdEstatusSolicitud = 3;
                    nuevaSolicituds.IdUserProfile = WebSecurity.GetUserId(User.Identity.Name);
                    nuevaSolicituds.IdLugar = Int32.Parse(form["SalonId"]);
                    db.Solicituds.Add(nuevaSolicituds);
                    foreach (var item in listaEquiposSelecionados)
                    {
                        db.SolicitudEquipoes.Add(item);
                    }
                    db.SaveChanges();
                }
                else
                {
                    //almacenar check activos por si hay error en el modelo
                    ViewBag.check = check;
                    ViewBag.cantidad = cantidad;

                    if (contieneEquipos == false)
                    {
                        ViewBag.Seleccionar = "Debe seleccionar al menos un equipo!";
                    }

                    //seleccionando los equipos disponibles
                    var modelosDisponibles = db.Equipoes.Where(e => e.IdEstatusEquipo == 1)
                        .GroupBy(e => e.IdModelo).ToList();

                    List<ModeloEquipo> equiposDisponibles = new List<ModeloEquipo>();
                    foreach (var modelos in modelosDisponibles)
                    {
                        equiposDisponibles.Add(db.ModeloEquipoes.SingleOrDefault(e => e.Id == modelos.Key));
                    }
                    ViewBag.ModeloEquipo = equiposDisponibles;

                    return View(nuevaSolicituds);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(nuevaSolicituds);
            }
        }

        /// <summary>
        /// ReporteSolicitudses Adm
        /// </summary>
        /// <returns></returns>
        public ActionResult ReporteSolicitudesAdm()
        {
            return View(db.Solicituds.ToList());
        }

        //
        // GET: /Solicituds/Edit/5

        public ActionResult Editar(int id)
        {

            //almacena la cantidad y el modelo del equipo seleccionado
            List<Tuple<String, String>> cantidadSel = new List<Tuple<string, string>>();

            var aulaEdificio = from s in db.Solicituds
                        join aula in db.AulaEdificios on s.IdLugar equals aula.Id
                               where s.Id == id
                        select s.AulaEdificio;

            int aulaId = 0;
            int edificioId = 0;

            foreach (var aula in aulaEdificio)
            {
                aulaId = aula.Id;
                edificioId = aula.IdLugar;

            }

            
           ViewBag.edificioId = new SelectList(db.Lugars, "Id", "Edificio", edificioId);


           
            ViewBag.IdAula = new SelectList(db.AulaEdificios.Where(x => x.IdLugar == edificioId), "Id", "Aula",aulaId);

            Solicitud editar = db.Solicituds.SingleOrDefault(x => x.Id == id);

            //seleccionando los equipos disponibles
            var modelosDisponibles = from mE in db.ModeloEquipoes
                                     join equipo in db.Equipoes on mE.Id equals equipo.IdModelo
                                     join solEquipo in db.SolicitudEquipoes on equipo.Id equals solEquipo.idEquipo
                                     join Solicituds in db.Solicituds on solEquipo.IdSolicitud equals Solicituds.Id

                                     where Solicituds.Id == id || equipo.IdEstatusEquipo == 1
                                     group mE by mE.Id into equipo
                                     select equipo;




            List<ModeloEquipo> equiposDisponibles = new List<ModeloEquipo>();

     
               
            foreach (var modelos in modelosDisponibles)
            {

                equiposDisponibles.Add(db.ModeloEquipoes.SingleOrDefault(e => e.Id == modelos.Key));

            }
            




            //consultando para obtener la cantidad de equipos seleccionados por modelos
            var cantidadXModelo = from se in db.SolicitudEquipoes
                                  join solEquipo in db.Solicituds on se.IdSolicitud equals solEquipo.Id
                                  join equipo in db.Equipoes on se.idEquipo equals equipo.Id
                                  join modelo in db.ModeloEquipoes on equipo.IdModelo equals modelo.Id

                                  where solEquipo.Id == id

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
        public ActionResult Editar(int id, Solicitud editada, FormCollection form)
        {
            try
            {
                //almacena la cantidad y el modelo del equipo seleccionado
                List<Tuple<String, String>> cantidadSel = new List<Tuple<string, string>>();

                IEnumerable<SelectListItem> salonList = new List<SelectListItem>();
                int edificioId = int.Parse(form["edificioId"]);

                bool contieneEquipo = false;

                var SolicitudsBD = db.Solicituds.SingleOrDefault(s => s.Id == id);

                SolicitudsBD.Descripcion = editada.Descripcion;
                SolicitudsBD.HoraInicio = editada.HoraInicio;
                SolicitudsBD.HoraFin = editada.HoraFin;
                SolicitudsBD.Fecha = editada.Fecha;

                if (form["IdAula"] == null || form["IdAula"] == "" || form["IdAula"] == "--Seleccione Salon--")
                {

                    ModelState.AddModelError("IdAula", "Debe Seleccionar el Salon");

                    salonList = (from a in db.AulaEdificios where a.IdLugar == edificioId select a).AsEnumerable().Select(a => new SelectListItem() { Text = a.Aula, Value = a.Id.ToString() });
                    ViewBag.IdAula = new SelectList(salonList, "Value", "Text");

                }
                else
                {

                    SolicitudsBD.IdLugar = Int32.Parse(form["IdAula"]);
                    salonList = (from a in db.AulaEdificios where a.IdLugar == edificioId select a).AsEnumerable().Select(a => new SelectListItem() { Text = a.Aula, Value = a.Id.ToString() });
                    ViewBag.IdAula = new SelectList(salonList, "Value", "Text", form["IdAula"]);


                }
                //lista para almacenar los equipos seleccionados en la Solicituds
                List<SolicitudEquipo> listaEquiposSelecionados = new List<SolicitudEquipo>();

                //lista de equipos ya preseleccionados cuando se creo la Solicituds
                List<SolicitudEquipo> listaEquiposPreseleccionados = new List<SolicitudEquipo>();

                listaEquiposPreseleccionados = db.SolicitudEquipoes.Where(e => e.IdSolicitud == id).ToList();

                //Verificando si hay equipos seleccionado en la Solicituds
                for (int i = 1; i < form.Count; i++)
                {
                    //Verificando si hay modelos de equipos eliminados
                    if (form["hChk" + i] != null)
                    {
                        if (form["chk" + i] == null)
                        {
                            //eliminamos los equipos con el modelo especifico
                            List<SolicitudEquipo> equiposEliminadosXmodelo = new List<SolicitudEquipo>();
                            equiposEliminadosXmodelo = db.SolicitudEquipoes.Where(e => e.IdSolicitud == id && e.Equipo.IdModelo == Int32.Parse(form["hChk" + i])).ToList();

                            foreach (var equipo in equiposEliminadosXmodelo)
                            {
                                db.Equipoes.SingleOrDefault(e => e.Id == equipo.idEquipo).IdEstatusEquipo = 1;
                                db.SolicitudEquipoes.Remove(equipo);
                            }

                            
                        }
                    }

                    if (form["chk" + i] != null)
                    {
                        int idModelo = Int32.Parse( form["chk" + i]);
                        int cantidad = Int32.Parse(form["cant" + i]);
                        //agregando los check seleccionado a la lista para mantener el modelo en caso de error

                        cantidadSel.Add(new Tuple<string, string>(i.ToString(), form["cant" + i]));

                        contieneEquipo = true;

                        var ques = from e in db.Equipoes select e;

                        int ke = ques.Count();
                        //Equipos disponibles por modelos
                        var equiposDispXmodelos = from e in db.Equipoes
                                                  join solicitudEquipo in db.SolicitudEquipoes
                                                  on e.Id equals solicitudEquipo.idEquipo
                                                  join Solicituds in db.Solicituds
                                                  on solicitudEquipo.IdSolicitud equals Solicituds.Id
                                                  where Solicituds.Id == id && e.IdEstatusEquipo == 1//ojo aquiiiiiiiiiiiiiiiiiiii******/*/*/******
                                                        || e.IdModelo == idModelo && Solicituds.Id == id

                                                  select e;
                        //Equipos x modelo previamente seleccionados al momento de crear la Solicituds

                        var equiposXmodelosPrevios = from e in db.Equipoes
                                                     join solicitudEquipo in db.SolicitudEquipoes
                                                     on e.Id equals solicitudEquipo.idEquipo
                                                     where e.IdModelo == idModelo && solicitudEquipo.IdSolicitud == id
                                                     select e;



                       
                        //verificamos si la cantidad selecionada al momento de crear la Solicituds
                        //es mayor que la cantidad seleccionada al momento de editar la Solicituds
                        //para proceder a reducir los equipos previamente seleccionados
                        if (equiposXmodelosPrevios.Count() > cantidad)
                        {
                            List<SolicitudEquipo> equiposAEliminar = new List<SolicitudEquipo>();

                            int cantidadEliminar = equiposXmodelosPrevios.Count() - cantidad;

                            foreach (var equipo in equiposXmodelosPrevios)
                            {

                                if (cantidadEliminar > 0)
                                {
                                    
                                    equiposAEliminar.Add(db.SolicitudEquipoes.SingleOrDefault(e => e.idEquipo == equipo.Id && e.IdSolicitud == id));
                                    db.Equipoes.SingleOrDefault(e => e.Id == equipo.Id).IdEstatusEquipo = 1;
                                    cantidadEliminar--;
                                }
                            }

                            foreach (var equipoAEliminar in equiposAEliminar)
                            {
                                db.SolicitudEquipoes.Remove(equipoAEliminar);
                            }
                        }
                        else if (equiposXmodelosPrevios.Count() < cantidad && equiposDispXmodelos.Count() >= cantidad - equiposXmodelosPrevios.Count())
                        {
                            //Seleccionamos lod equipos disponibles para agregar a la Solicituds
                            var equipoSelecionado = from e in db.Equipoes
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

                                    db.Equipoes.SingleOrDefault(e => e.Id == item.Id).IdEstatusEquipo = 5;
                                    cantidadSeleccionada--;
                                }



                            }
                            foreach (var equipoSolicitud in listaEquiposSelecionados)
                            {
                                db.SolicitudEquipoes.Add(equipoSolicitud);

                            }



                        }
                        else if (equiposDispXmodelos.Count() < Int32.Parse(form["cant" + i]))
                        {
                            //Mostramos un mensaje diciendo que la cantidad del equipo seleccionado no esta disponible
                            ModelState.AddModelError("", "Cantidad de " + db.ModeloEquipoes.SingleOrDefault(e => e.Id == i).Nombre + " no disponible");

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

                    db.SaveChanges();
                    return RedirectToAction("Detalles", new { Id = editada.Id });
                }
                else
                {


                    ViewBag.edificioId = new SelectList(db.Lugars, "Id", "Edificio", form["edificioId"]);


                    //seleccionando los equipos disponibles
                    var modelosDisponibles = from mE in db.ModeloEquipoes
                                             join equipo in db.Equipoes on mE.Id equals equipo.IdModelo
                                             join solEquipo in db.SolicitudEquipoes on equipo.Id equals solEquipo.idEquipo
                                             join Solicituds in db.Solicituds on solEquipo.IdSolicitud equals Solicituds.Id

                                             where Solicituds.Id == id || equipo.IdEstatusEquipo == 1
                                             group mE by mE.Id into equipo
                                             select equipo;




                    List<ModeloEquipo> equiposDisponibles = new List<ModeloEquipo>();



                    foreach (var modelos in modelosDisponibles)
                    {

                        equiposDisponibles.Add(db.ModeloEquipoes.SingleOrDefault(e => e.Id == modelos.Key));

                    }


                    ViewBag.ModeloEquipo = equiposDisponibles;
                    ViewBag.cantidadSel = cantidadSel;


                    return View(editada);
                }


            }
            catch(Exception e)
            {
                return View(e);
            }
        }




        //
        // GET: /Solicituds/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Solicituds/Delete/5

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

        public JsonResult getLugarsJson(string selectEdificioId = null)
        {
            return Json(getEdificio(selectEdificioId));
        }

        public SelectList getEdificio(string selectEdificioId = null)
        {
            IEnumerable<SelectListItem> edificioList = db.Lugars.ToList().ToSelectListItems(l => l.Edificio, l => l.Id.ToString());

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
                int _salonId = int.Parse(salonId);
                salonList = db.AulaEdificios.Where(a => a.IdLugar == _salonId).ToList().ToSelectListItems(e => e.Aula, e => e.Id.ToString()); 
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

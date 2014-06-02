using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Sigeret.CustomCode;
using System.Data.SqlClient;
using System.Data;
using Sigeret.Models.ViewModels;
using Sigeret.Models.ModelExtensions;

namespace Sigeret.Controllers
{
    [EsController("Solicitudes", "AE00")]
    public class SolicitudController : BaseController
    {

        //
        // GET: /Solicituds/
        [Vista("Pagina Principal", "AEA01")]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Solicituds/Details/5
        [Vista("Ver Detalles", "AEA02")]
        public ActionResult Detalles(int id)
        {
            List<Equipo> equiposSeleccionados = db.Solicituds.Find(id).Equipoes.ToList();
            ViewBag.equiposSeleccionados = equiposSeleccionados;

            return View(db.Solicituds.FirstOrDefault(s => s.Id == id));
        }

        //
        // GET: /Solicituds/Create
        [Vista("Nueva Solicitud", "AEA03")]
        public ActionResult Nueva()
        {
            ViewBag.EdificioID = getEdificio();
            ViewBag.SalonID = new List<SelectListItem> { };
            ViewBag.Lugars = new List<Tuple<string, string>>();
            ViewBag.Mensaje = "Seleccionar fecha";
            return View();
        }

        //Consultando los equipos disponibles
        //[HttpPost]
        public ActionResult EquiposDisponibles(string fecha, string horaInicio, string horaFin, List<ModeloEquipoItem> modelos)
        {

            DateTime fechaObj = new DateTime();
            fechaObj = DateTime.Parse(fecha);

            //Consultando los equipos que podrian estar disponible para la solicitud
            var EquiposDisponibles = db.Equipoes.Where(e => e.EstatusEquipo == 5 || e.EstatusEquipo == 1).ToList();

            //Consultando los equipos que no han sido solicitado para la fecha indicada para eliminarlos
            //de la lista de equipos disponibles

            var query = db.Database.SqlQuery<int>("EXEC EquiposNoDisponibles {0},{1},{2}", fecha, horaInicio, horaFin).ToList();

            //Agregando los equipos a la lista de equipos disponibles

            foreach (var item in query)
            {
                EquiposDisponibles.Remove(EquiposDisponibles.SingleOrDefault(e => e.Id == item));
            }

            List<ModeloEquipoItem> model = new List<ModeloEquipoItem>();

            foreach (var item in EquiposDisponibles.GroupBy(e => e.IdModeloEquipo))
            {
                model.Add(GlobalHelpers.Transfer<ModeloEquipo, ModeloEquipoItem>(db.ModeloEquipoes.Find(item.Key), null));
                //modelosDisponibles.Add(db.Equipoes.SingleOrDefault(e => e.Id == item.Key));
            }

            if (modelos != null)
                foreach (var item in modelos)
                {
                    model.ForEach(m =>
                    {
                        if (m.Id == item.Id)
                        {
                            m.IsSelected = item.IsSelected;
                            m.Cantidad = item.Cantidad;
                        }
                    });
                }

            return PartialView("PartialModeloEquipo", model);
        }

        //
        // POST: /Solicituds/Create

        [HttpPost]
        public ActionResult Nueva(SolicitudViewModel model)
        {
            if (ModelState.IsValid)
            {
                //verificar que la cantidad de modelos seleccionados esta disponible.
                var datosValidos = true;
                var EquiposDisponibles = db.Equipoes.Where(e => e.EstatusEquipo == 5 || e.EstatusEquipo == 1).ToList();
                var query = db.Database
                    .SqlQuery<int>("EXEC EquiposNoDisponibles {0},{1},{2}", model.Fecha, model.HoraInicio, model.HoraFin)
                    .ToList();
                foreach (var item in query)
                {
                    EquiposDisponibles.Remove(EquiposDisponibles.SingleOrDefault(e => e.Id == item));
                }

                foreach (var item in model.modelos)
                {
                    var cantidadDisponible = EquiposDisponibles.Where(e => e.IdModeloEquipo == item.Id).Count();
                    if (item.Cantidad > cantidadDisponible)
                    {
                        datosValidos = false;
                        var err = String
                            .Format(
                              "Cantidad requerida del equipo '{0}' no puede ser satisfecha, seleccione una cantidad menor o igual a {1}",
                              item.Descripcion, cantidadDisponible);
                        ModelState.AddModelError("", err);
                    }
                }

                if (datosValidos)
                {
                    var equiposNuevosSolicitados = new List<int>();
                    var nuevaSolicitud = GlobalHelpers.Transfer<SolicitudViewModel, Solicitud>(model, null);
                    nuevaSolicitud.IdUserProfile = WebSecurity.CurrentUserId;
                    nuevaSolicitud.EstatusSolicitud = (int)EstatusSolicitudes.En_Proceso;
                    nuevaSolicitud.TipoSolicitud = (int)TiposSolicitudes.Sistema;
                    nuevaSolicitud.IdAulaEdificio = model.SalonId;
                    foreach (var item in model.modelos)
                    {
                        var equipos = EquiposDisponibles.Where(e => e.IdModeloEquipo == item.Id);
                        for(int i = 0; i < item.Cantidad; i++){
                            nuevaSolicitud.Equipoes.Add(equipos.ElementAt(i));
                            if (equipos.ElementAt(i).EstatusEquipo == (int)EstatusEquipos.Nuevo_Equipo)
                            {
                                equiposNuevosSolicitados.Add(equipos.ElementAt(i).Id);
                            }
                        }
                    }

                    if (equiposNuevosSolicitados.Count() > 0)
                    {
                        var actualizarEquipos = db.Equipoes.Where(e => equiposNuevosSolicitados.Contains(e.Id)).ToList();
                        actualizarEquipos.ForEach(e => e.EstatusEquipo = (int)EstatusEquipos.Disponible);
                    }

                    db.Solicituds.Add(nuevaSolicitud);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            ViewBag.EdificioID = getEdificio(model.EdificioId);
            ViewBag.SalonID = db.AulaEdificios.Where(a => a.IdLugar == model.EdificioId)
                .ToList().ToSelectListItems(a => a.Aula, a => a.Id.ToString(), a => a.Id == model.SalonId);
            ViewBag.PostedBack = true;

            return View(model);
        }

        /// <summary>
        /// ReporteSolicitudses Adm
        /// </summary>
        /// <returns></returns>
        [Vista("Ver Detalles (Adm)", "AEA04")]
        public ActionResult ReporteSolicitudesAdm()
        {
            return View(db.Solicituds.ToList());
        }

        //
        // GET: /Solicituds/Edit/5
        [Vista("Editar Solicitud", "AEA05")]
        public ActionResult Editar(int id)
        {
            var solicitud = db.Solicituds.Find(id);
            var equipos = solicitud.Equipoes.ToList();
            var modelos = new List<ModeloEquipoItem>();
            var iniciales = new List<ModeloEquipoItem>();
            foreach (var item in equipos.GroupBy(e => e.IdModeloEquipo))
            {
                var modelo = db.ModeloEquipoes.Find(item.Key);
                modelos.Add(new ModeloEquipoItem
                {
                    Id = modelo.Id,
                    Nombre = modelo.Nombre,
                    Descripcion = modelo.Descripcion,
                    IsSelected = true,
                    Cantidad = equipos.Where(e => e.IdModeloEquipo == modelo.Id).Count()
                });
            }
            var model = GlobalHelpers.Transfer<Solicitud, SolicitudViewModel>(solicitud, null);
            var EquiposDisponibles = db.Equipoes.Where(e => e.EstatusEquipo == 5 || e.EstatusEquipo == 1).ToList();
            var query = db.Database
                .SqlQuery<int>("EXEC EquiposNoDisponibles {0},{1},{2}", model.Fecha, model.HoraInicio, model.HoraFin)
                .ToList();
            foreach (var item in query)
            {
                EquiposDisponibles.Remove(EquiposDisponibles.SingleOrDefault(e => e.Id == item));
            }
            foreach (var item in EquiposDisponibles.GroupBy(e => e.IdModeloEquipo))
            {
                iniciales.Add(GlobalHelpers.Transfer<ModeloEquipo, ModeloEquipoItem>(db.ModeloEquipoes.Find(item.Key), null));
                //modelosDisponibles.Add(db.Equipoes.SingleOrDefault(e => e.Id == item.Key));
            }

            foreach (var item in modelos)
            {
                iniciales.Remove(iniciales.Find(m => m.Id == item.Id));
                iniciales.Add(item);
            }

            model.modelos = iniciales.OrderBy(m => m.Nombre).ToList();
            ViewBag.EdificioId = db.Lugars.ToList()
                .ToSelectListItems(l => l.Edificio, l => l.Id.ToString(), l => l.Id == solicitud.AulaEdificio.IdLugar);
            ViewBag.SalonId = db.AulaEdificios.Where(a => a.IdLugar == solicitud.AulaEdificio.IdLugar)
                .ToSelectListItems(a => a.Aula, a => a.Id.ToString(), a => a.Id == solicitud.IdAulaEdificio);

            return View(model);
        }



        [HttpPost]
        public ActionResult Editar(int id, SolicitudViewModel editada, FormCollection form)
        {
            //try
            //{
            //    List<Tuple<String, String>> cantidadSel = new List<Tuple<string, string>>(); //almacena la cantidad y el modelo del equipo seleccionado
            //    IEnumerable<SelectListItem> salonList = new List<SelectListItem>();
            //    int edificioId = int.Parse(form["edificioId"]);
            //    bool contieneEquipo = false;
            //    var SolicitudsBD = db.Solicituds.SingleOrDefault(s => s.Id == id);

            //    SolicitudsBD.Descripcion = editada.Descripcion;
            //    SolicitudsBD.HoraInicio = editada.HoraInicio;
            //    SolicitudsBD.HoraFin = editada.HoraFin;
            //    SolicitudsBD.Fecha = editada.Fecha;

            //    if (form["SalonId"] == null || form["SalonId"] == "" || form["SalonId"] == "--Seleccione Salon--")
            //    {
            //        ModelState.AddModelError("SalonId", "Debe Seleccionar el Salon");
            //        salonList = (from a in db.AulaEdificios where a.IdLugar == edificioId select a).AsEnumerable().Select(a => new SelectListItem() { Text = a.Aula, Value = a.Id.ToString() });
            //        ViewBag.SalonId = new SelectList(salonList, "Value", "Text");

            //    }
            //    else
            //    {
            //        SolicitudsBD.IdLugar = Int32.Parse(form["SalonId"]);
            //        salonList = (from a in db.AulaEdificios where a.IdLugar == edificioId select a).AsEnumerable().Select(a => new SelectListItem() { Text = a.Aula, Value = a.Id.ToString() });
            //        ViewBag.SalonId = new SelectList(salonList, "Value", "Text", form["SalonId"]);
            //    }
            //    //lista para almacenar los equipos seleccionados en la Solicituds
            //    List<SolicitudEquipo> listaEquiposSelecionados = new List<SolicitudEquipo>();

            //    //lista de equipos ya preseleccionados cuando se creo la Solicituds
            //    List<SolicitudEquipo> listaEquiposPreseleccionados = new List<SolicitudEquipo>();

            //    listaEquiposPreseleccionados = db.SolicitudEquipoes.Where(e => e.IdSolicitud == id).ToList();

            //    //Verificando si hay equipos seleccionado en la Solicituds
            //    for (int i = 1; i < form.Count; i++)
            //    {
            //        //Verificando si hay modelos de equipos eliminados
            //        if (form["hChk" + i] != null)
            //        {
            //            if (form["chk" + i] == null)
            //            {
            //                //eliminamos los equipos con el modelo especifico
            //                List<SolicitudEquipo> equiposEliminadosXmodelo = new List<SolicitudEquipo>();
            //                equiposEliminadosXmodelo = db.SolicitudEquipoes.Where(e => e.IdSolicitud == id && e.Equipo.IdModelo == Int32.Parse(form["hChk" + i])).ToList();

            //                foreach (var equipo in equiposEliminadosXmodelo)
            //                {
            //                    db.Equipoes.SingleOrDefault(e => e.Id == equipo.idEquipo).IdEstatusEquipo = 1;
            //                    db.SolicitudEquipoes.Remove(equipo);
            //                }
            //            }
            //        }

            //        if (form["chk" + i] != null)
            //        {
            //            int idModelo = Int32.Parse( form["chk" + i]);
            //            int cantidad = Int32.Parse(form["cant" + i]);
            //            //agregando los check seleccionado a la lista para mantener el modelo en caso de error

            //            cantidadSel.Add(new Tuple<string, string>(i.ToString(), form["cant" + i]));
            //            contieneEquipo = true;
            //            var ques = from e in db.Equipoes select e;

            //            int ke = ques.Count();
            //            //Equipos disponibles por modelos
            //            var equiposDispXmodelos = from e in db.Equipoes
            //                                      join solicitudEquipo in db.SolicitudEquipoes
            //                                      on e.Id equals solicitudEquipo.idEquipo
            //                                      join Solicituds in db.Solicituds
            //                                      on solicitudEquipo.IdSolicitud equals Solicituds.Id
            //                                      where Solicituds.Id == id && e.IdEstatusEquipo == 1//ojo aquiiiiiiiiiiiiiiiiiiii******/*/*/******
            //                                            || e.IdModelo == idModelo && Solicituds.Id == id

            //                                      select e;
            //            //Equipos x modelo previamente seleccionados al momento de crear la Solicituds

            //            var equiposXmodelosPrevios = from e in db.Equipoes
            //                                         join solicitudEquipo in db.SolicitudEquipoes
            //                                         on e.Id equals solicitudEquipo.idEquipo
            //                                         where e.IdModelo == idModelo && solicitudEquipo.IdSolicitud == id
            //                                         select e;

            //            //verificamos si la cantidad selecionada al momento de crear la Solicituds
            //            //es mayor que la cantidad seleccionada al momento de editar la Solicituds
            //            //para proceder a reducir los equipos previamente seleccionados
            //            if (equiposXmodelosPrevios.Count() > cantidad)
            //            {
            //                List<SolicitudEquipo> equiposAEliminar = new List<SolicitudEquipo>();
            //                int cantidadEliminar = equiposXmodelosPrevios.Count() - cantidad;

            //                foreach (var equipo in equiposXmodelosPrevios)
            //                {

            //                    if (cantidadEliminar > 0)
            //                    {
            //                        equiposAEliminar.Add(db.SolicitudEquipoes.SingleOrDefault(e => e.idEquipo == equipo.Id && e.IdSolicitud == id));
            //                        db.Equipoes.SingleOrDefault(e => e.Id == equipo.Id).IdEstatusEquipo = 1;
            //                        cantidadEliminar--;
            //                    }
            //                }

            //                foreach (var equipoAEliminar in equiposAEliminar)
            //                {
            //                    db.SolicitudEquipoes.Remove(equipoAEliminar);
            //                }
            //            }
            //            else if (equiposXmodelosPrevios.Count() < cantidad && equiposDispXmodelos.Count() >= cantidad - equiposXmodelosPrevios.Count())
            //            {
            //                //Seleccionamos lod equipos disponibles para agregar a la Solicituds
            //                var equipoSelecionado = from e in db.Equipoes
            //                                        where (e.IdModelo == i && e.IdEstatusEquipo == 1)
            //                                        select e;

            //                int cantidadSeleccionada = int.Parse(form["cant" + i]) - equiposXmodelosPrevios.Count();

            //                //agregando los nuevos equipos
            //                foreach (var item in equipoSelecionado)
            //                {
            //                    if (cantidadSeleccionada > 0)
            //                    {
            //                        //Instancia de equipo para almacenar los nuevos equipos
            //                        SolicitudEquipo nuevoEquipo = new SolicitudEquipo();

            //                        nuevoEquipo.idEquipo = item.Id;
            //                        nuevoEquipo.IdSolicitud = id;
            //                        //agregando el equipo seleccionado a la lista de equipos seleccionados
            //                        listaEquiposSelecionados.Add(nuevoEquipo);

            //                        //Actualizar estatus equipo selecionado

            //                        db.Equipoes.SingleOrDefault(e => e.Id == item.Id).IdEstatusEquipo = 5;
            //                        cantidadSeleccionada--;
            //                    }
            //                }
            //                foreach (var equipoSolicitud in listaEquiposSelecionados)
            //                {
            //                    db.SolicitudEquipoes.Add(equipoSolicitud);
            //                }
            //            }
            //            else if (equiposDispXmodelos.Count() < Int32.Parse(form["cant" + i]))
            //            {
            //                //Mostramos un mensaje diciendo que la cantidad del equipo seleccionado no esta disponible
            //                ModelState.AddModelError("", "Cantidad de " + db.ModeloEquipoes.SingleOrDefault(e => e.Id == i).Nombre + " no disponible");

            //            }
            //            else
            //            {
            //            }
            //        }
            //    }

            //    if (!contieneEquipo)
            //    {
            //        ViewBag.Seleccionar = "Debe seleccionar al menos un equipo";
            //        ModelState.AddModelError("", "");
            //    }

            //    if (ModelState.IsValid)
            //    {

            //        db.SaveChanges();
            //        return RedirectToAction("Detalles", new { Id = editada.Id });
            //    }
            //    else
            //    {
            //        ViewBag.EdificioId = new SelectList(db.Lugars, "Id", "Edificio", form["edificioId"]);

            //        //seleccionando los equipos disponibles
            //        var modelosDisponibles = from mE in db.ModeloEquipoes
            //                                 join equipo in db.Equipoes on mE.Id equals equipo.IdModelo
            //                                 join solEquipo in db.SolicitudEquipoes on equipo.Id equals solEquipo.idEquipo
            //                                 join Solicituds in db.Solicituds on solEquipo.IdSolicitud equals Solicituds.Id

            //                                 where Solicituds.Id == id || equipo.IdEstatusEquipo == 1
            //                                 group mE by mE.Id into equipo
            //                                 select equipo;

            //        List<ModeloEquipo> equiposDisponibles = new List<ModeloEquipo>();

            //        foreach (var modelos in modelosDisponibles)
            //        {
            //            equiposDisponibles.Add(db.ModeloEquipoes.SingleOrDefault(e => e.Id == modelos.Key));
            //        }

            //        ViewBag.ModeloEquipo = equiposDisponibles;
            //        ViewBag.cantidadSel = cantidadSel;

            //        return View(editada);
            //    }
            //}
            //catch(Exception e)
            //{
            //    return View(e);
            //}
            return View();
        }

        //
        // GET: /Solicituds/Delete/5
        //[Vista("Eliminar Solicitud", "AEA06")]
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
            return Json(getEdificio(int.Parse(selectEdificioId)));
        }

        public IEnumerable<SelectListItem> getEdificio(Nullable<int> selectEdificioId = null)
        {
            IEnumerable<SelectListItem> edificioList = db.Lugars.ToList().ToSelectListItems(l => l.Edificio, l => l.Id.ToString(), l => l.Id == selectEdificioId);

            return edificioList;
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
                salonList = db.Lugars.Find(_salonId).AulaEdificios.ToSelectListItems(e => e.Aula, e => e.Id.ToString());
            }

            return new SelectList(salonList, "Value", "Text", selectSalonId);

        }
        //Metodo que recibe las solicitudes por mensajes de texto implementando la api de Twilio 
        public ActionResult nuevaSolicitudSms()
        {


            return View();
        }

    }
}

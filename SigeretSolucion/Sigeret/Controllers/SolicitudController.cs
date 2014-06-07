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
            var solicitud = db.Solicituds.FirstOrDefault(s => s.Id == id);
            //ViewBag.Estatus = ((EstatusSolicitudes)solicitud.EstatusSolicitud).EnumToStr();
            return View(solicitud);
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
                foreach (var item in modelos.OrderBy(m => m.Nombre))
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
                    foreach (var item in model.modelos.Where(m => m.IsSelected == true))
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
        public ActionResult Editar(SolicitudViewModel model)
        {
            if (ModelState.IsValid)
            {
                //verificar que la cantidad de modelos seleccionados esta disponible.
                var datosValidos = true;
                var EquiposDisponibles = db.Equipoes.Where(e => e.EstatusEquipo == 5 || e.EstatusEquipo == 1).ToList();
                var query = db.Database
                    .SqlQuery<int>("EXEC EquiposNoDisponibles {0},{1},{2}", model.Fecha, model.HoraInicio, model.HoraFin)
                    .ToList();
                var solicitud = db.Solicituds.Find(model.Id);
                foreach (var item in query)
                {
                    var equipo = EquiposDisponibles.SingleOrDefault(e => e.Id == item);
                    if (!solicitud.Equipoes.Contains(equipo, new GlobalHelpers.Compare<Equipo>((e, f) => e.Id == f.Id)))
                    {
                        EquiposDisponibles.Remove(EquiposDisponibles.SingleOrDefault(e => e.Id == item));
                    }
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
                    model.modelos = model.modelos.Where(m => m.IsSelected == true).ToList();
                    var equiposNuevosSolicitados = new List<int>();
                    GlobalHelpers.Transfer<SolicitudViewModel, Solicitud>(model, solicitud);
                    solicitud.IdAulaEdificio = model.SalonId;
                    solicitud.Equipoes.Clear();
                    foreach (var item in model.modelos)
                    {
                        var equipos = EquiposDisponibles.Where(e => e.IdModeloEquipo == item.Id);
                        for (int i = 0; i < item.Cantidad; i++)
                        {
                            solicitud.Equipoes.Add(equipos.ElementAt(i));
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

                    db.Entry(solicitud).State = EntityState.Modified;
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

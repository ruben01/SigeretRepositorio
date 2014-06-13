using Sigeret.CustomCode;
using Sigeret.Models;
using Sigeret.Models.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigeret.Controllers
{
    [EsController("Equipos", "AC00")]
    public class EquipoController : BaseController
    {

        //
        // GET: /Equipo/
        [Vista("Pagina Principal", "ACA01")]
        public ActionResult Index()
        {
            return View();
        }

        [Vista("Nuevo Equipo", "ACA02")]
        public ActionResult NuevoEquipo()
        {
            ViewBag.Marca = Sigeret.Properties.Settings.Default.Marcas.Cast<string>().ToList()
                .ToSelectListItems(a => a, a => a);

            return View();
        }
        [HttpPost]
        public ActionResult NuevoEquipo(ModeloEquipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.ModeloEquipoes.Add(equipo);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Marca = Sigeret.Properties.Settings.Default.Marcas.Cast<string>().ToList()
                .ToSelectListItems(a => a, a => a, a => a == equipo.Marca);

            return View(equipo);
        }

        [Vista("Listar Equipos", "ACA03")]
        public ActionResult ReporteEquipos()
        {

            return View(db.ModeloEquipoes.ToList());

        }

        public ActionResult EquipoXModelo(int Id)
        {
            ViewBag.Modelo = db.ModeloEquipoes.FirstOrDefault(m => m.Id == Id).Modelo;

            return View(db.Equipoes.Where(e => e.IdModeloEquipo == Id));
        }

        [Vista("Ver Detalles", "ACA04")]
        public ActionResult Detalles(int Id)
        {

            return View(db.Equipoes.Find(Id));
        }

        [Vista("Editar Equipo", "ACA05")]
        public ActionResult Editar(int Id)
        {
            var equipo = db.ModeloEquipoes.Find(Id);
            ViewBag.Marca = Sigeret.Properties.Settings.Default.Marcas.Cast<string>().ToList()
                .ToSelectListItems(a => a, a => a, a => a == equipo.Marca);

            return View(equipo);
        }

        [HttpPost]
        public ActionResult Editar(ModeloEquipo equipo)
        {
            if (ModelState.IsValid)
            {
                var editarEquipo = db.ModeloEquipoes.FirstOrDefault(e => e.Id == equipo.Id);

                editarEquipo.Nombre = equipo.Nombre;
                editarEquipo.Marca = equipo.Marca;
                editarEquipo.Modelo = equipo.Modelo;
                editarEquipo.Descripcion = equipo.Descripcion;
                db.Entry(editarEquipo).State = System.Data.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Detalles", new { Id = equipo.Id });
            }

            ViewBag.Marca = Sigeret.Properties.Settings.Default.Marcas.Cast<string>().ToList()
                .ToSelectListItems(a => a, a => a, a => a == equipo.Marca);

            return View(equipo);
        }

        [HttpPost]
        public ActionResult AgregarUnidad(Equipo model)
        {
            try
            {
                model.EstatusEquipo = (int)EstatusEquipos.Nuevo_Equipo;
                db.Equipoes.Add(model);
                db.SaveChanges();
                var equipos = db.ModeloEquipoes.Find(model.IdModeloEquipo).Equipoes.ToList();
                var result = this.PartialViewToString("PartialEquiposModelo", equipos);
                return Json(JsonResponseBase.SuccessResponse(result, "Unidad registrada satisfactoriamente"), JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(JsonResponseBase.ErrorResponse("Ha ocurrido un error al registrar la unidad"), JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public ActionResult EliminarUnidad(int id)
        {
            try
            {
                var equipo = db.Equipoes.Find(id);
                db.Equipoes.Remove(equipo);
                db.SaveChanges();
                var equipos = db.ModeloEquipoes.Find(equipo.IdModeloEquipo).Equipoes.ToList();
                var result = this.PartialViewToString("PartialEquiposModelo", equipos);
                return Json(JsonResponseBase.SuccessResponse(result, "Unidad eliminada satisfactoriamente"), JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(JsonResponseBase.ErrorResponse("Ha ocurrido un error al eliminar la unidad"), JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult EditarUnidad(int id)
        {
            try
            {
                var equipo = db.Equipoes.Find(id);
                ViewBag.EstatusEquipo = typeof(EstatusEquipos).EnumToList(true, (EstatusEquipos)equipo.EstatusEquipo);
                var result = this.PartialViewToString("PartialEquipoUnidadEditar", equipo);
                return Json(JsonResponseBase.SuccessResponse(result), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(JsonResponseBase.ErrorResponse("Ha ocurrido un error al obtener los datos de la unidad"), JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public ActionResult EditarUnidad(Equipo model)
        {
            try
            {
                db.Entry(model).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                var equipos = db.ModeloEquipoes.Find(model.IdModeloEquipo).Equipoes.ToList();
                var result = this.PartialViewToString("PartialEquiposModelo", equipos);
                return Json(JsonResponseBase.SuccessResponse(result, "Unidad actualizada satisfactoriamente"), JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(JsonResponseBase.ErrorResponse("Ha ocurrido un error al actualizar los datos de la unidad"), JsonRequestBehavior.DenyGet);
            }
        }

    }
}

using Sigeret.CustomCode;
using Sigeret.Models;
using Sigeret.Models.ModelExtensions;
using Sigeret.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

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
            var doc = XDocument.Load(Server.MapPath("~/App_Data/Marcas.xml"));
            IEnumerable<string> marcas = from d in doc.Descendants("Marca")
                                         select d.Element("nombre").Value;
            ViewBag.Marca = marcas.ToList()
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

            var doc = XDocument.Load(Server.MapPath("~/App_Data/Marcas.xml"));
            IEnumerable<string> marcas = from d in doc.Descendants("Marca")
                                         select d.Element("nombre").Value;
            ViewBag.Marca = marcas.ToList()
                .ToSelectListItems(a => a, a => a, a => a == equipo.Marca);

            return View(equipo);
        }

        [HttpPost]
        [Vista("Registrar Marca", "ACA06")]
        public ActionResult AgregaMarca(MarcaModel model)
        {
            try
            {
                var doc = XDocument.Load(Server.MapPath("~/App_Data/Marcas.xml"));
                IEnumerable<string> marcas = from d in doc.Descendants("Marca")
                                             select d.Element("nombre").Value.ToLower();
                var valid = !marcas.Contains(model.Marca.ToLower());
                if (ModelState.IsValid && valid)
                {
                    var path = Server.MapPath("~/App_Data/Marcas.xml");
                    var element = new XElement("Marca");
                    element.Add(new XElement("nombre", model.Marca));
                    doc.Root.Add(element);
                    doc.Save(path);
                    return Json(JsonResponseBase.SuccessResponse(null, "Marca registrada satisfactoriamente"), JsonRequestBehavior.DenyGet);
                }

                if (!valid)
                    return Json(JsonResponseBase.ErrorResponse("La Marca que intenta registrar ya existe"), JsonRequestBehavior.DenyGet);
                return Json(JsonResponseBase.ErrorResponse("Ha ocurrido un error al registrar la Marca"), JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(JsonResponseBase.ErrorResponse("Ha ocurrido un error al registrar la Marca"), JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult ComprobarSerie(String Serie)
        {
            var series = db.Equipoes.Select(e => e.Serie).ToList();
            var resultado = !series.Contains(Serie);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        [Vista("Listar Equipos", "ACA03")]
        public ActionResult Details()
        {
            return View("ReporteEquipos", db.ModeloEquipoes.ToList());
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
            var doc = XDocument.Load(Server.MapPath("~/App_Data/Marcas.xml"));
            IEnumerable<string> marcas = from d in doc.Descendants("Marca")
                                         select d.Element("nombre").Value;
            ViewBag.Marca = marcas.ToList()
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

            var doc = XDocument.Load(Server.MapPath("~/App_Data/Marcas.xml"));
            IEnumerable<string> marcas = from d in doc.Descendants("Marca")
                                         select d.Element("nombre").Value;
            ViewBag.Marca = marcas.ToList()
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

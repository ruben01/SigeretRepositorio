using Sigeret.CustomCode;
using Sigeret.Models;
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

            return View(db.Equipoes.ToList());

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

    }
}

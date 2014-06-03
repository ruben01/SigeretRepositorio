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
            return View();
        }
        [HttpPost]
        public ActionResult NuevoEquipo(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Equipoes.Add(equipo);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

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
            return View(db.Equipoes.Find(Id));
        }

        [HttpPost]
        public ActionResult Editar(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                var editarEquipo = db.Equipoes.FirstOrDefault(e => e.Id == equipo.Id);

                //editarEquipo.Nombre = equipo.Nombre;
                //editarEquipo.Marca = equipo.Marca;
                //editarEquipo.Modelo = equipo.Modelo;
                editarEquipo.EstatusEquipo = equipo.EstatusEquipo;
                editarEquipo.Serie = equipo.Serie;
                db.Entry(editarEquipo).State = System.Data.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Detalles", new { Id = equipo.Id });
            }

            return View(equipo);
        }

    }
}

using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigeret.Controllers
{
    public class EquipoController : BaseController
    {

        //
        // GET: /Equipo/

        public ActionResult Index()
        {
            return View();
        }

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

        public ActionResult ReporteEquipos()
        {

            return View(db.Equipoes.ToList());

        }

        public ActionResult Detalles(int Id)
        {

            return View(db.Equipoes.Find(Id));
        }

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
                editarEquipo.IdEstatusEquipo = equipo.IdEstatusEquipo;
                editarEquipo.Serie = equipo.Serie;
                db.Entry(editarEquipo).State = System.Data.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Detalles", new { Id = equipo.Id });
            }

            return View(equipo);
        }

    }
}

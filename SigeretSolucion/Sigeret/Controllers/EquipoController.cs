using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigeret.Controllers
{
    public class EquipoController : Controller
    {
        //Utilizar Singleton
        SigeretDBDataContext sigeretDb = new SigeretDBDataContext();
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
            sigeretDb.Equipo.InsertOnSubmit(equipo);
            sigeretDb.SubmitChanges();

            
            return View();
        }

        public ActionResult ReporteEquipos()
        {

            return View(sigeretDb.Equipo.ToList());
        }

        public ActionResult Detalles(int Id)
        {

            return View(sigeretDb.Equipo.Single( e=>e.Id == Id));
        }

        public ActionResult Editar(int Id)
        {

            return View(sigeretDb.Equipo.Single(e => e.Id == Id));
        }
        [HttpPost]
        public ActionResult Editar(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                                       
               try
               {
                   var editarEquipo = sigeretDb.Equipo.SingleOrDefault(e => e.Id == equipo.Id);

                  // editarEquipo.Nombre = equipo.Nombre;
                 //  editarEquipo.Marca = equipo.Marca;
                 //  editarEquipo.Modelo = equipo.Modelo;
                   editarEquipo.IdEstatusEquipo = equipo.IdEstatusEquipo;
                   editarEquipo.Serie = equipo.Serie;
                   
                   
                   sigeretDb.SubmitChanges();
                   return RedirectToAction("Detalles", new { Id = equipo.Id });
               }
               catch
               {
                   return View(equipo);
               }
            }

            return View(equipo);
            
        }



    }
}

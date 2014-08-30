using Sigeret.CustomCode;
using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigeret.Controllers
{
    [Authorize]
    [EsController("Parametrización", "AH00")]
    public class ParametrizacionController : BaseController
    {
        //
        // GET: /Parametrizacion/
        [Vista("Pagina Principal", "AHA01")]
        public ActionResult Index()
        {
            return View();
        }

        [Vista("Edificios (Página Principal)", "AHA02")]
        public ActionResult Edificios()
        {
            var edificios = db.Lugars.ToList();

            return View(edificios);
        }

        [Vista("Aulas (Pagina Principal)", "AHA03")]
        public ActionResult Aulas()
        {
            var aulas = db.AulaEdificios.ToList();

            return View(aulas);
        }

        [Vista("Registrar Edificio", "AHA04")]
        public ActionResult NuevoEdificio()
        {

            return View();
        }

        [HttpPost]
        public ActionResult NuevoEdificio(Lugar model)
        {
            if (ModelState.IsValid)
            {
                db.Lugars.Add(model);
                db.SaveChanges();

                return RedirectToAction("Edificios");
            }

            return View(model);
        }

        [Vista("Registrar Aula", "AHA05")]
        public ActionResult NuevaAula()
        {
            ViewBag.IdLugar = db.Lugars.ToList().ToSelectListItems(l => l.Edificio, l => l.Id.ToString());

            return View();
        }

        [HttpPost]
        public ActionResult NuevaAula(AulaEdificio model)
        {
            if (ModelState.IsValid)
            {
                db.AulaEdificios.Add(model);
                db.SaveChanges();

                return RedirectToAction("Aulas");
            }
            ViewBag.IdLugar = db.Lugars.ToList().ToSelectListItems(l => l.Edificio, l => l.Id.ToString(), l => l.Id == model.IdLugar);

            return View(model);
        }

        [Vista("Editar Edificio", "AHA06")]
        public ActionResult EditarEdificio(int id) 
        {
            var edificio = db.Lugars.Find(id);

            if (edificio == null)
            {
                return HttpNotFound();
            }

            return View(edificio);
        }

        [HttpPost]
        public ActionResult EditarEdificio(Lugar model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Edificios");
            }

            return View(model);
        }

        [Vista("Editar Aula", "AHA07")]
        public ActionResult EditarAula(int id)
        {
            var aula = db.AulaEdificios.Find(id);

            if (aula == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdLugar = db.Lugars.ToList().ToSelectListItems(l => l.Edificio, l => l.Id.ToString(), l => l.Id == aula.IdLugar);

            return View(aula);
        }

        [HttpPost]
        public ActionResult EditarAula(AulaEdificio model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Aulas");
            }
            ViewBag.IdLugar = db.Lugars.ToList().ToSelectListItems(l => l.Edificio, l => l.Id.ToString(), l => l.Id == model.IdLugar);

            return View(model);
        }

        [Vista("Eliminar Edificio", "AHA08")]
        public ActionResult EliminarEdificio(int id)
        {
            var edificio = db.Lugars.Find(id);

            if (edificio == null)
            {
                return HttpNotFound();
            }

            return View(edificio);
        }

        [HttpPost, ActionName("EliminarEdificio")]
        public ActionResult EliminarEdificioConfirmed(int id)
        {
            var edificio = db.Lugars.Find(id);
            db.Lugars.Remove(edificio);
            db.SaveChanges();

            return RedirectToAction("Edificios");
        }

        [Vista("Eliminar Aula", "AHA09")]
        public ActionResult EliminarAula(int id)
        {
            var aula = db.AulaEdificios.Find(id);

            if (aula == null)
            {
                return HttpNotFound();
            }

            return View(aula);
        }

        [HttpPost, ActionName("EliminarAula")]
        public ActionResult EliminarAulaConfirmed(int id)
        {
            var aula = db.AulaEdificios.Find(id);
            db.AulaEdificios.Remove(aula);
            db.SaveChanges();

            return RedirectToAction("Aulas");
        }
    }
}

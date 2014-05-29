using Sigeret.Models;
using Sigeret.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Sigeret.Models.ViewModels;

namespace Sigeret.Controllers
{
    [EsController("Contactos", "AB00")]
    public class ContactoController : BaseController
    {

        //
        // GET: /Contacto/
        [Vista("Pagina Principal", "ABA01")]
        public ActionResult Index()
        {
            var contactos = db.Contactoes.Where(c => c.IdUserProfile == WebSecurity.CurrentUserId);

            return View("MisContactos", contactos.ToList());
        }


        //
        // GET: /Contacto/Create
        [Vista("Agregar Contacto", "ABA02")]
        public ActionResult NuevoContacto()
        {
            ViewBag.IdTipoContacto = new SelectList(db.TipoContactoes, "Id", "Descripcion");

            return View();
        }

        //
        // POST: /Contacto/Creaar

        [HttpPost]
        public ActionResult NuevoContacto(Contacto nuevoContacto)
        {
            if (ModelState.IsValid)
            {
                nuevoContacto.IdUserProfile = WebSecurity.CurrentUserId;
                db.Contactoes.Add(nuevoContacto);
                db.SaveChanges();

                return RedirectToAction("MisContactos");
            }

            ViewBag.IdTipoContacto = new SelectList(db.TipoContactoes, "Id", "Descripcion", nuevoContacto.IdTipoContacto);

            return View(nuevoContacto);
        }

        [Vista("Mis Contactos", "ABA03")]
        public ActionResult MisContactos()
        {
            var contactos = db.Contactoes.Where(c => c.IdUserProfile == WebSecurity.CurrentUserId);

            return View(contactos.ToList());
        }

        //
        // GET: /Contacto/Edit/5
        [Vista("Editar Contacto", "ABA04")]
        public ActionResult Editar(int Id)
        {
            var contacto = db.Contactoes.FirstOrDefault(c => c.Id == Id);
            ViewBag.IdTipoContacto = new SelectList(db.TipoContactoes, "Id", "Descripcion", contacto.IdTipoContacto);

            return View(contacto);
        }

        //
        // POST: /Contacto/Edit/5

        [HttpPost]
        public ActionResult Editar(int Id, Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                var contactoBd = db.Contactoes.FirstOrDefault(c => c.Id == Id);

                contactoBd.IdTipoContacto = contacto.IdTipoContacto;
                contactoBd.Descripcion = contacto.Descripcion;

                db.Entry(contactoBd).State = System.Data.EntityState.Modified;

                return RedirectToAction("Detalles", new { Id = contacto.Id });
            }

            ViewBag.IdTipoContacto = new SelectList(db.TipoContactoes, "Id", "Descripcion", contacto.IdTipoContacto);

            return View(contacto);
        }

        public ActionResult DatosContactos()
        {
            var contactos = db.Contactoes.Where(c => c.IdUserProfile == WebSecurity.CurrentUserId).ToList();
            return PartialView("PartialContactosTable", contactos);
        }

        [HttpPost]
        [Vista("Agregar Contacto", "ABA05")]
        public ActionResult AgregarContacto(ContactoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contacto = new Contacto();
                GlobalHelpers.Transfer<ContactoViewModel, Contacto>(model, contacto);
                contacto.IdUserProfile = WebSecurity.CurrentUserId;
                db.Contactoes.Add(contacto);
                db.SaveChanges();

                ViewBag.Exito = true;
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            ViewBag.Exito = false;
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [Vista("Editar Contacto", "ABA06")]
        public ActionResult EditarContacto(int id)
        {
            var contacto = db.Contactoes.Find(id);
            var model = GlobalHelpers.Transfer<Contacto, ContactoViewModel>(contacto);
            ViewBag.IdTipoContacto = new SelectList(db.TipoContactoes, "Id", "Descripcion", contacto.IdTipoContacto);

            return PartialView("PartialEditarContacto", model);
        }

        public ActionResult AgregarContactoModal()
        {
            ViewBag.IdTipoContacto = new SelectList(db.TipoContactoes, "Id", "Descripcion");

            return PartialView("PartialContactosModal", null);
        }

        [HttpPost]
        public ActionResult EditarContacto(ContactoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contacto = db.Contactoes.Find(model.Id);
                GlobalHelpers.Transfer<ContactoViewModel, Contacto>(model, contacto, "IdUserProfile");
                db.Entry(contacto).State = System.Data.EntityState.Modified;
                db.SaveChanges();

                ViewBag.Exito = true;
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            ViewBag.Exito = false;
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Vista("Eliminar Contacto", "ABA07")]
        public ActionResult Eliminar(int id)
        {
            var contacto = db.Contactoes.Find(id);
            db.Contactoes.Remove(contacto);
            db.SaveChanges();
            ViewBag.Exito = true;

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Contacto/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Contacto/Delete/5

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
    }
}

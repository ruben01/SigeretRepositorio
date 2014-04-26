using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Sigeret.Controllers
{
    public class ContactoController : Controller
    {
        SigeretContext db = new SigeretContext();

        //
        // GET: /Contacto/

        public ActionResult Index()
        {
            return View();
        }


        //
        // GET: /Contacto/Create

        public ActionResult NuevoContacto()
        {
            ViewBag.IdTipoContacto = new SelectList(db.TipoContactoes, "Id","Descripcion");

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

        public ActionResult MisContactos()
        {
            var contactos = db.Contactoes.Where(c => c.IdUserProfile == WebSecurity.CurrentUserId);

            return View(contactos.ToList());
        }

        //
        // GET: /Contacto/Edit/5

        public ActionResult Editar(int Id)
        {
            var contacto = db.Contactoes.FirstOrDefault(c => c.Id == Id);
            ViewBag.IdTipoContacto = new SelectList(db.TipoContactoes, "Id", "Descripcion", contacto.IdTipoContacto );

            return View(contacto);
        }

        //
        // POST: /Contacto/Edit/5

        [HttpPost]
        public ActionResult Editar(int Id, Contacto contacto)
        {
            if(ModelState.IsValid){
                var contactoBd = db.Contactoes.FirstOrDefault(c => c.Id == Id);
                
                contactoBd.IdTipoContacto = contacto.IdTipoContacto;
                contactoBd.Descripcion = contacto.Descripcion;

                db.Entry(contactoBd).State = System.Data.EntityState.Modified;

                return RedirectToAction("Detalles", new { Id = contacto.Id });
            }

            ViewBag.IdTipoContacto = new SelectList(db.TipoContactoes, "Id", "Descripcion", contacto.IdTipoContacto);

            return View(contacto);
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

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
        SigeretDBDataContext sigeretDb = new SigeretDBDataContext();
        
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
            ViewBag.IdTipoContacto = new SelectList(sigeretDb.TipoContacto, "Id","Descripcion");

            return View();
        }

        //
        // POST: /Contacto/Creaar

        [HttpPost]
        public ActionResult NuevoContacto(Contacto nuevoContacto)
        {
            try
            {

             
                nuevoContacto.IdUserProfile=  WebSecurity.GetUserId(User.Identity.Name);

                sigeretDb.Contacto.InsertOnSubmit(nuevoContacto);
                sigeretDb.SubmitChanges();

                return RedirectToAction("MisContactos");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult MisContactos()
        {


            return View(sigeretDb.Contacto.ToList());
        }

        //
        // GET: /Contacto/Edit/5

        public ActionResult Editar(int Id)
        {
            var contacto = sigeretDb.Contacto.SingleOrDefault(c => c.Id == Id);
            ViewBag.IdTipoContacto = new SelectList(sigeretDb.TipoContacto, "Id", "Descripcion", contacto.IdTipoContacto );
            return View(contacto);
        }

        //
        // POST: /Contacto/Edit/5

        [HttpPost]
        public ActionResult Editar(int Id, Contacto contacto)
        {
            try
            {
                var contactoBd = sigeretDb.Contacto.SingleOrDefault(c => c.Id == Id);
                
                contactoBd.IdTipoContacto = contacto.IdTipoContacto;
                contactoBd.Descripcion = contacto.Descripcion;

                sigeretDb.SubmitChanges();

                return RedirectToAction("Detalles", new { Id = contacto.Id });

                
            }
            catch
            {
                return View();
            }
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

using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigeret.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/
        UsersContext sigeretDbEntity = new UsersContext();
        

        public ActionResult Index()
        {
            
            
            return View();
        }

        public ActionResult ReporteUsuarios()
        {
            
            return View(sigeretDbEntity.UserProfiles.ToList());
        }

        public ActionResult Detalles(int Id)
        {
            return View(sigeretDbEntity.UserProfiles.SingleOrDefault(u=>u.UserId==Id));
        }

        public ActionResult Suspender(int Id)
        {
            ViewBag.UsuarioSuspendido = "";

            return View(sigeretDbEntity.UserProfiles.SingleOrDefault(u=>u.UserId==Id));
        }
        
        [HttpPost, ActionName("Suspender")]
        [ValidateAntiForgeryToken]
        public ActionResult SuspenderConfirmado(int Id)
        {
            

           // Eliminar Usuario
            /*************
            //sigeretDbEntity.UserProfiles.Remove(sigeretDbEntity.UserProfiles.SingleOrDefault(u => u.UserId == Id));
            //sigeretDbEntity.SaveChanges();
             *////////

           var UsuarioSuspendido = sigeretDbEntity.UserProfiles.SingleOrDefault(u => u.UserId == Id);

           UsuarioSuspendido.IdEstatusUsuario = 2;

           sigeretDbEntity.SaveChanges();

            
            return RedirectToAction("Index");
        }


        public ActionResult Habilitar(int Id)
        {
            ViewBag.UsuarioHabilitado = "";

            return View(sigeretDbEntity.UserProfiles.SingleOrDefault(u => u.UserId == Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Habilitar(int Id, FormCollection formcol)
        {


            // Eliminar Usuario
            /*************
            //sigeretDbEntity.UserProfiles.Remove(sigeretDbEntity.UserProfiles.SingleOrDefault(u => u.UserId == Id));
            //sigeretDbEntity.SaveChanges();
             */
            ///////

            var UsuarioSuspendido = sigeretDbEntity.UserProfiles.SingleOrDefault(u => u.UserId == Id);

            UsuarioSuspendido.IdEstatusUsuario = 1;

            sigeretDbEntity.SaveChanges();


            return RedirectToAction("Index");
        }



    }
}

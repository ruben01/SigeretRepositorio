using Sigeret.CustomCode;
using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigeret.Controllers
{
    [EsController("Usuarios", "AF00")]
    public class UsuarioController : BaseController
    {

        //
        // GET: /Usuario/
        [Vista("Pagina Principal", "AFA01")]
        public ActionResult Index()
        {
            return View();
        }

        [Vista("Listar Usuarios", "AFA02")]
        public ActionResult ReporteUsuarios()
        {
            
            return View(db.UserProfiles.ToList());
        }

        [Vista("Ver Detalles", "AFA03")]
        public ActionResult Detalles(int Id)
        {
            return View(db.UserProfiles.SingleOrDefault(u=>u.UserId==Id));
        }

        [Vista("Suspender Usuario", "AFA04")]
        public ActionResult Suspender(int Id)
        {
            ViewBag.UsuarioSuspendido = "";

            return View(db.UserProfiles.SingleOrDefault(u=>u.UserId==Id));
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

           var UsuarioSuspendido = db.UserProfiles.SingleOrDefault(u => u.UserId == Id);
           UsuarioSuspendido.EstatusUsuario = 2;
           db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Vista("Habilitar Usuario", "AFA05")]
        public ActionResult Habilitar(int Id)
        {
            ViewBag.UsuarioHabilitado = "";

            return View(db.UserProfiles.SingleOrDefault(u => u.UserId == Id));
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

            var UsuarioSuspendido = db.UserProfiles.SingleOrDefault(u => u.UserId == Id);
            UsuarioSuspendido.EstatusUsuario = 1;
            db.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}

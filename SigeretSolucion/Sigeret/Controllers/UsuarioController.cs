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

        public ActionResult Eliminar(int Id)
        {
            ViewBag.UsuarioEliminado = "";

            return View(sigeretDbEntity.UserProfiles.SingleOrDefault(u=>u.UserId==Id));
        }
        
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarConfirmado(int Id)
        {
            

            
            sigeretDbEntity.UserProfiles.Remove(sigeretDbEntity.UserProfiles.SingleOrDefault(u => u.UserId == Id));
            sigeretDbEntity.SaveChanges();

            
            return RedirectToAction("Index");
        }

    }
}

using Sigeret.CustomCode;
using Sigeret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

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

            var UsuarioSuspendido = db.UserProfiles.SingleOrDefault(u => u.UserId == Id);
            UsuarioSuspendido.EstatusUsuario = 1;
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        [Vista("Editar", "AFA06")]
        public ActionResult Editar(int id)
        {
            var user = db.UserProfiles.FirstOrDefault(m => m.UserId == id);
            var model = GlobalHelpers.Transfer<UserProfile, EditUserModel>(user);
            ViewBag.RoleId = db.webpages_Roles.ToList()
                .ToSelectListItems(r => r.RoleName, r => r.RoleId.ToString(), r => r.RoleId == user.webpages_Roles.First().RoleId);
            model.Correo = user.Contactoes.FirstOrDefault(c => c.IdTipoContacto == 1).Descripcion;
            ViewBag.userId = id;

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(EditUserModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserProfiles.FirstOrDefault(m => m.UserId == id);
                GlobalHelpers.Transfer<EditUserModel, UserProfile>(model, user);
                user.webpages_Roles.Clear();
                user.webpages_Roles.Add(db.webpages_Roles.Find(model.RoleId));
                var ct = user.Contactoes.FirstOrDefault(c => c.IdTipoContacto == 1).Id;
                var contacto = db.Contactoes.Find(ct);
                contacto.Descripcion = model.Correo;
                db.Entry(contacto).State = System.Data.EntityState.Modified;
                db.Entry(user).State = System.Data.EntityState.Modified;
                db.SaveChanges();

                TempData["MessageToDeliver"] = "Datos de Usuario actualizados satisfactoriamente";
                return RedirectToAction("ReporteUsuarios");
            }
            ViewBag.RoleId = db.webpages_Roles.ToList()
                .ToSelectListItems(r => r.RoleName, r => r.RoleId.ToString(), r => r.RoleId == model.RoleId);

            return View(model);
        }

        [Vista("Eliminar", "AFA07")]
        public ActionResult Eliminar(int id)
        {
            var user = db.UserProfiles.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            foreach (var c in user.Contactoes.ToList())
            {
                db.Contactoes.Remove(c);
            }
            user.Contactoes.Clear();
            db.SaveChanges();
            var userName = user.UserName;
            var roles = Roles.GetRolesForUser(userName);
            if (roles.Count() > 0)
            {
                Roles.RemoveUserFromRoles(userName, roles);
            }
            ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(userName);
            Membership.Provider.DeleteUser(userName, true);
            Membership.DeleteUser(userName, true);

            TempData["MessageToDeliver"] = "Datos de Usuario eliminados satisfactoriamente";
            return RedirectToAction("ReporteUsuarios");
        }

    }
}

using Sigeret.CustomCode;
using Sigeret.Models;
using Sigeret.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigeret.Controllers
{

    [EsController("Permisos", "AG00")]
    public class PermisosController : BaseController
    {
        //
        // GET: /Permisos/Editar
        [Vista("Editar Permisos", "AGA01")]
        public ActionResult Editar()
        {
            ViewBag.RoleId = db.webpages_Roles.ToList()
                .ToSelectListItems(wr => wr.RoleName, wr => wr.RoleId.ToString());
            ViewBag.menus = db.Controladors.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Editar(PermisosViewModel model)
        {
            if (ModelState.IsValid)
            {
                webpages_Roles role = db.webpages_Roles.Find(model.RoleId);
                role.Accions.Clear();

                if (model.Acciones != null)
                {
                    foreach (int action in model.Acciones)
                    {
                        role.Accions.Add(db.Accions.Find(action));
                    }
                }

                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.RoleId = new SelectList(db.webpages_Roles.ToList(), "RoleID", "RoleName", model.RoleId);
            ViewBag.menus = db.Controladors.ToList();

            return View(model);
        }

        public ActionResult ObtenerPermisosRol(int id)
        {
            var role = db.webpages_Roles.Find(id);
            if (role == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            PermisosViewModelPartial model = new PermisosViewModelPartial();
            model.Controladores = db.Controladors.ToList();
            model.AccionesSeleccionadas = role.Accions.Select(ra => ra.Id).ToArray();

            return PartialView("_AutorizacionesRolPartial", model);
        }

    }
}

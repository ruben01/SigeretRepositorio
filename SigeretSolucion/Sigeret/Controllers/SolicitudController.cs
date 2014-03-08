using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigeret.Controllers
{
    public class SolicitudController : Controller
    {

        SigeretDBDataContext sigeretDb = new SigeretDBDataContext();
        //
        // GET: /Solicitud/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Solicitud/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Solicitud/Create

        public ActionResult Nueva()
        {
            return View();
        }

        //
        // POST: /Solicitud/Create

        [HttpPost]
        public ActionResult Nueva(Solicitud nuevaSolicitud)
        {
            try
            {
                sigeretDb.Solicitud.InsertOnSubmit(nuevaSolicitud);
                sigeretDb.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// ReporteSolicitudes Adm
        /// </summary>
        /// <returns></returns>
        public ActionResult ReporteSolicitudesAdm()
        {
           return View(sigeretDb.Solicitud.ToList());
        }

        //
        // GET: /Solicitud/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Solicitud/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Solicitud/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Solicitud/Delete/5

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigeret.Controllers
{
    public class ReporteController : BaseController
    {
        //
        // GET: /Reporte/

        public ActionResult Index()
        {
            return View();
        }


        // GET /Reporte/ReporteUsuario

        public ActionResult ReporteUsuarios()
        {
            ViewBag.ReportToRender = "Usuarios";
            ViewBag.TitleReport = "Reporte de Usuarios Registrados en la Aplicación";

            return View("ReportsGeneric");
        }

        public ActionResult ReportePrestamosUsuarios()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ReportePrestamosUsuarios(Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {
            ViewBag.IsPost = true;
            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;

            return View();
        }

        // GET /Reporte/ReporteUsuarioSolicitud

        public ActionResult ReporteUsuarioSolicitud()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReporteUsuarioSolicitud(Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {
            ViewBag.IsPost = true;
            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;

            return View();
        }
    }
}

using Sigeret.CustomCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigeret.Controllers
{
    [EsController("Reportes", "AD00")]
    public class ReporteController : BaseController
    {
        //
        // GET: /Reporte/
        [Vista("Pagina Principal", "ADA01")]
        public ActionResult Index()
        {
            return View();
        }


        // GET /Reporte/ReporteUsuario
        [Vista("Reporte Usuarios", "ADA02")]
        public ActionResult ReporteUsuarios()
        {
            ViewBag.ReportToRender = "Usuarios";
            ViewBag.TitleReport = "Reporte de Usuarios Registrados en la Aplicación";

            return View("ReportsGeneric");
        }

        [Vista("Reporte Prestamos", "ADA03")]
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
        [Vista("Reporte Solicitudes", "ADA04")]
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

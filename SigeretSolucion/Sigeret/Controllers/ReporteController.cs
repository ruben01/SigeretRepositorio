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
            return View();
        }

        // GET /Reporte/ReporteUsuarioSolicitud

        public ActionResult ReporteUsuarioSolicitud()
        {
            return View();
        }
    }
}

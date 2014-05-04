using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sigeret.Controllers
{
    public class CustomErrorsController : BaseController
    {
        //
        // GET: /CustomErrors/NotFound

        public ActionResult NotFound()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.NotFound;

            return View("NotFound");
        }

<<<<<<< HEAD
        public ActionResult ExceptionError(Exception e)
        {

            return View(e.ToString());
=======
        public ActionResult Error()
        {
            return View("Error");
>>>>>>> 98803ef7f2dcfec01769e714d4b33dbb228c1be6
        }
    }
}

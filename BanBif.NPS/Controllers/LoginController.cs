using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanBif.NPS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string nuevoCont, string oficina)
        {

            if (nuevoCont == null || oficina == null)
            {
                ViewBag.CargarPagina = "0";
                ViewBag.Mensaje = "La encuesta no esta disponible.";
            }

            return View();

        }
    }
}
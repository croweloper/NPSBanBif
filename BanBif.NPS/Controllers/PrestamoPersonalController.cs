using BanBif.NPS.BE;
using BanBif.NPS.BL;
using System.Web.Mvc;
using System.Configuration;

namespace BanBif.NPS.Controllers
{
    public class PrestamoPersonalController : Controller
    {
        // GET: PrestamoPersonal
        public ActionResult Index(string dni)
        {
            ViewBag.APPURL = ConfigurationManager.AppSettings.Get("APPUrl").ToString();
          //  ViewBag.RutaImagen = ConfigurationManager.AppSettings.Get("RutaImagen").ToString();

            ViewBag.CargarPagina = "1";
            ViewBag.Rating = "0";
            ViewBag.Available = "1";
            ViewBag.Mensaje = "";
            ViewBag.IdUsuario = dni;

            var idTry = 0;
            var idEncuestado = int.TryParse(dni, out idTry);

            if (dni == null )
            {
                ViewBag.CargarPagina = "0";
                ViewBag.Mensaje = "La encuesta ya ha terminado.";
            }
            else if (idTry == 0)
            {
                ViewBag.Available = "1";
                ViewBag.Mensaje = "";
            }
            else
            {                
                    ViewBag.Available = "1";
                    ViewBag.Mensaje = "";
                
            }

            return View();
        }
    }
}
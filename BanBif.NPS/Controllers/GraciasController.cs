
using System.Configuration;
using System.Web.Mvc;


namespace BanBif.NPS.Controllers
{
    public class GraciasController : Controller
    {
        // GET: Gracias
        public ActionResult Index()
        {
           // ViewBag.RutaImagen = ConfigurationManager.AppSettings.Get("RutaImagen").ToString();
            return View();
        }
    }
}
using BanBif.NPS.BE;
using BanBif.NPS.BL;
using System.Web.Mvc;
using System.Configuration;

namespace BanBif.NPS.Controllers
{
    public class OficinaController : Controller
    {
        // GET: Oficina
        public ActionResult Index(string dni)
        {

            ViewBag.APPURL = ConfigurationManager.AppSettings.Get("APPUrl").ToString();
            //ViewBag.RutaImagen = ConfigurationManager.AppSettings.Get("RutaImagen").ToString();
            
            ViewBag.CargarPagina = "1";
            ViewBag.Rating = "1";
            ViewBag.Available = "1";
            ViewBag.Mensaje = "";
            ViewBag.IdUsuario = dni;

            var idTry = 0;
            var idEncuestado = int.TryParse(dni, out idTry);

            if (dni == null)
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
                //ViewBag.Rating = rating;

                //var pollUserBL = new PollUserBL();
                //var request = new PollUserRequest { id = id, token = key };
                //var resultado = pollUserBL.CheckPollUser(request);

                ViewBag.Available = "1";
                ViewBag.Mensaje = "";

                //resultado.data.available => 1=Apto encuesta; 2=Encuesta calificada; 0:Encuesta vencida
                //if (resultado.data.available == "1")
                //{
                //    ViewBag.Available = "1";
                //    ViewBag.Mensaje = "";
                //}
                //else if (resultado.data.available == "2")
                //{
                //    ViewBag.Available = "2";
                //    ViewBag.Mensaje = "Ya realizaste esta encuesta.";
                //}
                //else
                //{
                //    ViewBag.Available = "3";
                //    ViewBag.Mensaje = "La encuesta ya ha terminado.";
                //}
            }

            return View();
        }
    }
}
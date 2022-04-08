using BanBif.NPS.BE;
using BanBif.NPS.BL;
using System.Web.Mvc;

namespace BanBif.NPS.Controllers
{
    public class VotoController : Controller
    {

        // GET: Voto
        [HttpPost]
        public ActionResult Index(PollResultData data) 
        {
            var pollUserBL = new PollUserBL();
            var response = pollUserBL.SavePollResult(data);
            //var notificacionRequest = new NPS_Notificacion_request { id = 11789 };
            //var responseNotificacion = pollUserBL.EnvioNotificacion(notificacionRequest);
            return Json(new { result = true, data = response, mensaje = "Información guardada con éxito." }, JsonRequestBehavior.AllowGet);
        }


   
    }
}
using BanBif.NPS.BE;
using BanBif.NPS.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanBif.NPS.Controllers
{
    public class SaveDigitalController : Controller
    {
        //// GET: SaveDigital
        //[HttpPost]
        //public ActionResult Index(NpsDigitalRequest request)
        //{
        //    var pollUserBL = new PollUserBL();
        //    var response = pollUserBL.RegistrarDigital(request);
        //    return Json(response);
        //}


        //// Post Nuevo NPS Matriz: SaveDigital
        //[HttpPost]
        //public ActionResult Index(NpsDigitalRequest request)
        //{
        //    var pollUserBL = new PollUserBL();
        //    var response = pollUserBL.RegistrarDigital(request);
        //    return Json(response);
        //}

        [HttpPost]
        public ActionResult Index(NpsDigitalRequest request)
        {
            var oBL = new NPSNuevoRegistro();
            int resultado = oBL.RegistrarMatrizNPS(request);

            if (resultado==1)
            {
                return Json(new { result = true, data = request ,mensaje = "Información guardada con éxito." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false, data = request, mensaje = "Información no guardada." }, JsonRequestBehavior.AllowGet);

            }

        }

    }
}
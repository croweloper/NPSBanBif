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
        // GET: SaveDigital
        [HttpPost]
        public ActionResult Index(NpsDigitalRequest request)
        {
            var pollUserBL = new PollUserBL();
            var response = pollUserBL.RegistrarDigital(request);
            return Json(response);
        }
    }
}
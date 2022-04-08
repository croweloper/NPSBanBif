using BanBif.NPS.BE;
using BanBif.NPS.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanBif.NPS.Controllers
{
    public class SaveCuentaDigitalController : Controller
    {
        // GET: SaveCuentaDigital
        [HttpPost]
        public ActionResult Index(NPSCuentaDigitalRequest request)
        {
            var pollUserBL = new PollUserBL();
            var response = pollUserBL.RegistrarCuentaDigital(request);
            return Json(response);
        }
    }
}
using BanBif.NPS.BE;
using BanBif.NPS.BL;
using System.Web.Mvc;
using System.Configuration;
using System;
using System.Collections.Generic;

namespace BanBif.NPS.Controllers
{
    public class DigitalController : Controller
    {
        // GET: Digital
        //public ActionResult Index(string id, string key, string rating)
        //{

        //    ViewBag.APPURL = ConfigurationManager.AppSettings.Get("APPUrl").ToString();

        //    ViewBag.CargarPagina = "1";
        //    ViewBag.Rating = "1";
        //    ViewBag.Available = "1";
        //    ViewBag.Mensaje = "";
        //    ViewBag.IdUsuario = id;
        //    ViewBag.Pregunta = "";

        //    var idTry = 0;
        //    var idEncuestado = int.TryParse(id, out idTry);

        //    if (id == null || key == null || rating == null)
        //    {
        //        ViewBag.CargarPagina = "0";
        //        ViewBag.Mensaje = "La encuesta ya ha terminado.";
        //    }
        //    else if (idTry == 0)
        //    {
        //        ViewBag.Available = "1";
        //        ViewBag.Mensaje = "";
        //        ViewBag.Pregunta = "Según su reciente experiencia usando la APP BANBIF, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la APP a familiares y amigos ?";
        //    }
        //    else
        //    {
        //        ViewBag.Rating = rating;

        //        var pollUserBL = new PollUserBL();
        //        var request = new PollUserRequest { id = id, token = key };
        //        var resultado = pollUserBL.CheckPollUser(request);
        //        ViewBag.CANAL = "";
        //        ViewBag.Pregunta = "";
        //        if (resultado != null && resultado.data != null) {
        //            ViewBag.CANAL = resultado.data.canal;

        //            if (resultado.data.canal == "APP")
        //            {
        //                ViewBag.Pregunta = "Según su reciente experiencia usando la APP BANBIF, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la APP a familiares y amigos ?";
        //            }
        //            else if (resultado.data.canal == "BPI")
        //            {
        //                ViewBag.Pregunta = "Según su reciente experiencia usando la Banca por Internet BANBIF por la página WEB, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la Banca por Internet a familiares y amigos?";
        //            }
        //            else if (resultado.data.canal == "BT")
        //            {
        //                ViewBag.Pregunta = "Según su reciente experiencia comunicándose con la Banca Telefónica BANBIF, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la Banca Telefónica a familiares y amigos?";
        //            }
        //        }


        //        //resultado.data.available => 1=Apto encuesta; 2=Encuesta calificada; 0:Encuesta vencida
        //        if (resultado.data.available == "1")
        //        {
        //            ViewBag.Available = "1";
        //            ViewBag.Mensaje = "";
        //        }
        //        else if (resultado.data.available == "2")
        //        {
        //            ViewBag.Available = "2";
        //            ViewBag.Mensaje = "Ya realizaste esta encuesta.";
        //        }
        //        else
        //        {
        //            ViewBag.Available = "3";
        //            ViewBag.Mensaje = "La encuesta ya ha terminado.";
        //        }
        //    }

        //    return View();
        //}

        public ActionResult Index(string dni,string banca)
        {

            ViewBag.APPURL = ConfigurationManager.AppSettings.Get("APPUrl").ToString();

            ViewBag.CargarPagina = "1";
            ViewBag.Rating = "1";
            ViewBag.Available = "1";
            ViewBag.Mensaje = "";
            ViewBag.IdUsuario = dni;
            ViewBag.Pregunta = "";
            ViewBag.BancaCanal = "";

            List<string> canal = new List<string>()
                {
                   "APP",
                    "BPI",
                    "BT"

                };

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
                ViewBag.Pregunta = "Según su reciente experiencia usando la APP BANBIF, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la APP a familiares y amigos ?";
            }
            else
            {
                ViewBag.CANAL = "";
                ViewBag.Pregunta = "";

                ViewBag.Available = "1";
                ViewBag.Mensaje = "";

            }


            try
            {
                int bancaint = Int32.Parse(banca);

                if (bancaint == 0)
                {
                    ViewBag.Pregunta = "Según su reciente experiencia usando la APP BANBIF, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la APP a familiares y amigos ?";

                }
                else if (bancaint == 1)
                {
                    ViewBag.Pregunta = "Según su reciente experiencia usando la Banca por Internet BANBIF por la página WEB, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la Banca por Internet a familiares y amigos?";
                }
                else if (bancaint == 2)
                {
                    ViewBag.Pregunta = "Según su reciente experiencia comunicándose con la Banca Telefónica BANBIF, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la Banca Telefónica a familiares y amigos?";
                }else {
                    ViewBag.Pregunta = "Según su reciente experiencia comunicándose con la Banca Telefónica BANBIF, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la Banca Telefónica a familiares y amigos?";
                }

                ViewBag.BancaCanal = canal[bancaint];

            }
            catch (System.Exception)
            {

                throw;
            }

            


            return View();
        }


    }
}
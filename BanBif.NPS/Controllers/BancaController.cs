using BanBif.NPS.BE;
using BanBif.NPS.BL;
using System.Web.Mvc;
using System.Configuration;
using System.Collections.Generic;
using System;

namespace BanBif.NPS.Controllers
{
    public class BancaController : Controller
    {
        // GET: Banca
        public ActionResult Index(string dni,string banca)
        {
            ViewBag.APPURL = ConfigurationManager.AppSettings.Get("APPUrl").ToString();

            ViewBag.CargarPagina = "1";
            ViewBag.Rating = "";
            ViewBag.Available = "1";
            ViewBag.Mensaje = "";
            ViewBag.IdUsuario = dni;
            ViewBag.Pregunta = "";
            ViewBag.BancaCanal = "";


            List<string> canal = new List<string>()
                {
                   "BANCA PREMIUM",
                    "BANCA CORPORATIVA",
                    "BANCA EMPRESA",
                    "BANCA NEGOCIOS",
                    "APP"

                };


            var idTry = 0;

            try
            {
                int bancaint = Int32.Parse(banca);
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


                    if (bancaint == 0)
                    {
                        ViewBag.Pregunta = "Según su reciente experiencia con Banca Premium de BanBif, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de BanBif a familiares y amigos?";

                    }
                    else if (bancaint == 1)
                    {
                        ViewBag.Pregunta = "Según su experiencia de trabajar con la Banca Corporativa de BanBif, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la Banca Corporativa a otra empresa, proveedor o cliente?";
                    }
                    else if (bancaint == 2)
                    {
                        ViewBag.Pregunta = "Según su experiencia de trabajar con la Banca Empresa de BanBif, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la Banca Empresa a otra empresa, proveedor o cliente?";
                    }
                    else if (bancaint == 3)
                    {
                        ViewBag.Pregunta = "Según su experiencia de trabajar con la Banca Negocios de BanBif, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la Banca Negocios a otra empresa, proveedor o cliente?";
                    }
                    else
                    {
                        ViewBag.Pregunta = "Según su reciente experiencia usando la APP BANBIF, en una escala del 0 al 10, ¿Qué tan probable es que recomiende el servicio de la APP a familiares y amigos ?";
                    }

                    ViewBag.Available = "1";
                    ViewBag.Mensaje = "";
                    ViewBag.BancaCanal = canal[bancaint];
                }


            }
            catch (Exception)
            {

                
            }
            

            return View();
        }
    }
}
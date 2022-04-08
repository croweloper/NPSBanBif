
using BanBif.NPS.BE;
using BanBif.NPS.BL.Interface;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using BanBif.NPS.BE.Correo;

namespace BanBif.NPS.BL
{
    public class PollUserBL: IPollUserBL
    {
        public PollUserResponse CheckPollUser(PollUserRequest request) {
            var appKeyApiURL = System.Configuration.ConfigurationManager.AppSettings.Get("ApiUrl").ToString();
            string apiUrl = appKeyApiURL + "api/PollNPS/CheckPollUser";
            var result = new HttpResponseMessage();

            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = "";

                dataObjects = result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<PollUserResponse>(dataObjects);
                return response;
            }
        }
        public int SavePollResult(PollResultData data) {
            var appKeyApiURL = System.Configuration.ConfigurationManager.AppSettings.Get("ApiUrl").ToString();
            string apiUrl = appKeyApiURL + "api/PollNPS/SavePollResult";
            
            var result = new HttpResponseMessage();

            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                var jsonObject = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = "";

                dataObjects = result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<PollResultResponse>(dataObjects);
                return response.data;
            }
        }

        public bool EnvioNotificacion(NPS_Notificacion_request request) {
            var appKeyApiURL = System.Configuration.ConfigurationManager.AppSettings.Get("ApiUrl").ToString();
            string apiUrl = appKeyApiURL + "api/PollNPS/GetDataNotificacion";

            var result = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = "";

                dataObjects = result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<NPS_Notificacion_response>(dataObjects);
                if (response.result)
                {
                    var correoRequest = new CorreoRequest
                    {
                        From = "NPS System <NPSSYSTEM@BANBIF.COM.PE>",
                        To = response.data.EMAIL_GO,
                        Cc = "dquispe@banbif.com.pe",
                        Asunto = "Notificación de usuario " + (response.data.RESULT <= 6 ? "Detractor" : response.data.RESULT <= 8 ? "Neutro" : "Promotor"),
                        Mensaje = MensajeCorreo(response.data)
                    };

                    return EnviarCorreo(correoRequest);
                }
                else {
                    return false;
                }
                
            }

            
        }

        bool EnviarCorreo(CorreoRequest request) {
            //EmailApiUrl
            var appKeyApiURL = System.Configuration.ConfigurationManager.AppSettings.Get("EmailApiUrl").ToString();
            string apiUrl = appKeyApiURL + "api/Correo/EnviarCorreo";

            var result = new HttpResponseMessage();

            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = "";

                dataObjects = result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<CorreoResult>(dataObjects);
                return response.Enviado;
            }
        }

        string MensajeCorreo(NPS_NotificacionBE data) {
            var strHtml = "<div>";

            strHtml += "<b>";
            strHtml += "Notificación de" + (data.RESULT <= 6 ? "Detractor" : data.RESULT <= 8 ? "Neutro" : "Promotor");
            strHtml += "</b>";
            strHtml += "</br>";
            strHtml += "Encuesta NPS satisfactoria adjunta.";
            strHtml += "Por favor, comience la gestión de " + (data.RESULT <= 6 ? "detractor" : data.RESULT <= 8 ? "neutro" : "promotor");

            strHtml += "</br>";
            strHtml += "</br>";

            strHtml += "<b>Datos de la encuesta</b>";
            strHtml += "</br>";

            strHtml += "<table>";

            strHtml += "<tr>";
            strHtml += "<td><b>Nombre:</b></td>";
            strHtml += "<td>"+data.NOMBRE_CLIENTE+"</td>";
            strHtml += "</tr>";

            strHtml += "<tr>";
            strHtml += "<td><b>Teléfono:</b></td>";
            strHtml += "<td>"+data.CELULAR+"</td>";
            strHtml += "</tr>";

            strHtml += "<tr>";
            strHtml += "<td><b>Email:</b></td>";
            strHtml += "<td>"+data.EMAIL+"</td>";
            strHtml += "</tr>";

            strHtml += "<tr>";
            strHtml += "<td><b>Canal utilizado:</b></td>";
            strHtml += "<td>"+data.CANAL+"</td>";
            strHtml += "</tr>";

            strHtml += "<tr>";
            strHtml += "<td><b>Centro:</b></td>";
            strHtml += "<td>OFICINA "+ data.AGENCIA.ToUpper()+"</td>";
            strHtml += "</tr>";

            strHtml += "<tr>";
            strHtml += "<td><b>Respuesta NPS:</b></td>";
            strHtml += "<td>"+data.RESULT+"</td>";
            strHtml += "</tr>";

            strHtml += "<tr>";
            strHtml += "<td><b>Comentario del cliente:</b></td>";
            strHtml += "<td>"+data.COMMENTS+"</td>";
            strHtml += "</tr>";

            strHtml += "</table>";

            strHtml += "</div>";
            return strHtml;
        }

        public NpsDigitalResponse RegistrarDigital(NpsDigitalRequest request)
        {
            var appKeyApiURL = System.Configuration.ConfigurationManager.AppSettings.Get("ApiUrl").ToString();
            string apiUrl = appKeyApiURL + "api/PollNPS/SaveResultDigital";

            var result = new HttpResponseMessage();

            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = "";

                dataObjects = result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<NpsDigitalResponse>(dataObjects);
                return response;
            }
        }

        public NpsDigitalResponse RegistrarCuentaDigital(NPSCuentaDigitalRequest request)
        {
            var appKeyApiURL = System.Configuration.ConfigurationManager.AppSettings.Get("ApiUrl").ToString();
            string apiUrl = appKeyApiURL + "api/PollNPS/SaveResultCuentaDigital";

            var result = new HttpResponseMessage();

            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = "";

                dataObjects = result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<NpsDigitalResponse>(dataObjects);
                return response;
            }
        }
    }
}

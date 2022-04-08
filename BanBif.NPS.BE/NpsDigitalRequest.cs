using System.Collections.Generic;

namespace BanBif.NPS.BE
{
    public class NpsDigitalRequest
    {
        public int ID { get; set; }
        public string Respuesta3 { get; set; }
        public List<NpsDigitalDetalleRequest> Respuestas2 { get; set; }
    }
}

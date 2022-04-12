using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.NPS.BE
{
    public class NPSNuevoRegistro
    {

        //public NPS_NUEVO_RESULTADO RegistarEncuesta(NPS_NUEVO_RESULTADO request)
        //{
        //    using (var db = new panelEntities())
        //    {

        //        var Registro = db.NPS_NUEVO_RESULTADO.Where(p => p.fecharesultado == request.fecharesultado).FirstOrDefault();
        //        var result = new NPS_NUEVO_RESULTADO();

        //        if (Registro != null)
        //        {
        //            //result.IDRESULTADO = Registro.IDRESULTADO;
        //            result.fecharesultado = Registro.fecharesultado.Value;
        //            result.canal = Registro.canal;
        //            result.COMENTARIO_PREG2 = Registro.COMENTARIO_PREG3;
        //            result.COMENTARIO_PREG3 = Registro.COMENTARIO_PREG3;
        //            result.comentario_VOTO = Registro.comentario_VOTO;
        //            result.VOTO = Registro.VOTO;
        //            result.oficina = Registro.oficina;
        //            result.documento = Registro.documento;

        //        }

        //        return result;
        //    }

        //}

        public int RegistarEncuestaNPS(NPS_NUEVO_RESULTADO request)
        {
            int vResult = 0;
            try
            {

                var db = new panelEntities();
                vResult = db.SP_RegistrarNuevoNPS(request.documento, request.canal, request.oficina, request.VOTO, request.comentario_VOTO, request.COMENTARIO_PREG2, request.COMENTARIO_PREG3);
                return vResult;                

            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public SP_NPSConsultarUltimoRegistroPorId_Result ConsultarEncuestaNPS(NPS_NUEVO_RESULTADO request)
        {
            int vResult = 0;
            SP_NPSConsultarUltimoRegistroPorId_Result consulta;
            try
            {

                var db = new panelEntities();
                consulta = db.SP_NPSConsultarUltimoRegistroPorId(request.documento).FirstOrDefault(); ;
                return consulta;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int RegistrarMatrizNPS(NpsDigitalRequest request)
        {
            int vResult = 0;
            
            try
            {
                foreach (var rpta in request.Respuestas2)
                {
                    var db = new panelEntities();
                    vResult=db.SP_RegistrarNuevoMartrizNPS(request.ID, rpta.IdPregunta, rpta.IdRespuesta);
                }
                
                return vResult;

            }
            catch (Exception ex)
            {
                return vResult;
            }
        }

        



    }
}

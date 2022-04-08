using BanBif.NPS.BE;

namespace BanBif.NPS.BL.Interface
{
    public interface IPollUserBL
    {
        PollUserResponse CheckPollUser(PollUserRequest request);
        int SavePollResult(PollResultData data);
        bool EnvioNotificacion(NPS_Notificacion_request request);
        NpsDigitalResponse RegistrarDigital(NpsDigitalRequest request);
    }
}

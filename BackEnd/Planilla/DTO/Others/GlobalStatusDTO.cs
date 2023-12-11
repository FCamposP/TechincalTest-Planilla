using Planilla.DTO.Others;

namespace Planilla.DTO.Others
{
    public class GlobalStatusDTO
    {
        /// <summary>
        /// Estado específico de la solicitud
        /// </summary>
        public RequestStatusDTO RequestStatus { get; set; }
        /// <summary>
        /// Estado específico de la respuesta, basada en el resultado del proceso
        /// </summary>
        public ResponseStatusDTO ResponseStatus { get; set; }

        public GlobalStatusDTO()
        {
            RequestStatus = new RequestStatusDTO();
            ResponseStatus = new ResponseStatusDTO();
        }
    }
}

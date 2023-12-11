using System.Net;

namespace Planilla.DTO.Others
{
    public class ResponseWrapperDTO<T>
    {
        /// <summary>
        /// Datos relevantes de la respuesta
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Estado general de la solicitud
        /// </summary>
        public GlobalStatusDTO Status { get; set; }
        public ResponseWrapperDTO()
        {
            Status = new GlobalStatusDTO();
        }
        public ResponseWrapperDTO(T data)
        {
            Data = data;
            Status = new GlobalStatusDTO();
        }

        public void AddRequestStatus(HttpStatusCode statusCode)
        {
            AddRequestStatus(statusCode, statusCode.ToString());
        }
        public void AddRequestStatus(HttpStatusCode statusCode, string statusMessage)
        {
            if (Status == null)
            {
                Status = new GlobalStatusDTO();
            }

            if (Status.RequestStatus == null)
            {
                Status.RequestStatus = new RequestStatusDTO();
            }
            Status.RequestStatus.Codigo = (int)statusCode;
            Status.RequestStatus.Mensaje = statusMessage;
        }
        public void AddResponseStatus(int statusCode, string statusMessage, string mensajeError)
        {
            if (Status == null)
            {
                Status = new GlobalStatusDTO();
            }

            if (Status.ResponseStatus == null)
            {
                Status.ResponseStatus = new ResponseStatusDTO();
            }
            Status.ResponseStatus.Codigo = statusCode;
            Status.ResponseStatus.Mensaje = statusMessage;
            Status.ResponseStatus.MensajeError = mensajeError;
        }

        
    }
}
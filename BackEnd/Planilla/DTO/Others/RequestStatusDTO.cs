namespace Planilla.DTO.Others
{
    public class RequestStatusDTO
    {
        /// <summary>
        /// Código HTTP estándar del estado de la solicitud
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Descripción del estado
        /// </summary>
        public string Mensaje { get; set; }

        public RequestStatusDTO()
        {
            Codigo = 200;
            Mensaje = "SUCCESS";
        }
    }
}

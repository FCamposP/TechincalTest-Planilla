using System;

namespace Planilla.DTO.Others
{
    public class ResponseStatusDTO
    {
        /// <summary>
        /// Código HTTP estándar del estado de la respuesta
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Descripción del estado
        /// </summary>
        public string Mensaje { get; set; }
        /// <summary>
        /// Descripción de mensaje ocurrido, usualmente mensajes de excepciones
        /// </summary>
        public string MensajeError { get; set; }

        public ResponseStatusDTO()
        {
            Codigo = 0;
            Mensaje = "SUCCESS";
            MensajeError = String.Empty;
        }
    }
}

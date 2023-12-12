using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.DTO.Componente
{
    public class ComponenteDTO
    {
        /// <summary>
        /// Identificador de componente
        /// </summary>
        public int ComponenteId { get; set; }

        /// <summary>
        /// Identificador de tipo de componente
        /// </summary>
        public int? TipoComponenteId { get; set; }

        /// <summary>
        /// Identificador del componente padre
        /// </summary>
        public int? PadreId { get; set; }

        /// <summary>
        /// Nombre del registro
        /// </summary>
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Nombre a mostrar del componente en pantalla
        /// </summary>
        public string? NombreMostrar { get; set; }

        /// <summary>
        /// Descripción del registro
        /// </summary>
        public string? Descripcion { get; set; }

        /// <summary>
        /// Ruta de endpoint si fuese requerido por el componente
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Indica si un componente pertenece a FrontOffice o BackOffice
        /// </summary>
        public bool? EsFrontOffice { get; set; }

        /// <summary>
        /// Icono del componente, si fuese necesario
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Indica si un registro esta activo en el sistema
        /// </summary>
        public bool? Activo { get; set; }

        /// <summary>
        /// Nombre del componente Padre
        /// </summary>
        public string? NombrePadre { get; set; }

        /// <summary>
        /// Nombre del tipo de componenten
        /// </summary>
        public string? NombreTipoComponente { get; set; }

        /// <summary>
        /// Id de usuario creador del registro
        /// </summary>
        public int Creador { get; set; }

        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime Creado { get; set; }
    }
}

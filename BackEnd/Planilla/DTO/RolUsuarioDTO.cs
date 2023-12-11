using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.DTO
{
    public class RolUsuarioDTO
    {
        /// <summary>
        /// Identificador de rol y usuario
        /// </summary>
        public int RolUsuarioId { get; set; }

        /// <summary>
        /// Identificador de Usuario
        /// </summary>
        public int? UsuarioId { get; set; }

        /// <summary>
        /// Identificador de un rol de usuario
        /// </summary>
        public int? RolId { get; set; }

        /// <summary>
        /// Indica si un registro esta activo en el sistema
        /// </summary>
        public bool? Activo { get; set; }
    }
}

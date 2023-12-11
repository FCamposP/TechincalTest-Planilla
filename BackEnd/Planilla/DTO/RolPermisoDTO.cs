using Planilla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.DTO
{
    public class RolPermisoDTO
    {
        /// <summary>
        /// Identificador de relación rol y permiso
        /// </summary>
        public int RolPermisoId { get; set; }

        /// <summary>
        /// Identificador de componente
        /// </summary>
        public int? ComponenteId { get; set; }

        /// <summary>
        /// Identificador de un rol de usuario
        /// </summary>
        public int? RolId { get; set; }

        /// <summary>
        /// Indica si un registro esta activo en el sistema
        /// </summary>
        public bool? Activo { get; set; }
    }

    public class RolPermisoUsuarioDTO : RolPermisoDTO
    {
        public string? NombreComponente { get; set; }

        public string? Padre { get; set; }
        public string? Descripcion { get; set; }
        public string? Url { get; set; }
        public bool superUsuario { get; set; }

    }
}

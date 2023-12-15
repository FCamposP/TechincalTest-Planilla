using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.Abstractions
{
    public interface IAutorizacionRolPermiso
    {
        /// <summary>
        /// Validará si el usuario tiene acceso para consumir un endpoint específico
        /// </summary>
        /// <param name="url"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        bool PermisoEndpoint(string url, int usuarioId);

    }
}

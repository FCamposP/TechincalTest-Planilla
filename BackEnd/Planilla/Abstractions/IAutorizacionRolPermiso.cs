using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.Abstractions
{
    public interface IAutorizacionRolPermiso
    {
        bool PermisoEndpoint(string url, int usuarioId);

    }
}

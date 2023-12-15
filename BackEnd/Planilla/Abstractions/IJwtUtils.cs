using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.Abstractions
{
    public interface IJwtUtils
    {
        /// <summary>
        /// Verifica si el token es válido
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int? ValidateToken(string token);
    }
}

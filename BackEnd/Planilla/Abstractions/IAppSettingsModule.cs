using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.Abstractions
{
    public interface IAppSettingsModule
    {
        /// <summary>
        /// Método que obtendrá el identificador del sistema en ejecución
        /// </summary>
        /// <returns></returns>
        string ObtenerCodigoModulo();
        /// <summary>
        /// Método que obtendrá el codigo del ambiente configurado en appsettings.json
        /// </summary>
        /// <returns></returns>
        string ObtenerCodigoEntorno();

    }
}

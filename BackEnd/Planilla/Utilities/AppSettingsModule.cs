using Microsoft.Extensions.Options;
using Planilla.Abstractions;
using Planilla.DTO.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.Utilities
{
    public class AppSettingsModule : IAppSettingsModule
    {
        private readonly ConfiguracionModulo _configModulo;

        public AppSettingsModule(IOptions<ConfiguracionModulo> configModulo)
        {
            _configModulo = configModulo.Value;
        }
        public string ObtenerCodigoEntorno()
        {
            return _configModulo.CodigoEntorno;
        }

        public string ObtenerCodigoModulo()
        {
            return _configModulo.CodigoModulo;
        }
    }
}

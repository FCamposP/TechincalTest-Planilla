using Planilla.Entities;
using System;

namespace Planilla.Abstractions
{
    public interface ILogErrorger
    {
        /// <summary>
        /// Guardará en la base de datos una excepción no controlada en el sistema
        /// </summary>
        /// <param name="ex"></param>
        void RegisterException(Exception ex);
        /// <summary>
        /// Guardará una excepción personalizada en la base de datos
        /// </summary>
        /// <param name="ex"></param>
        void RegisterException(LogError ex);
    }
}

using Planilla.Entities;
using System;

namespace Planilla.Abstractions
{
    public interface ILogErrorger
    {
        void RegisterException(Exception ex);
        void RegisterException(LogError ex);
    }
}

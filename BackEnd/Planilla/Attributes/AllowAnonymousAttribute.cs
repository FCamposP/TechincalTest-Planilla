using System;

namespace Planilla.Attributes
{
    /// <summary>
    /// No valida token en endpoint que usen este atributo
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    { }
}

using Planilla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.DTO
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }

        public int? EmpleadoId { get; set; }

        public string? EmpleadoNombre { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? PasswordConfirmacion { get; set; }

        public bool ActualizarPassword { get; set; }

        public int IntentosFallidos { get; set; }

        public bool Activo { get; set; }

        public int Creador { get; set; }

        public DateTime Creado { get; set; }

    }
}

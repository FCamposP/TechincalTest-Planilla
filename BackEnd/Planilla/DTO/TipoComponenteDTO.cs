using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

﻿namespace Planilla.DTO
{
    public class TipoComponenteDTO
    {
        public int TipoComponenteId { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string? Nombre { get; set; }
        public bool? Activo { get; set; }
        public DateTime creado { get; set; }
    }
}

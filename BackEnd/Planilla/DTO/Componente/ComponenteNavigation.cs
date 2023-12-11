using System.Collections.Generic;

namespace Planilla.DTO.Componente
{
    public class ComponenteNavigation
    {
        public int Id { get; set; }
        public string? Label { get; set; }
        public string? Icon { get; set; }
        public List<string>? RouterLink { get; set; }
        public virtual List<ComponenteNavigation>? Items { get; set; }
    }
}

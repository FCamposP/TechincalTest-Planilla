using System.Collections.Generic;

namespace Planilla.DTO
{
    public class RolTree
    {
        public string Key { get; set; }
        public string label { get; set; }
        public string data { get; set; }
        public string icon { get; set; }
        public virtual List<RolTree> Children { get; set; } = new List<RolTree>();
    }
}

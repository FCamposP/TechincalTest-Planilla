using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Planilla.Controllers
{
    /// <summary>
    /// Api Base del cual heredan el resto de controllers, configurando la estructura api/[controller]
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ControllerBaseCustom : ControllerBase
    {

        public ControllerBaseCustom()
        {
        }
    }
}

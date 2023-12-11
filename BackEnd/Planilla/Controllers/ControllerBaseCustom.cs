using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Planilla.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class ControllerBaseCustom : ControllerBase
    {

        public ControllerBaseCustom()
        {
        }
    }
}

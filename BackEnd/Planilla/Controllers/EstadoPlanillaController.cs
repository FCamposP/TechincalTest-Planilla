using AutoMapper;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;
using Planilla.DTO;
using Planilla.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Planilla.Controllers
{
    public class EstadoPlanillaController : ControllerBaseCustom
    {
        readonly protected EstadoPlanillaService _service;
        public EstadoPlanillaController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            _service = new EstadoPlanillaService(context, appSettingsModule, mapper);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<EstadoPlanillaDTO>>> Get()
        {
            return await _service.GetAllDTO();
        }
    }
}

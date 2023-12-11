using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Planilla.Abstractions;
using Planilla.DTO;
using Planilla.DTO.ConfiguracionGlobal;
using Planilla.DTO.Others;
using Planilla.Services;
using Planilla.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planilla.Controllers
{
    public class ConfiguracionGlobalController : ControllerBaseCustom
    {
        readonly protected ConfiguracionGlobalService configuracionGlobalService;

        public ConfiguracionGlobalController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            configuracionGlobalService = new ConfiguracionGlobalService(context,appSettingsModule,mapper);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<ConfiguracionGlobalDTO>>> Get()
        {
            return await configuracionGlobalService.GetAllDTO();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<ConfiguracionGlobalDTO>> GetById(int id)
        {
            return await configuracionGlobalService.GetByIdDTO(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<ConfiguracionGlobalDTO>> Actualizar(ConfiguracionGlobalDTO entity, int userId)
        {
            return await configuracionGlobalService.ActualizarDTO(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<ConfiguracionGlobalDTO>> Crear(ConfiguracionGlobalDTO entity, int userId)
        {
            return await configuracionGlobalService.CrearDTO(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<ConfiguracionGlobalDTO>> Eliminar(int id, int userId)
        {
            return await configuracionGlobalService.EliminarDTO(id, userId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<int>> EliminarMultiples(List<int> ids, int userId)
        {
            return await configuracionGlobalService.EliminarMultiples(ids, userId);
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO;
using Planilla.DTO.Others;
using Planilla.Entities;
using Planilla.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planilla.Controllers
{
    public class RolPermisoController : ControllerBaseCustom
    {
        readonly protected RolPermisoService _rolPermisoService;

        public RolPermisoController(ApiDBContext context, IAppSettingsModule appSettingsModule)
        {
            _rolPermisoService = new RolPermisoService(context, appSettingsModule);
        }

        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<RolPermisoDTO>>> Get()
        {
            return await _rolPermisoService.GetAll();
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<RolPermiso>> GetById(int id)
        {
            return await _rolPermisoService.GetById(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<RolPermiso>> Actualizar(RolPermiso entity, int userId)
        {
            return await _rolPermisoService.Actualizar(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<RolPermiso>> Crear(RolPermiso entity, int userId)
        {
            //CASTEAR OBJETO
            // AUTOMAPPER
            return await _rolPermisoService.Crear(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<RolPermiso>> Eliminar(int id, int userId)
        {
            return await _rolPermisoService.Eliminar(id, userId);
        }
    }
}

using AutoMapper;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;
using Planilla.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Planilla.Services;

namespace Planilla.Controllers
{
    public class TipoDatoController : ControllerBaseCustom
    {
        readonly protected TipoDatoService _tipoDato;
        public TipoDatoController(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper)
        {
            _tipoDato = new TipoDatoService(context, appSettingsModule, mapper);
        }
        [HttpGet]
        public async Task<ResponseWrapperDTO<IList<TipoDatoDTO>>> Get()
        {
            return await _tipoDato.GetAllDTO();
        }

        [HttpGet("[action]")]
        public async Task<ResponseWrapperDTO<TipoDatoDTO>> GetById(int id)
        {
            return await _tipoDato.GetByIdDTO(id);
        }

        [HttpPut]
        public async Task<ResponseWrapperDTO<TipoDatoDTO>> Actualizar(TipoDatoDTO entity, int userId)
        {
            return await _tipoDato.ActualizarDTO(entity, userId);
        }

        [HttpPost]
        public async Task<ResponseWrapperDTO<TipoDatoDTO>> Crear(TipoDatoDTO entity, int userId)
        {
            return await _tipoDato.CrearDTO(entity, userId);
        }

        [HttpDelete]
        public async Task<ResponseWrapperDTO<TipoDatoDTO>> Eliminar(int id, int userId)
        {
            return await _tipoDato.EliminarDTO(id, userId);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseWrapperDTO<int>> EliminarMultiples(List<int> ids, int userId)
        {
            return await _tipoDato.EliminarMultiples(ids, userId);
        }

    }
}

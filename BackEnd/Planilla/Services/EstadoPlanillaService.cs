using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Planilla.Abstractions;
using Planilla.DataAccess;
using Planilla.DTO.Others;
using Planilla.DTO;
using Planilla.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Planilla.Services
{
    public class EstadoPlanillaService : BaseService<EstadoPlanilla>
    {
        private readonly IMapper _mapper;

        public EstadoPlanillaService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }

        public async Task<ResponseWrapperDTO<IList<EstadoPlanillaDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<EstadoPlanillaDTO>> response = new ResponseWrapperDTO<IList<EstadoPlanillaDTO>>();
            try
            {
                var registros = await _dBContext.EstadoPlanilla.OrderBy(x => x.Creado).ToListAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<List<EstadoPlanillaDTO>>(registros);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("EstadoPlanillaService", "GetAllDTO");
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

    }
}

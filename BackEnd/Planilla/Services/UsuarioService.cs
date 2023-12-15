using System.Net;
using System.Reflection;
using Planilla.DTO;
using Planilla.DTO.Others;
using AutoMapper;
using Planilla.Utilities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Planilla.Entities;
using Planilla.DataAccess;
using Planilla.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Planilla.Services
{
    public class UsuarioService : BaseService<Usuario>
    {
        private readonly IMapper _mapper;
        readonly protected AuthService _authService;

        public UsuarioService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
            _authService= new AuthService(context, appSettingsModule,mapper);
        }
        public async Task<ResponseWrapperDTO<IList<UsuarioDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<UsuarioDTO>> response = new ResponseWrapperDTO<IList<UsuarioDTO>>();
            try
            {
                var registros = await _dBContext.Usuario.Include(x => x.Empleado).OrderByDescending(x => x.Creado).ToListAsync();
                if (registros != null)
                {
                    response.Data = _mapper.Map<List<UsuarioDTO>>(registros);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<UsuarioDTO>> GetByIdDTO(int id)
        {
            ResponseWrapperDTO<UsuarioDTO> response = new ResponseWrapperDTO<UsuarioDTO>();
            try
            {
                var detalle = await _dBContext.Usuario.Where(x => x.UsuarioId == id).FirstOrDefaultAsync();
                if (detalle != null)
                {
                    response.Data = _mapper.Map<Usuario, UsuarioDTO>(detalle);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró obtener el datelle del registro.", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<UsuarioDTO>> ActualizarDTO(UsuarioDTO registro, int userId)
        {
            ResponseWrapperDTO<UsuarioDTO> response = new ResponseWrapperDTO<UsuarioDTO>();
            try
            {
                Usuario registroGuardar = new Usuario();
                registroGuardar = _mapper.Map<UsuarioDTO, Usuario>(registro);
                if (registro.ActualizarPassword)
                {
                    var encription = await _dBContext.ConfiguracionGlobal.Where(x => x.Codigo == "ENCRIPTIONKEY").FirstOrDefaultAsync();
                    registroGuardar.Password = AesManaged.Encrypt(registro.Password ?? "", encription.Valor);
                }
                registroGuardar.Empleado = null;
                var result = await Actualizar(registroGuardar, userId);
                response.Data = _mapper.Map<Usuario, UsuarioDTO>(result.Data ?? new Usuario());


            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró actualizar el registro", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<UsuarioDTO>> CrearDTO(UsuarioDTO registro, int userId)
        {
            ResponseWrapperDTO<UsuarioDTO> response = new ResponseWrapperDTO<UsuarioDTO>();
            try
            {
                Usuario registroGuardar = new Usuario();
                registroGuardar = _mapper.Map<UsuarioDTO, Usuario>(registro);
                var result = await _authService.RegistrarUsuario(registroGuardar,userId);
                //var result = await Crear(registroGuardar, userId);
                response.Data = _mapper.Map<Usuario, UsuarioDTO>(result.Data ?? new Usuario());

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró crear el registro", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<UsuarioDTO>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<UsuarioDTO> response = new ResponseWrapperDTO<UsuarioDTO>();
            try
            {
                var result = await Eliminar(id, userId);
                response.Data = _mapper.Map<Usuario, UsuarioDTO>(result.Data ?? new Usuario());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró crear el registro", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<int>> EliminarMultiples(List<int> ids, int userId)
        {
            ResponseWrapperDTO<int> response = new ResponseWrapperDTO<int>();
            try
            {
                foreach (int id in ids)
                {
                    var result = await Eliminar(id, userId);
                    if (result != null)
                    {
                        response.Data++;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Data = 0;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron eliminar los registros", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }
    }
}

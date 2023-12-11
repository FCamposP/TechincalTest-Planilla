using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Planilla.DTO;
using Planilla.DTO.ConfiguracionGlobal;
using Planilla.Entities;
using System.Reflection;
using System;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Planilla.DataAccess;
using Planilla.Abstractions;
using Planilla.DTO.Others;

namespace Planilla.Services
{
    public class ConfiguracionGlobalService : BaseService<ConfiguracionGlobal>
    {
        private readonly IMapper _mapper;

        public ConfiguracionGlobalService(ApiDBContext context, IAppSettingsModule appSettingsModule, IMapper mapper) : base(context, appSettingsModule)
        {
            _mapper = mapper;
        }

        public async Task<ResponseWrapperDTO<IList<ConfiguracionGlobalDTO>>> GetAllDTO()
        {
            ResponseWrapperDTO<IList<ConfiguracionGlobalDTO>> response = new ResponseWrapperDTO<IList<ConfiguracionGlobalDTO>>();
            try
            {
                var registros = await _dBContext.ConfiguracionGlobal.OrderByDescending(x => x.Creado).ToListAsync();
                response.Data = _mapper.Map<List<ConfiguracionGlobal>, List<ConfiguracionGlobalDTO>>(registros);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron obtener los registros.", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<ConfiguracionGlobalDTO>> GetByIdDTO(int id)
        {
            ResponseWrapperDTO<ConfiguracionGlobalDTO> response = new ResponseWrapperDTO<ConfiguracionGlobalDTO>();
            try
            {
                var registro = await _dBContext.ConfiguracionGlobal.Where(c => c.ConfiguracionId == id).FirstOrDefaultAsync();
                if (registro != null)
                {
                    response.Data = _mapper.Map<ConfiguracionGlobalDTO>(registro);
                }
             
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error, no se logró obtener el registro.", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        private Dictionary<string, ConfiguracionGlobalDTO> GetSettings()
        {
            var newsettings = GetAllDTO();
            var result = new Dictionary<string, ConfiguracionGlobalDTO>();
            if (newsettings != null)
            {
                if (newsettings.Result.Data?.Count() > 1)
                {
                    result = newsettings.Result.Data.ToDictionary(x => x.Codigo!, x => x);
                }
            }
            return result;
        }



        public async Task<ResponseWrapperDTO<ConfiguracionGlobalDTO>> ActualizarDTO(ConfiguracionGlobalDTO registro, int userId)
        {
            ResponseWrapperDTO<ConfiguracionGlobalDTO> response = new ResponseWrapperDTO<ConfiguracionGlobalDTO>();
            try
            {
                ConfiguracionGlobal registroGuardar = new ConfiguracionGlobal();

                registroGuardar = _mapper.Map<ConfiguracionGlobalDTO, ConfiguracionGlobal>(registro);
                var result = await Actualizar(registroGuardar, userId);

                response.Data = _mapper.Map<ConfiguracionGlobal, ConfiguracionGlobalDTO>(result.Data ?? new ConfiguracionGlobal());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("BaseService", "Actualizar");
                exceptionHandler.SaveException(excepcion);
            }

            return response;
        }

        public async Task<ResponseWrapperDTO<ConfiguracionGlobalDTO>> CrearDTO(ConfiguracionGlobalDTO registro, int userId)
        {
            ResponseWrapperDTO<ConfiguracionGlobalDTO> response = new ResponseWrapperDTO<ConfiguracionGlobalDTO>();
            try
            {
                bool codigoExistente = await _dBContext.ConfiguracionGlobal.AnyAsync(c => c.Codigo.ToLower() == registro.Codigo!.ToLower());
                if (codigoExistente)
                {
                    response.Data = null;
                    response.AddResponseStatus(1, "Ocurrió un error al crear el registro", "Ya existe un registro con dicho código");
                    return response;
                }

                ConfiguracionGlobal registroGuardar = new ConfiguracionGlobal();

                registroGuardar = _mapper.Map<ConfiguracionGlobalDTO, ConfiguracionGlobal>(registro);
                var result = await Crear(registroGuardar, userId);

                response.Data = _mapper.Map<ConfiguracionGlobal, ConfiguracionGlobalDTO>(result.Data ?? new ConfiguracionGlobal());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("BaseService", "Actualizar");
                exceptionHandler.SaveException(excepcion);
            }
            return response;
        }

        public async Task<ResponseWrapperDTO<ConfiguracionGlobalDTO>> EliminarDTO(int id, int userId)
        {
            ResponseWrapperDTO<ConfiguracionGlobalDTO> response = new ResponseWrapperDTO<ConfiguracionGlobalDTO>();
            try
            {
                var result = await Eliminar(id, userId);
                response.Data = _mapper.Map<ConfiguracionGlobal, ConfiguracionGlobalDTO>(result.Data ?? new ConfiguracionGlobal());
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddResponseStatus(1, "Ocurrió un error al actualizar el registro", ex.Message);
                LogError excepcion = (LogError)ex;
                excepcion.InformacionAdicional = exceptionHandler.InformacionAdicionalMetodo("BaseService", "Actualizar");
                exceptionHandler.SaveException(excepcion);
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
                response.Data = 1;
                response.AddResponseStatus(1, "Ocurrió un error, no se lograron eliminar los registros", ex.Message);
                exceptionHandler.SaveException(ex);
            }
            return response;
        }

        public string UploadImage(ConfiguracionGlobalDTO data)
        {

            if (data != null && !string.IsNullOrEmpty(data.Valor))
            {
                // Decodificar la imagen base64 y guardarla en la carpeta deseada

                byte[] imageBytes = Convert.FromBase64String(data.Valor);

                // Ruta completa de la carpeta donde deseas guardar el archivo
                string folderPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Content\\Images\\Recomendaciones");


                // Nombre del archivo
                string fileName = data.Codigo!.ToLower() + ".jpg";

                // Ruta completa del archivo
                string filePath = System.IO.Path.Combine(folderPath, fileName);

                // Escribe los bytes del archivo en un flujo de memoria
                using (var stream = new MemoryStream(imageBytes))
                {
                    // Crea la carpeta si no existe
                    Directory.CreateDirectory(folderPath);

                    // Guarda el archivo en la carpeta
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        stream.CopyTo(fileStream);
                    }
                }

                //System.IO.File.WriteAllBytes(absolutePath, imageBytes);                

                return filePath;
            }
            else
            {
                return "";
            }
        }
    }
}

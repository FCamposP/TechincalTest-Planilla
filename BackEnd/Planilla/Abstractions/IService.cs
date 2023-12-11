using Planilla.DTO;
using Planilla.DTO.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.Abstractions
{
    public interface IService<T>
    {
        Task<ResponseWrapperDTO<IList<T>>> GetAll();
        Task<ResponseWrapperDTO<T>> GetById(int id);
        Task<ResponseWrapperDTO<T>> Crear(T entity, int userId);
        Task<ResponseWrapperDTO<T>> CrearLog(T entity);
        Task<ResponseWrapperDTO<T>> Actualizar(T entity, int userId);
        Task<ResponseWrapperDTO<T>> Eliminar(int id, int userId);
    }
}

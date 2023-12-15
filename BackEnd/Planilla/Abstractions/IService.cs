using Planilla.DTO;
using Planilla.DTO.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.Abstractions
{
    /// <summary>
    /// Métodos generales para los recursos
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IService<T>
    {
        /// <summary>
        /// Obtiene todos los registros de una entidad en especifico desde la base de datos
        /// </summary>
        /// <returns></returns>
        Task<ResponseWrapperDTO<IList<T>>> GetAll();

        /// <summary>
        /// Obtiene el detalle de un registro de una entidad en especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseWrapperDTO<T>> GetById(int id);

        /// <summary>
        /// Crea un nuevo registro en la base de datos
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ResponseWrapperDTO<T>> Crear(T entity, int userId);

        /// <summary>
        /// Guarda excepciones o errores en la base de datos
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ResponseWrapperDTO<T>> CrearLog(T entity);

        /// <summary>
        /// Actualiza un registro en la base de datos
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ResponseWrapperDTO<T>> Actualizar(T entity, int userId);

        /// <summary>
        /// Elimina registro a nivel de sistema (Aplica SoftDelete)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResponseWrapperDTO<T>> Eliminar(int id, int userId);
    }
}

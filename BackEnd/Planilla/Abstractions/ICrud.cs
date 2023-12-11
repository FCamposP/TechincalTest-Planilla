using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planilla.Abstractions
{
    public interface ICrud<T>
    {
        Task<IList<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Save(T entity);
        Task<T> Update(T entity);
    }
}

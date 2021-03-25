using LabaratoryWork7Part2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaratoryWork7Part2.Repository
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<IQueryable<T>> GetValues();
        Task Add(T entity);
        Task Edit(T entity);
        Task Remove(T entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public interface IRepository<T> where T : class
    {
        Task  CreateAsync(T entity);
        Task UpdateAsync(int id, T newEntity );
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> GetFromConditionAsync(Expression<Func<T, bool>> condition);
    }
}

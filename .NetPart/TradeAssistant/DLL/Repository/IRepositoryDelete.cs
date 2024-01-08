using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public interface IRepositoryDelete<TEntity>:IRepositoryUpdate<TEntity>
    {
        Task<OperationDetails> DeleteAsync(int id);
    }
}

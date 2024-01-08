using DLL.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly TradeAssistantContext _context;
        private DbSet<TEntity> entities;
        protected DbSet<TEntity> Entities=>this.entities??_context.Set<TEntity>();
        
        public BaseRepository(TradeAssistantContext context)
        {
            _context = context;
        }

        public async Task<OperationDetails> CreateAsync(TEntity entity)
        {
            try
            {
                await Entities.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new OperationDetails { IsError = false, Message = "Entity Created" };
            }
            catch (Exception ex)
            {

                return new OperationDetails { IsError = true, Message = "Error", Exception = ex };
            }
        }

        public Task<IEnumerable<TEntity>> GetFromConditionAsync(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITHSManagement.Models
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<int> Delete(object id);
        Task<int> Delete(TEntity entityToDelete);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetByID(object id);
        Task<int> Insert(TEntity entity);
        Task<int> Update(TEntity entityToUpdate);
    }
}

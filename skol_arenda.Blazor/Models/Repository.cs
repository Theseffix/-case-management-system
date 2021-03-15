using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITHSManagement.Data;

namespace ITHSManagement.Models
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        internal IDbContextFactory<EFContext> DbFactory;


        public Repository(IDbContextFactory<EFContext> DbFactory)
        {
            this.DbFactory = DbFactory;
        }

        public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            using var context = DbFactory.CreateDbContext();
            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {

                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task<TEntity> GetByID(object id)
        {
            using var context = DbFactory.CreateDbContext();
            var dbSet = context.Set<TEntity>();

            return await dbSet.FindAsync(id);
        }
        public virtual async Task<int> Insert(TEntity entity)
        {
            using var context = DbFactory.CreateDbContext();
            var dbSet = context.Set<TEntity>();
            dbSet.Add(entity);
            return await context.SaveChangesAsync();

        }

        public virtual async Task<int> Delete(object id)
        {
            using var context = DbFactory.CreateDbContext();
            var dbSet = context.Set<TEntity>();
            TEntity entityToDelete = dbSet.Find(id);
            return await Delete(entityToDelete);

        }

        public virtual async Task<int> Delete(TEntity entityToDelete)
        {
            using var context = DbFactory.CreateDbContext();
            var dbSet = context.Set<TEntity>();
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            return await context.SaveChangesAsync();
        }

        public virtual async Task<int> Update(TEntity entityToUpdate)
        {
            using var context = DbFactory.CreateDbContext();
            var dbSet = context.Set<TEntity>();
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            return await context.SaveChangesAsync();
        }
    }
}

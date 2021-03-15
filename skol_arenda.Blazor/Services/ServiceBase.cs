using ITHSManagement.Data;
using ITHSManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITHSManagement.Services
{
    public abstract class Service<T> : IService<T> where T : class 
    {
        private readonly IDbContextFactory<EFContext> dbContextFactory;

        protected Service(IDbContextFactory<EFContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<int> Delete(T TEntity)
        {
            var context = dbContextFactory.CreateDbContext();
            var TEntityToRemove = context.Set<T>().Find(TEntity);
            context.Remove(TEntityToRemove);
            return await context.SaveChangesAsync();
        }
        public async Task<int> Delete(object id)
        {
            var context = dbContextFactory.CreateDbContext();
            var TEntityToRemove = context.Set<T>().Find(id);
            context.Remove(TEntityToRemove);
            return await context.SaveChangesAsync();
        }
        public async Task<T> Get(object id)
        {
            var context = dbContextFactory.CreateDbContext();
            return await context.Set<T>().FindAsync(id);
        }
        public async Task<T> Get(T TEntity)
        {
            var context = dbContextFactory.CreateDbContext();
            return await context.Set<T>().FindAsync(TEntity);
        }

        public async Task<List<T>> Get()
        {
            var context = dbContextFactory.CreateDbContext();
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> Insert(T TEntity)
        {
            var context = dbContextFactory.CreateDbContext();
            await context.AddAsync(TEntity);
            await context.SaveChangesAsync();
            return TEntity;
        }
        public async Task<bool> Update(T TEntity)
        {
            var context = dbContextFactory.CreateDbContext();
            context.Set<T>().Update(TEntity);
            return await context.SaveChangesAsync() > 1;
        }
    }
}
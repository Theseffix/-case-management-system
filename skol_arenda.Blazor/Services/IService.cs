using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITHSManagement.Services
{
    public interface IService<T> where T : class
    {
        Task<int> Delete(object id);
        Task<int> Delete(T TEntity);
        Task<List<T>> Get();
        Task<T> Get(object id);
        Task<T> Get(T TEntity);
        Task<T> Insert(T TEntity);
        Task<bool> Update(T TEntity);
    }
}
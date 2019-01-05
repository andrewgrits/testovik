using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Context.Repository
{
    public interface IRepository<T> : IQueryable<T> where T : class
    {
        void SaveContext();
        Task SaveContextAsync();
        void Create(T entity);
        Task CreateAsync(T entity);
        void CreateMany(List<T> entities);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Context.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly BaseDbContext context;
        private readonly DbSet<T> set;
        private readonly IQueryable<T> queryableSet;

        public BaseRepository(BaseDbContext context)
        {
            this.context = context;
            set = context.Set<T>();
            queryableSet = set.AsQueryable();
        }

        public void Create(T entity)
        {
            set.Add(entity);
            SaveContext();
        }

        public async Task CreateAsync(T entity)
        {
            await set.AddAsync(entity);
            await SaveContextAsync();
        }

        public void CreateMany(List<T> entities)
        {
            set.AddRange(entities);
            SaveContext();
        }

        public void SaveContext()
        {
            context.SaveChanges();
        }

        public async Task SaveContextAsync()
        {
            await context.SaveChangesAsync();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return queryableSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression => queryableSet.Expression;
        public Type ElementType => queryableSet.ElementType;
        public IQueryProvider Provider => queryableSet.Provider;
    }
}

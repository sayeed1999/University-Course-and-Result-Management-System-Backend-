using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Repository
{
    public interface IRepository<T> where T : class
    {
        // PRIMARY CRUD Operations ...
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Find(long id); // Find() is only for PK's!
        public Task AddAsync(T item);
        public void Update(T item);
        public void Update(long id, T item);
        public void Delete(T item);
        public void DeleteById(long id);
        // Additional Operations ...
        public T? SingleOrDefault(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes); 
        public IQueryable<T> Where(Expression<Func<T,bool>> expression, params Expression<Func<T, object>>[] includes);
        public long Count();
        public long Count(Expression<Func<T, bool>> expression);
        public T? FirstOrDefault(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        public T? LastOrDefault(params Expression<Func<T, object>>[] includes);
        public IEnumerable<T> ToList();
        public bool Contains(Expression<Func<T, bool>> expression);
    }
}

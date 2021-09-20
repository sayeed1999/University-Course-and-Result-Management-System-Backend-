using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
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
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> FindAsync(long id); // FindAsync() is only for PK's!
        public Task AddAsync(T item);
        public void Update(T item);
        public void Update(long id, T item);
        public void Delete(T item);
        public void DeleteById(long id);
        // Additional Operations ...
        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes); 
        public IQueryable<T> Where(Expression<Func<T,bool>> expression, params Expression<Func<T, object>>[] includes);
        public Task<long> CountAsync();
        public Task<long> CountAsync(Expression<Func<T, bool>> expression);
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        public Task<T> LastOrDefaultAsync(params Expression<Func<T, object>>[] includes);
        public Task<bool> ContainsAsync(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> ToListAsync();
        public IQueryable<T> GetByWhereClause(Expression<Func<T, bool>> expression, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
        public Task<T> GetBySingleOrDefaultAsync(Expression<Func<T, bool>> expression, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
        public Task<T> GetByFirstOrDefaultAsync(Expression<Func<T, bool>> expression, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
        public IQueryable<T> FromSql(string rawsql, params SqlParameter[] parameters);

    }
}

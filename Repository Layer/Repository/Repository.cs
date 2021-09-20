using Data_Access_Layer;
using Entity_Layer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        /// <summary>
        /// Initialization & Declarations ...
        /// </summary>
        
        protected readonly ApplicationDbContext _dbContext;
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }



        /// <summary>
        /// Primary CRUD Operations ...
        /// </summary>
        /// <returns></returns>
        
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Data fetching error from the database");
            }
        }

        public virtual async Task<T> FindAsync(long id)
        {
            return await _dbSet.FindAsync(id);    
        }

        public virtual async Task AddAsync(T item)
        {
            item.GetType().GetProperty("Id")?.SetValue(item, 0); // setting the PK of the row as 0 when the PK is Id int
            await _dbSet.AddAsync(item);
        }

        public virtual void Update(long id, T item)
        {
            // trick from StackOverFlow
            Type t = item.GetType();
            PropertyInfo prop = t.GetProperty("Id");
            long itemId = (long)prop.GetValue(item);

            if (id != itemId)
            {
                throw new Exception("You have no right to change an object's primary key..");
            }
            Update(item);
        }

        public virtual void Update(T item)
        {
            _dbSet.Update(item);
            //_dbContext.Entry<T>(serviceResponse.Data).CurrentValues.SetValues(item);
            // SaveChangesAsync will be called from UnitOfWork!
        }

        public virtual void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public virtual async void DeleteById(long id)
        {
            var itemToBeDeleted = await _dbSet.FindAsync(id);
            Delete(itemToBeDeleted);
        }



        /// <summary>
        /// Additional operations ...
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        
        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                T? ret = null;
                if(includes.Length > 0)
                {
                    IQueryable<T> queryable = _dbSet.Include(includes[0]);
                    for(int i=1; i<includes.Length; i++)
                    {
                        queryable.Include(includes[i]);
                    }
                    ret = await queryable.SingleOrDefaultAsync(expression);
                }
                else
                {
                    ret = await _dbSet.SingleOrDefaultAsync(expression);
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This Where() doesn't support ThenInclude().
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IQueryable<T> Where(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> ret = _dbSet.Where(expression);
                foreach(Expression<Func<T, object>> i in includes)
                {
                    ret = ret.Include(i);
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<long> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.CountAsync(expression);
        }

        /// <summary>
        /// This FirstOrDefaultAsync() doesn't support ThenInclude().
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            T? ret = null;
            if (includes.Length > 0)
            {
                IQueryable<T> queryable = _dbSet.Include(includes[0]);
                for (int i = 1; i < includes.Length; i++)
                {
                    queryable.Include(includes[i]);
                }
                ret = await queryable.FirstOrDefaultAsync(expression);
            }
            else
            {
                ret = await _dbSet.FirstOrDefaultAsync(expression);
            }
            return ret;
        }

        // Since LastOrDefault doesn't support anymore, we customized it!
        public async Task<T> LastOrDefaultAsync(params Expression<Func<T, object>>[] includes)
        {
            T? ret = null;
            if (includes.Length > 0)
            {
                IQueryable<T> queryable = _dbSet.Include(includes[0]);
                for (int i = 1; i < includes.Length; i++)
                {
                    queryable.Include(includes[i]);
                }
                ret = await queryable.Skip(_dbSet.Count() - 1).FirstOrDefaultAsync();
            }
            else
            {
                ret = await _dbSet.Skip(_dbSet.Count() - 1).FirstOrDefaultAsync();
            }
            return ret;
        }

        public async Task<bool> ContainsAsync(Expression<Func<T, bool>> expression)
        {
            return await FirstOrDefaultAsync(expression) != null;
        }

        public async Task<IEnumerable<T>> ToListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// This method supports multi level ThenIncludes().
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IQueryable<T> GetByWhereClause(
            Expression<Func<T, bool>> expression,
            params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes
        ) {
            IQueryable<T> queryable = _dbSet.Where(expression);

            foreach (var include in includes)
            {
                queryable = include(queryable);
            }

            return queryable;
        }

        /// <summary>
        /// This method supports multi level ThenIncludes().
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<T> GetBySingleOrDefaultAsync(
            Expression<Func<T, bool>> expression, 
            params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes
        ) {
            IQueryable<T> queryable = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                queryable = include(queryable);
            }

            return await queryable.SingleOrDefaultAsync(expression);
        }

        /// <summary>
        /// This method supports multi level ThenIncludes().
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<T> GetByFirstOrDefaultAsync(
            Expression<Func<T, bool>> expression,
            params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes
        )
        {
            IQueryable<T> queryable = _dbSet.AsQueryable();

            //use aggregate
            foreach (var include in includes)
            {
                queryable = include(queryable);
            }

            return await queryable.FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> FromSql(string rawsql, params SqlParameter[] parameters)
        {
            return _dbSet.FromSqlRaw(rawsql, parameters);
        }

    }
}

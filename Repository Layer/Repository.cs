using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer
{
    public class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// I made it 'protected', because child services will use it!
        /// </summary>
        protected readonly ApplicationDbContext _dbContext;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<ServiceResponse<IEnumerable<T>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<T>>();
            try
            {
                serviceResponse.Data = await _dbContext.Set<T>().ToListAsync();
                serviceResponse.Message = "Data fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Data fetching error from the database";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public virtual async Task<ServiceResponse<T>> GetById(long id)
        {
            var serviceResponse = new ServiceResponse<T>();
            try
            {
                serviceResponse.Data = await _dbContext.Set<T>().FindAsync(id);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.Message = "Data not found with the given constraint.";
                    serviceResponse.Success = false;
                }
                else serviceResponse.Message = "Data fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Data fetching error in the database.";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public virtual async Task<ServiceResponse<T>> Add(T item)
        {
            var serviceResponse = new ServiceResponse<T>();
            try
            {
                item.GetType().GetProperty("Id")?.SetValue(item, 0); // setting the PK of the row as 0 when the PK is Id int
                await _dbContext.Set<T>().AddAsync(item);
                await _dbContext.SaveChangesAsync();
                serviceResponse.Data = item;
                serviceResponse.Message = "Item stored successfully to the database.";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = $"Item storing failed in the database.";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public virtual async Task<ServiceResponse<T>> Update(long id, T item)
        {
            // trick from StackOverFlow
            Type t = item.GetType();
            PropertyInfo prop = t.GetProperty("Id");
            long itemId = (long)prop.GetValue(item);

            if (id != itemId)
            {
                var serviceResponse = new ServiceResponse<T>();
                serviceResponse.Data = item;
                serviceResponse.Message = "You have no right to change an object's PK (primary key)!";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            return await Update(item);
        }

        public virtual async Task<ServiceResponse<T>> Update(T item)
        {
            var serviceResponse = new ServiceResponse<T>();
            serviceResponse.Data = item;
            try
            {
                _dbContext.Set<T>().Update(item);
                //_dbContext.Entry<T>(serviceResponse.Data).CurrentValues.SetValues(item);
                await _dbContext.SaveChangesAsync();
                serviceResponse.Message = "Data was updated successfully in the database.";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = $"Error occurred when updating in the database.";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public virtual async Task<ServiceResponse<T>> Delete(T item)
        {
            var serviceResponse = new ServiceResponse<T>();
            serviceResponse.Data = item;

            if (serviceResponse.Data == null)
            {
                serviceResponse.Message = "Item not found on the database";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            try
            {
                _dbContext.Set<T>().Remove(item);
                await _dbContext.SaveChangesAsync();
                serviceResponse.Message = "Item deleted successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Item deletion failed in the database";
            }
            return serviceResponse;
        }

        public virtual async Task<ServiceResponse<T>> DeleteById(long id) // this method is only applicable when the PK is an int Id
        {
            var itemToBeDeleted = await _dbContext.Set<T>().FindAsync(id);
            return await Delete(itemToBeDeleted);
        }

    }
}

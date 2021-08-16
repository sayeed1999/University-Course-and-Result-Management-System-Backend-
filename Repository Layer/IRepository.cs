using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer
{
    public interface IRepository<T> where T : class
    {
        public Task<ServiceResponse<IEnumerable<T>>> GetAll();
        public Task<ServiceResponse<T>> GetById(int id);
        public Task<ServiceResponse<T>> Add(T item);
        public Task<ServiceResponse<T>> Update(T item);
        public Task<ServiceResponse<T>> Update(int id, T item);
        public Task<ServiceResponse<T>> Delete(T item);
        public Task<ServiceResponse<T>> DeleteById(int id);
    }
}

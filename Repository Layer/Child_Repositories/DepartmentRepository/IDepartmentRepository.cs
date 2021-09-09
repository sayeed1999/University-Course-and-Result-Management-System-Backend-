using Entity_Layer;
using Repository_Layer;
using Repository_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Child_Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        // to check duplicates!
        public Task<ServiceResponse<Department>> GetDepartmentByCode(string code);
        // to check duplicates!
        public Task<ServiceResponse<Department>> GetDepartmentByName(string name);
        public Task<ServiceResponse<IEnumerable<Department>>> GetAllIncludingTeachersAndCourses();
        public Task<ServiceResponse<IEnumerable<Department>>> GetAllIncludingCourses();
    }
}

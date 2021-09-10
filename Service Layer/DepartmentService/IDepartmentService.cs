using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.DepartmentService
{
    public interface IDepartmentService
    {
        public Task<ServiceResponse<Department>> SaveDepartment(Department department);
        public Task<ServiceResponse<IEnumerable<Department>>> GetDepartments();
    }
}

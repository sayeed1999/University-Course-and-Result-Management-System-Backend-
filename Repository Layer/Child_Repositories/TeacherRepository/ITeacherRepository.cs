using Entity_Layer;
using Repository_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Child_Repositories
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        public Task<ServiceResponse<Teacher>> GetTeacherByEmail(string email);
        public Task<ServiceResponse<IEnumerable<TeacherView>>> GetTeachersByDepartmentWithAssignedCourses(long departmentId);
    }
}

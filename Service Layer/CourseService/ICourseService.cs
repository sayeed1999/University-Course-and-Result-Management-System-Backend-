using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.CourseService
{
    public interface ICourseService : IRepository<Course>
    {
        public Task<ServiceResponse<Course>> GetByCompositeKey(int departmentId, string courseCode);
        public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentIncludingTeachersAndSemisters(int departmentId);
        public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentIncludingTeachers(int departmentId);
        public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(int departmentId);
    }
}

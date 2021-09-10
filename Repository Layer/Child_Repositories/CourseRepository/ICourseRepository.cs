using Entity_Layer;
using Repository_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Child_Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        // to check duplicates!
        public Task<ServiceResponse<Course>> GetCourseByCode(string code);
        // to check duplicates!
        public Task<ServiceResponse<Course>> GetCourseByName(string name);

        //public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentIncludingTeachersAndSemisters(long departmentId);
        //public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentIncludingTeachers(long departmentId);
        //public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(long departmentId);
        //public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(string departmentCode);
        //public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesWithAllocatedRoomsByDepartment(long departmentId);
        //public Task<ServiceResponse<List<CourseHistory>>> UnassignAllCourses();
    }
}

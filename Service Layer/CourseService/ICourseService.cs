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
        //public Task<ServiceResponse<Course>> GetCourseByCompositeKeyIncludingTeacher(long departmentId, string courseCode);
        //public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentIncludingTeachersAndSemisters(long departmentId);
        //public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentIncludingTeachers(long departmentId);
        //public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(long departmentId);
        //public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(string departmentCode);
        //public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesWithAllocatedRoomsByDepartment(long departmentId);
        //public Task<ServiceResponse<List<CourseHistory>>> UnassignAllCourses();
    }
}

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
        // to check if course is under the department or not!
        public Task<bool> IsCourseInDepartment(long courseId, long departmentId);
        public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(long departmentId);
        public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentWithTeacherAndSemister(long departmentId);
        public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesWithAllocatedRoomsByDepartment(long departmentId);
        public Task<ServiceResponse<IEnumerable<Course>>> GetEnrolledCoursesByStudent(long studentId);
        public Task<ServiceResponse<IEnumerable<Course>>> UnassignAllCourses();
    }
}

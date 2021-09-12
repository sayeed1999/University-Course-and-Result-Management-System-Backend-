using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.CourseService
{
    public interface ICourseService
    {
        public Task<ServiceResponse<Course>> GetCourseById(long courseId);
        public Task<ServiceResponse<Course>> SaveCourse(Course course); // Story 03
        public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(long departmentId); // Story 05
        public Task<ServiceResponse<Course>> UpdateCourse(Course course); // Story 05
        public Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentWithTeacher(long departmentId); // Story 06
        public Task<ServiceResponse<IEnumerable<ClassSchedule>>> GetClassScheduleByDepartment(long departmentId); // Story 09
        public Task<ServiceResponse<IEnumerable<Course>>> GetEnrolledCoursesByStudent(long studentId);

    }
}
